using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using StandardAPISolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Http;

namespace StandardAPISolution.Controllers
{
    public class GetAllClientController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Paging paging)
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
                    ReturnClientObj returnClientObj = ClientRepository.GetAllClient(context, paging.Position, paging.Items);
                    var response = Request.CreateResponse<ReturnClientObj>(System.Net.HttpStatusCode.Created, returnClientObj);
                    return response;
                }
                catch (Exception ex)
                {
                    ReturnClientObj returnObjcatch = new ReturnClientObj { Message = ex.Message };
                    var responsecatch = Request.CreateResponse<ReturnClientObj>(System.Net.HttpStatusCode.Created, returnObjcatch);
                    return responsecatch;
                }
            }
        }
    }
}
