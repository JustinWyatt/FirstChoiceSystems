app.controller('marketplaceController', function ($http, $scope) {

    var marketplace = function() {
        $http.get('/marketplace/marketplace').then(function(result) {
            $scope.marketplace = result.data;
        });
    }

    marketplace();
});