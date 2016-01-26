app.controller('orderController', ['$http', '$scope', function ($http, $scope) {

    var order = function () {
        $http.get('/order/order').then(function (result) {
            $scope.order = result.data;
            var tax = result.data.Tax;
            var brokerFee = result.data.BrokerFee;
            $scope.orderTotal = (tax + brokerFee).toFixed(2);
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