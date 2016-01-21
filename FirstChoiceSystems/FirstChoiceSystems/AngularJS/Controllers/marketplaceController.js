app.controller('marketplaceController', ['$http', '$scope', function ($http, $scope) {

    var marketplace = function() {
        $http.get('/marketplace/marketplace').then(function(result) {
            $scope.marketplace = result.data;
        });
    }

    marketplace();

    $scope.addItem = function(id) {
        $http.post('/order/additem?itemid=' + id).success(function() {
            console.log('success');
        });
    }
}]);