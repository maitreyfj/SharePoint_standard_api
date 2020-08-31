using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using File = Microsoft.SharePoint.Client.File;

namespace StandardAPISolution.Services
{
    public class DocumentsRepository
    {
        public static ReturnObjDocuments GetAllDocuments(ClientContext clientContext, string BaseURL, string FilePath)
        {
            ReturnObjDocuments retReturnObjDocuments = new ReturnObjDocuments();
            List<Documents> listDocuments = new List<Documents>();
            try
            {
                List lstDocuments = clientContext.Web.Lists.GetByTitle(CommonConstants.DOCUMENTS_ListTitle);
                if (lstDocuments != null)
                {
                    //string qryFields = Common.GetViewFieldsQuery(new string[]
                    //{
                    //  CommonConstants.DOCUMENTS_TITLE,
                    //  CommonConstants.DOCUMENTS_ORDER,
                    //  CommonConstants.ID
                    //});
                    string qryFields = "<View>" +
                        "<ViewFields>" +
                        "<FieldRef Name='ID'/>" +
                        "<FieldRef Name='_CopySource'/>" +
                        "<FieldRef Name='FileLeafRef'/>" +
                        "<FieldRef Name='File_x0020_Type'/>" +
                        "<FieldRef Name='FileRef'/>" +
                        "<FieldRef Name='DocIcon'/>" +
                        "<FieldRef Name='ItemChildCount'/>" +
                        "<FieldRef Name='Title'/>" +
                        "<FieldRef Name='Order'/>" +
                        "</ViewFields>" +
                        "</View>";
                    ListItem lstItem = null;
                    
                    if (FilePath == null || FilePath == "")
                    {
                        //Get list item by id including all lookup columns
                        ListItemCollection lstItemCollDocuments = Common.GetListItems(clientContext, CommonConstants.DOCUMENTS_ListTitle, qryFields);
                        clientContext.Load(lstItemCollDocuments);
                        clientContext.ExecuteQuery();

                        foreach (ListItem lstItemDoc in lstItemCollDocuments)
                        {
                            lstItem = lstItemDoc;
                            Documents itemDocument = Common.SetDataInRepository(lstItem);
                            FileStream strDownloadDocument = Common.DownloadDocument(clientContext, lstItemDoc, itemDocument);
                            //Add Values to Repository
                            listDocuments.Add(itemDocument);
                        }
                    }
                    else
                    {
                        ListItemCollection lstItemsChildDoc = Common.GetChildDocLibItems(clientContext, CommonConstants.DOCUMENTS_ListTitle, qryFields, FilePath);
                        foreach (ListItem listItemDoc in lstItemsChildDoc)
                        {
                            lstItem = listItemDoc;
                            Documents itemDocument = Common.SetDataInRepository(listItemDoc);
                            FileStream strDownloadDocument = Common.DownloadDocument(clientContext, listItemDoc, itemDocument);
                            listDocuments.Add(itemDocument);
                        }
                    }

                    retReturnObjDocuments.Status = "Success";
                    retReturnObjDocuments.StatusCode = 200;
                    retReturnObjDocuments.Message = "Record Found";
                    retReturnObjDocuments.Result = listDocuments;

                    //Return Repository

                }
                else
                {
                    retReturnObjDocuments.Status = "Success";
                    retReturnObjDocuments.StatusCode = 200;
                    retReturnObjDocuments.Message = "No Data Found";
                    retReturnObjDocuments.Result = null;
                }
                return retReturnObjDocuments;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                retReturnObjDocuments.Message = ex.Message;
                retReturnObjDocuments.Status = "Failed";
                retReturnObjDocuments.Result = null;
                return retReturnObjDocuments;
            }
        }


        public static FileStream DownloadDocuments(ClientContext clientContext, string BaseURL, string FilePath)
        {
            FileStream strDownloadDocument = null;
            List<Documents> listDocuments = new List<Documents>();
            try
            {
                List lstDocuments = clientContext.Web.Lists.GetByTitle(CommonConstants.DOCUMENTS_ListTitle);
                if (lstDocuments != null)
                {
                    //string qryFields = Common.GetViewFieldsQuery(new string[]
                    //{
                    //  CommonConstants.DOCUMENTS_TITLE,
                    //  CommonConstants.DOCUMENTS_ORDER,
                    //  CommonConstants.ID
                    //});
                    string qryFields = "<View>" +
                        "<ViewFields>" +
                        "<FieldRef Name='ID'/>" +
                        "<FieldRef Name='_CopySource'/>" +
                        "<FieldRef Name='FileLeafRef'/>" +
                        "<FieldRef Name='File_x0020_Type'/>" +
                        "<FieldRef Name='FileRef'/>" +
                        "<FieldRef Name='DocIcon'/>" +
                        "<FieldRef Name='ItemChildCount'/>" +
                        "<FieldRef Name='Title'/>" +
                        "<FieldRef Name='Order'/>" +
                        "</ViewFields>" +
                        "</View>";
                    ListItem lstItem = null;

                    if (FilePath == null || FilePath == "")
                    {
                        //Get list item by id including all lookup columns
                        ListItemCollection lstItemCollDocuments = Common.GetListItems(clientContext, CommonConstants.DOCUMENTS_ListTitle, qryFields);
                        clientContext.Load(lstItemCollDocuments);
                        clientContext.ExecuteQuery();

                        foreach (ListItem lstItemDoc in lstItemCollDocuments)
                        {
                            lstItem = lstItemDoc;
                            Documents itemDocument = Common.SetDataInRepository(lstItem);
                            strDownloadDocument = Common.DownloadDocument(clientContext, lstItemDoc, itemDocument);
                            //Add Values to Repository
                            listDocuments.Add(itemDocument);
                        }
                    }
                    else
                    {
                        ListItemCollection lstItemsChildDoc = Common.GetChildDocLibItems(clientContext, CommonConstants.DOCUMENTS_ListTitle, qryFields, FilePath);
                        foreach (ListItem listItemDoc in lstItemsChildDoc)
                        {
                            lstItem = listItemDoc;
                            Documents itemDocument = Common.SetDataInRepository(listItemDoc);
                            strDownloadDocument = Common.DownloadDocument(clientContext, listItemDoc, itemDocument);
                            listDocuments.Add(itemDocument);
                        }
                    }
                    return strDownloadDocument;

                    //Return Repository

                }
                else
                {
                    strDownloadDocument = null;
                    return strDownloadDocument;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                strDownloadDocument = null;
                return strDownloadDocument;
            }
        }
    }
}