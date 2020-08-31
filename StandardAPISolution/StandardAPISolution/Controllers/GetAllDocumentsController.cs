using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using StandardAPISolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace StandardAPISolution.Controllers
{
    public class GetAllDocumentsController : ApiController
    {
        public HttpResponseMessage Post([FromBody]FileDetails fileDetails)
        {
            string strFedAuth = null;
            string strrtFA = null;
            string strKeyExpires = null;
            string strsharepointhostHost = null;

            HttpRequestHeaders headers = this.Request.Headers;
            if (headers.Contains("fedAuth"))
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
            string baseurl = "https://amspecllc.sharepoint.com/sites/MarketingDivisionUAT";
            using (ClientContext context = new ClientContext(baseurl))
            {
                try
                {
                    //if (msoCookies.Expires < DateTime.Now)
                    //{
                    //    MsOnlineClaimsHelper claimsHelper = new MsOnlineClaimsHelper();
                    //    msoCookies = claimsHelper.GetCookies_Custom();
                    //    context.ExecutingWebRequest += (s, e) =>
                    //    {
                    //        e.WebRequestExecutor.WebRequest.CookieContainer = MsOnlineClaimsHelper.GetCookieContainer_NEW(msoCookies);
                    //        // e.WebRequestExecutor.WebRequest.UserAgent = userAgent;
                    //    };
                    //}
                    //else {
                    //    context.ExecutingWebRequest += (s, e) =>
                    //    {
                    //        e.WebRequestExecutor.WebRequest.CookieContainer = MsOnlineClaimsHelper.GetCookieContainer_NEW(msoCookies);
                    //        // e.WebRequestExecutor.WebRequest.UserAgent = userAgent;
                    //    };
                    //}
                    //context.ExecutingWebRequest += claimsHelper.clientContext_ExecutingWebRequest;
                    context.ExecutingWebRequest += (s, e) =>
                    {
                        e.WebRequestExecutor.WebRequest.CookieContainer = MsOnlineClaimsHelper.GetCookieContainer_NEW(msoCookies);
                        // e.WebRequestExecutor.WebRequest.UserAgent = userAgent;
                    };

                    context.Load(context.Web);

                    context.ExecuteQuery();
                    ReturnObjDocuments returnObj = DocumentsRepository.GetAllDocuments(context, baseurl, fileDetails.FilePath);
                    var response = Request.CreateResponse<ReturnObjDocuments>(System.Net.HttpStatusCode.Created, returnObj);
                    return response;
                }
                catch (Exception ex)
                {
                    ReturnObjDocuments returnObjcatch = new ReturnObjDocuments { Message = ex.Message };
                    var responsecatch = Request.CreateResponse<ReturnObjDocuments>(System.Net.HttpStatusCode.Created, returnObjcatch);
                    return responsecatch;
                }


            }
        }
    }
}
