'use strict';

heartBeatApp.factory("heartBeatService", function ($http) {
    var url = "/api/HeartBeat";

    return {

        getLastBeats: function () {
            return $http.get(url, {cache: true});
        },

        getLastBeatsFresh: function () {
            return $http.get(url, {cache: false});
        },
        
        getCustomer: function (Id) {
            return $http.get(url+ "/" +Id);
        },

    }

})