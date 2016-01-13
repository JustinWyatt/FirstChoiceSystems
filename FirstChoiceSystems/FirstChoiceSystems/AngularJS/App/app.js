var app = angular.module("app", ['ngRoute']);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/account/portal",
        {
            controller: 'DashboardController',
            templateUrl: '/AngularViews/screentest.html'
        })

        .when("/item/:id",
        {
            controller: 'itemController',
            templateUrl: '/AngularViews/itemdetails.html'
        })

        .when("/reports",
        {
            templateUrl: '/AngularViews/screentest.html'
        })

        .when("/marketplace",
        {
            templateUrl: '/AngularViews/screentest.html'
        })

        .when("/businessdirectory",
        {
            templateUrl: '/AngularViews/screentest.html'
        })

        .when("/servicedirectory",
        {
            templateUrl: '/AngularViews/screentest.html'
        })

        .when("/shoppingcart",
        {
            templateUrl: '/AngularViews/screentest.html'
        })

        .when("/account",
        {
            templateUrl: '/AngularViews/screentest.html'
        })

        .otherwise({ redirectTo: '/account/portal' });
});