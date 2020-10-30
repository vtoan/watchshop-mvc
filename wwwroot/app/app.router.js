angular.module("app").config([
	"$routeProvider",
	function ($routeProvider) {
		$routeProvider
			.when("/", {
				templateUrl: "app/templates/home/index.html",
			})
			.when("/product", {
				templateUrl: "app/templates/product/list-product.html",
				controller: "listProductCtrl",
			})
			.when("/product-detail/:id", {
				templateUrl: "app/templates/product/product-detail.html",
				controller: "productDetailCtrl",
			})
			.when("/post/:id", {
				templateUrl: "app/templates/post/post.html",
				controller: "postCtrl",
			})
			.when("/order", {
				templateUrl: "app/templates/order/list-order.html",
				controller: "listOrderCtrl",
			})
			.when("/order-detail/:id", {
				templateUrl: "app/templates/order/order-detail.html",
				controller: "orderDetailCtrl",
			})
			.when("/promotion", {
				templateUrl: "app/templates/promotion/list-promotion.html",
				controller: "listPromCtrl",
			})
			.when("/promotion-detail-product/:id", {
				templateUrl:
					"app/templates/promotion/promotion-detail-product.html",
				controller: "promDetailProductCtrl",
			})
			.when("/promotion-detail-bill/:id", {
				templateUrl:
					"app/templates/promotion/promotion-detail-bill.html",
				controller: "promDetailBillCtrl",
			})
			.when("/category", {
				templateUrl: "app/templates/category/category.html",
				controller: "tabCategoryCtrl",
			})
			.when("/policy", {
				templateUrl: "app/templates/policy/policy.html",
				controller: "tabPolicyCtrl",
			})
			.when("/info", {
				templateUrl: "app/templates/info/info.html",
				controller: "infoCtrl",
			})
			.otherwise("/");
	},
]);
