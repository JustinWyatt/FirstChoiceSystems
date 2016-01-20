app.controller('profileController', function ($http, $scope, $routeParams) {
    var profile = function () {

        $http.get('/account/userprofile?id=' + $routeParams.id).then(function (result) {
            $scope.profile = result.data;
        });
    }
        
    profile();

    var myProfile = function() {
        $http.get('/account/myprofile').then(function(result) {
            $scope.myProfile = result.data;
            console.log(result.data)
        });
    }

    myProfile();

})