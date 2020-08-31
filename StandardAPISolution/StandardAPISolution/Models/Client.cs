using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Models
{

    public class ResultClient
    {
        public ListItemCollectionPosition TotalPages { get; set; }
        public List<LookupField> Client { get; set; }
    }
    public class ReturnClientObj
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ResultClient Result { get; set; }
    }
}