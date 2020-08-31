using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;

namespace StandardAPISolution.Services
{
    public class VisitDetailsRepository
    {
        public static ReturnObj GetByVisitDetailsID(ClientContext clientContext, int VisitDetailID, string baseurl)
        {
            //Fetching credentials from web.config
            List<VisitDetails> listVisitDetails = new List<VisitDetails>();
            try
            {
                //Get List using client context
                List lstVisitDetails = clientContext.Web.Lists.GetByTitle(CommonConstants.VISIT_DETAILS_ListTitle);
                if (lstVisitDetails != null)
                {
                    string qry = Common.GetViewFieldsQuery(new string[]
                    {
                        CommonConstants.VISIT_DETAILS_SUBJECT,
                        CommonConstants.VISIT_DETAILS_COMPANYNAME,
                        CommonConstants.VISIT_DETAILS_CLIENTNAME,
                        CommonConstants.VISIT_DETAILS_LOCATION,
                        CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFIVE,
                        CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFOUR,
                        CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTHREE,
                        CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTWO,
                        CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGONE,
                        CommonConstants.VISIT_DETAILS_AMSPECATTENDEES,
                        CommonConstants.VISIT_DETAILS_COMMUNICATIONTYPE,
                        CommonConstants.VISIT_DETAILS_SUBMITTEDBY,
                        CommonConstants.VISIT_DETAILS_VISITDATE,
                        CommonConstants.VISIT_DETAILS_OBJECTIVEOFVISIT,
                        CommonConstants.VISIT_DETAILS_PRODUCTDESK,
                        CommonConstants.VISIT_DETAILS_SECTOR,
                        CommonConstants.VISIT_DETAILS_MEETINGMINUTES,
                        CommonConstants.VISIT_DETAILS_MCLINPUT,
                        CommonConstants.VISIT_DETAILS_ACTIONREQUIRED,
                        CommonConstants.VISIT_DETAILS_ACTIONDETAILS,
                        CommonConstants.VISIT_DETAILS_ASSIGNEDTO,
                        CommonConstants.VISIT_DETAILS_DEADLINEFORACTION,
                        CommonConstants.VISIT_DETAILS_OVERALLCUSTOMERRATING,
                        CommonConstants.VISIT_DETAILS_PUBLISHED,
                        CommonConstants.VISIT_DETAILS_PUBLISHTO,
                        CommonConstants.VISIT_DETAILS_ATTACHMENTS
                        //CommonConstants.CREATED,
                        //CommonConstants.CREATEDBY,
                        //CommonConstants.MODIFIED,
                        //CommonConstants.MODIFIEDBY
                    });

                    //Get list item by id including all lookup columns
                    ListItem lstItemVisitDetails = Common.GetListItemByIDWithLookups(clientContext, CommonConstants.VISIT_DETAILS_ListTitle, VisitDetailID);
                    clientContext.Load(lstItemVisitDetails);
                    clientContext.ExecuteQuery();

                    //Initialize Variables
                    string strSubject = "";
                    List<LookupField> listCompany = new List<LookupField>();
                    List<LookupField> listClient = new List<LookupField>();
                    LookupField oClientLocation = new LookupField();
                    List<LookupField> listAmSpecLocationRating5 = new List<LookupField>();
                    List<LookupField> listAmSpecLocationRating4 = new List<LookupField>();
                    List<LookupField> listAmSpecLocationRating3 = new List<LookupField>();
                    List<LookupField> listAmSpecLocationRating2 = new List<LookupField>();
                    List<LookupField> listAmSpecLocationRating1 = new List<LookupField>();
                    string strAmSpecAttendees = "";
                    string strCommunicationType = "";
                    Users usersSubmittedBy = new Users();
                    DateTime dtVisitDate = new DateTime();
                    string strObjectiveofVisit = "";
                    LookupField oSector = new LookupField();
                    List<LookupField> listProductDesk = new List<LookupField>();
                    string strMeetingMinutes = "";
                    string strMCLInput = "";
                    string strActionRequired = "";
                    string strActionDetails = "";
                    Users usersAssignedTo = new Users();
                    DateTime dtDeadlineforAction = new DateTime();
                    string strOverallCustomerRating = "";
                    string strSend = "";
                    List<string> AttachmentsLinks = new List<string>();
                    List<Users> usersSendTo = new List<Users>();
                    AttachmentCollection Attachments = lstItemVisitDetails.AttachmentFiles;
                    clientContext.Load(Attachments);
                    clientContext.ExecuteQuery();

                    //Assigning Values to Variables
                    strSubject = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_SUBJECT]);
                           
                    FieldLookupValue fldLookupClientLocation = (FieldLookupValue)lstItemVisitDetails[CommonConstants.VISIT_DETAILS_LOCATION];
                    if (fldLookupClientLocation != null)
                    {
                        var lNewClientLocation = new LookupField { ID = fldLookupClientLocation.LookupId, Name = fldLookupClientLocation.LookupValue };
                        oClientLocation = lNewClientLocation;
                    }
                    FieldLookupValue[] fldLookupAmSpecLocationRating5 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFIVE];
                    if (fldLookupAmSpecLocationRating5 != null)
                    {
                        foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating5)
                        {
                            var lNewAmSpecLocationRating5 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                            listAmSpecLocationRating5.Add(lNewAmSpecLocationRating5);
                        }
                    }
                    FieldLookupValue[] fldLookupAmSpecLocationRating4 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFOUR];
                    if (fldLookupAmSpecLocationRating4 != null)
                    {
                        foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating4)
                        {
                            var lNewAmSpecLocationRating4 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                            listAmSpecLocationRating4.Add(lNewAmSpecLocationRating4);
                        }
                    }
                    FieldLookupValue[] fldLookupAmSpecLocationRating3 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTHREE];
                    if (fldLookupAmSpecLocationRating3 != null)
                    {
                        foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating3)
                        {
                            var lNewAmSpecLocationRating3 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                            listAmSpecLocationRating3.Add(lNewAmSpecLocationRating3);
                        }
                    }
                    FieldLookupValue[] fldLookupAmSpecLocationRating2 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTWO];
                    if (fldLookupAmSpecLocationRating2 != null)
                    {
                        foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating2)
                        {
                            var lNewAmSpecLocationRating2 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                            listAmSpecLocationRating2.Add(lNewAmSpecLocationRating2);
                        }
                    }
                    FieldLookupValue[] fldLookupAmSpecLocationRating1 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGONE];
                    if (fldLookupAmSpecLocationRating1 != null)
                    {
                        foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating1)
                        {
                            var lNewAmSpecLocationRating1 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                            listAmSpecLocationRating1.Add(lNewAmSpecLocationRating1);
                        }
                    }
                    strAmSpecAttendees = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECATTENDEES]);
                    strCommunicationType = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_COMMUNICATIONTYPE]);
                    FieldUserValue userSubmittedBy = (FieldUserValue)lstItemVisitDetails[CommonConstants.VISIT_DETAILS_SUBMITTEDBY];
                    if (userSubmittedBy != null)
                    {
                        var lNewUserSubmittedBy = new Users { UserID = userSubmittedBy.LookupId, UserName = userSubmittedBy.LookupValue, UserEmail = userSubmittedBy.Email };
                        usersSubmittedBy = lNewUserSubmittedBy;
                    }
                    dtVisitDate = Convert.ToDateTime(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_VISITDATE]);
                    strObjectiveofVisit = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_OBJECTIVEOFVISIT]);
                    FieldLookupValue fldLookupSector = (FieldLookupValue)lstItemVisitDetails[CommonConstants.VISIT_DETAILS_SECTOR];
                    if (fldLookupSector != null)
                    {
                        var lNewSector = new LookupField { ID = fldLookupSector.LookupId, Name = fldLookupSector.LookupValue };
                        oSector = lNewSector;
                    }
                    FieldLookupValue[] fldLookupProductDesk = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_PRODUCTDESK];
                    if (fldLookupProductDesk != null)
                    {
                        foreach (var fldProductDesk in fldLookupProductDesk)
                        {
                            var lNewProductDesk = new LookupField { ID = fldProductDesk.LookupId, Name = fldProductDesk.LookupValue };
                            listProductDesk.Add(lNewProductDesk);
                        }
                    }
                    strMeetingMinutes = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_MEETINGMINUTES]);
                    strMCLInput = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_MCLINPUT]);
                    strActionRequired = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_ACTIONREQUIRED]);
                    strActionDetails = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_ACTIONDETAILS]);
                    FieldUserValue userAssignedTo = (FieldUserValue)lstItemVisitDetails[CommonConstants.VISIT_DETAILS_ASSIGNEDTO];
                    if (userAssignedTo != null)
                    {
                        var lNewUserAssignedTo = new Users { UserID = userAssignedTo.LookupId, UserName = userAssignedTo.LookupValue, UserEmail = userAssignedTo.Email };
                        usersAssignedTo = lNewUserAssignedTo;
                    }
                    dtDeadlineforAction = Convert.ToDateTime(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_DEADLINEFORACTION]);
                    strOverallCustomerRating = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_OVERALLCUSTOMERRATING]);
                    strSend = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_PUBLISHED]);
                    FieldUserValue[] userSendTo = (FieldUserValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_PUBLISHTO];
                    if (userSendTo != null)
                    {
                        foreach (FieldUserValue fldSendTo in userSendTo)
                        {
                            var lNewUserSendTo = new Users { UserID = fldSendTo.LookupId, UserName = fldSendTo.LookupValue, UserEmail = fldSendTo.Email };
                            usersSendTo.Add(lNewUserSendTo);
                        }
                    }
                    foreach (Attachment oAttachment in Attachments)
                    {
                        var strAttachmentPath = WebConfigurationManager.AppSettings.Get(CommonConstants.TENANTURL);
                        strAttachmentPath += oAttachment.ServerRelativePath.DecodedUrl;
                        AttachmentsLinks.Add(strAttachmentPath);
                    }

                    //Add Values to Repository
                    var itemVisitDetails = new VisitDetails
                    {
                        Subject = strSubject,
                        Company = listCompany,
                        Client = listClient,
                        ClientLocation = oClientLocation,
                        AmSpecLocationRating5 = listAmSpecLocationRating5,
                        AmSpecLocationRating4 = listAmSpecLocationRating4,
                        AmSpecLocationRating3 = listAmSpecLocationRating3,
                        AmSpecLocationRating2 = listAmSpecLocationRating2,
                        AmSpecLocationRating1 = listAmSpecLocationRating1,
                        AmSpecAttendees = strAmSpecAttendees,
                        CommunicationType = strCommunicationType,
                        SubmittedBy = usersSubmittedBy,
                        VisitDate = dtVisitDate,
                        ObjectiveofVisit = strObjectiveofVisit,
                        Sector = oSector,
                        ProductDesk = listProductDesk,
                        MeetingMinutes = strMeetingMinutes,
                        MCLInput = strMCLInput,
                        ActionItems = strActionRequired,
                        ActionDetails = strActionDetails,
                        AssignedTo = usersAssignedTo,
                        DeadlineForAction = dtDeadlineforAction,
                        OverallCustomerRating = strOverallCustomerRating,
                        Send = strSend,
                        Sendto = usersSendTo,
                        IsAttachmentsAvailable = Attachments.AreItemsAvailable,
                        AttachmentsLink = AttachmentsLinks
                    };
                    listVisitDetails.Add(itemVisitDetails);

                    //set values in Result object
                    Result result = new Result
                    {
                        TotalPages = null,
                        VisitDetails = listVisitDetails
                    };

                    //Set values in returning object
                    ReturnObj returnObj = new ReturnObj
                    {
                        Status = "Success",
                        StatusCode = 200,
                        Message = "Record Found",
                        Result = result
                    };

                    //Return object
                    return returnObj;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public static ReturnObj GetAllVisitDetails(ClientContext clientContext, int Position, int Items)
        {
            ReturnObj retReturnObj = new ReturnObj();
            List<VisitDetails> listVisitDetails = new List<VisitDetails>();
            try
            {
                List lstVisitDetails = clientContext.Web.Lists.GetByTitle(CommonConstants.VISIT_DETAILS_ListTitle);
                if (lstVisitDetails != null)
                {

                    CamlQuery camlQuery = new CamlQuery();

                    ListItemCollectionPosition collPosition = new ListItemCollectionPosition
                    {
                        PagingInfo = Position.ToString()
                    };
                    camlQuery.ListItemCollectionPosition = collPosition;
                    camlQuery.ViewXml = "<View Scope='RecursiveAll'>" +
                                            "<ViewFields>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_SUBJECT + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_COMPANYNAME + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_SUBJECT + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_LOCATION + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFIVE + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFOUR + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTHREE + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTWO + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGONE + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_AMSPECATTENDEES + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_COMMUNICATIONTYPE + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_VISITDATE + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_OBJECTIVEOFVISIT + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_PRODUCTDESK + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_SECTOR + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_MEETINGMINUTES + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_MCLINPUT + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_ACTIONREQUIRED + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_ACTIONDETAILS + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_DEADLINEFORACTION + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_OVERALLCUSTOMERRATING + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_PUBLISHED + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_SUBMITTEDBY + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_ASSIGNEDTO + "'/>" +
                                                "<FieldRef Name='" + CommonConstants.VISIT_DETAILS_ATTACHMENTS + "'/>" +
                                                //"<FieldRef Name='PublishTo'/>" +
                                            "</ViewFields>" +
                                                "<RowLimit>" + Items + "</RowLimit>" +
                                        "</View>";

                    List<ListItem> items = new List<ListItem>();
                    ListItemCollection lstItemCollVisitDetails = lstVisitDetails.GetItems(camlQuery);
                    clientContext.Load(lstItemCollVisitDetails);
                    clientContext.ExecuteQuery();
                    foreach (ListItem lstItemVisitDetails in lstItemCollVisitDetails)
                    {
                        //Initialize Variables
                        string strSubject = "";
                        List<LookupField> listCompany = new List<LookupField>();
                        List<LookupField> listClient = new List<LookupField>();
                        LookupField oClientLocation = new LookupField();
                        List<LookupField> listAmSpecLocationRating5 = new List<LookupField>();
                        List<LookupField> listAmSpecLocationRating4 = new List<LookupField>();
                        List<LookupField> listAmSpecLocationRating3 = new List<LookupField>();
                        List<LookupField> listAmSpecLocationRating2 = new List<LookupField>();
                        List<LookupField> listAmSpecLocationRating1 = new List<LookupField>();
                        string strAmSpecAttendees = "";
                        string strCommunicationType = "";
                        Users usersSubmittedBy = new Users();
                        DateTime dtVisitDate = new DateTime();
                        string strObjectiveofVisit = "";
                        LookupField oSector = new LookupField();
                        List<LookupField> listProductDesk = new List<LookupField>();
                        string strMeetingMinutes = "";
                        string strMCLInput = "";
                        string strActionRequired = "";
                        string strActionDetails = "";
                        Users usersAssignedTo = new Users();
                        DateTime dtDeadlineforAction = new DateTime();
                        string strOverallCustomerRating = "";
                        string strSend = "";
                        List<Users> usersSendTo = new List<Users>();
                        AttachmentCollection Attachments = lstItemVisitDetails.AttachmentFiles;
                        clientContext.Load(Attachments);
                        clientContext.ExecuteQuery();
                        bool isAttachmentAvailable; 
                        if (Attachments.Count > 0)
                        {
                            isAttachmentAvailable = true;
                        }
                        else
                        {
                            isAttachmentAvailable = false;
                        }

                        //Assigning Values to Variables
                        strSubject = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_SUBJECT]);
                        FieldLookupValue[] fldLookupCompany = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_COMPANYNAME];
                        if (fldLookupCompany != null)
                        {
                            foreach (var fldCompany in fldLookupCompany)
                            {
                                var lNewCompany = new LookupField { ID = fldCompany.LookupId, Name = fldCompany.LookupValue };
                                listCompany.Add(lNewCompany);
                            }
                        }

                        FieldLookupValue[] fldLookupClient = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_CLIENTNAME];
                        if (fldLookupClient != null)
                        {
                            foreach (var fldClient in fldLookupClient)
                            {
                                var lNewClient = new LookupField { ID = fldClient.LookupId, Name = fldClient.LookupValue };
                                listClient.Add(lNewClient);
                            }
                        }
                        FieldLookupValue fldLookupClientLocation = (FieldLookupValue)lstItemVisitDetails[CommonConstants.VISIT_DETAILS_LOCATION];
                        if (fldLookupClientLocation != null)
                        {
                            var lNewClientLocation = new LookupField { ID = fldLookupClientLocation.LookupId, Name = fldLookupClientLocation.LookupValue };
                            oClientLocation = lNewClientLocation;
                        }
                        FieldLookupValue[] fldLookupAmSpecLocationRating5 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFIVE];
                        if (fldLookupAmSpecLocationRating5 != null)
                        {
                            foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating5)
                            {
                                var lNewAmSpecLocationRating5 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                                listAmSpecLocationRating5.Add(lNewAmSpecLocationRating5);
                            }
                        }
                        FieldLookupValue[] fldLookupAmSpecLocationRating4 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFOUR];
                        if (fldLookupAmSpecLocationRating4 != null)
                        {
                            foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating4)
                            {
                                var lNewAmSpecLocationRating4 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                                listAmSpecLocationRating4.Add(lNewAmSpecLocationRating4);
                            }
                        }
                        FieldLookupValue[] fldLookupAmSpecLocationRating3 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTHREE];
                        if (fldLookupAmSpecLocationRating3 != null)
                        {
                            foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating3)
                            {
                                var lNewAmSpecLocationRating3 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                                listAmSpecLocationRating3.Add(lNewAmSpecLocationRating3);
                            }
                        }
                        FieldLookupValue[] fldLookupAmSpecLocationRating2 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTWO];
                        if (fldLookupAmSpecLocationRating2 != null)
                        {
                            foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating2)
                            {
                                var lNewAmSpecLocationRating2 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                                listAmSpecLocationRating2.Add(lNewAmSpecLocationRating2);
                            }
                        }
                        FieldLookupValue[] fldLookupAmSpecLocationRating1 = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGONE];
                        if (fldLookupAmSpecLocationRating1 != null)
                        {
                            foreach (var fldAmSpecLocation in fldLookupAmSpecLocationRating1)
                            {
                                var lNewAmSpecLocationRating1 = new LookupField { ID = fldAmSpecLocation.LookupId, Name = fldAmSpecLocation.LookupValue };
                                listAmSpecLocationRating1.Add(lNewAmSpecLocationRating1);
                            }
                        }
                        strAmSpecAttendees = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_AMSPECATTENDEES]);
                        strCommunicationType = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_COMMUNICATIONTYPE]);
                        FieldUserValue userSubmittedBy = (FieldUserValue)lstItemVisitDetails[CommonConstants.VISIT_DETAILS_SUBMITTEDBY];
                        if (userSubmittedBy != null)
                        {
                            var lNewUserSubmittedBy = new Users { UserID = userSubmittedBy.LookupId, UserName = userSubmittedBy.LookupValue, UserEmail = userSubmittedBy.Email };
                            usersSubmittedBy = lNewUserSubmittedBy;
                        }
                        dtVisitDate = Convert.ToDateTime(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_VISITDATE]);
                        strObjectiveofVisit = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_OBJECTIVEOFVISIT]);
                        FieldLookupValue fldLookupSector = (FieldLookupValue)lstItemVisitDetails[CommonConstants.VISIT_DETAILS_SECTOR];
                        if (fldLookupSector != null)
                        {
                            var lNewSector = new LookupField { ID = fldLookupSector.LookupId, Name = fldLookupSector.LookupValue };
                            oSector = lNewSector;
                        }
                        FieldLookupValue[] fldLookupProductDesk = (FieldLookupValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_PRODUCTDESK];
                        if (fldLookupProductDesk != null)
                        {
                            foreach (var fldProductDesk in fldLookupProductDesk)
                            {
                                var lNewProductDesk = new LookupField { ID = fldProductDesk.LookupId, Name = fldProductDesk.LookupValue };
                                listProductDesk.Add(lNewProductDesk);
                            }
                        }
                        strMeetingMinutes = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_MEETINGMINUTES]);
                        strMCLInput = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_MCLINPUT]);
                        strActionRequired = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_ACTIONREQUIRED]);
                        strActionDetails = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_ACTIONDETAILS]);
                        FieldUserValue userAssignedTo = (FieldUserValue)lstItemVisitDetails[CommonConstants.VISIT_DETAILS_ASSIGNEDTO];
                        if (userAssignedTo != null)
                        {
                            var lNewUserAssignedTo = new Users { UserID = userAssignedTo.LookupId, UserName = userAssignedTo.LookupValue, UserEmail = userAssignedTo.Email };
                            usersAssignedTo = lNewUserAssignedTo;
                        }
                        dtDeadlineforAction = Convert.ToDateTime(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_DEADLINEFORACTION]);
                        strOverallCustomerRating = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_OVERALLCUSTOMERRATING]);
                        strSend = Convert.ToString(lstItemVisitDetails[CommonConstants.VISIT_DETAILS_PUBLISHED]);
                        //FieldUserValue[] userSendTo = (FieldUserValue[])lstItemVisitDetails[CommonConstants.VISIT_DETAILS_PUBLISHTO];
                        //if (userSendTo != null)
                        //{
                        //    foreach (FieldUserValue fldSendTo in userSendTo)
                        //    {
                        //        var lNewUserSendTo = new Users { UserID = fldSendTo.LookupId, UserName = fldSendTo.LookupValue, UserEmail = fldSendTo.Email };
                        //        usersSendTo.Add(lNewUserSendTo);
                        //    }
                        //}

                        //Add Values to Repository
                        var itemVisitDetails = new VisitDetails
                        {
                            Subject = strSubject,
                            Company = listCompany,
                            Client = listClient,
                            ClientLocation = oClientLocation,
                            AmSpecLocationRating5 = listAmSpecLocationRating5,
                            AmSpecLocationRating4 = listAmSpecLocationRating4,
                            AmSpecLocationRating3 = listAmSpecLocationRating3,
                            AmSpecLocationRating2 = listAmSpecLocationRating2,
                            AmSpecLocationRating1 = listAmSpecLocationRating1,
                            AmSpecAttendees = strAmSpecAttendees,
                            CommunicationType = strCommunicationType,
                            SubmittedBy = usersSubmittedBy,
                            VisitDate = dtVisitDate,
                            ObjectiveofVisit = strObjectiveofVisit,
                            Sector = oSector,
                            ProductDesk = listProductDesk,
                            MeetingMinutes = strMeetingMinutes,
                            MCLInput = strMCLInput,
                            ActionItems = strActionRequired,
                            ActionDetails = strActionDetails,
                            AssignedTo = usersAssignedTo,
                            DeadlineForAction = dtDeadlineforAction,
                            OverallCustomerRating = strOverallCustomerRating,
                            Send = strSend,
                            Sendto = usersSendTo,
                            IsAttachmentsAvailable = isAttachmentAvailable

                        };
                        listVisitDetails.Add(itemVisitDetails);
                    }

                    //Adding the current set of ListItems in our single buffer
                    items.AddRange(lstItemCollVisitDetails);
                    //Reset the current pagination info
                    camlQuery.ListItemCollectionPosition = collPosition;



                    Result result = new Result
                    {
                        TotalPages = collPosition,
                        VisitDetails = listVisitDetails
                    };

                    retReturnObj.Status = "Success";
                    retReturnObj.StatusCode = 200;
                    retReturnObj.Message = "Record Found";
                    retReturnObj.Result = result;

                    //Return Repository
                       
                }
                else {
                    retReturnObj.Status = "Success";
                    retReturnObj.StatusCode = 200;
                    retReturnObj.Message = "No Data Found";
                    retReturnObj.Result = null;
                }
                return retReturnObj;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                retReturnObj.Message = ex.Message;
                retReturnObj.Status = "Failed";
                retReturnObj.Result = null;
                return retReturnObj;
            }
        }
        public ReturnObj CreateVisitDetail(ClientContext clientContext,List<VisitDetails> visitDetails)
        {
            ReturnObj retReturnObj = new ReturnObj();
            try
            {
                List oList = clientContext.Web.Lists.GetByTitle(CommonConstants.VISIT_DETAILS_ListTitle);
                clientContext.Load(oList);
                clientContext.ExecuteQuery();
                Result result = new Result();
                foreach (VisitDetails visitDetail in visitDetails)
                {
                    string Subject = visitDetail.Subject;
                    List<LookupField> clients = visitDetail.Client;
                    List<LookupField> companies = visitDetail.Company;
                    LookupField clientLocation = visitDetail.ClientLocation;
                    List<LookupField> AmSpecLocationsRating5 = visitDetail.AmSpecLocationRating5;
                    List<LookupField> AmSpecLocationsRating4 = visitDetail.AmSpecLocationRating4;
                    List<LookupField> AmSpecLocationsRating3 = visitDetail.AmSpecLocationRating3;
                    List<LookupField> AmSpecLocationsRating2 = visitDetail.AmSpecLocationRating2;
                    List<LookupField> AmSpecLocationsRating1 = visitDetail.AmSpecLocationRating1;
                    string AmSpecAttendees = visitDetail.AmSpecAttendees;
                    string CommunicationType = visitDetail.CommunicationType;
                    Users SubmittedBy = visitDetail.SubmittedBy;
                    DateTime VisitDate = visitDetail.VisitDate;
                    string ObjectiveofVisit = visitDetail.ObjectiveofVisit;
                    LookupField Sector = visitDetail.Sector;
                    List<LookupField> ProductDesks = visitDetail.ProductDesk;
                    string MeetingMinutes = visitDetail.MeetingMinutes;
                    string MCLInput = visitDetail.MCLInput;
                    string ActionItems = visitDetail.ActionItems;
                    string ActionDetails = visitDetail.ActionDetails;
                    Users AssignedTo = visitDetail.AssignedTo;
                    DateTime DeadlineForAction = visitDetail.DeadlineForAction;
                    string OverallCustomerRating = visitDetail.OverallCustomerRating;
                    string Send = visitDetail.Send;
                    List<Users> Sendto = visitDetail.Sendto;

                    var itemCreateInfo = new ListItemCreationInformation();
                    ListItem oListItem = oList.AddItem(itemCreateInfo);
                    oListItem[CommonConstants.VISIT_DETAILS_SUBJECT] = Subject;
                    oListItem[CommonConstants.VISIT_DETAILS_AMSPECATTENDEES] = AmSpecAttendees;
                    oListItem[CommonConstants.VISIT_DETAILS_COMMUNICATIONTYPE] = CommunicationType;
                    oListItem[CommonConstants.VISIT_DETAILS_OBJECTIVEOFVISIT] = ObjectiveofVisit;
                    oListItem[CommonConstants.VISIT_DETAILS_MEETINGMINUTES] = MeetingMinutes;
                    oListItem[CommonConstants.VISIT_DETAILS_MCLINPUT] = MCLInput;
                    oListItem[CommonConstants.VISIT_DETAILS_ACTIONREQUIRED] = ActionItems;
                    oListItem[CommonConstants.VISIT_DETAILS_ACTIONDETAILS] = ActionDetails;
                    oListItem[CommonConstants.VISIT_DETAILS_OVERALLCUSTOMERRATING] = OverallCustomerRating;
                    oListItem[CommonConstants.VISIT_DETAILS_PUBLISHED] = Send;
                    oListItem[CommonConstants.VISIT_DETAILS_LOCATION] = clientLocation.ID;
                    //oListItem[CommonConstants.VISIT_DETAILS_SUBMITTEDBY] = SubmittedBy.UserID;
                    oListItem[CommonConstants.VISIT_DETAILS_VISITDATE] = VisitDate;
                    oListItem[CommonConstants.VISIT_DETAILS_SECTOR] = Sector.ID;
                    oListItem[CommonConstants.VISIT_DETAILS_DEADLINEFORACTION] = DeadlineForAction;
                    oListItem[CommonConstants.VISIT_DETAILS_ASSIGNEDTO] = AssignedTo.UserID;
                    oListItem.Update();
                    clientContext.ExecuteQuery();

                    if (clients != null)
                    {
                        List<FieldLookupValue> fldValuesClients = new List<FieldLookupValue>();
                        foreach (LookupField client in clients)
                        {
                            fldValuesClients.Add(new FieldLookupValue { LookupId = client.ID });
                        }
                        oListItem[CommonConstants.VISIT_DETAILS_CLIENTNAME] = fldValuesClients;
                        oListItem.Update();
                        clientContext.ExecuteQuery();
                    }

                    if (companies != null)
                    {
                        List<FieldLookupValue> fldValuesCompanies = new List<FieldLookupValue>();
                        foreach (LookupField company in companies)
                        {
                            fldValuesCompanies.Add(new FieldLookupValue { LookupId = company.ID });
                        }
                        oListItem[CommonConstants.VISIT_DETAILS_COMPANYNAME] = fldValuesCompanies;
                        oListItem.Update();
                        clientContext.ExecuteQuery();
                    }

                    if (AmSpecLocationsRating5 != null)
                    {
                        List<FieldLookupValue> fldValuesAmSpecLocationRating5 = new List<FieldLookupValue>();
                        foreach (LookupField amSpecLocationRating5 in AmSpecLocationsRating5)
                        {
                            fldValuesAmSpecLocationRating5.Add(new FieldLookupValue { LookupId = amSpecLocationRating5.ID });
                        }
                        oListItem[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFIVE] = fldValuesAmSpecLocationRating5;
                        oListItem.Update();
                        clientContext.ExecuteQuery();
                    }

                    if (AmSpecLocationsRating4 != null)
                    {
                        List<FieldLookupValue> fldValuesAmSpecLocationRating4 = new List<FieldLookupValue>();
                        foreach (LookupField amSpecLocationRating4 in AmSpecLocationsRating4)
                        {
                            fldValuesAmSpecLocationRating4.Add(new FieldLookupValue { LookupId = amSpecLocationRating4.ID });
                        }
                        oListItem[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFOUR] = fldValuesAmSpecLocationRating4;
                        oListItem.Update();
                        clientContext.ExecuteQuery();
                    }

                    if (AmSpecLocationsRating3 != null)
                    {
                        List<FieldLookupValue> fldValuesAmSpecLocationRating3 = new List<FieldLookupValue>();
                        foreach (LookupField amSpecLocationRating3 in AmSpecLocationsRating3)
                        {
                            fldValuesAmSpecLocationRating3.Add(new FieldLookupValue { LookupId = amSpecLocationRating3.ID });
                        }
                        oListItem[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTHREE] = fldValuesAmSpecLocationRating3;
                        oListItem.Update();
                        clientContext.ExecuteQuery();
                    }

                    if (AmSpecLocationsRating2 != null)
                    {
                        List<FieldLookupValue> fldValuesAmSpecLocationRating2 = new List<FieldLookupValue>();
                        foreach (LookupField amSpecLocationRating2 in AmSpecLocationsRating2)
                        {
                            fldValuesAmSpecLocationRating2.Add(new FieldLookupValue { LookupId = amSpecLocationRating2.ID });
                        }
                        oListItem[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTWO] = fldValuesAmSpecLocationRating2;
                        oListItem.Update();
                        clientContext.ExecuteQuery();
                    }

                    if (AmSpecLocationsRating1 != null)
                    {
                        List<FieldLookupValue> fldValuesAmSpecLocationRating1 = new List<FieldLookupValue>();
                        foreach (LookupField amSpecLocationRating1 in AmSpecLocationsRating1)
                        {
                            fldValuesAmSpecLocationRating1.Add(new FieldLookupValue { LookupId = amSpecLocationRating1.ID });
                        }
                        oListItem[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGONE] = fldValuesAmSpecLocationRating1;
                        oListItem.Update();
                        clientContext.ExecuteQuery();
                    }

                    if (ProductDesks != null)
                    {
                        List<FieldLookupValue> fldValuesProductDesk = new List<FieldLookupValue>();
                        foreach (LookupField productDesk in ProductDesks)
                        {
                            fldValuesProductDesk.Add(new FieldLookupValue { LookupId = productDesk.ID });
                        }
                        oListItem[CommonConstants.VISIT_DETAILS_PRODUCTDESK] = fldValuesProductDesk;
                        oListItem.Update();
                        clientContext.ExecuteQuery();
                    }

                    if (Sendto != null)
                    {
                        List<FieldLookupValue> fldValuesSendTo = new List<FieldLookupValue>();
                        foreach (Users UserSendTo in Sendto)
                        {
                            fldValuesSendTo.Add(new FieldLookupValue { LookupId = UserSendTo.UserID });
                        }
                        oListItem[CommonConstants.VISIT_DETAILS_PUBLISHTO] = fldValuesSendTo;
                        oListItem.Update();
                        clientContext.ExecuteQuery();
                    }
                    result.VisitDetails.Add(visitDetail);
                }
                retReturnObj.Message = "Item Created Successful";
                retReturnObj.Result = result;
                retReturnObj.Status = "Success";
                retReturnObj.StatusCode = 200;
                return retReturnObj;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ReturnObj
                {
                    Message = "",
                    Result = null,
                    Status = "",
                    StatusCode = 401
                };
            }
        }

        public ReturnObj UpdateVisitDetail(ClientContext clientContext, VisitDetails VisitDetail)
        {
            ReturnObj returnObj = new ReturnObj();
            ListItem listItemVisitDetail = Common.GetListItemByIDWithLookups(clientContext, CommonConstants.VISIT_DETAILS_ListTitle, VisitDetail.ID);
            if (listItemVisitDetail != null)
            {
                if(VisitDetail.Subject != null || VisitDetail.Subject != "") listItemVisitDetail[CommonConstants.VISIT_DETAILS_SUBJECT] = VisitDetail.Subject;
                if (VisitDetail.AmSpecAttendees != null || VisitDetail.AmSpecAttendees != "") listItemVisitDetail[CommonConstants.VISIT_DETAILS_AMSPECATTENDEES] = VisitDetail.AmSpecAttendees;
                if(VisitDetail.CommunicationType != null || VisitDetail.CommunicationType != "") listItemVisitDetail[CommonConstants.VISIT_DETAILS_COMMUNICATIONTYPE] = VisitDetail.CommunicationType;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_OBJECTIVEOFVISIT] = VisitDetail.ObjectiveofVisit;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_MEETINGMINUTES] = VisitDetail.MeetingMinutes;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_MCLINPUT] = VisitDetail.MCLInput;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_ACTIONREQUIRED] = VisitDetail.ActionItems;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_ACTIONDETAILS] = VisitDetail.ActionDetails;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_OVERALLCUSTOMERRATING] = VisitDetail.OverallCustomerRating;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_PUBLISHED] = VisitDetail.Send;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_LOCATION] = VisitDetail.ClientLocation.ID;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_VISITDATE] = VisitDetail.VisitDate;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_SECTOR] = VisitDetail.Sector.ID;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_DEADLINEFORACTION] = VisitDetail.DeadlineForAction;
                listItemVisitDetail[CommonConstants.VISIT_DETAILS_ASSIGNEDTO] = VisitDetail.AssignedTo.UserID;
                listItemVisitDetail.Update();
                clientContext.ExecuteQuery();

                if (VisitDetail.Client != null)
                {
                    List<FieldLookupValue> fldValuesClients = new List<FieldLookupValue>();
                    foreach (LookupField client in VisitDetail.Client)
                    {
                        fldValuesClients.Add(new FieldLookupValue { LookupId = client.ID });
                    }
                    listItemVisitDetail[CommonConstants.VISIT_DETAILS_CLIENTNAME] = fldValuesClients;
                    listItemVisitDetail.Update();
                    clientContext.ExecuteQuery();
                }

                if (VisitDetail.Company != null)
                {
                    List<FieldLookupValue> fldValuesCompanies = new List<FieldLookupValue>();
                    foreach (LookupField company in VisitDetail.Company)
                    {
                        fldValuesCompanies.Add(new FieldLookupValue { LookupId = company.ID });
                    }
                    listItemVisitDetail[CommonConstants.VISIT_DETAILS_COMPANYNAME] = fldValuesCompanies;
                    listItemVisitDetail.Update();
                    clientContext.ExecuteQuery();
                }

                if (VisitDetail.AmSpecLocationRating5 != null)
                {
                    List<FieldLookupValue> fldValuesAmSpecLocationRating5 = new List<FieldLookupValue>();
                    foreach (LookupField amSpecLocationRating5 in VisitDetail.AmSpecLocationRating5)
                    {
                        fldValuesAmSpecLocationRating5.Add(new FieldLookupValue { LookupId = amSpecLocationRating5.ID });
                    }
                    listItemVisitDetail[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFIVE] = fldValuesAmSpecLocationRating5;
                    listItemVisitDetail.Update();
                    clientContext.ExecuteQuery();
                }

                if (VisitDetail.AmSpecLocationRating4 != null)
                {
                    List<FieldLookupValue> fldValuesAmSpecLocationRating4 = new List<FieldLookupValue>();
                    foreach (LookupField amSpecLocationRating4 in VisitDetail.AmSpecLocationRating4)
                    {
                        fldValuesAmSpecLocationRating4.Add(new FieldLookupValue { LookupId = amSpecLocationRating4.ID });
                    }
                    listItemVisitDetail[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGFOUR] = fldValuesAmSpecLocationRating4;
                    listItemVisitDetail.Update();
                    clientContext.ExecuteQuery();
                }

                if (VisitDetail.AmSpecLocationRating3 != null)
                {
                    List<FieldLookupValue> fldValuesAmSpecLocationRating3 = new List<FieldLookupValue>();
                    foreach (LookupField amSpecLocationRating3 in VisitDetail.AmSpecLocationRating3)
                    {
                        fldValuesAmSpecLocationRating3.Add(new FieldLookupValue { LookupId = amSpecLocationRating3.ID });
                    }
                    listItemVisitDetail[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTHREE] = fldValuesAmSpecLocationRating3;
                    listItemVisitDetail.Update();
                    clientContext.ExecuteQuery();
                }

                if (VisitDetail.AmSpecLocationRating2 != null)
                {
                    List<FieldLookupValue> fldValuesAmSpecLocationRating2 = new List<FieldLookupValue>();
                    foreach (LookupField amSpecLocationRating2 in VisitDetail.AmSpecLocationRating2)
                    {
                        fldValuesAmSpecLocationRating2.Add(new FieldLookupValue { LookupId = amSpecLocationRating2.ID });
                    }
                    listItemVisitDetail[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGTWO] = fldValuesAmSpecLocationRating2;
                    listItemVisitDetail.Update();
                    clientContext.ExecuteQuery();
                }

                if (VisitDetail.AmSpecLocationRating1 != null)
                {
                    List<FieldLookupValue> fldValuesAmSpecLocationRating1 = new List<FieldLookupValue>();
                    foreach (LookupField amSpecLocationRating1 in VisitDetail.AmSpecLocationRating1)
                    {
                        fldValuesAmSpecLocationRating1.Add(new FieldLookupValue { LookupId = amSpecLocationRating1.ID });
                    }
                    listItemVisitDetail[CommonConstants.VISIT_DETAILS_AMSPECLOCATIONRATINGONE] = fldValuesAmSpecLocationRating1;
                    listItemVisitDetail.Update();
                    clientContext.ExecuteQuery();
                }

                if (VisitDetail.ProductDesk != null)
                {
                    List<FieldLookupValue> fldValuesProductDesk = new List<FieldLookupValue>();
                    foreach (LookupField productDesk in VisitDetail.ProductDesk)
                    {
                        fldValuesProductDesk.Add(new FieldLookupValue { LookupId = productDesk.ID });
                    }
                    listItemVisitDetail[CommonConstants.VISIT_DETAILS_PRODUCTDESK] = fldValuesProductDesk;
                    listItemVisitDetail.Update();
                    clientContext.ExecuteQuery();
                }

                if (VisitDetail.Sendto != null)
                {
                    List<FieldLookupValue> fldValuesSendTo = new List<FieldLookupValue>();
                    foreach (Users UserSendTo in VisitDetail.Sendto)
                    {
                        fldValuesSendTo.Add(new FieldLookupValue { LookupId = UserSendTo.UserID });
                    }
                    listItemVisitDetail[CommonConstants.VISIT_DETAILS_PUBLISHTO] = fldValuesSendTo;
                    listItemVisitDetail.Update();
                    clientContext.ExecuteQuery();
                }
            }
            returnObj.Message = "Visit Detail with ID - " + VisitDetail.ID + " Updated Successfully";
            returnObj.Result = null;
            returnObj.Status = "Sucessful";
            returnObj.StatusCode = 200;
            return returnObj;
        }


        public static ReturnObj DeleteVisitDetail(ClientContext clientContext, int VisitDetailID)
        {
            ReturnObj returnObj = new ReturnObj();
            ListItem listItemVisitDetail = Common.GetListItemByIDWithLookups(clientContext, CommonConstants.VISIT_DETAILS_ListTitle, VisitDetailID);
            if (listItemVisitDetail != null)
            {
                listItemVisitDetail.DeleteObject();
                clientContext.ExecuteQuery();
            }
            returnObj.Message = "Visit Detail with ID - " + VisitDetailID + " Deleted Successfully";
            returnObj.Result = null;
            returnObj.Status = "Sucessful";
            returnObj.StatusCode = 200;
            return returnObj;
        }
    }
}