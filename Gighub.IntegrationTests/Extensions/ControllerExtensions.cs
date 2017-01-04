using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace GigHub.IntegrationTests.Extensions
{
    public static class ControllerExtensions
    {
        public static void MockCurrentUser(this Controller controller, string userId, string userName)
        {
            var identity = new GenericIdentity(userName);
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userName));
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));

            var prinicpal = new GenericPrincipal(identity, null);

            controller.ControllerContext = Mock.Of<ControllerContext>(
                ctx => ctx.HttpContext == Mock.Of<HttpContextBase>(
                    http => http.User == prinicpal));
        }
    }
}
