angular.module("app").controller("tabPolicyCtrl", [
    "$scope",
    "policyService",
    "feeService",
    "helper",
    function ($scope, policyService, feeService, helper) {
        let listServiceName = ["chính sách", "phí"];
        let listService = [policyService, feeService];
        //
        let objectService = {};
        let objectName = "";
        //
        let dataHolder = [];
        //
        let tabCurr = -1;
        let toaster = {};
        let validNotify = helper.notifyStatus("Chưa nhập các thông tin sản phẩm bắt buộc");
        // ================= property ===================
        //state
        $scope.ex = new Exception();
        $scope.isLoading = true;
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
                modified.icon = helper.imageToObject(modified.icon)[0];
            }
            $scope.item = modified;
            dataHolder = Object.assign({}, modified);
        };

        function validite() {
            if (tabCurr == 0) {
                return $scope.item.policyContent;
            }
            if (tabCurr == 1) {
                if ($scope.item.name && $scope.item.cost) return true;
                return false;
            }
        }

        $scope.onSave = function () {
            if (!validite()) validNotify.show(false);
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
            UIPopup(`Thay đổi ' ${objectName} ' đơn hàng này ?`, "Đồng ý", "Không", function () {
                callService(
                    objectService.reqDelete(id).then(() => {
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
                $scope.item.icon = obj;
                $scope.$apply();
            });
        };

        $scope.onRemoveImage = function () {
            UIPopup("Xoá hình ảnh này", "Đồng ý", "Không", function () {
                $scope.item.icon.status = -1;
                $scope.$apply();
            });
        };

        // ================= private ==================
        function refresh() {
            $scope.onChangeTab(tabCurr);
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
            $scope.isLoading = true;
            $scope.$apply();
            toaster = helper.notifyStatus(msg, itemId);
            obj.catch((erCode) => toaster.show(false, erCode)).finally(() => ($scope.isLoading = false));
        }
        // ================= exce ==================
        $scope.onChangeTab(0);
        //================= desctroy ctrl ===================
    },
]);
