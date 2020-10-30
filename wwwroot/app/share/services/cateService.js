angular.module("app").factory("cateService", [
    "$q",
    "$http",
    function ($q, $http) {
        return {
            reqLists: function () {
                console.log(`Category: get list data`);
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/category/listdata",
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
                if (!modified.id) defered.reject("id is null");
                modified.seoImage = "";
                $http({
                    method: "PUT",
                    url: "/admin/category/update",
                    params: { id: modified.id, item: JSON.stringify(modified) },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
        };
    },
]);
