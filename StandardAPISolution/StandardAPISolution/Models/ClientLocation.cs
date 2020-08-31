using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Models
{

    public class ResultClientLocation
    {
        public ListItemCollectionPosition TotalPages { get; set; }
        public List<LookupField> ClientLocation { get; set; }
    }
    public class ReturnClientLocationObj
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ResultClientLocation Result { get; set; }
    }
}