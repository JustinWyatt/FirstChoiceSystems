app.controller('DashboardController', function ($http, $scope) {

    var purchases = function() {
        $http.get('/purchase/purchaserequesthistory').then(function (result) {
            $scope.purchases = result.data;
        }); 
    }

    purchases();

    var sales = function() {
        $http.get('/sales/pendingsales').then(function (result) {
            $scope.sales = result.data;
        });
    }

    sales();

    var marketplace = function() {
        $http.get('/marketplace/marketplace').then(function (result) {
            $scope.marketplace = result.data;
        });
    }

    marketplace();

    $scope.approveSale = function(id) {
        $http.post('/sales/approvesale?id=' + id).then(function(result) {
            $scope.saleItem = result.data;
            console.log(result.data);
        });
    }

    $scope.rejectSale = function (id) {
        $http.post('/sales/rejectsale?id=' + id).then(function (result) {
            $scope.saleItem = result.data;
            console.log(result.data);
        });
    }


})