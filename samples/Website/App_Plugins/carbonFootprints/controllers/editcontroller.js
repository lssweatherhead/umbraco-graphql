angular.module('umbraco').controller('UmbGQLDemo.EditCarbonFootprint.Controller', function ($scope, $routeParams, $http) {

    // Get the ID from the route parameters (URL)
    var itemId = $routeParams.id;

    // Get the Pokémon from the API
    $scope.variants = null;
    $http.get('/umbraco/backoffice/api/CarbonFootprintApi/GetVariants?itemId=' + itemId).success(function(response) {
      $scope.variants = response.variants;
      $scope.itemName = response.name;
    });

    $scope.options = {
      includeProperties: [
        { alias: "co2e", header: "Carbon Dioxide Equivalent (CO2e) [grams]" }
      ]
    };

});
