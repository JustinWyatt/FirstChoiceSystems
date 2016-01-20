app.controller('inventoryController', function ($http, $scope) {

    var inventory = function () {

        $http.get('/inventory/inventory').then(function (result) {
            $scope.inventory = result.data;
        });
    }

    inventory();

    $scope.addItem = function(data) {
        $http.post('inventory/additem', { item: data } ).then(function(result) {
            $scope.inventory.push(result.data);
        });
    }

    $scope.removeItem = function (id) {
        $http.post('/inventory/additem?id=' + id).then(function (result) {
            $scope.inventory.remove(result.data);
        });
    }
})