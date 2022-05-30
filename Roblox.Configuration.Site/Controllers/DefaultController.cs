using System.Web.Mvc;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    [RoutePrefix("")]
    public class DefaultController : Controller
    {
        //
        // GET: /

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
