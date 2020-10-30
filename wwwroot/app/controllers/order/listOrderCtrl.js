angular.module("app").controller("listOrderCtrl", [
    "$scope",
    "$cookies",
    "$location",
    "orderService",
    "helper",
    function ($scope, $cookies, $location, orderService, helper) {
        let dataHolder = [];
        let dateFormat = "DD/MM/YYYY";
        let startDate = moment().subtract(30, "days").format(dateFormat);
        let endDate = moment().format(dateFormat);
        let dropDown = new UIDropDown();
        // ================= property ===================
        //state
        $scope.ex = new Exception();
        $scope.isLoading = true;
        $scope.statusOrder = ["Huỷ", "Chưa xác nhận", "Đã xác nhận", "Đã giao", "Hoàn thành"];
        $scope.statusColor = ["red", "", "blue", "orange", "green"];
        //
        $scope.beginPage = 0;
        $scope.numberPage = 0;
        $scope.itemLimit = 5;
        //
        $scope.orders = [];
        // ================= event ===================
        $scope.onSearch = function (query) {
            if (query == "") showData(dataHolder);
        };

        $scope.onFind = function (e, query) {
            if (e.keyCode == 13) {
                $scope.isLoading = true;
                orderService
                    .reqFind(query)
                    .then((response) => showData(response))
                    .catch((ex) => $scope.ex.setError(ex))
                    .finally(() => ($scope.isLoading = false));
            }
        };

        $scope.onChangeStatus = function (id, status) {
            UIPopup(`Thay đổi ' ${$scope.statusOrder[status]} ' đơn hàng này ?`, "Đồng ý", "Không", () =>
                updateStatus(id, status)
            );
        };

        $scope.onShowDetail = function (id) {
            $cookies.put("order_" + id, JSON.stringify(dataHolder.find((p) => p.id == id)));
            $location.url("/order-detail/" + id);
        };

        $scope.onExport = function () {
            $scope.isLoading = true;
            let toaster = helper.notifyStatus("Export Đơn hàng");
            orderService
                .reqExportData()
                .then(() => toaster.show())
                .catch((ex) => toaster.show(false, ex))
                .finally(() => ($scope.isLoading = false));
        };

        $scope.onImport = function () {
            let toaster = helper.notifyStatus("Import đơn hàng");
            helper.uploadFile(null, (obj) => {
                $scope.isLoading = true;
                orderService
                    .reqImportData(obj)
                    .then(() => toaster.show())
                    .catch((ex) => toaster.show(false, ex))
                    .finally(() => ($scope.isLoading = false));
            });
        };

        $scope.$watch("beginPage", function (oldVal, newVal, scope) {
            window.scrollTo({
                top: 0,
            });
        });

        // ================= private ==================
        function initData(start, end) {
            $("#date-range .startDate").text(start);
            $("#date-range .endDate").text(end);
            orderService
                .reqLists(start, end)
                .then((response) => showData(response))
                .catch((ex) => $scope.ex.setError(ex))
                .finally(() => ($scope.isLoading = false));
        }

        function updateStatus(id, status) {
            let toaster = helper.notifyStatus("Cập nhật trạng thái đơn hàng", id);
            orderService
                .reqUpdateStatus(id, status)
                .then(() => {
                    toaster.show();
                    let idx = $scope.orders.findIndex((item) => item.id == id);
                    $scope.orders[idx].status = status;
                    dataHolder[idx].status = status;
                })
                .catch((errorCode) => toaster.show(false, errorCode));
        }

        function calPage() {
            let len = $scope.orders.length;
            $scope.numberPage = Math.ceil(len / $scope.itemLimit);
            $scope.beginPage = 0;
            if (len == 0) $scope.ex.setError("Danh sách trống");
            else $scope.ex.default();
        }

        function showData(asset) {
            if (!asset) {
                $scope.ex.setError("Không có đơn hàng phù hợp");
                return;
            }
            dataHolder = Array.from(asset);
            console.log(dataHolder);
            $scope.orders = asset;
            calPage();
        }

        function UIDateRangePicker(startDate, endDate) {
            $("#date-range").daterangepicker(
                {
                    showDropdowns: true,
                    startDate: startDate,
                    endDate: endDate,
                    locale: {
                        format: "DD/MM/YYYY",
                    },
                    ranges: {
                        Today: [moment(), moment()],
                        Yesterday: [moment().subtract(1, "days"), moment().subtract(1, "days")],
                        "Last 7 Days": [moment().subtract(6, "days"), moment()],
                        "Last 30 Days": [moment().subtract(29, "days"), moment()],
                        "This Month": [moment().startOf("month"), moment().endOf("month")],
                        "Last Month": [
                            moment().subtract(1, "month").startOf("month"),
                            moment().subtract(1, "month").endOf("month"),
                        ],
                    },
                },
                function (start, end) {
                    $scope.isLoading = true;
                    $scope.query = "";
                    $scope.$apply();
                    initData(start.format(dateFormat), end.format(dateFormat));
                }
            );
        }
        // ================= exce ===================
        initData(startDate, endDate);
        UIDateRangePicker(startDate, endDate);
        dropDown.attach();
        //================= desctroy ctrl ===================
        $scope.$on("$destroy", function () {
            dropDown.detach();
        });
    },
]);
