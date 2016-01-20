var app = angular.module("app", ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/account/portal",
        {
            controller: 'dashboardController',
            templateUrl: '/AngularViews/dashboard.html'
        })
        .when("/item/:id",
        {
            controller: 'itemController',
            templateUrl: '/AngularViews/itemdetails.html'
        })
        .when("/purchases",
        {
            controller: 'purchaseController',
            templateUrl: '/AngularViews/purchases.html'
        })
        .when("/sales",
        {
            controller: 'salesController',
            templateUrl: '/AngularViews/sales.html'
        })
        .when("/marketplace",
        {
            controller: 'marketplaceController',
            templateUrl: '/AngularViews/marketplace.html'
        })
        .when("/yourmarketplaceitems",
        {
            controller: "yourMarketplaceController",
            templateUrl: "yourmarketplaceitems"
        })
        .when("/marketplaceitem/:id",
        {
            template: '<h1>MarketPlaceItem</h1>'
        })
        .when("/businessdirectory",
        {
            controller: 'businessController',
            templateUrl: '/AngularViews/businessdirectory.html'
        })
        .when("/profile/:id",
        {
            controller: 'profileController',
            templateUrl: '/AngularViews/profile.html'
        })
        .when("/order",
        {
            controller: 'orderController',
            templateUrl: '/AngularViews/order.html'

        })

.when("/account",
{
    controller: 'profileController',
    templateUrl: '/AngularViews/profile.html'
})

.when("/inventory",
{
    controller: 'inventoryController',
    templateUrl: '/AngularViews/inventory.html'
})

.otherwise({ redirectTo: '/account/portal' });
});