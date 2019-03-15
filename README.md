# mini-scheduling
## Info
This is a basic application that runs a simple mrp algorithm and displays the data

## Why
This is a thought experiment for a potential manufacturing scheduling mini service

## How to use
1) Run MRP and see details (a count of supply/demand items) of past runs
2) Add/update master schedules
3) View mrp allocations for a given part/runs
4) View bills of material for a part
5) View spend forecast

## How to run this application
1) Use mySQL
2) Update web.config settings to your database in "connectionStrings" section
2) Run the "DatabaseCreateScript.mysql" script to populate all of the tables
3) Run the "DataSeedScript.mysql" to get some initial data

## Overview: I/O/Importance
| Inputs        	  	| Outputs        	| What can we do with that?  	|
| --------------------- | -----------------	| -----------------------------	|
| Parts         	  	| Planned Orders 	| % complete of builds 			|
| Master Prod Schedule  | ECDs		      	| Know which order to work on 	|
| BOMs			 		| Due Dates      	| Know which orders to create 	|
| Current Supply		| Critical Path     | Know how to allocate resources|
| Workcenter Routing	| 			      	| Labor/Material spend forecasts|
| Labor Estimates		| 			      	| Know which orders to push/pull|
| Capacity			 	| 			      	| 								|
| Cost			 		| 			      	| 								|

## Next steps
- Find a good way to pass secrets into web.config file
- MRP II capabilities
- Add more unit tests
- Add graph navigator
- Add gantt chart
- Add screen to see all planned orders and give a way to convert to WOs or POs


