angular.module("app").factory("orderService", [
    "$q",
    "$http",
    function ($q, $http) {
        return {
            reqLists: function (start, end) {
                console.log(`Order: get list ${start} - ${end}`);
                start = moment(start, "DD/MM/YYYY").format("MM/DD/YYYY");
                end = moment(end, "DD/MM/YYYY").add(1, "days").format("MM/DD/YYYY");
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/order/listdata",
                    params: { start: start, end: end },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqDetail: function (id) {
                console.log(`Order: get detail ${id}`);
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/order/detail",
                    params: { id: id },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqUpdateStatus: function (id, status) {
                console.log(`Order: update status ${id} - ${status}`);
                let defered = $q.defer();
                $http({
                    method: "PUT",
                    url: "/admin/order/updatestatus",
                    params: { id: id, stt: status },
                }).then(
                    (resp) => defered.resolve(),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },

            reqFind: function (query) {
                console.log(`Order: find ${query}`);
                let defered = $q.defer();
                $http({
                    method: "GET",
                    url: "/admin/order/find",
                    params: { find: query },
                }).then(
                    (resp) => defered.resolve(resp.data),
                    (err) => defered.reject(err.statusText)
                );
                return defered.promise;
            },
            reqExportData: function () {
                console.log(`Order: export`);
                let defered = $q.defer();
                setTimeout(() => defered.reject("chức năng chưa sẵn sàng"), 1000);
                return defered.promise;
            },
            reqImportData: function () {
                console.log(`Order: import`);
                let defered = $q.defer();
                setTimeout(() => defered.reject("chức năng chưa sẵn sàng"), 1000);
                return defered.promise;
            },
        };
    },
]);
