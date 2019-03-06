angular.module('SchedulingApp', [])
    .controller('AllocationController', function ($scope, $http) {
        $scope.allocationStatus;
        $scope.runs;

        $http.get("/api/GetRuns").then(function (response) {
            $scope.runs = response.data;
        });

        $http.get("/api/GetAllParts").then(function (response) {
            $scope.parts = response.data;
        });

        $scope.loadStatus = function (partID, runID) {
            $http.get("/api/PartAllocationStatus/" + partID + "/" + runID).then(function (response) {
                $scope.allocationStatus = response.data;
            });
        };
    });