using System.Web.Mvc;
using PagedList;

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

        //
        // GET: /list-groups
        // Max per page is 25

        [HttpGet]
        [Route("ListConfigurationGroups")]
        [Route("list-groups")]
        public ActionResult ListConfigurationGroups(int page = 1)
        {
            // TODO: Actual config groups from config service client impl
            if (page < 1)
                page = 1;

            int count = 150;
            string[] configGroups = new string[count];
            for (int i = 0; i < count; i++)
                configGroups[i] = "Config Group #" + i.ToString();

            return View(configGroups.ToPagedList(page, 25));
        }
    }
}
