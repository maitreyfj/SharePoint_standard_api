using Microsoft.SharePoint.Client;
using StandardAPISolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security;
using System.Web;

namespace StandardAPISolution
{
    public class SignInHelper
    {
        public static MsOnlineClaimsHelper.MsoCookies SignIn(Users UserDetail)
        {
            string userName = UserDetail.UserName;
            string password = UserDetail.Password;
            string baseurl = "https://amspecllc.sharepoint.com/sites/MarketingDivisionUAT";
            Uri uri = new Uri(baseurl);
            var securePassword = new SecureString();
            foreach (var c in password) { securePassword.AppendChar(c); }
            var credentials = new SharePointOnlineCredentials(userName, securePassword);
            MsOnlineClaimsHelper claimsHelper = new MsOnlineClaimsHelper(baseurl, userName, password);
            MsOnlineClaimsHelper.MsoCookies msoCookies = claimsHelper.GetCookies_Custom();
          
            return msoCookies;
        }
       
    }
}