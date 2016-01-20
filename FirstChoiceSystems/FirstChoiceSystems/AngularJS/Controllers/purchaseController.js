app.controller('purchaseController', function ($http, $scope) {

    var purchases = function() {
        $http.get('/purchases/purchaserequesthistory').then(function(result) {
            $scope.purchases = result.data;
        });
    }

    purchases();
});