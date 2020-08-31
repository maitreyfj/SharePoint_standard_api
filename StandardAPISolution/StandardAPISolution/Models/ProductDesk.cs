﻿using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StandardAPISolution.Models
{

    public class ResultProductDesk
    {
        public ListItemCollectionPosition TotalPages { get; set; }
        public List<LookupField> ProductDesk { get; set; }
    }
    public class ReturnProductDeskObj
    {
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ResultProductDesk Result { get; set; }
    }
}