app.controller('DashboardController', function ($http, $scope) {

    var latestPurchases = function () {
        $http.get('/purchase/purchaserequesthistory').then(function (result) {
            $scope.latestPurchases = result.data;
        });
    }

    latestPurchases();

    var salesCount = function () {
        $http.get('/sales/pendingsales').then(function (result) {
            $scope.salesCount = result.data;
        });
    }

    salesCount();

    var newestMarketplaceItems = function () {
        $http.get('/marketplace/marketplace').then(function (result) {
            $scope.newestMarketplaceItems = result.data;
        });
    }

    newestMarketplaceItems();

    var inventoryCount = function () {
        $http.get('/inventory/inventory').then(function (result) {
            $scope.inventoryCount = result.data;
        });
    }

    inventoryCount();

    var businessCount = function () {
        $http.get('/business/businesscount').then(function (result) {
            $scope.businessCount = result.data;
        });
    }

    businessCount();

    var newServicesCount = function () {
        $http.get('/services/servicescount').then(function (resutl) {
            $scope.newServicesCount = result.data;
        });
    }

    newServicesCount();

    var totalProfit = function () {
        $http.get('/account/totalprofit').then(function (result) {
            $scope.totalProfit = result.data;
        });
    }

    totalProfit();

    var shoppingCart = function() {
        $http.get('order/order').then(function(result) {
            $scope.shoppingCart = result.data;
        });
    }

    shoppingCart();


    var totalRevenue = function() {
        $http.get('account/revenue').then(function(result) {
            $scope.totalRevenue = result.data;
        });
    }

    totalRevenue();

    var totalMoneySpent = function() {
        $http.get('account/totalmoneyspent').then(function(result) {
            $scope.totalCost = result.data;
        });
    }

    totalMoneySpent();
})