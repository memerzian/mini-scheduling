using min_scheduling.Models.Enums;
using min_scheduling.MRP_Engine;
using mini_scheduling.DAL;
using mini_scheduling.Models;
using System.Linq;
using System.Web.Http;

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

        [Route("api/GetRuns")]
        public Run[] GetRuns()
        {
            return db.Runs
                .OrderByDescending(r => r.RunID)
                .Select(r => new Run
                {
                    RunID = r.RunID,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Status = r.Status.Name
                })
                .ToArray();
        }

        [Route("api/GetRunInfo/{runID}")]
        public RunDetail GetRunInfo(int runID)
        {
            RunEntity run = db.Runs.Find(runID);

            var detail = new RunDetail()
            {
                ElapsedMinutes = (int)(run.EndDate - run.StartDate).TotalMinutes
            };

            detail.Items = db.ScheduledObjects
                .Where(s => s.RunID == runID)
                .GroupBy(s => s.Type.Name)
                .Select(g => new RunDetailItem
                {
                    Type = g.Key,
                    Count = g.Count()
                }).ToArray();

            return detail;
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