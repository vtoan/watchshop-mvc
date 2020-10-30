angular.module("app").factory("productService", [
    "$q",
    "$http",
    function ($q, $http) {
        return {
            reqLists: function () {
                console.log("Product: get data");
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/product/listdata",
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqDetail: function (id) {
                console.log("Product: get detail " + id);
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/product/detail",
                    params: { id: id },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqUpdateStatus: function (id, status) {
                console.log(`Product: update status ${id} ${status}`);
                let defered = $q.defer();
                $http({
                    method: "PUT",
                    url: "/admin/product/updatestatus",
                    params: { id: id, stt: status },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqDelete: function (id) {
                console.log(`Product: delete ${id}`);
                //
                let defered = $q.defer();
                $http({
                    method: "PUT",
                    url: "/admin/product/delete",
                    params: { id: id },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.data)
                );
                return defered.promise;
            },
            reqUpdate: function (root, modified, images) {
                console.log(`Product: update`);
                console.log("Root");
                console.log(root);
                console.log("Modified");
                console.log(modified);
                console.log("Image");
                console.log(images);
                console.log("=================");
                //
                let defered = $q.defer();
                if (!modified.id) defered.reject("Id is null");
                $http({
                    method: "PUT",
                    url: "/admin/product/update",
                    params: { id: modified.id, item: JSON.stringify(modified) },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqAdd: function (item, images) {
                console.log(`Product: add`);
                console.log("Item");
                console.log(item);
                console.log("Image");
                console.log(images);
                console.log("=================");
                //
                let defered = $q.defer();
                $http({
                    method: "POST",
                    url: "admin/product/add",
                    params: { item: JSON.stringify(item) },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqItems: function (stringIds) {
                console.log(`Product: get items ${stringIds}`);
                //
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/product/getproductsbyids",
                    params: { idString: stringIds },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            //////////////////////////
            reqExportData: function () {
                console.log(`Product: export`);
                let defered = $q.defer();
                setTimeout(() => defered.reject("chức năng chưa sẵn sàng"), 1000);
                return defered.promise;
            },
            reqImportData: function () {
                console.log(`Product: import`);
                let defered = $q.defer();
                setTimeout(() => defered.reject("chức năng chưa sẵn sàng"), 1000);
                return defered.promise;
            },
        };
    },
]);
