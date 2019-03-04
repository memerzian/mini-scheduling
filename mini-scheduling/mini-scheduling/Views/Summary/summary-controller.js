angular.module('SchedulingApp', [])
    .controller('SummaryController', function ($scope, $http) {

        $scope.runs;

        $scope.runMRP = function () {
            $http.get("/api/TriggerMRP").then(function (response) {
            });
        };

        $http.get("/api/GetRuns").then(function (response) {
            $scope.runs = response.data;
        });
    });