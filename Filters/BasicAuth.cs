using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Shopbridge_base.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.Unauthorized, "Login failed");
            }
            else
            {
                try
                {
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;

                    string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                    string[] usernamepassword = decodedAuthToken.Split(':');

                    string username = usernamepassword[0];
                    string password = usernamepassword[1];

                    if (ValidateUser.Login(username, password))
                    {
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username),null);
                       
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Invaild Credentials");
                    }
                }
                catch (Exception)
                {

                    actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.InternalServerError, " InternalServerError - Please try after sometime");
                }

            }
        }
    }
}
