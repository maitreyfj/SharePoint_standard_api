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
    public class PostVisitDetailController : ApiController
    {
        private readonly VisitDetailsRepository VisitDetailsRepository;

        public PostVisitDetailController()
        {
            this.VisitDetailsRepository = new VisitDetailsRepository();
        }

        [HttpPost]
        public HttpResponseMessage Post(List<VisitDetails> visitDetails)
        {
            string strFedAuth = null;
            string strrtFA = null;
            string strKeyExpires = null;
            string strsharepointhostHost = null;
            string strOperationType = null;
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
            if (headers.Contains("OperationType"))
            {
                strOperationType = headers.GetValues("OperationType").First();
            }

            MsOnlineClaimsHelper.MsoCookies msoCookies = new MsOnlineClaimsHelper.MsoCookies();
            msoCookies.FedAuth = strFedAuth;
            msoCookies.rtFa = strrtFA;
            msoCookies.Expires = Convert.ToDateTime(strKeyExpires);
            msoCookies.Host = new Uri(strsharepointhostHost);
            string baseurl = WebConfigurationManager.AppSettings.Get(CommonConstants.SITEURL);
            ReturnObj returnObj = new ReturnObj();
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
                    if (visitDetails[0].ID == 0)
                    {
                        returnObj = VisitDetailsRepository.CreateVisitDetail(context, visitDetails);
                        var response = Request.CreateResponse<ReturnObj>(System.Net.HttpStatusCode.Created, returnObj);
                        return response;
                    }
                    else
                    {
                        returnObj = VisitDetailsRepository.UpdateVisitDetail(context, visitDetails[0]);
                        var response = Request.CreateResponse<ReturnObj>(System.Net.HttpStatusCode.Created, returnObj);
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    returnObj = new ReturnObj { Message = ex.Message };
                    var responsecatch = Request.CreateResponse<ReturnObj>(System.Net.HttpStatusCode.Created, returnObj);
                    return responsecatch;
                }
            }
        }
    }
}
