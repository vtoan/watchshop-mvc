angular.module("app").factory("policyService", [
    "$q",
    "$http",
    function ($q, $http) {
        return {
            reqLists: function () {
                console.log(`Policy: get list data`);
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/policy/listdata",
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqDelete: function (id) {
                console.log(`Policy: delete ${id}`);
                let defered = $q.defer();
                $http({
                    method: "PUT",
                    url: "/admin/policy/remove",
                    params: { id: id },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqUpdate: function (root, modified) {
                console.log(`Policy: update`);
                console.log("root");
                console.log(root);
                console.log("modified");
                console.log(modified);
                console.log("===========");
                let defered = $q.defer();
                if (!modified.id) defered.reject("id is null");
                modified.icon = "";
                $http({
                    method: "PUT",
                    url: "/admin/policy/update",
                    params: { id: modified.id, item: JSON.stringify(modified) },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqAdd: function (item) {
                console.log("Policy: add");
                console.log(item);
                console.log("===========");
                let defered = $q.defer();
                $http({
                    method: "POST",
                    url: "/admin/policy/add",
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
