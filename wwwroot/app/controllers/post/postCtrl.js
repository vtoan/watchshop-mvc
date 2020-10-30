angular.module("app").controller("postCtrl", [
	"$scope",
	"$location",
	"$routeParams",
	"toaster",
	"postService",
	function ($scope, $location, $routeParams, toaster, postService) {
		let itemId = $routeParams.id;
		// ============== property  ===============
		//state
		$scope.ex = new Exception();
		$scope.isLoading = true;
		//
		$scope.post;
		// ============== event ===============
		$scope.onSave = function () {
			$scope.isLoading = true;
			let toaster = new CtToaster("Lưu bài viết", itemId);
			postService
				.reqUpdate(itemId, content)
				.then(() => {
					toaster.show();
					$location.url("/");
				})
				.catch((erCode) => toaster.show(false, erCode))
				.finally(() => ($scope.isLoading = false));
		};

		$scope.onDelete = function () {
			$scope.isLoading = true;
			let toaster = new CtToaster("Xóa bài viết", itemId);
			postService
				.reqDelete(itemId)
				.then(() => {
					toaster.show();
					$location.url("/");
				})
				.catch((erCode) => toaster.show(false, erCode))
				.finally(() => ($scope.isLoading = false));
		};
		//================= private ===================
		function CtToaster(msg, id) {
			this.show = function (flag = true, code = "") {
				toaster.pop(
					flag ? "success" : "error",
					msg + " - #" + id,
					(flag ? "Thành công " : "Thất bại ") + code
				);
			};
		}

		function initData() {
			postService
				.reqItem(itemId)
				.then((response) => ($scope.post = response))
				.catch((ex) => $scope.ex.setError(ex))
				.finally(() => ($scope.isLoading = false));
		}

		// ================= exce ===================
		initData();
	},
]);
