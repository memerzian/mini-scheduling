using min_scheduling.Models.Enums;
using mini_scheduling.Models;
using System;
using System.Collections.Generic;

namespace min_scheduling.MRP_Engine
{
    public class MRPEngine
    {
        public void RunMRP()
        {
            // Start a new run and add run to the database
            var dataService = new MRPDataService();
            int runID = dataService.RecordRun(Status.InProgress);

            try
            {
                // Load in all of the relevant data
                var dataLoader = new DataLoader();
                DataLoad data = dataLoader.LoadData();

                // Determine the order that the parts can be processed in
                var dependencyAnalyzer = new DependencyAnalyzer();
                List<int> partIDOrder = dependencyAnalyzer.AnalyzeDependencies(data.BomRequirements, data.WorkOrderRequirements, data.PartDictionary);

                // Allocate and plan based on demand and supply
                var planner = new Planner();
                MRPResult results = planner.Plan(data, partIDOrder);

                // Commit to the database
                var dataCommitter = new DataCommitter();
                dataCommitter.CommitData(runID, results);

                // Mark run as completed
                dataService.UpdateRun(runID, Status.Completed);
            }
            catch (Exception e)
            {
                // Mark run as failed if there is an error
                dataService.UpdateRun(runID, Status.Failed);
            }
        }
    }
}