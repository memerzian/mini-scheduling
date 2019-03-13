angular.module('SchedulingApp', ['chart.js'])
    .controller('ProgressController', function ($scope, $http) {
        $scope.masterSchedules;
        $scope.parts;

        $scope.partDictionary;

        $scope.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
        $scope.data = [300, 500, 100];

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
            $http.get("/api/MasterScheduleProgress").then(function (response) {
                $scope.masterSchedules = response.data;

             
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

        reload = function () {
            $window.location.reload();
        }
    });