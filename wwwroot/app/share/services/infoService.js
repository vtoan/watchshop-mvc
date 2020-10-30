angular.module("app").factory("infoService", [
    "$q",
    "$http",
    function ($q, $http) {
        return {
            reqData: function () {
                console.log(`Info: get data`);
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/info/data",
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqUpdate: function (root, modified) {
                console.log(`Category: update`);
                console.log("root");
                console.log(root);
                console.log("modified");
                console.log(modified);
                console.log("===========");
                let defered = $q.defer();
                modified.logo = "";
                modified.seoImage = "";
                $http({
                    method: "PUT",
                    url: "/admin/info/update",
                    params: { item: modified },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
        };
    },
]);
