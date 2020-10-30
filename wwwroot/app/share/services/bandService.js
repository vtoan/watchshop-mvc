angular.module("app").factory("bandService", [
    "$q",
    "$http",
    function ($q, $http) {
        return {
            reqLists: function () {
                console.log(`Band: get list data`);
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/band/listdata",
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqDelete: function (id) {
                console.log(`Band: delete ${id}`);
                let defered = $q.defer();
                $http({
                    method: "PUT",
                    url: "/admin/band/remove",
                    params: { id: id },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqUpdate: function (root, modified) {
                console.log(`Band: update`);
                console.log("root");
                console.log(root);
                console.log("modified");
                console.log(modified);
                console.log("===========");
                let defered = $q.defer();
                if (!modified.id) defered.reject("id is null");
                $http({
                    method: "PUT",
                    url: "/admin/band/update",
                    params: { id: modified.id, item: JSON.stringify(modified) },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqAdd: function (item) {
                console.log("Band: add");
                console.log(item);
                console.log("===========");
                let defered = $q.defer();
                $http({
                    method: "POST",
                    url: "/admin/band/add",
                    params: { item: JSON.stringify(item) },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
        };
    },
]);
