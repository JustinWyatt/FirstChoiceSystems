var app = angular.module("app", ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/account/portal",
        {
            controller: 'DashboardController',
            templateUrl: '/AngularViews/dashboard.html'
        })

        .when("/item/:id",
        {
            controller: 'itemController',
            templateUrl: '/AngularViews/itemdetails.html'
        })

        .when("/purchases",
        {
            template: '<h1>Purchases</h1>'
        })

        .when("/sales",
        {
            template: '<h1>Sales</h1>'
        })

        .when("/myserviceads",
        {
            template: '<h1>My Service Ads</h1>'
        })

        .when("/reports",
        {
            template: '<h1>Reports</h1>'
        })

        .when("/marketplace",
        {
            template: '<h1>Marketplace</h1>'
        })

        .when("/marketplaceitems/:id",
        {
            template: '<h1>MarketPlaceItem</h1>'
        })

        .when("/businessdirectory",
        {
            template: '<h1>BusinessDirectory</h1>'
        })

        .when("/profile/:id",
        {
            template: '<h1>Profile</h1>'
        })

        .when("/servicedirectory",
        {
            template: '<h1>ServiceDirectory</h1>'
        })

        .when("/servicead/:id",
        {
            template: '<h1>ServiceAd</h1>'
        })

        .when("/shoppingcart",
        {
            template: '<h1>ShoppingCart</h1>'
        })

        .when("/account",
        {
            template: '<h1>Account</h1>'
        })

        .when("/checkoutresult/:id",
        {
            template: '<h1>CheckoutResult</h1>'
        })

        .when("/inventory",
        {
            template: '<h1>Inventory</h1>'
        })

        .otherwise({ redirectTo: '/account/portal' });
});