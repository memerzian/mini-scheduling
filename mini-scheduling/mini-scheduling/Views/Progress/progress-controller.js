angular.module('SchedulingApp', [])
    .controller('ProgressController', function ($scope, $http) {
        $scope.masterSchedules;

        $http.get("/api/MasterSchedules").then(function (response) {
            $scope.masterSchedules = response.data;
        });
    });