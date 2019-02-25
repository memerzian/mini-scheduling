using min_scheduling.MRP_Engine;
using mini_scheduling.DAL;
using mini_scheduling.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace mini_scheduling.Controllers
{
    public class SummaryController : ApiController
    {
        private SchedulingContext db = new SchedulingContext();

        [Route ("api/TriggerMRP")]
        public IHttpActionResult GetTriggerMRP()
        {
            var mrpEngine = new MRPEngine();
            mrpEngine.RunMRP();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}