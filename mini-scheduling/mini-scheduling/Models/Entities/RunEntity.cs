using System;

namespace mini_scheduling.Models
{
    public class RunEntity
    {
        public int RunID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusID { get; set; }
    }
}