'use strict';

heartBeatApp.controller('detailsController', function ($scope, $routeParams, heartBeatService) {

    $scope.customer = {
        id: $routeParams.Id,
        name: $routeParams.CustomerName,
    }

    $scope.customerDetails = heartBeatService.getCustomer($routeParams.Id)
        .then(function (response) {
            $scope.messages = response.data;

            for (var i = 0; i < $scope.messages.length; i++) {
                $scope.messages[i].errorMessage = errorMessage($scope.messages[i]);
            }

        })
        .catch(function (response) { console.log(response) });

    var errorMessage = function (message) {
        var msg = "";

        if (!message.UniSqlOk) {
            msg += " UniSql,";
        }
        if (!message.V3Ok) {
            msg += " V3,";
        }
        if (!message.RedisOk) {
            msg += " Redis,";
        }
        if (!message.SqlOk) {
            msg += " Sql";
        }
        if (msg != "") {
            msg = "Feil: " + msg;
        }

        return msg;
    }

});