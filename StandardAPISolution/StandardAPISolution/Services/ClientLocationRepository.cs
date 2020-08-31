using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Services
{
    public class ClientLocationRepository
    {
        public static ReturnClientLocationObj GetAllClientLocation(ClientContext clientContext, int Position, int Items)
        {
            ReturnClientLocationObj returnObj = new ReturnClientLocationObj();
            List<LookupField> listClientLocation = new List<LookupField>();
            try
            {
                List lstClientLocation = clientContext.Web.Lists.GetByTitle(CommonConstants.LOCATIONS_ListTitle);
                if (lstClientLocation != null)
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
                    ListItemCollection lstItemCollClientLocation = lstClientLocation.GetItems(camlQuery);
                    clientContext.Load(lstItemCollClientLocation);
                    clientContext.ExecuteQuery();
                    foreach (ListItem lstItemClientLocation in lstItemCollClientLocation)
                    {
                        //Initialize Variables
                        string strClientLocationName = "";
                        int intClientLocationID = 0;

                        //Assigning Values to Variables
                        strClientLocationName = Convert.ToString(lstItemClientLocation[CommonConstants.LOCATIONS_TITLE]);
                        intClientLocationID = Convert.ToInt32(lstItemClientLocation[CommonConstants.ID]);

                        //Add Values to Repository
                        var itemClientLocation = new LookupField
                        {
                            ID = intClientLocationID,
                            Name = strClientLocationName

                        };
                        listClientLocation.Add(itemClientLocation);
                    }

                    //Adding the current set of ListItems in our single buffer
                    items.AddRange(lstItemCollClientLocation);
                    //Reset the current pagination info
                    camlQuery.ListItemCollectionPosition = collPosition;



                    ResultClientLocation result = new ResultClientLocation
                    {
                        TotalPages = collPosition,
                        ClientLocation = listClientLocation
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