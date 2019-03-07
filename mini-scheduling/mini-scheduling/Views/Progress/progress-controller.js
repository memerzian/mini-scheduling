angular.module('SchedulingApp', [])
    .controller('ProgressController', function ($scope, $http) {
        $scope.masterSchedules;
        $scope.parts;

        $scope.partDictionary;

        $http.get("/api/GetAllParts").then(function (response) {
            $scope.parts = response.data;
            $scope.partDictionary = _.keyBy($scope.parts, o => o.PartID);
        });

        $http.get("/api/MasterSchedules").then(function (response) {
            $scope.masterSchedules = response.data;

            $scope.masterSchedules.forEach(function (element) {
                element.Date = new Date(element.Date);
            });
        });

        $scope.addMasterSchedule = function () {
            var masterSchedule = {
                Name: "New",
                Date: ""
            }

            $scope.masterSchedules.push(masterSchedule);
        };

        $scope.saveMasterSchedules = function (masterSchedules) {
            $http.put("/api/SaveMasterSchedules", JSON.stringify(masterSchedules));
        }
    });