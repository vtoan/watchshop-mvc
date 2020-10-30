angular.module("app").controller("orderDetailCtrl", [
    "$scope",
    "$routeParams",
    "$cookies",
    "$filter",
    "orderService",
    "productService",
    "helper",
    function ($scope, $routeParams, $cookies, $filter, orderService, productService, helper) {
        let urlResources = "asset/products/";
        let itemId = $routeParams.id;
        let dropDown = new UIDropDown();

        // ============== property  ===============
        //state
        $scope.ex = new Exception();
        $scope.isLoading = true;
        $scope.statusOrder = ["Huỷ", "Chưa xác nhận", "Đã xác nhận", "Đã giao", "Hoàn thành"];
        $scope.statusColor = ["red", "", "blue", "orange", "green"];
        //
        $scope.srcImages = [];
        $scope.order = {};
        $scope.orderDetails = [];
        $scope.productOrders = [];
        $scope.totalItem = 0;
        $scope.totalAmount = 0;
        $scope.fees = [];
        // ============== event ===============
        $scope.onChangeStatus = function (status) {
            UIPopup(`Thay đổi ' ${$scope.statusOrder[status]} ' đơn hàng này ?`, "Đồng ý", "Không", () =>
                updateStatus(itemId, status)
            );
        };

        $scope.findProduct = function (id) {
            let idx = $scope.productOrders.findIndex((p) => p.id == id);
            if (idx == -1) {
                // $scope.ex.setError("Không tìm thấy sản phẩm phù hợp với giỏ hàng");
                return;
            }
            return $scope.productOrders[idx];
        };

        $scope.getResources = function () {
            return urlResources;
        };

        $scope.getPriceCurr = function (obj) {
            let priceCurr = calPriceDiscount(obj.price, obj.discount);
            calAmountPay(priceCurr, obj.quantity);
            return priceCurr;
        };

        // Parse cost to display
        $scope.parseToCost = function (val) {
            if (!val) return 0;
            return !parseInt(val) ? val * 100 + " %" : $filter("currency")(val, "", 0) + " đ";
        };

        $scope.getAmountPay = function () {
            let pay = calPriceDiscount($scope.totalAmount, $scope.order.promotion);
            //add fee/
            $scope.fees.forEach((item) => {
                let cost = item.cost;
                pay += parseInt(cost) == cost ? cost : pay * cost;
            });
            return pay;
        };
        //================= private ===================
        function initData() {
            let obj = getDataStored("order_", itemId);
            if (!obj) {
                $scope.isLoading = false;
                return;
            }
            //
            $scope.order = obj;
            $scope.fees = JSON.parse(obj.fees);
            //
            orderService
                .reqDetail(itemId)
                .then((response) => {
                    $scope.orderDetails = response;
                    return response.map((od) => od.productId);
                })
                .then((ids) => getProductInCart(ids))
                .catch((ex) => $scope.ex.setError(ex))
                .finally(() => {
                    $scope.isLoading = false;
                });
        }

        function getProductInCart(stringIDs) {
            productService
                .reqItems(stringIDs)
                .then((response) => {
                    $scope.productOrders = response;
                    console.log(response);
                })
                .catch((ex) => $scope.ex.setError(ex));
        }

        function calAmountPay(amount, quantity) {
            $scope.totalItem += quantity;
            $scope.totalAmount += amount * quantity;
        }

        function calPriceDiscount(price, discount) {
            if (!discount) return price;
            return parseInt(discount) == discount ? price - discount : price - Math.round(price * discount);
        }

        // get data stored in cookie form prev page
        function getDataStored(stringKey, id) {
            if (!id) {
                $scope.ex.setError("Truy cập trang không đúng cách.");
                return;
            }
            let key = stringKey + id;
            let data = $cookies.getObject(key);
            if (data != null) {
                $cookies.remove(key);
            } else $scope.ex.setError("Truy cập trang không đúng cách.");
            return data;
        }

        function updateStatus(id, status) {
            let toaster = helper.notifyStatus("Cập nhật trạng thái đơn hàng", id);
            orderService
                .reqUpdateStatus(id, status)
                .then(() => {
                    $scope.order.status = status;
                    toaster.show();
                })
                .catch((errorCode) => toaster.show(false, errorCode));
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
