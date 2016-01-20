app.controller('businessController', ['$http', '$scope', function ($http, $scope) {

    var business = function () {
        $http.get('/account/businessdirectory').then(function (result) {
            $scope.business = result.data;
        });
    }

    business();
}]);