using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Models
{

    public class ResultCompany
    {
        public ListItemCollectionPosition TotalPages { get; set; }
        public List<LookupField> Company { get; set; }
    }
    public class ReturnCompanyObj
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ResultCompany Result { get; set; }
    }
}