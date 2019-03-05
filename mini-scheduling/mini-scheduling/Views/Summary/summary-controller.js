angular.module('SchedulingApp', [])
    .controller('SummaryController', function ($scope, $http) {
        $scope.runs;
        $scope.detail;

        $scope.runMRP = function () {
            $http.get("/api/TriggerMRP").then(function (response) {
            });
        };

        $http.get("/api/GetRuns").then(function (response) {
            $scope.runs = response.data;
        });

        $scope.loadDetails = function (runID) {
            $http.get("/api/GetRunInfo/" + runID).then(function (response) {
                $scope.detail = response.data;
            });
        };
    });