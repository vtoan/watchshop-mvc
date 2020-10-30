angular.module("app").controller("tabCategoryCtrl", [
    "$scope",
    "cateService",
    "wireService",
    "bandService",
    "helper",
    function ($scope, cateService, wireService, bandService, helper) {
        let listServiceName = ["phân loại", "loại dây", "thương hiệu"];
        let listService = [cateService, wireService, bandService];
        //
        let objectService = {};
        let objectName = "";
        //
        let dataHolder;
        //
        let tabCurr = -1;
        let toaster = {};
        let validNotify = helper.notifyStatus("Chưa nhập các thông tin sản phẩm bắt buộc");
        // ================= property ===================
        //state
        $scope.ex = new Exception();
        $scope.isLoading = true;
        $scope.srcImage;
        //
        $scope.item = {};
        $scope.data = [];
        // ================= event ===================
        $scope.onChangeTab = function (tabId) {
            //reset
            $scope.item = {};
            $scope.data = [];
            dataHolder = [];
            //get data
            $scope.tabId = tabId;
            tabCurr = tabId;
            objectName = listServiceName[tabId];
            objectService = listService[tabId];
            initData();
        };

        $scope.onShowDetail = function (item) {
            let modified = Object.assign({}, item);
            if (tabCurr == 0) {
                modified.seoImage = helper.imageToObject(modified.seoImage)[0];
            }
            $scope.item = modified;
            dataHolder = Object.assign({}, modified);
        };

        $scope.onSave = function () {
            if (!$scope.item.name) validNotify.show(false);
            else
                UIPopup(`Lưu ${objectName} này ?`, "Đồng ý", "Không", function () {
                    let item = $scope.item;
                    if (!item.id) {
                        callService(
                            objectService.reqAdd(item).then(() => {
                                toaster.show();
                                refresh();
                            }),
                            "Thêm " + objectName
                        );
                    } else {
                        callService(
                            objectService.reqUpdate(dataHolder, item).then(() => {
                                toaster.show();
                                refresh();
                            }),
                            "Lưu " + objectName
                        );
                    }
                });
        };

        $scope.onRemove = function (id) {
            UIPopup(`Các SP thuộc '${objectName}' sẽ bị xoá ?`, "Đồng ý", "Không", function () {
                callService(
                    objectService.reqDelete(id).then(function () {
                        toaster.show();
                        refresh();
                    }),
                    "Xoá " + objectName
                );
            });
        };

        $scope.onCancel = function () {
            UIPopup(`Huỷ thay đổi ${objectName} này ?`, "Đồng ý", "Không", function () {
                $scope.item = {};
                $scope.$apply();
            });
        };

        //category
        $scope.onUploadImage = function () {
            helper.uploadFile(null, (obj) => {
                $scope.item.seoImage = obj;
                $scope.$apply();
            });
        };

        $scope.onRemoveImage = function () {
            UIPopup("Xoá hình ảnh này", "Đồng ý", "Không", function () {
                $scope.item.seoImage.status = -1;
                $scope.$apply();
            });
        };

        // ================= private ==================
        function refresh() {
            $scope.onChangeTab(tabCurr);
            // $scope.$apply();
        }

        function initData() {
            objectService
                .reqLists()
                .then((response) => {
                    $scope.data = response;
                })
                .catch((ex) => $scope.ex.setError(ex))
                .finally(() => ($scope.isLoading = false));
        }

        function callService(obj, msg, itemId) {
            toaster = helper.notifyStatus(msg, itemId);
            $scope.isLoading = true;
            $scope.$apply();
            obj.catch((erCode) => toaster.show(false, erCode)).finally(() => ($scope.isLoading = false));
        }
        // ================= exce ==================
        $scope.onChangeTab(0);
        //================= desctroy ctrl ===================
    },
]);
