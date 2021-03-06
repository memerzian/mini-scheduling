﻿using min_scheduling.Models.Enums;
using mini_scheduling.DAL;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace min_scheduling.MRP_Engine
{
    public class MRPDataService
    {
        public SchedulingContext db = new SchedulingContext();

        public int RecordRun(Status status)
        {
            var run = new RunEntity
            {
                StartDate = DateTime.Now,
                StatusID = (int)status
            };

            db.Runs.Add(run);
            db.SaveChanges();

            return run.RunID;
        }

        public void UpdateRun(int runID, Status status)
        {
            RunEntity run = db.Runs.Find(runID);

            run.StatusID = (int)status;
            run.EndDate = DateTime.Now;
            db.SaveChanges();
        }
    }
}