angular.module("app").controller("infoCtrl", [
    "$scope",
    "$location",
    "infoService",
    "helper",
    function ($scope, $location, infoService, helper) {
        //
        let objectService = infoService;
        let objectName = "info";
        //
        let dataHolder;
        //
        let toaster = {};
        // ================= property ===================
        //state
        $scope.ex = new Exception();
        $scope.isLoading = true;
        //
        $scope.item = {};

        $scope.onSave = function () {
            UIPopup(`Lưu ${objectName} này ?`, "Đồng ý", "Không", function () {
                callService(
                    objectService.reqUpdate(dataHolder, $scope.item).then(() => {
                        toaster.show();
                    }),
                    "Lưu " + objectName
                );
            });
        };

        $scope.onCancel = function () {
            console.log("?");
            UIPopup(`Huỷ thay đổi ${objectName} này ?`, "Đồng ý", "Không", function () {
                $location.url("/");
                $scope.item = {};
                $scope.$apply();
            });
        };
        //Icon
        $scope.onUploadIcon = function () {
            helper.uploadFile(null, (obj) => {
                $scope.item.logo = obj;
                $scope.$apply();
            });
        };

        $scope.onRemoveIcon = function () {
            UIPopup("Xoá hình ảnh này", "Đồng ý", "Không", function () {
                $scope.item.logo.status = -1;
                $scope.$apply();
            });
        };

        //
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
        function initData() {
            objectService
                .reqData()
                .then((response) => {
                    response.logo = helper.imageToObject(response.logo)[0];
                    response.seoImage = helper.imageToObject(response.seoImage)[0];
                    $scope.item = response;
                    dataHolder = Object.assign({}, response);
                })
                .catch((ex) => $scope.ex.setError(ex))
                .finally(() => ($scope.isLoading = false));
        }

        function callService(obj, msg, itemId) {
            $scope.isLoading = true;
            $scope.$apply();
            toaster = helper.notifyStatus(msg, itemId);
            obj.catch((erCode) => toaster.show(false, erCode)).finally(() => ($scope.isLoading = false));
        }
        // ================= exce ==================
        initData();
        //================= desctroy ctrl ===================
    },
]);
