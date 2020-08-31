using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using StandardAPISolution.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Web.Http;

namespace StandardAPISolution.Controllers
{
    public class SignInController : ApiController
    {
        [HttpPost]
        public MsOnlineClaimsHelper.MsoCookies Post([FromBody]Users UserCred)
        {
            MsOnlineClaimsHelper.MsoCookies msOnlineClaimsHelper = SignInHelper.SignIn(UserCred);
            return msOnlineClaimsHelper;
        }
    }
}
