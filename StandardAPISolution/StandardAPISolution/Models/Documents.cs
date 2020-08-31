using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Models
{
    public class Documents
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int ItemChildCount { get; set; }
        public bool HasChild { get; set; }
    }
    public class ReturnObjDocuments
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<Documents> Result { get; set; }
    }
    public class FileDetails
    {
        public string FilePath { get; set; }
    }
}