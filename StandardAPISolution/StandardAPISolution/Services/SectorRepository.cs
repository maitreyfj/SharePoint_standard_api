using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Services
{
    public class SectorRepository
    {
        public static ReturnSectorObj GetAllSector(ClientContext clientContext, int Position, int Items)
        {
            ReturnSectorObj returnObj = new ReturnSectorObj();
            List<LookupField> listSector = new List<LookupField>();
            try
            {
                List lstSector = clientContext.Web.Lists.GetByTitle(CommonConstants.SECTORS_ListTitle);
                if (lstSector != null)
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
                    ListItemCollection lstItemCollSector = lstSector.GetItems(camlQuery);
                    clientContext.Load(lstItemCollSector);
                    clientContext.ExecuteQuery();
                    foreach (ListItem lstItemSector in lstItemCollSector)
                    {
                        //Initialize Variables
                        string strSectorName = "";
                        int intSectorID = 0;

                        //Assigning Values to Variables
                        strSectorName = Convert.ToString(lstItemSector[CommonConstants.SECTORS_TITLE]);
                        intSectorID = Convert.ToInt32(lstItemSector[CommonConstants.ID]);

                        //Add Values to Repository
                        var itemSector = new LookupField
                        {
                            ID = intSectorID,
                            Name = strSectorName

                        };
                        listSector.Add(itemSector);
                    }

                    //Adding the current set of ListItems in our single buffer
                    items.AddRange(lstItemCollSector);
                    //Reset the current pagination info
                    camlQuery.ListItemCollectionPosition = collPosition;



                    ResultSector result = new ResultSector
                    {
                        TotalPages = collPosition,
                        Sector = listSector
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