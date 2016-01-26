app.controller('purchaseController', ['$http', '$scope', function ($http, $scope) {

    var purchases = function() {
        $http.get('/purchase/purchaserequesthistory').then(function(result) {
            $scope.purchases = result.data;
        });
    }

    purchases();
}]);