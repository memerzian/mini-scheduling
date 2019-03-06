angular.module('SchedulingApp', [])
    .controller('AllocationController', function ($scope, $http) {
        $scope.allocationStatus;

        $http.get("/api/GetAllParts").then(function (response) {
            $scope.parts = response.data;
        });

        $http.get("/api/PartAllocationStatus/6/9").then(function (response) {
            debugger;
            $scope.allocationStatus = response.data;
        });

    });