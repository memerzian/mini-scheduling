angular.module('SchedulingApp', [])
    .controller('SummaryController', function ($scope, $http) {
        $scope.runs;
        $scope.detail = [];

        $scope.detailExists = function () {
            return Object.keys($scope.detail).length > 0;
        };

        $scope.runMRP = function () {
            $http.get("/api/TriggerMRP").then(function (response) {
            });
        };

        $http.get("/api/GetRuns").then(function (response) {
            $scope.runs = response.data;
        });

        $scope.loadDetails = function (runID) {
            debugger;
            $http.get("/api/GetRunInfo/" + runID).then(function (response) {
                $scope.detail = response.data;
            });
        };
    });