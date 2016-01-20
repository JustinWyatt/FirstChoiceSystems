app.controller('orderController', ['$http', '$scope', function ($http, $scope) {

    var order = function () {
        $http.get('/order/order').then(function (result) {
            $scope.order = result.data;
        });
    }

    order();

    var user = function() {
        $http.get('/user/user').then(function(result) {
            $scope.user = result.data;
        });
    }
}]);