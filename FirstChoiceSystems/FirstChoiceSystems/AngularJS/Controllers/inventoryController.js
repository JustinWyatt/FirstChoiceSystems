app.controller('inventoryController', ['$http', '$scope', function ($http, $scope) {

    $scope.inventory = [];  

    var inventory = function () {

        $http.get('/inventory/inventory').then(function (result) {
            $scope.inventory = result.data;
            console.log(result.data);   
        });
    }

    inventory();

    $scope.addItem = function(data) {
        $http.post('/inventory/additem', { item: data }).then(function (result) {
            console.log(result.data);
            $scope.inventory.push(result.data);
            $scope.item.ItemName = '';
            $scope.item.ItemDescription = '';
            $scope.item.UnitsAvailable = '';
            $scope.item.PricePerUnit = '';
        });
    }

    $scope.removeItem = function (id) {
        $http.post('/inventory/removeitem?id=' + id).success(function () {
            for (var i = 0; i < $scope.inventory.length; i++)
                if ($scope.inventory[i].ItemId === id) {
                    $scope.inventory.splice(i, 1);
                }
            console.log($scope.inventory);
        });
    }
}])