using min_scheduling.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace min_scheduling.MRP_Engine
{
    public class MRPEngine
    {
        public void RunMRP()
        {
            // Start a new run and add run to the database
            var dataService = new MRPDataService();
            dataService.RecordRun(Status.InProgress);


        }
    }
}