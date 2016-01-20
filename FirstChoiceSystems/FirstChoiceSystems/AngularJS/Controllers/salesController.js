app.controller('salesController', ['$http', '$scope', function ($http, $scope) {

    var sales = function () {
        $http.get('/sales/saleshistory').then(function (result) {
            $scope.sales = result.data;
        });
    }

    sales();

    var pendingSales = function() {
        $http.get('/sales/pendingsales').then(function(result) {
            $scope.pendingSales = result.data;
        });
    }

    pendingSales();
}]);