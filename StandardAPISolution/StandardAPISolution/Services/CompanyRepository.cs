using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Services
{
    public class CompanyRepository
    {
        public static ReturnCompanyObj GetAllCompany(ClientContext clientContext, int Position, int Items)
        {
            ReturnCompanyObj returnObj = new ReturnCompanyObj();
            List<LookupField> listCompany = new List<LookupField>();
            try
            {
                List lstCompany = clientContext.Web.Lists.GetByTitle(CommonConstants.COMPANIES_ListTitle);
                if (lstCompany != null)
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
                    ListItemCollection lstItemCollCompany = lstCompany.GetItems(camlQuery);
                    clientContext.Load(lstItemCollCompany);
                    clientContext.ExecuteQuery();
                    foreach (ListItem lstItemCompany in lstItemCollCompany)
                    {
                        //Initialize Variables
                        string strCompanyName = "";
                        int intCompanyID = 0;

                        //Assigning Values to Variables
                        strCompanyName = Convert.ToString(lstItemCompany[CommonConstants.COMPANIES_TITLE]);
                        intCompanyID = Convert.ToInt32(lstItemCompany[CommonConstants.ID]);

                        //Add Values to Repository
                        var itemCompany = new LookupField
                        {
                            ID = intCompanyID,
                            Name = strCompanyName

                        };
                        listCompany.Add(itemCompany);
                    }

                    //Adding the current set of ListItems in our single buffer
                    items.AddRange(lstItemCollCompany);
                    //Reset the current pagination info
                    camlQuery.ListItemCollectionPosition = collPosition;



                    ResultCompany result = new ResultCompany
                    {
                        TotalPages = collPosition,
                        Company = listCompany
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