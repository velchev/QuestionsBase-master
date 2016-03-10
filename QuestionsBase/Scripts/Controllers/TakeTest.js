var TakeTestController = function ($scope, TakeTestFactory) {
    $scope.models = {
        helloAngular: ''
    };
    $scope.currentUser = {
        firstName: "John",
        lastName: "Doe",
        email: ""
    };

    $scope.showAnswer = false;


    $scope.fun1 = function () {
        console.log($scope.currentUser.firstName);
        //$scope.helloAngular = LyuboFactory();
        console.log($scope.currentUser.email);
        TakeTestFactory($scope.currentUser.email);

        //result.then(function (result) {
        //    console.log(result);
        //    if (result.success) {
        //        console.log('Success');

        //    } else {
        //        console.log('Fail');
        //    }
        //});
    };
    $scope.showAnswer = function() {
        console.log('showAnswer called');
    };

    $scope.name = "";

}

// The $inject property of every controller (and pretty much every other type of object in Angular) needs to be a string array equal to the controllers arguments, only as strings
TakeTestController.$inject = ['$scope', 'TakeTestFactory'];

