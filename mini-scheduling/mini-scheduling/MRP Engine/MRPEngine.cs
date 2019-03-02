﻿using min_scheduling.Models.Enums;
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
            dataService.RecordRun(Status.InProgress);

            // Load in all of the relevant data
            var dataLoader = new DataLoader();
            DataLoad data = dataLoader.LoadData();

            // Determine the order that the parts can be processed in
            var dependencyAnalyzer = new DependencyAnalyzer();
            List<int> partIDOrder = dependencyAnalyzer.AnalyzeDependencies(data.BomRequirements, data.WorkOrderRequirements, data.PartDictionary);

            // Allocate and plan based on demand and supply
            var planner = new Planner();
            MRPResults results = planner.Plan(data, partIDOrder);

            // Commit to the database

            // Mark run as completed


            Console.WriteLine();
        }
    }
}