angular.module('SchedulingApp', [])
    .controller('SummaryController', function ($scope, $http, $window, $timeout) {
        $scope.runs;
        $scope.detail = [];

        $scope.detailExists = function () {
            return Object.keys($scope.detail).length > 0;
        };

        $scope.runMRP = function () {
            $http.get("/api/TriggerMRP").then(function (response) {

                // Show toast
                $('.toast').toast('show');

                // Wait 2 seconds before reloading the page
                $timeout(function () {
                    reload();
                }, 2000)
            });
        };

        reload = function () {
            $window.location.reload();
        }

        $http.get("/api/GetRuns").then(function (response) {
            $scope.runs = response.data;
        });

        $scope.loadDetails = function (runID) {
            $http.get("/api/GetRunInfo/" + runID).then(function (response) {
                $scope.detail = response.data;
            });
        };
    });