'use strict';

heartBeatApp.controller('statusController', function ($scope, $timeout, heartBeatService) {

    updateValues();

    function updateValues() {
        heartBeatService.getLastBeats()
            .then(function (response) {

                $scope.lastBeats = response.data;

                var dateLast = $scope.lastBeats[0].Time;
                var dateDelay = $scope.lastBeats[0].Time;
                var customerLastUpdated = $scope.lastBeats[0].CustomerName;
                var customerFirstUpdated = $scope.lastBeats[0].CustomerName;

                for (var i = 0; i < $scope.lastBeats.length; i++) {
                    if($scope.lastBeats[i].Time > dateLast){
                        dateLast = $scope.lastBeats[i].Time;
                        customerLastUpdated = $scope.lastBeats[i].CustomerName;
                    }
                    if($scope.lastBeats[i].Time < dateDelay){
                        dateDelay = $scope.lastBeats[i].Time;
                        customerFirstUpdated = $scope.lastBeats[i].CustomerName;
                    }
                    if ($scope.lastBeats[i].RebusAlive &&
                        $scope.lastBeats[i].UniSqlOk &&
                        $scope.lastBeats[i].V3Ok &&
                        $scope.lastBeats[i].RedisOk &&
                        $scope.lastBeats[i].SqlOk
                    ) {
                        $scope.lastBeats[i].AllOk = true;
                        $scope.lastBeats[i].statusText = 'Online';
                        $scope.lastBeats[i].errorStatus = 0;
                    }
                    else {
                        $scope.lastBeats[i].errorStatus = 2;
                        $scope.lastBeats[i].AllOk = false;
                        $scope.lastBeats[i].statusText = errorMessage($scope.lastBeats[i], i);
                    }
                }

                $scope.lastUpdate = dateLast;
                $scope.lastUpdateName = customerLastUpdated;
                $scope.delayedUpdate = dateDelay;
                $scope.delayedUpdateName = customerFirstUpdated;

                $timeout(function () {
                    updateValues();
                }, 120000);
            })
            .catch(function (response) {
                console.log(response)
            })
    };



    var errorMessage = function (message, messageNr) {
        var msg = "Feil: ";
        
        if (message.uaUrl != null && message.uaUrl != "Missing url") {

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
        }
        else {
            msg = "Unialltid url mangler";
            $scope.lastBeats[messageNr].errorStatus = 1;
        }

        if (!message.RebusAlive) {
            msg = "Rebus offline!";
        }
        return msg;
    }
});