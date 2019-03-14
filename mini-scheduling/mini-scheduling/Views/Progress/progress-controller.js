angular.module('SchedulingApp', ['chart.js'])
    .controller('ProgressController', function ($scope, $http) {
        $scope.masterSchedules;
        $scope.parts;

        $scope.partDictionary;

        $scope.labels;
        $scope.data;
        // https://www.color-hex.com/color-palette/74824
        $scope.colors = ['#7fe5b9', '#bde592', '#ffba50', '#ff9535', '#fc6060'];

        $scope.options = {
            legend:
            {
                display: true,
                position: 'right'
            }
        };

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

        $scope.loadDetails = function (masterScheduleID) {
            $http.get("/api/MasterScheduleProgress/" + masterScheduleID).then(function (response) {
                $scope.progressData = response.data;

                $scope.labels = Object.keys($scope.progressData.CostItems);
                $scope.data = Object.values($scope.progressData.CostItems);
            });
        }

        $scope.saveMasterSchedules = function (masterSchedules) {
            $http.put("/api/SaveMasterSchedules", JSON.stringify(masterSchedules)).then(function () {
                // Show toast
                $('.toast').toast('show');

                // Wait 2 seconds before reloading the page
                $timeout(function () {
                    reload();
                }, 2000);
            })
        }
    });