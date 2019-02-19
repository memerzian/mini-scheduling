angular.module('SchedulingApp', [])
    .controller('ProgressController', function ($scope, $http) {
        $scope.masterSchedules;

        $http.get("/api/MasterSchedule").then(function (response) {
            $scope.masterSchedules = response.data;
        });

        $scope.answer = function () {
            return $scope.correctAnswer ? 'correct' : 'incorrect';
        };
    });