using System;
using System.Collections.Generic;

namespace mini_scheduling.Models
{
    public class Run
    {
        public int RunID { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}