using System.Web.Mvc;
using System.Web.Security;
using System.Security.Principal;
//using Roblox.ActiveDirectory;
using Roblox.Configuration.Site.ViewModels;

namespace Roblox.Configuration.Site.Controllers
{
    [RoutePrefix("")]
    public class AccountController : Controller
    {
        private ActionResult ShowError(string errorMessage, string returnUrl) => RedirectToAction("Login", new { ReturnUrl = returnUrl, ErrorMessage = errorMessage });

        [Route("Logout")]
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.User = null;

            return RedirectToAction("Login", new { ReturnUrl = "/" });
        }

        [Route("Login")]
        [HttpGet]
        public ActionResult Login(string returnUrl = "/", string errorMessage = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
                return RedirectToAction("Index");
            }

            if (string.IsNullOrEmpty(errorMessage)) errorMessage = null;
            if (string.IsNullOrEmpty(returnUrl)) returnUrl = "/";

            return View(
                new LoginViewModel
                {
                    ErrorMessage = errorMessage,
                    ReturnUrl = returnUrl
                }
            );
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult PostLogin(string userName, string password, string returnUrl = "/")
        {
#if DEBUG
            if (userName == "testing")
            {
                FormsAuthentication.SetAuthCookie(userName.ToLower(), true);
                AuthenticateThisRequest();

                return Redirect(returnUrl);
            }
#endif

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return ShowError("Please fill out all fields.", returnUrl);

            /*userName = GetUsername(userName);

            if (ActiveDirectoryAccessVerifier.VerifyActiveDirectoryPassword(userName, password, global::Roblox.Configuration.Site.Properties.Settings.Default.ActiveDirectoryDomain))
            {
                FormsAuthentication.SetAuthCookie(userName.ToLower(), true);
                AuthenticateThisRequest();

                return Redirect(returnUrl);
            }*/

            return ShowError("Invalid username or password.", returnUrl);
        }

        private string GetUsername(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;

            var split = userName.Split('\\');
            if (split.Length < 2) return userName;

            // We assume it's in the format 'DOMAIN\USERNAME'
            return split[1];
        }

        private void AuthenticateThisRequest()
        {
            //NOTE:  if the user is already logged in (e.g. under a different user account)
            //       then this will NOT reset the identity information.  Be aware of this if
            //       you allow already-logged in users to "re-login" as different accounts 
            //       without first logging out.
            if (HttpContext.User.Identity.IsAuthenticated) return;

            var name = FormsAuthentication.FormsCookieName;
            var cookie = Response.Cookies[name];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null && !ticket.Expired)
                {
                    string[] roles = (ticket.UserData ?? "").Split(',');
                    HttpContext.User = new GenericPrincipal(new FormsIdentity(ticket), roles);
                }
            }
        }
    }
}
