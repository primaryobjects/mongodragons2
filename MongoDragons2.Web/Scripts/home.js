var app = angular.module('homeApplication', []);

app.controller('homeController', function ($scope, $http) {
    $http.get('/service/dragons').success(function (data) {
        $scope.dragons = data;
    });

    $scope.spawn = function () {
        $http.post('/service/spawn').success(function (data) {
            $scope.dragons.push(data);
        });
    }

    $scope.remove = function (dragon) {
        if (confirm('Are you sure?')) {
            $http.post('/service/remove', JSON.stringify(dragon)).success(function (result) {
                if (result) {
                    $scope.dragons.splice($scope.dragons.indexOf(dragon), 1);
                }
            });
        }
    }
});