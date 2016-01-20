app.controller('dashboardController', ['$http', '$scope', function ($http, $scope) {


    var dashboard = function() {
        $http.get('/account/dashboard').then(function(result) {
            console.log(result.data);
            console.log($scope.dashboard);
            $scope.dashboard = result.data;
        });
    }
    dashboard();

    var shoppingCart = function() {
        $http.get('/order/order').then(function (result) {
            $scope.shoppingCart = result.data.Items.length;
            console.log(result.data.Items.length);
        });
    }
    shoppingCart();

    var savingsCaluclation = function() {
        $http.get('/account/savingscalculation').then(function(result) {
            $scope.savingsCaluclation = result.data;
            console.log("savings : "  + result.data);
        });
    }
    savingsCaluclation();

    $scope.logOff = function() {
        $http.post('/account/logoff').success(function() {
            console.log('success');
        });
    }

}])