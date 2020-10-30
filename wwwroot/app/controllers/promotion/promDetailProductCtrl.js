angular.module("app").controller("promDetailProductCtrl", [
    "$scope",
    "$routeParams",
    "$q",
    "$location",
    "promService",
    "cateService",
    "bandService",
    "helper",
    function ($scope, $routeParams, $q, $location, promService, cateService, bandService, helper) {
        let dataHolder;
        let itemId = $routeParams.id;
        let dropDown = new UIDropDown();
        let urlBack = "/promotion";
        let validNotify = helper.notifyStatus("Chưa nhập các thông tin sản phẩm bắt buộc");

        // ============== property  ===============
        //state
        $scope.ex = new Exception();
        $scope.isLoading = true;
        $scope.isCreate = itemId == 0;
        //
        $scope.prom = {};
        $scope.categories = [];
        $scope.bands = [];
        // ============== event ===============
        $scope.onSave = function () {
            //validate;
            $scope.group == 0 ? ($scope.prom.bandId = 0) : ($scope.prom.categoryId = 0);
            console.log($scope.group);
            if (!$scope.prom.name || !$scope.prom.discount) validNotify.show(false);
            else
                UIPopup("Lưu thay đổi sản phẩm", "Đồng ý", "Không", function () {
                    parseDiscountVal();
                    if ($scope.isCreate)
                        callService(
                            promService.reqAddPromProduct($scope.prom).then((response) => {
                                toaster.show(response);
                                $location.url(urlBack);
                            }),
                            "Thêm khuyễn mãi",
                            itemId
                        );
                    else
                        callService(
                            promService.reqUpdatePromProduct(dataHolder, $scope.prom).then((response) => {
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
                        "Xoá khuyễn mãi",
                        itemId
                    );
            });
        };

        $scope.onRemoveSelect = function () {
            $scope.group = null;
            $scope.prom.bandId = 0;
            $scope.prom.categoryId = 0;
        };

        // //================= private ===================
        function initData() {
            if (!$scope.isCreate) {
                promService
                    .reqItem(itemId)
                    .then((response) => showData(response))
                    .catch((error) => $scope.ex.setError(error));
            }
            $q.all([
                cateService.reqLists().then((response) => ($scope.categories = response)),
                bandService.reqLists().then((response) => ($scope.bands = response)),
            ])
                .catch((error) => $scope.ex.setError(error))
                .finally(() => {
                    if ($scope.isCreate) getDetail();
                    $scope.isLoading = false;
                });
        }

        function showData(response) {
            console.log(response);
            response.fromDate = new Date(response.fromDate);
            response.toDate = new Date(response.toDate);
            $scope.prom = response;
            dataHolder = Object.assign({}, response);
            parseToCost();
        }

        function getDetail() {
            let cateId = $scope.prom.categoryId;
            let bandId = $scope.prom.bandId;
            if (cateId) {
                let cate = response.find((item) => item.id == cateId);
                if (cate) $scope.prom.categoryName = cate.name;
                else $scope.ex.setError("Không tìm thấy phân loại phù hợp");
            }
            if (bandId) {
                let band = response.find((item) => item.id == bandId);
                if (band) $scope.prom.bandName = band.name;
                else $scope.ex.setError("Không tìm thấy thương hiệu phù hợp");
            }
        }

        function parseDiscountVal() {
            let valDiscount = $scope.prom.discount;
            if (!valDiscount.endsWith("%")) return Number(valDiscount) ? true : false;
            //
            valDiscount = valDiscount.substring(0, valDiscount.length - 1);
            $scope.prom.discount = Number(valDiscount / 100);
            return true;
        }

        function parseToCost() {
            let val = $scope.prom.discount;
            if (!val) return 0;
            $scope.prom.discount = !parseInt(val) ? val * 100 + "%" : val;
        }

        function callService(obj, msg, itemId) {
            toaster = helper.notifyStatus(msg, itemId);
            $scope.isLoading = true;
            $scope.$apply();
            obj.catch((erCode) => toaster.show(false, erCode)).finally(() => ($scope.isLoading = false));
        }

        // //================= exec ===================
        initData();
        dropDown.attach();
        //================= desctroy ctrl ===================
        $scope.$on("$destroy", function () {
            dropDown.detach();
        });
    },
]);
