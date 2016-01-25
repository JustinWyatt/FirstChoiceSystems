app.controller('marketplaceController', ['$http', '$scope', function ($http, $scope) {

    var marketplace = function() {
        $http.get('/marketplace/marketplace').then(function(result) {
            $scope.marketplace = result.data;
        });
    }

    marketplace();

    
}]);