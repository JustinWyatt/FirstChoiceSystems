app.controller('itemController', function ($http, $scope, $routeParams) {

    var itemDetails = function () {

        $http.get('/marketplace/marketplaceitem?itemid=' + $routeParams.id).then(function (result) {
            $scope.itemDetails = result.data;
        });
    }

    itemDetails();
})