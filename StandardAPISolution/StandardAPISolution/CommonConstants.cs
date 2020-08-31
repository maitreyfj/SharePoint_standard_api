using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution
{
    public class CommonConstants
    {
        //Common Constants
        public const string PLACEHOLDER = "Placeholder";
        public const string USERNAME = "UserName";
        public const string PASSWORD = "Password";
        public const string SITEURL = "SiteURL";
        public const string TENANTURL = "TenantURL";
        public const string TRUE = "True";
        public const string FALSE = "False";
        public const string YES = "Yes";
        public const string NO = "NO";
        public const string ID = "ID";
        public const string TITLE = "Title";
        public const string CREATEDBY = "Author";
        public const string CREATED = "Created";
        public const string MODIFIEDBY = "Editor";
        public const string MODIFIED = "Modified";
        public const string ERRORLOG_TYPE_EXCEPTION = "EXCEPTION";
        public const string ERRORLOG_TYPE_LOG = "LOG";
        public const int LOOKUP_COLUMN_BUNCH = 8;
        public const string FIEDTYPE_COUNTER = "Counter";
        public const string FIEDTYPE_LOOKUP = "Lookup";
        public const string FIEDTYPE_LOOKUP_MULTI = "LookupMulti";
        public const string FIEDTYPE_USER = "User";
        public const string FIEDTYPE_USER_MULTI = "UserMulti";


        //Live Site
        // Visit Details List
        //public const string VISIT_DETAILS_ListTitle = "Visit Details";
        //public const string VISIT_DETAILS_SUBJECT = "Subject";
        //public const string VISIT_DETAILS_TITLE = "Title";
        //public const string VISIT_DETAILS_COMPANYNAME = "CompanyName";
        //public const string VISIT_DETAILS_COMPANYREPRESENTATIVE = "CompanyRepresentative";
        //public const string VISIT_DETAILS_CLIENTNAME = "ClientName";
        //public const string VISIT_DETAILS_LOCATION = "Location";
        //public const string VISIT_DETAILS_AMSPECLOCATION = "AmSpecLocation";
        //public const string VISIT_DETAILS_AMSPECLOCATIONRATINGFIVE = "AmspecLocationRatingFive";
        //public const string VISIT_DETAILS_AMSPECLOCATIONRATINGFOUR = "AmspecLocationRatingFour";
        //public const string VISIT_DETAILS_AMSPECLOCATIONRATINGTHREE = "AmspecLocationRatingThree";
        //public const string VISIT_DETAILS_AMSPECLOCATIONRATINGTWO = "AmspecLocationRatingTwo";
        //public const string VISIT_DETAILS_AMSPECLOCATIONRATINGONE = "AmspecLocationRatingOne";
        //public const string VISIT_DETAILS_AMSPECATTENDEES = "AmSpecAttendees";
        //public const string VISIT_DETAILS_COMMUNICATIONTYPE = "CommunicationType";
        //public const string VISIT_DETAILS_COMMUNICATIONTYPE_CHOICES_OfficeVisit = "Office Visit";
        //public const string VISIT_DETAILS_COMMUNICATIONTYPE_CHOICES_PhoneCall = "Phone Call";
        //public const string VISIT_DETAILS_COMMUNICATIONTYPE_CHOICES_LunchorDinner = "Lunch or Dinner";
        //public const string VISIT_DETAILS_COMMUNICATIONTYPE_CHOICES_Event = "Event";
        //public const string VISIT_DETAILS_CLIENTTYPE = "ClientType";
        //public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_OGCClient = "OGC Client";
        //public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_Agent = "Agent";
        //public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_Terminal = "Terminal";
        //public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_Retail = "Retail";
        //public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_Other = "Other";
        //public const string VISIT_DETAILS_AMSPECREPRESENTATIVE = "AmSpecRepresentative";
        //public const string VISIT_DETAILS_SUBMITTEDBY = "SubmittedBy";
        //public const string VISIT_DETAILS_VISITDATE = "VisitDate";
        //public const string VISIT_DETAILS_DATEOFNEXTVISIT = "DateofNextVisit";
        //public const string VISIT_DETAILS_OBJECTIVEOFVISIT = "ObjectiveofVisit";
        //public const string VISIT_DETAILS_OBJECTIVEOFVISIT_CHOICES_Courtesy_Maintenance = "Courtesy & Maintenance";
        //public const string VISIT_DETAILS_OBJECTIVEOFVISIT_CHOICES_ProblemResolution = "Problem Resolution";
        //public const string VISIT_DETAILS_OBJECTIVEOFVISIT_CHOICES_NewBusiness = "New Business";
        //public const string VISIT_DETAILS_PRODUCTDESK = "ProductDesk";
        //public const string VISIT_DETAILS_SECTOR = "Sector";
        //public const string VISIT_DETAILS_SPECIFICNEEDS_X002F_CONCERNS = "SpecificNeeds_x002f_Concerns";
        //public const string VISIT_DETAILS_MEETINGMINUTES = "MeetingMinutes";
        //public const string VISIT_DETAILS_MCLINPUT = "MCLInput";
        //public const string VISIT_DETAILS_ACTIONPOINTS = "ActionPoints";
        //public const string VISIT_DETAILS_ACTIONREQUIRED = "ActionRequired";
        //public const string VISIT_DETAILS_ACTIONREQUIRED_CHOICES_Yes = "Yes";
        //public const string VISIT_DETAILS_ACTIONREQUIRED_CHOICES_No = "No";
        //public const string VISIT_DETAILS_ACTIONREQUIRED_CHOICES_Completed = "Completed";
        //public const string VISIT_DETAILS_ACTIONDETAILS = "ActionDetails";
        //public const string VISIT_DETAILS_ASSIGNEDTO = "AssignedTo";
        //public const string VISIT_DETAILS_DEADLINEFORACTION = "DeadlineforAction";
        //public const string VISIT_DETAILS_OVERALLCUSTOMERRATING = "OverallCustomerRating";
        //public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_1 = "1 (not very satisfied with AmSpec)";
        //public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_2 = "2";
        //public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_3 = "3 (Neutral)";
        //public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_4 = "4";
        //public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_5 = "5 (Very Satisfied with AmSpec)";
        //public const string VISIT_DETAILS_RATINGREASON = "RatingReason";
        //public const string VISIT_DETAILS_FURTHERCOMMENTS = "FurtherComments";
        //public const string VISIT_DETAILS_PUBLISHED = "Published";
        //public const string VISIT_DETAILS_PUBLISHED_CHOICES_YesandMarketingDistribution = "Yes and Marketing Distribution";
        //public const string VISIT_DETAILS_PUBLISHED_CHOICES_YesandLimitedDistribution = "Yes and Limited Distribution";
        //public const string VISIT_DETAILS_PUBLISHED_CHOICES_No = "No";
        //public const string VISIT_DETAILS_PUBLISHTO = "PublishTo";
        //public const string VISIT_DETAILS_STARTOFMONTH = "StartofMonth";
        //public const string VISIT_DETAILS_ENDOFMONTH = "EndofMonth";
        //public const string VISIT_DETAILS_STARTMONTH = "StartMonth";
        //public const string VISIT_DETAILS_ENDMONTH = "EndMonth";
        //public const string VISIT_DETAILS_KCSDISABLEEVENTFIRING = "kcsDisableEventFiring";
        //public const string VISIT_DETAILS_ORDER = "Order";

        //UAT site
        // Visit Details List
        public const string VISIT_DETAILS_ListTitle = "Visit Details";
        public const string VISIT_DETAILS_ATTACHMENTS = "Attachments";
        public const string VISIT_DETAILS_SUBJECT = "Subject";
        public const string VISIT_DETAILS_TITLE = "Title";
        public const string VISIT_DETAILS_COMPANYNAME = "CompanyName";
        public const string VISIT_DETAILS_COMPANYREPRESENTATIVE = "CompanyRepresentative";
        public const string VISIT_DETAILS_CLIENTNAME = "ClientName";
        public const string VISIT_DETAILS_LOCATION = "Location";
        public const string VISIT_DETAILS_AMSPECLOCATION = "AmSpecLocation";
        public const string VISIT_DETAILS_AMSPECLOCATIONRATINGFIVE = "AmspecLocationRatingFive";
        public const string VISIT_DETAILS_AMSPECLOCATIONRATINGFOUR = "AmspecLocationRatingFour";
        public const string VISIT_DETAILS_AMSPECLOCATIONRATINGTHREE = "AmspecLocationRatingThree";
        public const string VISIT_DETAILS_AMSPECLOCATIONRATINGTWO = "AmspecLocationRatingTwo";
        public const string VISIT_DETAILS_AMSPECLOCATIONRATINGONE = "AmspecLocationRatingOne";
        public const string VISIT_DETAILS_AMSPECATTENDEES = "AmSpecAttendees";
        public const string VISIT_DETAILS_COMMUNICATIONTYPE = "CommunicationType";
        public const string VISIT_DETAILS_COMMUNICATIONTYPE_CHOICES_OfficeVisit = "Office Visit";
        public const string VISIT_DETAILS_COMMUNICATIONTYPE_CHOICES_PhoneCall = "Phone Call";
        public const string VISIT_DETAILS_COMMUNICATIONTYPE_CHOICES_LunchorDinner = "Lunch or Dinner";
        public const string VISIT_DETAILS_COMMUNICATIONTYPE_CHOICES_Event = "Event";
        public const string VISIT_DETAILS_CLIENTTYPE = "ClientType";
        public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_OGCClient = "OGC Client";
        public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_Agent = "Agent";
        public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_Terminal = "Terminal";
        public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_Retail = "Retail";
        public const string VISIT_DETAILS_CLIENTTYPE_CHOICES_Other = "Other";
        public const string VISIT_DETAILS_AMSPECREPRESENTATIVE = "AmSpecRepresentative";
        public const string VISIT_DETAILS_SUBMITTEDBY = "SubmittedBy";
        public const string VISIT_DETAILS_VISITDATE = "VisitDate";
        public const string VISIT_DETAILS_DATEOFNEXTVISIT = "DateofNextVisit";
        public const string VISIT_DETAILS_OBJECTIVEOFVISIT = "ObjectiveofVisit";
        public const string VISIT_DETAILS_OBJECTIVEOFVISIT_CHOICES_Courtesy_Maintenance = "Courtesy & Maintenance";
        public const string VISIT_DETAILS_OBJECTIVEOFVISIT_CHOICES_ProblemResolution = "Problem Resolution";
        public const string VISIT_DETAILS_OBJECTIVEOFVISIT_CHOICES_NewBusiness = "New Business";
        public const string VISIT_DETAILS_PRODUCTDESK = "ProductDesk";
        public const string VISIT_DETAILS_SECTOR = "Sector";
        public const string VISIT_DETAILS_SPECIFICNEEDS_X002F_CONCERNS = "SpecificNeeds_x002f_Concerns";
        public const string VISIT_DETAILS_MEETINGMINUTES = "MeetingMinutes";
        public const string VISIT_DETAILS_MCLINPUT = "MCLInput";
        public const string VISIT_DETAILS_ACTIONPOINTS = "ActionPoints";
        public const string VISIT_DETAILS_ACTIONREQUIRED = "ActionRequired";
        public const string VISIT_DETAILS_ACTIONREQUIRED_CHOICES_Yes = "Yes";
        public const string VISIT_DETAILS_ACTIONREQUIRED_CHOICES_No = "No";
        public const string VISIT_DETAILS_ACTIONREQUIRED_CHOICES_Completed = "Completed";
        public const string VISIT_DETAILS_ACTIONDETAILS = "ActionDetails";
        public const string VISIT_DETAILS_ASSIGNEDTO = "AssignedTo";
        public const string VISIT_DETAILS_DEADLINEFORACTION = "DeadlineforAction";
        public const string VISIT_DETAILS_OVERALLCUSTOMERRATING = "OverallCustomerRating";
        public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_1 = "1 (not very satisfied with AmSpec)";
        public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_2 = "2";
        public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_3 = "3 (Neutral)";
        public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_4 = "4";
        public const string VISIT_DETAILS_OVERALLCUSTOMERRATING_CHOICES_5 = "5 (Very Satisfied with AmSpec)";
        public const string VISIT_DETAILS_RATINGREASON = "RatingReason";
        public const string VISIT_DETAILS_FURTHERCOMMENTS = "FurtherComments";
        public const string VISIT_DETAILS_PUBLISHED = "Published";
        public const string VISIT_DETAILS_PUBLISHED_CHOICES_YesandMarketingDistribution = "Yes and Marketing Distribution";
        public const string VISIT_DETAILS_PUBLISHED_CHOICES_YesandLimitedDistribution = "Yes and Limited Distribution";
        public const string VISIT_DETAILS_PUBLISHED_CHOICES_No = "No";
        public const string VISIT_DETAILS_PUBLISHTO = "PublishTo";
        public const string VISIT_DETAILS_STARTOFMONTH = "StartofMonth";
        public const string VISIT_DETAILS_ENDOFMONTH = "EndofMonth";
        public const string VISIT_DETAILS_STARTMONTH = "StartMonth";
        public const string VISIT_DETAILS_ENDMONTH = "EndMonth";
        public const string VISIT_DETAILS_KCSDISABLEEVENTFIRING = "kcsDisableEventFiring";
        public const string VISIT_DETAILS_ORDER = "Order";

        // Documents List
        public const string DOCUMENTS_ListTitle = "Documents";
        public const string DOCUMENTS_ListInternalName = "sites";
        public const string DOCUMENTS_FILENAME = "FileLeafRef";
        public const string DOCUMENTS_FILETYPE = "File_x0020_Type";
        public const string DOCUMENTS_ITEMCHILDCOUNT = "ItemChildCount";
        public const string DOCUMENTS__SOURCEURL = "_SourceUrl";
        public const string DOCUMENTS__SHAREDFILEINDEX = "_SharedFileIndex";
        public const string DOCUMENTS_TITLE = "Title";
        public const string DOCUMENTS_TEMPLATEURL = "TemplateUrl";
        public const string DOCUMENTS_XD_PROGID = "xd_ProgID";
        public const string DOCUMENTS__SHORTCUTURL = "_ShortcutUrl";
        public const string DOCUMENTS__SHORTCUTSITEID = "_ShortcutSiteId";
        public const string DOCUMENTS__SHORTCUTWEBID = "_ShortcutWebId";
        public const string DOCUMENTS__SHORTCUTUNIQUEID = "_ShortcutUniqueId";
        public const string DOCUMENTS_ORDER = "Order";
        public const string DOCUMENTS_FILEPATH = "FileRef";

        // AmSpec Locations List
        public const string AMSPEC_LOCATIONS_ListTitle = "AmSpec Locations";
        public const string AMSPEC_LOCATIONS_TITLE = "Title";
        public const string AMSPEC_LOCATIONS_DESCRIPTION = "Description";
        public const string AMSPEC_LOCATIONS_REPRESENTATIVENAME = "RepresentativeName";
        public const string AMSPEC_LOCATIONS_ADDRESS = "Address";
        public const string AMSPEC_LOCATIONS_CITY = "City";
        public const string AMSPEC_LOCATIONS_STATE = "State";
        public const string AMSPEC_LOCATIONS_ZIPCODE = "ZipCode";
        public const string AMSPEC_LOCATIONS_OFFICEPHONE = "OfficePhone";
        public const string AMSPEC_LOCATIONS_ORDER = "Order";

        // Client Representatives List
        public const string CLIENT_REPRESENTATIVES_ListTitle = "Client Representatives";
        public const string CLIENT_REPRESENTATIVES_CLIENTTYPE = "ClientType";
        public const string CLIENT_REPRESENTATIVES_CLIENTTYPE_CHOICES_EU = "EU";
        public const string CLIENT_REPRESENTATIVES_CLIENTTYPE_CHOICES_NonEU = "Non EU";
        public const string CLIENT_REPRESENTATIVES_CLIENTTYPE_CHOICES_Domestic = "Domestic";
        public const string CLIENT_REPRESENTATIVES_TITLE = "Title";
        public const string CLIENT_REPRESENTATIVES_COMPANYNAME = "CompanyName";
        public const string CLIENT_REPRESENTATIVES_DESCRIPTION = "Description";
        public const string CLIENT_REPRESENTATIVES_POSITION = "Position";
        public const string CLIENT_REPRESENTATIVES_ADDRESS = "Address";
        public const string CLIENT_REPRESENTATIVES_CITY = "City";
        public const string CLIENT_REPRESENTATIVES_STATE = "State";
        public const string CLIENT_REPRESENTATIVES_COUNTRY = "Country";
        public const string CLIENT_REPRESENTATIVES_ZIPCODE = "ZipCode";
        public const string CLIENT_REPRESENTATIVES_OFFICEPHONE = "OfficePhone";
        public const string CLIENT_REPRESENTATIVES_EMAIL = "Email";
        public const string CLIENT_REPRESENTATIVES_CELLPHONE = "CellPhone";
        public const string CLIENT_REPRESENTATIVES_ORDER = "Order";

        // Companies List
        public const string COMPANIES_ListTitle = "Companies";
        public const string COMPANIES_TITLE = "Title";
        public const string COMPANIES_DESCRIPTION = "Description";
        public const string COMPANIES_ADDRESS = "Address";
        public const string COMPANIES_CITY = "City";
        public const string COMPANIES_STATE = "State";
        public const string COMPANIES_ZIPCODE = "ZipCode";
        public const string COMPANIES_OFFICEPHONE = "OfficePhone";
        public const string COMPANIES_CELLPHONE = "CellPhone";
        public const string COMPANIES_EMAIL = "Email";
        public const string COMPANIES_COMPANYFILTER = "CompanyFilter";
        public const string COMPANIES_ORDER = "Order";

        // Locations List
        public const string LOCATIONS_ListTitle = "Locations";
        public const string LOCATIONS_TITLE = "Title";
        public const string LOCATIONS_DESCRIPTION = "Description";
        public const string LOCATIONS_ORDER = "Order";

        // Product Desks List
        public const string PRODUCT_DESKS_ListTitle = "Product Desks";
        public const string PRODUCT_DESKS_TITLE = "Title";
        public const string PRODUCT_DESKS_DESCRIPTION = "Description";
        public const string PRODUCT_DESKS_ORDER = "Order";

        // Sectors List
        public const string SECTORS_ListTitle = "Sectors";
        public const string SECTORS_TITLE = "Title";
        public const string SECTORS_ORDER = "Order";
    }
}