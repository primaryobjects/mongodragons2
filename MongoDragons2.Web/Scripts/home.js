var app = angular.module('homeApplication', []);

app.controller('homeController', function ($scope, $http) {
    $http.get('/service/dragons').then(function(data) {
        $scope.dragons = data.data;
    });

    $scope.spawn = function () {
        $http.post('/service/spawn').then(function(data) {
            $scope.dragons.push(data.data);
        });
    }

    $scope.remove = function (dragon) {
        if (confirm('Are you sure?')) {
            $http.post('/service/remove', JSON.stringify(dragon)).then(function(data) {
                if (data.data) {
                    $scope.dragons.splice($scope.dragons.indexOf(dragon), 1);
                }
            });
        }
    }
});