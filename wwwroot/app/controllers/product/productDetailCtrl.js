angular.module("app").controller("productDetailCtrl", [
    "$scope",
    "$routeParams",
    "$cookies",
    "$q",
    "$location",
    "productService",
    "cateService",
    "wireService",
    "bandService",
    "helper",
    function (
        $scope,
        $routeParams,
        $cookies,
        $q,
        $location,
        productService,
        cateService,
        wireService,
        bandService,
        helper
    ) {
        let dataHolder;
        let backLink = "/product";
        let itemId = $routeParams.id;
        let dropDown = new UIDropDown();
        let isCreate = itemId == 0;
        //toaset
        let toaster = {};
        let validNotify = helper.notifyStatus("Chưa nhập các thông tin sản phẩm bắt buộc");
        // ============== property  ===============
        //state
        $scope.ex = new Exception();
        $scope.isLoading = true;
        //
        $scope.srcImages = [];
        $scope.prod = {};
        $scope.prodDetail = {};
        $scope.categorys = [];
        $scope.wires = [];
        $scope.bands = [];
        // ============== event ===============
        $scope.onSave = function () {
            if (
                !$scope.prod.name ||
                !$scope.prod.price ||
                !$scope.prod.categoryId ||
                !$scope.prod.typeWireId ||
                !$scope.prod.bandId
            )
                validNotify.show(false);
            else
                UIPopup("Lưu thay đổi sản phẩm", "Đồng ý", "Không", function () {
                    if (isCreate)
                        callService(
                            productService.reqAdd($scope.prod, $scope.srcImages).then(() => {
                                toaster.show();
                                $location.url(backLink);
                            }),
                            "Thêm sản phẩm",
                            itemId
                        );
                    else
                        callService(
                            productService.reqUpdate(dataHolder, $scope.prod, $scope.srcImages).then(() => {
                                toaster.show();
                                $location.url(backLink);
                            }),
                            "Lưu thay đổi",
                            itemId
                        );
                });
        };

        $scope.onCannel = function () {
            let msg = isCreate ? "Huỷ thêm sản phẩm" : "Xoá sản phẩm";
            UIPopup(msg, "Đồng ý", "Không", function () {
                if (isCreate) {
                    $location.url(backLink);
                    $scope.$apply();
                } else {
                    callService(
                        productService.reqDelete(itemId).then(() => {
                            toaster.show();
                            $location.url(backLink);
                        }),
                        "Xoá sản phẩm",
                        itemId
                    );
                }
            });
        };

        // image
        $scope.onChangeImage = function (img) {
            $scope.prod.image = img;
        };

        $scope.onAddImage = function () {
            helper.uploadFile(null, (obj) => {
                $scope.srcImages.push(obj);
                $scope.$apply();
            });
        };
        $scope.onRemoveImage = function (e, imgName) {
            UIPopup("Xoá hình ảnh này", "Đồng ý", "Không", function () {
                $(e.target).parent().parent().remove();
                let imgObj = $scope.srcImages.find((item) => item.name == imgName);
                if (imgObj) imgObj.status = -1;
            });
        };

        //================= private ===================
        function initData() {
            let obj = {};
            // create new product
            if (isCreate) $scope.prod = obj;
            // get product exsist;
            else {
                obj = getDataStored("product_", itemId);
                if (!obj) {
                    $scope.isLoading = false;
                    return;
                }
                // GET DETAIL
                productService
                    .reqDetail(itemId)
                    .then(function (response) {
                        dataHolder = Object.assign(obj, response);
                        $scope.prod = Object.assign({}, dataHolder);
                        $scope.srcImages = helper.imageToObject(response.images);
                    })
                    .catch((ex) => $scope.ex.setError(ex))
                    .finally(() => ($scope.isLoading = false));
            }
            // get sub info
            $q.all([
                cateService.reqLists().then((response) => ($scope.categorys = response)),
                wireService.reqLists().then((response) => ($scope.wires = response)),
                bandService.reqLists().then((response) => ($scope.bands = response)),
            ])
                .catch((ex) => $scope.ex.setError(ex))
                .finally(() => ($scope.isLoading = false));
        }

        // get product stored in cookie form list produc page
        function getDataStored(stringKey, id) {
            if (!id) {
                $scope.ex.setError(
                    "Lỗi, trang truy cập không đúng cách, chọn sản phẩm từ trang danh sách sản phẩm để xem chi tiết."
                );
                return;
            }
            let key = stringKey + id;
            let data = $cookies.getObject(key);
            if (data != null) {
                $cookies.remove(key);
            } else
                $scope.ex.setError(
                    "Lỗi, trang truy cập không đúng cách, chọn sản phẩm từ trang danh sách sản phẩm để xem chi tiết."
                );
            return data;
        }

        function callService(obj, msg, itemId) {
            toaster = helper.notifyStatus(msg, itemId);
            $scope.isLoading = true;
            $scope.$apply();
            obj.catch((erCode) => toaster.show(false, erCode)).finally(() => ($scope.isLoading = false));
        }
        //================= exec ===================
        initData();
        dropDown.attach();
        //================= desctroy ctrl ===================
        $scope.$on("$destroy", function () {
            dropDown.detach();
        });
    },
]);
