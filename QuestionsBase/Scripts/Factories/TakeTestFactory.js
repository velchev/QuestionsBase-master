var TakeTestFactory = function ($http, $q, $location) {
    return function (emailAddress) {
        //var deferredObject = $q.defer();
        console.log("TakeTestFactory called");
        console.log($location.absUrl());
        console.log($location.protocol());
        //var email = $scope.currentUser.email;
        $http.post(
                '/Account/Test1', {
                    email: emailAddress
                }
            ).success(function (data) {

                //$scope.models.helloAngular = data;

                console.log(data);
            });
    }
}

TakeTestFactory.$inject = ['$http', '$q', '$location'];