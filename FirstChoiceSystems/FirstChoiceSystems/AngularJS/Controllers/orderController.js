app.controller('orderController', ['$http', '$scope', function ($http, $scope) {

    var order = function () {
        $http.get('/order/order').then(function (result) {
            $scope.order = result.data;
            console.log('order');
            console.log(result.data);
        });
    }
    order();

    var user = function() {
        $http.get('/account/dashboard').then(function(result) {
            $scope.user = result.data;
        });
    }

    user();

    $scope.makePurchase = function() {
        $http.post('/purchase/purchaserequest').success(function() {
            console.log('success');
        });
    }
}]);