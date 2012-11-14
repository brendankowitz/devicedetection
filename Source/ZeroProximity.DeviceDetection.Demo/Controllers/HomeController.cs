using System.Web.Mvc;
using ZeroProximity.DeviceDetection.Demo.Models;
namespace ZeroProximity.DeviceDetection.Demo.Controllers
{
    public class HomeController : Controller
    {
        private static readonly IMobileDeviceDetection LevenshtienDetection;

        static HomeController()
        {
            LevenshtienDetection = new LevenshtienDistanceDeviceDetection();
        }

        public ActionResult Index(string customUA = null)
        {
            var model = new UserAgentModel();

            if (string.IsNullOrWhiteSpace(customUA))
                customUA = null;
            model.UA = customUA ?? Request.UserAgent;
            model.MatchingDevice = LevenshtienDetection.Match(model.UA);
            

            return View(model);
        }
         
    }
}