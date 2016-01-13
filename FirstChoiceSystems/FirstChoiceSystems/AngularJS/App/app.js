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

        .when("/profile/:id",
        {
            templateUrl: '/AngularViews/screentest.html'
        })

        .when("/servicead/:id",
        {
            templateUrl: '/AngularViews/servicescreentest.html'
        })

        .when("/reports",
        {
            templateUrl: '/AngularViews/reportsscreentest.html'
        })

        .when("/marketplace",
        {
            templateUrl: '/AngularViews/marketplacescreentest.html'
        })

        .when("/businessdirectory",
        {
            templateUrl: '/AngularViews/businessscreentest.html'
        })

        .when("/servicedirectory",
        {
            templateUrl: '/AngularViews/servicescreentest.html'
        })

        .when("/shoppingcart",
        {
            templateUrl: '/AngularViews/shoppingscreentest.html'
        })

        .when("/account",
        {
            templateUrl: '/AngularViews/accountscreentest.html'
        })

        .otherwise({ redirectTo: '/account/portal' });
});