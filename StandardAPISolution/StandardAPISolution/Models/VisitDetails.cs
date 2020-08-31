using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Models
{
    public class VisitDetails
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public List<LookupField> Company { get; set; }
        public List<LookupField> Client { get; set; }
        public LookupField ClientLocation { get; set; }
        public List<LookupField> AmSpecLocationRating5 { get; set; }
        public List<LookupField> AmSpecLocationRating4 { get; set; }
        public List<LookupField> AmSpecLocationRating3 { get; set; }
        public List<LookupField> AmSpecLocationRating2 { get; set; }
        public List<LookupField> AmSpecLocationRating1 { get; set; }
        public string AmSpecAttendees { get; set; }
        public string CommunicationType { get; set; }
        public Users SubmittedBy { get; set; }
        public DateTime VisitDate { get; set; }
        public string ObjectiveofVisit { get; set; }
        public LookupField Sector { get; set; }
        public List<LookupField> ProductDesk { get; set; }
        public string MeetingMinutes { get; set; }
        public string MCLInput { get; set; }
        public string ActionItems { get; set; }
        public string ActionDetails { get; set; }
        public Users AssignedTo { get; set; }
        public DateTime DeadlineForAction { get; set; }
        public string OverallCustomerRating { get; set; }
        public string Send { get; set; }
        public List<Users> Sendto { get; set; }
        public bool IsAttachmentsAvailable { get; set; }
        public List<string> AttachmentsLink { get; set; }
    }

    public class LookupField
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
    public class ReturnObj
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Result Result { get; set; }
    }
    public class Result
    {
        public ListItemCollectionPosition TotalPages { get; set; }
        public List<VisitDetails> VisitDetails { get; set; }
    }
    public class Paging
    {
        public int Position { get; set; }
        public int Items { get; set; }
    }
}