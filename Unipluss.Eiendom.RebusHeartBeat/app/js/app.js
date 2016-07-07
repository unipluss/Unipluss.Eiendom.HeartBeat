
var heartBeatApp = angular.module('heartBeatApp', ['ngRoute', 'ngMaterial'])
    .config(

    function ($routeProvider, $mdThemingProvider) {
        $routeProvider
            .when('/status', {
                templateUrl: '/app/templates/status.html',
                controller: 'statusController'
            })
            .when('/details/:Id/:CustomerName', {
                templateUrl: '/app/templates/details.html',
                controller: 'detailsController'
            })
            .otherwise({
                redirectTo: '/status'
            });

        $mdThemingProvider.theme('default')
            .primaryPalette('blue-grey')
            .accentPalette('deep-orange');
    });
