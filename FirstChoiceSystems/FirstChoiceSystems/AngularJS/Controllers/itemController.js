app.controller('itemController', ['$http', '$scope', '$routeParams', function ($http, $scope, $routeParams) {

    var itemDetails = function () {

        $http.get('/marketplace/marketplaceitem?itemid=' + $routeParams.id).then(function (result) {
            $scope.itemDetails = result.data;
        });
    }

    itemDetails();

    var relatedItems = function(id) {

        $http.get('/marketplace/relateditems?id=' + id).then(function(result) {
            $scope.relatedItems = result.data;
        });
    }

    relatedItems($routeParams.id);

    $scope.addItem = function (id) {
        $http.post('/order/additem?itemid=' + id).success(function () {
            console.log('success');
        });
    }
}])