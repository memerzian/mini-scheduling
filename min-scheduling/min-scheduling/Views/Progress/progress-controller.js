angular.module('SchedulingApp', [])
    .controller('ProgressController', function ($scope, $http) {
        $scope.correctAnswer = false;

        $scope.answer = function () {
            return $scope.correctAnswer ? 'correct' : 'incorrect';
        };
    });