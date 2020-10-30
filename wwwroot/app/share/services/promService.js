angular.module("app").factory("promService", [
    "$q",
    "$http",
    function ($q, $http) {
        return {
            // ================ Promotion ================
            reqLists: function () {
                console.log("Promotion: get list");
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/promotion/listdata",
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqItem: function (id) {
                console.log("Promotion: get " + id);
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/promotion/data",
                    params: { id: id },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqUpdateStatus: function (id, status) {
                console.log(`Promotion: update status ${id} ${status}`);
                let defered = $q.defer();
                $http({
                    method: "PUT",
                    url: "/admin/promotion/updatestatus",
                    params: { id: id, stt: status },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqDelete: function (id) {
                console.log(`Promotion: delete ${id}`);
                let defered = $q.defer();
                $http({
                    method: "PUT",
                    url: "/admin/promotion/remove",
                    params: { id: id },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            // ================ Prom Product ================
            reqPromProduct: function () {
                console.log("Promotion: get prom product data");
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/promotion/product",
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqAddPromProduct: function (item) {
                console.log(`Promotion: product add `);
                console.log(item);
                console.log("===========");
                let defered = $q.defer();
                $http({
                    method: "POST",
                    url: "/admin/promotion/addforproduct",
                    params: { item: JSON.stringify(item) },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqUpdatePromProduct: function (root, modified) {
                console.log(`Promotion: product update `);
                console.log(modified[0]);
                console.log("===========");
                let defered = $q.defer();
                if (!modified.id) defered.reject("id is null");
                modified.discount = Number(modified.discount);
                $http({
                    method: "PUT",
                    url: "/admin/promotion/updateforproduct",
                    params: { id: modified.id, item: JSON.stringify(modified) },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqChangeProductProm: function (promIdOld, promIDNew, productId) {
                console.log(`Promotion: change - product ${productId} prom ${promIdOld} => prom ${promIDNew}`);
                let defered = $q.defer();
                if (promIDNew === promIdOld) defered.reject("SP đã thuộc khuyễn mãi này");
                setTimeout(() => defered.resolve(), 1000);
                return defered.promise;
            },

            reqRemoveProductProm: function (promId, productId) {
                console.log(`Promotion: change - product ${productId} prom ${promId} => prom 0`);
                let defered = $q.defer();
                if (promId == 0) defered.reject("Không có khuyễn mãi để xoá");
                setTimeout(() => defered.resolve(), 1000);
                return defered.promise;
            },
            // ================ Prom Bil ================

            reqAddPromBill: function (item) {
                console.log(`Promotion: bill add `);
                console.log(item);
                console.log("===========");
                let defered = $q.defer();
                $http({
                    method: "POST",
                    url: "/admin/promotion/addforbill",
                    params: { item: JSON.stringify(item) },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqUpdatePromBill: function (root, modified) {
                console.log(`Promotion: bill update `);
                console.log(modified[0]);
                console.log("===========");
                let defered = $q.defer();
                $http({
                    method: "PUT",
                    url: "/admin/promotion/updateforbill",
                    params: { id: modified.id, item: JSON.stringify(modified) },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
        };
    },
]);
