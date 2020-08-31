using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace StandardAPISolution
{
    public class Common
    {
        public static ClientContext GetClientContextWithCredentials(string strURL, string strUserName,
                                                                    string strPassword, bool blIsOnline)
        {
            try
            {
                ClientContext clientContext = new ClientContext(strURL);
                if (!blIsOnline)
                {
                    System.Net.NetworkCredential cred = new System.Net.NetworkCredential(strUserName, strPassword);
                    clientContext.Credentials = cred;
                }
                else
                {
                    var securePassword = new SecureString();
                    foreach (char c in strPassword)
                    {
                        securePassword.AppendChar(c);
                    }
                    var onlineCredentials = new SharePointOnlineCredentials(strUserName, securePassword);
                    clientContext.Credentials = onlineCredentials;
                }

                clientContext.RequestTimeout = int.MaxValue;
                clientContext.PendingRequest.RequestExecutor.WebRequest.KeepAlive = false;
                clientContext.Load(clientContext.Web);
                clientContext.ExecuteQuery();

                return clientContext;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public static ClientContext GetClientContextWithCookies(CookieContainer cookieContainer) {
            using (ClientContext context = new ClientContext("https://amspecllc.sharepoint.com/sites/MarketingDivision"))
            {
                context.Load(context.Web);
                context.ExecuteQuery();
                return context;
            }
        }
        public static string GetDataFromHeaders(string key, HttpRequestHeaders headers)
        {
            if (headers.Contains(key))
            {
                return key = headers.GetValues(key).First();
            }
            else
            {
                return key + " not Found";
            }
        }

        public static ListItem GetListItemByIDWithLookups(ClientContext clientContext, string strListTitle, int intItemID)
        {
            ListItem spListItem = null;
            try
            {
                List spList = clientContext.Web.Lists.GetByTitle(strListTitle);
                clientContext.Load(spList);
                clientContext.ExecuteQuery();

                FieldCollection fieldCollection = spList.Fields;
                clientContext.Load(fieldCollection);
                clientContext.ExecuteQuery();

                ListItem spListTempItem = spList.GetItemById(intItemID);
                clientContext.Load(spListTempItem);
                clientContext.ExecuteQuery();

                string[] arrLookupFields = null;
                spListItem = Common.GetLookupColumnsInBunch(fieldCollection, clientContext, spListTempItem
                                                                    , ref arrLookupFields, CommonConstants.LOOKUP_COLUMN_BUNCH, CommonConstants.ID,
                                                                    spListTempItem.Id);

                //ExceptionHelper.LogMessage(properties, clientContext, CLASS_NAME, "GetListItemByIDWithLookups",
                //     "arrLookupFields : " + arrLookupFields.Length +  " : " + string.Format(",", arrLookupFields.ToList()));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return spListItem;
        }

        public static ListItem GetLookupColumnsInBunch(FieldCollection sourcefieldColl,
                                                        ClientContext clientContext,
                                                        ListItem spSourceItem,
                                                        ref string[] arrLookupFields,
                                                        int intLookupProcessingGroupsCount,
                                                        string strColumnToFilter = "",
                                                        int intColumnFilterValue = 0)
        {
            // load source list
            clientContext.Load(spSourceItem.ParentList);
            clientContext.ExecuteQuery();

            // get source list title
            string strSourceListTitle = spSourceItem.ParentList.Title;

            // variables to track execution status
            string strLookupFieldsInternalNames = string.Empty;
            string strColumnName = string.Empty;

            Dictionary<string, string> dicFieldTypes = new Dictionary<string, string>();

            // copy source item
            ListItem tempSourceItem = spSourceItem;

            try
            {
                int intLookupFieldsCount = 0;

                // iterate each field in source item
                foreach (Microsoft.SharePoint.Client.Field sourceField in sourcefieldColl)
                {
                    // check if the field is lookup, lookupmulti, user, usermulti and not hidden or readonly
                    if ((sourceField.TypeAsString == CommonConstants.FIEDTYPE_LOOKUP ||
                        sourceField.TypeAsString == CommonConstants.FIEDTYPE_LOOKUP_MULTI ||
                        sourceField.TypeAsString == CommonConstants.FIEDTYPE_USER ||
                        sourceField.TypeAsString == CommonConstants.FIEDTYPE_USER_MULTI) &&
                        !sourceField.ReadOnlyField && !sourceField.Hidden)
                    {
                        dicFieldTypes.Add(sourceField.InternalName, sourceField.TypeAsString);

                        // add field title in the list of fields to be used later
                        if (string.IsNullOrEmpty(strLookupFieldsInternalNames))
                            strLookupFieldsInternalNames += sourceField.InternalName;
                        else
                            strLookupFieldsInternalNames += '#' + sourceField.InternalName;
                    }
                }

                // convert field names into array
                arrLookupFields = strLookupFieldsInternalNames.Split('#');

                intLookupFieldsCount = arrLookupFields.Length;

                int intMainIndex = 0;

                repeat:

                // check if column to filter has been provided else set it to ID column
                if (!string.IsNullOrEmpty(strColumnToFilter))
                {
                    strColumnToFilter = CommonConstants.ID;
                }

                // create query based on column to filter and column filter value
                string strQuery = @"<View><Query><Where><Eq><FieldRef Name='" + strColumnToFilter + "' /><Value Type='Counter'>" + intColumnFilterValue
                    + "</Value></Eq></Where></Query><ViewFields>";

                // calculate no. of lookup columns to be taken in this iteration
                if ((intMainIndex + intLookupProcessingGroupsCount) > intLookupFieldsCount)
                {
                    intLookupProcessingGroupsCount = intLookupProcessingGroupsCount -
                        ((intMainIndex + intLookupProcessingGroupsCount) - intLookupFieldsCount);
                }

                // iterate the columns to be fetched
                for (int intLookupField = intMainIndex; intLookupField < intMainIndex + intLookupProcessingGroupsCount; intLookupField++)
                {
                    strColumnName = Convert.ToString(arrLookupFields[intLookupField]);
                    if (!string.IsNullOrEmpty(strColumnName))
                    {
                        strQuery += "<FieldRef Name='" + strColumnName + "' />";
                    }
                }
                strQuery += "</ViewFields></View>";

                // get list item collection based on the query built above
                ListItemCollection lstItemCollection = GetListRecords(clientContext, strSourceListTitle, strQuery);

                // check list item collection 
                if (lstItemCollection != null && lstItemCollection.Count > 0 && lstItemCollection[0] != null)
                {
                    // get the list item 
                    ListItem lstItem = lstItemCollection[0];
                    int intLookupField;

                    // get lookup values from each lookup field
                    for (intLookupField = intMainIndex; intLookupField < intMainIndex + intLookupProcessingGroupsCount; intLookupField++)
                    {
                        // get the item column name
                        strColumnName = Convert.ToString(arrLookupFields[intLookupField]);
                        try
                        {
                            // check if the item field has a value
                            if (lstItem[strColumnName] != null)
                            {
                                string strCurrentFieldType = dicFieldTypes[strColumnName];

                                if (!string.IsNullOrEmpty(strCurrentFieldType))
                                {
                                    if (strCurrentFieldType == CommonConstants.FIEDTYPE_LOOKUP)
                                    {
                                        // get lookup value and set it to the temp item
                                        FieldLookupValue FiledLookupValue = lstItem[strColumnName] as FieldLookupValue;
                                        tempSourceItem[strColumnName] = FiledLookupValue;
                                    }
                                    else if (strCurrentFieldType == CommonConstants.FIEDTYPE_LOOKUP_MULTI)
                                    {
                                        // get lookupmulti value and set it to the temp item
                                        FieldLookupValue[] FieldlookupValues = lstItem[strColumnName] as FieldLookupValue[];
                                        tempSourceItem[strColumnName] = FieldlookupValues;

                                    }
                                    else if (strCurrentFieldType == CommonConstants.FIEDTYPE_USER)
                                    {
                                        // get user value and set it to the temp item
                                        FieldUserValue FieldUserValue = lstItem[strColumnName] as FieldUserValue;
                                        tempSourceItem[strColumnName] = FieldUserValue;
                                    }
                                    else if (strCurrentFieldType == CommonConstants.FIEDTYPE_USER_MULTI)
                                    {
                                        // get usermulti value and set it to the temp item
                                        FieldUserValue[] FieldUserValues = lstItem[strColumnName] as FieldUserValue[];
                                        tempSourceItem[strColumnName] = FieldUserValues;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    intMainIndex = intLookupField;
                }
                else
                {
                    intMainIndex = intMainIndex + intLookupProcessingGroupsCount;
                }

                // check if more fields need to be processed, send execution control above again
                if (intMainIndex < intLookupFieldsCount - 1)
                {
                    goto repeat;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return tempSourceItem;
        }

        public static ListItemCollection GetListItems(ClientContext clientContext, string strListName, string strCamlQuery = "")
        {
            return GetListRecords(clientContext, strListName, strCamlQuery);
        }
        public static ListItemCollection GetListRecords(ClientContext clientContext, string listName, string queryValue,
            int intDelayInMiliSeconds = 0)
        {
            if (clientContext != null)
            {
                try
                {
                    if (intDelayInMiliSeconds != 0)
                    {
                        Thread.Sleep(intDelayInMiliSeconds);
                    }
                    List spList = clientContext.Web.Lists.GetByTitle(listName);
                    clientContext.Load(spList);
                    clientContext.ExecuteQuery();
                    CamlQuery qry = new CamlQuery
                    {
                        ViewXml = queryValue
                    };
                    ListItemCollection itemCollection = spList.GetItems(qry);
                    clientContext.Load(itemCollection);
                    clientContext.ExecuteQuery();
                    return itemCollection;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static ListItemCollection GetChildDocLibItems(ClientContext clientContext, string listName, string queryValue, string FilePath,
            int intDelayInMiliSeconds = 0)
        {
            if (clientContext != null)
            {
                try
                {
                    if (intDelayInMiliSeconds != 0)
                    {
                        Thread.Sleep(intDelayInMiliSeconds);
                    }
                    List spList = clientContext.Web.Lists.GetByTitle(listName);
                    clientContext.Load(spList);
                    clientContext.ExecuteQuery();
                    CamlQuery qry = new CamlQuery
                    {
                        ViewXml = queryValue,
                        FolderServerRelativeUrl = FilePath
                    };
                    ListItemCollection itemCollection = spList.GetItems(qry);
                    clientContext.Load(itemCollection);
                    clientContext.ExecuteQuery();
                    return itemCollection;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static string GetViewFieldsQuery(string[] arrViewFields, bool blAddViewsTag = false)
        {
            if (arrViewFields.Length > 0)
            {
                StringBuilder sb = new StringBuilder();

                if (blAddViewsTag)
                    sb.Append("<View>");

                sb.Append("<ViewFields>");
                foreach (var field in arrViewFields)
                {
                    sb.Append("<FieldRef Name='" + field + "' />");
                }
                sb.Append("</ViewFields>");

                if (blAddViewsTag) sb.Append("</View>");
                return Convert.ToString(sb);
            }
            else
            {
                return "";
            }
        }
        public static string GetAttachmentURL(AttachmentCollection attchmentcollection)
        {
            string URL = "#";
            foreach (var item in attchmentcollection)
            {
                URL = attchmentcollection.ToString();
                break;
            }

            return URL;
        }

        public static Documents SetDataInRepository(ListItem lstItemDocument)
        {
            string filePath = null;
            Documents itemDocument = new Documents
            {
                ID = lstItemDocument.Id,
                Name = Convert.ToString(lstItemDocument[CommonConstants.DOCUMENTS_FILENAME]),
                Type = Convert.ToString(lstItemDocument[CommonConstants.DOCUMENTS_FILETYPE]),
                ItemChildCount = Convert.ToInt32(lstItemDocument[CommonConstants.DOCUMENTS_ITEMCHILDCOUNT])
            };
            if (itemDocument.ItemChildCount > 0)
            {
                itemDocument.Type = "Folder";
                itemDocument.HasChild = true;
                filePath = Convert.ToString(lstItemDocument[CommonConstants.DOCUMENTS_FILEPATH]);
            }
            else if (itemDocument.ItemChildCount == 0)
            {
                itemDocument.HasChild = false;
            }
           
            return itemDocument;
        }
        public static FileStream DownloadDocument(ClientContext clientContext, ListItem lstItemDocument, Documents itemDocument)
        {
            string strReturn = null;
            FileInformation fileInfo = null;
            if (itemDocument.Type != "Folder")
            {

                // Access the file 
                Microsoft.SharePoint.Client.File file = lstItemDocument.File;
                clientContext.Load(file);
                clientContext.ExecuteQuery();
                if (file != null)
                {
                    var fileRef = file.ServerRelativeUrl;
                    fileInfo = Microsoft.SharePoint.Client.File.OpenBinaryDirect(clientContext, fileRef.ToString());
                    string fileLocalPath = @"D:\Ritika\";
                    var fileName = Path.Combine(fileLocalPath, (string)file.Name);
                    using (var fileStream = System.IO.File.Create(fileName))
                    {
                        fileInfo.Stream.CopyTo(fileStream);
                        strReturn = "File Saved at " + fileLocalPath;
                        return fileStream;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}