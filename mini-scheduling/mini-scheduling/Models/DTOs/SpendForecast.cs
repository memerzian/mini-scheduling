using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class SpendForecast
    {
        public string MonthYear { get; set; }
        public Dictionary<string, int> ForecastItems {get; set;}
    }
}