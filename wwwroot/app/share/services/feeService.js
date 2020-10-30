angular.module("app").factory("feeService", [
    "$q",
    "$http",
    function ($q, $http) {
        function parseDiscountVal(valDiscount) {
            if (!valDiscount.endsWith("%")) return Number(valDiscount);
            //
            valDiscount = valDiscount.substring(0, valDiscount.length - 1);
            return +valDiscount / 100;
        }

        return {
            reqLists: function () {
                console.log(`Fee: get data`);
                //
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/fee/listdata",
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqDelete: function (id) {
                console.log(`Fee: delete ${id}`);
                //
                let defered = $q.defer();
                $http({
                    method: "PUT",
                    url: "/admin/fee/remove",
                    params: { id: id },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqUpdate: function (root, modified) {
                console.log(`Fee: update`);
                console.log("root");
                console.log(root);
                console.log("modified");
                console.log(modified);
                console.log("===========");
                let defered = $q.defer();
                if (!modified.id) defered.reject("id is null");
                modified.cost = parseDiscountVal(modified.cost);
                console.log(modified);
                $http({
                    method: "PUT",
                    url: "/admin/fee/update",
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
                item.cost = parseDiscountVal(item.cost);
                $http({
                    method: "POST",
                    url: "/admin/fee/add",
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
