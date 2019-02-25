angular.module('SchedulingApp', [])
    .controller('SummaryController', function ($scope, $http) {

        $scope.runMRP = function () {
            $http.get("/api/TriggerMRP").then(function (response) {
            });
        };
    });