using min_scheduling.Models.Enums;
using mini_scheduling.DAL;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace mini_scheduling.Controllers
{
    public class SpendController : ApiController
    {
        private SchedulingContext db = new SchedulingContext();

        [Route ("api/SpendForecast/{runID}")]
        public IHttpActionResult GetSpendForecast(int runID)
        {
            var acceptableTypes = new int[]
            {
                (int)ObjectType.PurchaseOrder,
                (int)ObjectType.WorkOrder,
                (int)ObjectType.PlannedOrder
            };

            DateTime endDate = DateTime.Today.AddYears(1);

            var data = db.ScheduledObjects
                .Where(s => s.RunID == runID)
                .Where(s => acceptableTypes.Contains(s.TypeID))
                .Where(s => s.DueDate < endDate)
                .GroupBy(s => new { Month = s.DueDate.Month, Year = s.DueDate.Year, Type = s.Type.Name })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    Type = g.Key.Type,
                    Cost = g.Sum(so => so.Part.ThisLevelUnitCost * so.Quantity)
                })
                .ToArray();

            DateTime startDate = DateTime.Today;

            var forecasts = new List<SpendForecast>();

            while (startDate < endDate)
            {
                int month = startDate.Month;
                string monthValue = startDate.ToString("MMMM");
                int year = startDate.Year;

                var forecast = new SpendForecast
                {
                    MonthYear = monthValue + "/" + year,
                    ForecastItems = new Dictionary<string, int>()
                };

                forecast.ForecastItems = data
                    .Where(d => d.Month == month)
                    .Where(d => d.Year == year)
                    .ToDictionary(d => d.Type, d => d.Cost);

                forecasts.Add(forecast);

                startDate = startDate.AddMonths(1);
            }

            return Ok(forecasts);
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