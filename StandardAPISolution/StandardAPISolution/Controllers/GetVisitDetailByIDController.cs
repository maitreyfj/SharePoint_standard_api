﻿using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using StandardAPISolution.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;

namespace StandardAPISolution.Controllers
{
    public class GetVisitDetailByIDController : ApiController
    {
        public HttpResponseMessage Post([FromBody]VisitDetails VisitDetail)
        {
            string strFedAuth = null;
            string strrtFA = null;
            string strKeyExpires = null;
            string strsharepointhostHost = null;

            HttpRequestHeaders headers = this.Request.Headers;
            if (headers.Contains("fedAuth") )
                {
                strFedAuth = headers.GetValues("fedAuth").First();
            } 
            if (headers.Contains("rtFa"))
            {
                strrtFA = headers.GetValues("rtFa").First();
            }
            if (headers.Contains("keyexpires"))
            {
                strKeyExpires = headers.GetValues("keyexpires").First();
            }
            if (headers.Contains("sharepointhost"))
            {
                strsharepointhostHost = headers.GetValues("sharepointhost").First();
            }

            MsOnlineClaimsHelper.MsoCookies msoCookies = new MsOnlineClaimsHelper.MsoCookies();
            msoCookies.FedAuth = strFedAuth;
            msoCookies.rtFa = strrtFA;
            msoCookies.Expires = Convert.ToDateTime(strKeyExpires);
            msoCookies.Host = new Uri(strsharepointhostHost);
            string baseurl = WebConfigurationManager.AppSettings.Get(CommonConstants.SITEURL);
            using (ClientContext context = new ClientContext(baseurl))
            {
                try
                {
                    //context.ExecutingWebRequest += claimsHelper.clientContext_ExecutingWebRequest;
                    context.ExecutingWebRequest += (s, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.CookieContainer = MsOnlineClaimsHelper.GetCookieContainer_NEW(msoCookies);
                        // e.WebRequestExecutor.WebRequest.UserAgent = userAgent;
                    };

                    context.Load(context.Web);

                    context.ExecuteQuery();
                    ReturnObj returnObj = VisitDetailsRepository.GetByVisitDetailsID(context, VisitDetail.ID, baseurl);
                    var response = Request.CreateResponse<ReturnObj>(System.Net.HttpStatusCode.Created, returnObj);
                    return response;
                }
                catch (Exception ex)
                {
                    ReturnObj returnObjcatch = new ReturnObj { Message = ex.Message};
                    var responsecatch = Request.CreateResponse<ReturnObj>(System.Net.HttpStatusCode.Created, returnObjcatch);
                    return responsecatch;
                }
                

            }
        }
    }
}
