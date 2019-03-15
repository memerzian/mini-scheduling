angular.module('SchedulingApp', ['chart.js'])
    .controller('SpendController', function ($scope, $http) {
        $scope.runs;
        $scope.forecastData;
        $scope.labels;

        $scope.series = ['Purchase Orders', 'Work Orders', 'Planned Orders'];
        $scope.data;

        https://www.color-hex.com/color-palette/807
        $scope.colors = ['#fdf498', '#f37736', '#0392cf'];

        $scope.purchaseOrderData;
        $scope.workOrderData;
        $scope.plannedOrderData;

        $http.get("/api/GetRuns").then(function (response) {
            $scope.runs = response.data;
        });

        $scope.loadForecast = function (runID) {
            $http.get("/api/SpendForecast/" + runID).then(function (response) {
                $scope.forecastData = response.data;
                $scope.labels = $scope.forecastData.map(f => f.MonthYear);

                $scope.purchaseOrderData = [];
                $scope.workOrderData = []
                $scope.plannedOrderData = [];

                $scope.forecastData.forEach(function (item) {
                    if (item.ForecastItems["Purchase Order"] !== undefined) {
                        $scope.purchaseOrderData.push(item.ForecastItems["Purchase Order"]);
                    }
                    else {
                        $scope.purchaseOrderData.push(0)
                    }

                    if (item.ForecastItems["Work Order"] !== undefined) {
                        $scope.workOrderData.push(item.ForecastItems["Work Order"]);
                    }
                    else {
                        $scope.workOrderData.push(0)
                    }

                    if (item.ForecastItems["Planned Order"] !== undefined) {
                        $scope.plannedOrderData.push(item.ForecastItems["Planned Order"]);
                    }
                    else {
                        $scope.plannedOrderData.push(0)
                    }
                });

                $scope.data = [$scope.purchaseOrderData, $scope.workOrderData, $scope.plannedOrderData];
            });
        };
        
        $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }];

        $scope.options = {
            scales: {
                yAxes: [{
                        id: 'y-axis-1',
                        type: 'linear',
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Spend'
                        },
                        position: 'left',
                        stacked: true,
                        ticks: {
                            callback: function (value, index, values) {
                                return '$' + value;
                            }
                        }
                    }
                ]
            },
            legend:
            {
                display: true,
                position: 'right'
            }
        };
    });