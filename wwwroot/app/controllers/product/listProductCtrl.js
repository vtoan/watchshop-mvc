angular.module("app").controller("listProductCtrl", [
    "$scope",
    "$cookies",
    "$location",
    "$q",
    "productService",
    "promService",
    "helper",
    function ($scope, $cookies, $location, $q, productService, promService, helper) {
        let dataHolder = [];
        let dropDown = new UIDropDown();
        // ================= property ===================
        //state
        $scope.ex = new Exception();
        $scope.isLoading = true;
        //pagination
        $scope.beginPage = 0;
        $scope.numberPage = 0;
        $scope.itemLimit = 15;
        //display
        $scope.onShelf = 0;
        $scope.warehouse = 0;
        $scope.products = [];
        $scope.promotions = [];
        // ================= event ===================
        $scope.onSearch = function (query) {
            $scope.products = dataHolder.filter((p) =>
                Object.values(p).some((item) => item != null && item.toString().includes(query))
            );
            calPage();
        };

        $scope.onChangeStatus = function (id) {
            let toaster = helper.notifyStatus("Cập nhật trạng thái sản phẩm", id);
            let idx = $scope.products.findIndex((item) => item.id == id);
            let status = $scope.products[idx].isShow;
            productService
                .reqUpdateStatus(id, status)
                .then(() => {
                    toaster.show();
                    dataHolder[idx].isShow = status;
                    $scope.onShelf = dataHolder.filter((p) => p.isShow == true).length;
                })
                .catch((errorCode) => {
                    $scope.products[idx].isShow = !status;
                    toaster.show(false, errorCode);
                });
        };

        $scope.onChangeProm = function (product, promotion) {
            // UIPopup(`Thay đổi khuyến mãi SP này ?`, "Đồng ý", "Không", function () {
            //     changePromotion(product, promotion);
            // });
            let toaster = helper.notifyStatus("Cập nhật khuyễn mãi SP");
            toaster.show(false, "Đang phát triển");
        };

        $scope.onRemoveProm = function (product) {
            let toaster = helper.notifyStatus("Cập nhật khuyễn mãi SP");
            toaster.show(false, "Đang phát triển");
            // UIPopup(`Xoá khuyến mãi SP này ?`, "Đồng ý", "Không", () => removeProm(product));
        };

        $scope.onShowDetail = function (id) {
            $cookies.put("product_" + id, JSON.stringify(dataHolder.find((p) => p.id == id)));
            $location.url("/product-detail/" + id);
        };

        $scope.onExport = function () {
            // productService.reqItems("8,7,6");
            // let toaster = helper.notifyStatus("Export sản phẩm");
            // $scope.isLoading = true;
            // productService
            //     .reqExportData()
            //     .then(() => toaster.show())
            //     .catch((ex) => toaster.show(false, ex))
            //     .finally(() => ($scope.isLoading = false));
        };

        $scope.onImport = function () {
            // let toaster = helper.notifyStatus("Import sản phẩm");
            // helper.uploadFile(null, (obj) => {
            //     $scope.isLoading = true;
            //     productService
            //         .reqImportData(obj)
            //         .then(() => toaster.show())
            //         .catch((ex) => toaster.show(false, ex))
            //         .finally(() => ($scope.isLoading = false));
            // });
        };

        $scope.$watch("beginPage", function (oldVal, newVal, scope) {
            window.scrollTo({
                top: 0,
            });
        });

        // ================= private ===================
        function initData() {
            $q.all([
                productService.reqLists().then((response) => showData(response)),
                promService.reqPromProduct().then((response) => addPromotion(response)),
            ])
                .catch((ex) => $scope.ex.setError(ex))
                .finally(() => ($scope.isLoading = false));
        }

        function showData(asset) {
            dataHolder = Array.from(asset);
            $scope.products = asset;
            $scope.warehouse = asset.length;
            $scope.onShelf = asset.filter((p) => p.isShow == true).length;
            calPage();
        }

        function calPage() {
            let len = $scope.products.length;
            $scope.numberPage = Math.ceil(len / $scope.itemLimit);
            $scope.beginPage = 0;
            if (len == 0) $scope.ex.setError("Danh sách trống");
            else $scope.ex.default();
        }

        // promotion
        function addPromotion(response) {
            $scope.promotions = response;
            $scope.products.forEach((item) => (item.discountId = 0));
            //
            console.log(response);
            response.forEach((prom) => {
                let promName = prom.name;
                let promId = prom.id;
                let cateId = prom.categoryId;
                let bandId = prom.bandId;
                let prodIDs = prom.productIds;
                //
                if (cateId) {
                    $scope.products.forEach((item) => {
                        if (item.categoryId == cateId) {
                            item.discountId = promId;
                            item.discount = promName;
                        }
                    });
                }
                if (bandId) {
                    $scope.products.forEach((item) => {
                        if (item.bandId == bandId) {
                            item.discountId = promId;
                            item.discount = promName;
                        }
                    });
                }
                if (prodIDs && prodIDs.length > 0)
                    $scope.products.forEach((item) => {
                        prodIDs.forEach((id) => {
                            if (item.id == +id && item.categoryId != cateId && item.bandId != bandId) {
                                item.discountId = promId;
                                item.discount = promName;
                            }
                        });
                    });
            });
        }

        function removeProm(product) {
            let toaster = helper.notifyStatus("Xoá khuyễn mãi SP", product.id);
            let productId = product.id;
            let promId = product.discountId;
            //
            promService
                .reqRemoveProductProm(promId, productId)
                .then(() => {
                    toaster.show();
                    product.discount = "Không";
                    product.discountId = 0;
                })
                .catch((errorCode) => {
                    toaster.show(false, errorCode);
                });
        }

        function changePromotion(product, promotion) {
            let toaster = helper.notifyStatus("Cập nhật khuyễn mãi SP", product.id);
            let promIdOld = product.discountId;
            let promIDNew = promotion.id;
            let productId = product.id;
            //
            promService
                .reqChangeProductProm(promIdOld, promIDNew, productId)
                .then(() => {
                    toaster.show();
                    product.discount = promotion.name;
                    product.discountId = promIDNew;
                })
                .catch((errorCode) => {
                    toaster.show(false, errorCode);
                });
        }
        // ================= exce ===================
        initData();
        dropDown.attach();
        //================= desctroy ctrl ===================
        $scope.$on("$destroy", function () {
            dropDown.detach();
        });
    },
]);
