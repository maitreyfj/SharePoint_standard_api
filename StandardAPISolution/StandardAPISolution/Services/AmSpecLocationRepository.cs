using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Services
{
    public class AmSpecLocationRepository
    {
        public static ReturnAmSpecLocationObj GetAllAmSpecLocation(ClientContext clientContext, int Position, int Items)
        {
            ReturnAmSpecLocationObj returnObj = new ReturnAmSpecLocationObj();
            List<LookupField> listAmSpecLocation = new List<LookupField>();
            try
            {
                List lstAmSpecLocation = clientContext.Web.Lists.GetByTitle(CommonConstants.AMSPEC_LOCATIONS_ListTitle);
                if (lstAmSpecLocation != null)
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
                    ListItemCollection lstItemCollAmSpecLocation = lstAmSpecLocation.GetItems(camlQuery);
                    clientContext.Load(lstItemCollAmSpecLocation);
                    clientContext.ExecuteQuery();
                    foreach (ListItem lstItemAmSpecLocation in lstItemCollAmSpecLocation)
                    {
                        //Initialize Variables
                        string strAmSpecLocationName = "";
                        int intAmSpecLocationID = 0;

                        //Assigning Values to Variables
                        strAmSpecLocationName = Convert.ToString(lstItemAmSpecLocation[CommonConstants.AMSPEC_LOCATIONS_TITLE]);
                        intAmSpecLocationID = Convert.ToInt32(lstItemAmSpecLocation[CommonConstants.ID]);

                        //Add Values to Repository
                        var itemAmSpecLocation = new LookupField
                        {
                            ID = intAmSpecLocationID,
                            Name = strAmSpecLocationName

                        };
                        listAmSpecLocation.Add(itemAmSpecLocation);
                    }

                    //Adding the current set of ListItems in our single buffer
                    items.AddRange(lstItemCollAmSpecLocation);
                    //Reset the current pagination info
                    camlQuery.ListItemCollectionPosition = collPosition;



                    ResultAmSpecLocation result = new ResultAmSpecLocation
                    {
                        TotalPages = collPosition,
                        AmSpecLocation = listAmSpecLocation
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