var app = angular.module("app", ['ngRoute', 'ngAnimate']);

app.config(function ($routeProvider) {
    $routeProvider
        .when("/account/portal",
        {
            controller: 'DashboardController',
            templateUrl: '/AngularViews/test.html'
        })

        .when("/item/:id",
        {
            controller: 'itemController',
            templateUrl: '/AngularViews/itemdetails.html'
        })

        .otherwise({ redirectTo: '/account/portal' });
});