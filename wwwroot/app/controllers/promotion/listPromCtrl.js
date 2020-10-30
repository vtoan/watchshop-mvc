angular.module("app").controller("listPromCtrl", [
	"$scope",
	"$location",
	"promService",
	"helper",
	function ($scope, $location, promService, helper) {
		let dataHolder = [];
		// ================= property ===================
		//state
		$scope.ex = new Exception();
		$scope.isLoading = true;
		$scope.typeProm = ["KM Sản phẩm", "KM Hoá đơn"];
		//
		$scope.beginPage = 0;
		$scope.numberPage = 0;
		$scope.itemLimit = 5;
		//
		$scope.onApply = 0;
		$scope.promotions = [];
		// ================= event ===================
		$scope.onSearch = function (query) {
			$scope.promotions = dataHolder.filter(
				(p) =>
					Object.values(p).some((item) =>
						item.toString().includes(query)
					) == true
			);
			calPage();
		};

		$scope.onChangeStatus = function (id) {
			let toaster = helper.notifyStatus(
				"Cập nhật trạng thái khuyễn mãi",
				id
			);
			let idx = $scope.promotions.findIndex((item) => item.id == id);
			let status = $scope.promotions[idx].status;
			promService
				.reqUpdateStatus(id, status)
				.then(() => {
					toaster.show();
					dataHolder[idx].status = status;
					updateItemApply();
					console.log($scope.onApply);
				})
				.catch((errorCode) => {
					$scope.promotions[idx].status = !status;
					toaster.show(false, errorCode);
				});
		};

		$scope.onShowDetail = function (id, type) {
			type
				? $location.url("/promotion-detail-bill/" + id)
				: $location.url("/promotion-detail-product/" + id);
		};

		// ================= private ===================
		function calPage() {
			let len = $scope.promotions.length;
			$scope.numberPage = Math.ceil(len / $scope.itemLimit);
			$scope.beginPage = 0;
			if (len == 0) $scope.ex.setError("Danh sách trống");
			else $scope.ex.default();
		}

		function showData(asset) {
			dataHolder = asset;
			$scope.promotions = asset;
			updateItemApply();
			calPage();
		}

		function updateItemApply() {
			$scope.onApply = $scope.promotions.filter(
				(p) => p.status == true
			).length;
			// $scope.$apply();
		}

		function initData() {
			promService
				.reqLists()
				.then((response) => showData(response))
				.catch((ex) => $scope.ex.setError(ex))
				.finally(() => ($scope.isLoading = false));
		}
		// ================= exce ===================
		initData();
	},
]);
