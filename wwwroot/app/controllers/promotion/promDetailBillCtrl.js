angular.module("app").controller("promDetailBillCtrl", [
    "$scope",
    "$routeParams",
    "$location",
    "promService",
    "helper",
    function ($scope, $routeParams, $location, promService, helper) {
        let dataHolder;
        let itemId = $routeParams.id;
        let urlBack = "/list-promotion";
        let validNotify = helper.notifyStatus("Chưa nhập các thông tin sản phẩm bắt buộc");
        // ============== property  ===============
        //state
        $scope.ex = new Exception();
        $scope.isLoading = true;
        $scope.isCreate = itemId == 0;
        //
        $scope.prom = {};
        // ============== event ===============
        $scope.onSave = function () {
            //validate;
            if (!$scope.prom.name || !$scope.prom.discount || !$scope.group) {
                validNotify.show(false);
                return;
            }
            if (
                ($scope.group == 0 && !$scope.prom.conditionItem) ||
                ($scope.group == 1 && !$scope.prom.conditionAmount)
            ) {
                validNotify.show(false);
            } else
                UIPopup("Lưu thay đổi sản phẩm", "Đồng ý", "Không", function () {
                    parseDiscountVal();
                    if ($scope.isCreate)
                        callService(
                            promService.reqAddPromBill($scope.prom).then((response) => {
                                toaster.show(response);
                                $location.url(urlBack);
                            }),
                            "Thêm sản phẩm",
                            itemId
                        );
                    else
                        callService(
                            promService.reqUpdatePromBill(dataHolder, $scope.prom).then((response) => {
                                toaster.show(response);
                                $location.url(urlBack);
                            }),
                            "Lưu thay đổi",
                            itemId
                        );
                });
        };

        $scope.onCannel = function () {
            let msg = $scope.isCreate ? "Huỷ thêm sản phẩm" : "Xoá sản phẩm";
            UIPopup(msg, "Đồng ý", "Không", function () {
                if ($scope.isCreate) {
                    $location.url(urlBack);
                    $scope.$apply();
                } else
                    callService(
                        promService.reqDelete(itemId).then(() => {
                            toaster.show();
                            $location.url(urlBack);
                        }),
                        "Xoá sản phẩm",
                        itemId
                    );
            });
        };

        //================= private ===================
        function initData() {
            if (!$scope.isCreate) {
                promService
                    .reqItem(itemId)
                    .then((response) => showData(response))
                    .catch((error) => $scope.ex.setError(error))
                    .finally(() => ($scope.isLoading = false));
            } else $scope.isLoading = false;
        }

        function showData(response) {
            $scope.prom = response;
            dataHolder = Object.assign({}, response);
            parseToCost();
        }

        function callService(obj, msg, itemId) {
            toaster = helper.notifyStatus(msg, itemId);
            $scope.isLoading = true;
            $scope.$apply();
            obj.catch((erCode) => toaster.show(false, erCode)).finally(() => ($scope.isLoading = false));
        }

        function parseDiscountVal() {
            let valDiscount = $scope.prom.discount;
            if (!valDiscount.endsWith("%")) return Number(valDiscount) ? true : false;
            //
            valDiscount = valDiscount.substring(0, valDiscount.length - 1);
            $scope.prom.discount = +valDiscount / 100;
            return true;
        }

        function parseToCost() {
            let val = $scope.prom.discount;
            if (!val) return 0;
            $scope.prom.discount = !parseInt(val) ? val * 100 + "%" : val;
        }
        //================= exec ===================
        initData();
        //================= desctroy ctrl ===================
        $scope.$on("$destroy", function () {});
    },
]);
