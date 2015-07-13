angular.module('GapandApp', ["kendo.directives"])
    .controller('GapandCtrl', function ($scope, $http) {
        $scope.follower = [];
        $scope.grid = false;
        $scope.button = false;
        if ($scope.follower.length === 0) {
            $http.get("/api/twitter").success(function (data, status, headers, config) {
                $scope.follower = data;
                $scope.source = new kendo.data.DataSource({
                    data: $scope.follower,
                    pageSize: 24
                });
                $scope.button = true;
            }).error(function (data, status, headers, config) {

            });
        }
        $scope.showGrid = function () {

            return $scope.grid;
        }
        $scope.showData = function () {
            return $scope.button;
        }

        $scope.validate = function (event) {
            event.preventDefault();

            var obj = document.createElement("audio");

            setTimeout(
                function () {
                    $('#minion-overlay').toggle();
                    obj.src = "/Content/sounds/minions-hahaha.mp3";
                    obj.volume = 1;
                    obj.autoPlay = false;
                    obj.preLoad = true;
                    obj.play();

                },
                2750);
            $('#minion-overlay').toggle();
            obj.src = "/Content/sounds/minions-fireman.mp3";
            obj.volume = 1;
            obj.autoPlay = false;
            obj.preLoad = true;
            obj.play();

            $scope.followerFilter = $scope.follower;
            if ($scope.fullName !== undefined) {
                $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                    if (element.name.toUpperCase().search($scope.fullName.toUpperCase()) >= 0) { return element }
                });

            }
            if ($scope.Sexo === "Hombre") {
                $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                    if (element.gender === "male") {
                        return element;
                    }
                });
            }
            if ($scope.Sexo === "Mujer") {
                $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                    if (element.gender === "female") {
                        return element;
                    }
                });
            }
            if ($scope.edadIni !== undefined) {
                $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                    if (element.age >= $scope.edadIni) {
                        return element;
                    }
                });
            }

            if ($scope.edadFin !== undefined) {
                $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                    if (element.age <= $scope.edadFin) {
                        return element;
                    }
                });
            }
            if ($scope.similar === true) {
                $scope.followerFilter = $scope.follower;
                if ($scope.Sexo === "Hombre") {
                    $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                        if (element.gender === "male") {
                            return element;
                        }
                    });
                }
                if ($scope.Sexo === "Mujer") {
                    $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                        if (element.gender === "female") {
                            return element;
                        }
                    });
                }
                var id = 0;
                var max = 0;
                var i = 0;
                $scope.followerFilter.forEach(function (element) {
                    if (element.similar > max) {
                        id = i;
                        max = element.similar;
                    }
                    i++;
                });
                $scope.followerFilter = [$scope.followerFilter[id]];
            }
            if ($scope.similarChuck === true) {
                $scope.followerFilter = $scope.follower;
                if ($scope.Sexo === "Hombre") {
                    $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                        if (element.gender === "male") {
                            return element;
                        }
                    });
                }
                if ($scope.Sexo === "Mujer") {
                    $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                        if (element.gender === "female") {
                            return element;
                        }
                    });
                }
                var id = 0;
                var max = 0;
                var i = 0;
                $scope.followerFilter.forEach(function (element) {
                    if (element.similarChuck > max) {
                        id = i;
                        max = element.similarChuck;
                    }
                    i++;
                });
                $scope.followerFilter = [$scope.followerFilter[id]];
            }
            if ($scope.similarCarmen === true) {
                $scope.followerFilter = $scope.follower;
                if ($scope.Sexo === "Hombre") {
                    $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                        if (element.gender === "male") {
                            return element;
                        }
                    });
                }
                if ($scope.Sexo === "Mujer") {
                    $scope.followerFilter = $scope.followerFilter.filter(function (element) {
                        if (element.gender === "female") {
                            return element;
                        }
                    });
                }
                var id = 0;
                var max = 0;
                var i = 0;
                $scope.followerFilter.forEach(function (element) {
                    if (element.similarCarmen > max) {
                        id = i;
                        max = element.similarCarmen;
                    }
                    i++;
                });
                $scope.followerFilter = [$scope.followerFilter[id]];
            }
            $scope.source = new kendo.data.DataSource({
                data: $scope.followerFilter,
                pageSize: 24
            });
            $scope.grid = true;
        }
    });