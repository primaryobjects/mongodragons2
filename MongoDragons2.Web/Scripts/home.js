var app = angular.module('homeApplication', []);

// Route configuration.
app.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode(true);
}]);

app.controller('homeController', function ($scope, $http, $location) {
    $scope.q = $scope.q || $location.search().q;

    $http.get('/service/dragons?q=' + ($scope.q ? $scope.q : '')).success(function (data) {
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

    $scope.search = function () {
        // Reset any filter.
        $scope.keyword = null;

        // Set search term.
        $scope.q = $('#txtKeyword').val();

        // Update the url.
        $location.search('q', $scope.q ? $scope.q : null);

        // Render.
        $http.get('/service/dragons?q=' + ($scope.q ? $scope.q : '')).success(function (data) {
            $scope.dragons = data;
        });
    }

    $scope.filter = function () {
        $scope.keyword = { Name: $('#txtKeyword').val() };
    }
});