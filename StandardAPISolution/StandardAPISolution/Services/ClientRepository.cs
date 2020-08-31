using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Services
{
    public class ClientRepository
    {
        public static ReturnClientObj GetAllClient(ClientContext clientContext, int Position, int Items)
        {
            ReturnClientObj returnObj = new ReturnClientObj();
            List<LookupField> listClient = new List<LookupField>();
            try
            {
                List lstClient = clientContext.Web.Lists.GetByTitle(CommonConstants.CLIENT_REPRESENTATIVES_ListTitle);
                if (lstClient != null)
                {

                    CamlQuery camlQuery = new CamlQuery();

                    ListItemCollectionPosition collPosition = new ListItemCollectionPosition
                    {
                        PagingInfo = Position.ToString()
                    };
                    camlQuery.ListItemCollectionPosition = collPosition;
                    camlQuery.ViewXml = "<View Scope='RecursiveAll'>" +
                                            "<ViewFields>" +
                                                "<FieldRef Name='Title'/>" +
                                                "<FieldRef Name='CompanyName'/>" +
                                                "<FieldRef Name='ID'/>" +
                                              "</ViewFields>" +
                                                "<RowLimit>" + Items + "</RowLimit>" +
                                        "</View>";

                    List<ListItem> items = new List<ListItem>();
                    ListItemCollection lstItemCollClient = lstClient.GetItems(camlQuery);
                    clientContext.Load(lstItemCollClient);
                    clientContext.ExecuteQuery();
                    foreach (ListItem lstItemClient in lstItemCollClient)
                    {
                        //Initialize Variables
                        string strClientName = "";
                        int intClientID = 0;

                        //Assigning Values to Variables
                        strClientName = Convert.ToString(lstItemClient[CommonConstants.CLIENT_REPRESENTATIVES_TITLE]);
                        intClientID = Convert.ToInt32(lstItemClient[CommonConstants.ID]);

                        //Add Values to Repository
                        var itemClient = new LookupField
                        {
                            ID = intClientID,
                            Name = strClientName

                        };
                        listClient.Add(itemClient);
                    }

                    //Adding the current set of ListItems in our single buffer
                    items.AddRange(lstItemCollClient);
                    //Reset the current pagination info
                    camlQuery.ListItemCollectionPosition = collPosition;



                    ResultClient result = new ResultClient
                    {
                        TotalPages = collPosition,
                        Client = listClient
                    };

                    returnObj.Status = "Success";
                    returnObj.StatusCode = 200;
                    returnObj.Message = "Record Found";
                    returnObj.Result = result;

                    //Return Repository

                }
                else
                {
                    returnObj.Status = "Success";
                    returnObj.StatusCode = 200;
                    returnObj.Message = "No Data Found";
                    returnObj.Result = null;
                }
                return returnObj;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                returnObj.Message = ex.Message;
                returnObj.Status = "Failed";
                returnObj.Result = null;
                return returnObj;
            }
        }
    }
}