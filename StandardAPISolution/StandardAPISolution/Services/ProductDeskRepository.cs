using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Services
{
    public class ProductDeskRepository
    {
        public static ReturnProductDeskObj GetAllProductDesk(ClientContext clientContext, int Position, int Items)
        {
            ReturnProductDeskObj returnObj = new ReturnProductDeskObj();
            List<LookupField> listProductDesk = new List<LookupField>();
            try
            {
                List lstProductDesk = clientContext.Web.Lists.GetByTitle(CommonConstants.PRODUCT_DESKS_ListTitle);
                if (lstProductDesk != null)
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
                                                "<FieldRef Name='ID'/>" +
                                              "</ViewFields>" +
                                                "<RowLimit>" + Items + "</RowLimit>" +
                                        "</View>";

                    List<ListItem> items = new List<ListItem>();
                    ListItemCollection lstItemCollProductDesk = lstProductDesk.GetItems(camlQuery);
                    clientContext.Load(lstItemCollProductDesk);
                    clientContext.ExecuteQuery();
                    foreach (ListItem lstItemProductDesk in lstItemCollProductDesk)
                    {
                        //Initialize Variables
                        string strProductDeskName = "";
                        int intProductDeskID = 0;

                        //Assigning Values to Variables
                        strProductDeskName = Convert.ToString(lstItemProductDesk[CommonConstants.PRODUCT_DESKS_TITLE]);
                        intProductDeskID = Convert.ToInt32(lstItemProductDesk[CommonConstants.ID]);

                        //Add Values to Repository
                        var itemProductDesk = new LookupField
                        {
                            ID = intProductDeskID,
                            Name = strProductDeskName

                        };
                        listProductDesk.Add(itemProductDesk);
                    }

                    //Adding the current set of ListItems in our single buffer
                    items.AddRange(lstItemCollProductDesk);
                    //Reset the current pagination info
                    camlQuery.ListItemCollectionPosition = collPosition;



                    ResultProductDesk result = new ResultProductDesk
                    {
                        TotalPages = collPosition,
                        ProductDesk = listProductDesk
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