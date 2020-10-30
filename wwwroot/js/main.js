var orderObj = new Order();
/*=======?request======= */
function reqListProducts(urlString, pageCode, callBack, number) {
    return $.ajax({
        url: urlString,
        data: {
            pageCode: pageCode,
            numberItem: number,
        },
        dataType: "json",
        method: "GET",
    }).done((response) => {
        callBack(response);
    });
}
/*=======?debug======= */
$(document).ajaxComplete(function (event, xhr, opts) {
    console.log(event.type + " - " + opts.url + " - " + xhr.status);
});
/*=======?ui-event======= */
let addCartEvent = function (elm) {
    let id = $(elm).parents(".product").data("itemid");
    if (!id) id = $(elm).parents(".product-text").data("itemid");
    if (typeof id == "undefined") window.location.href = "/error";
    //add item
    orderObj.addItem(id);
    //
    updateViewCount();
    //
    UIPopup("Thêm vào giỏ hàng thành công", "Xem Giỏ hàng", "Không", function () {
        window.location.href = "/gio-hang";
    });
};

/*=======?render product======= */
function renderProducts(items, target) {
    let container = $(target).empty();
    if (!items || items.length == 0)
        container
            .removeClass("row")
            .append(`<p class="text-center text-4" style="line-height:405px">Không có sản phẩm phù hợp</p>`);
    else {
        container.addClass("row");
        items.forEach((item) => container.append(crtProductElm(item)));
    }
}

function crtProductElm(p) {
    let priceCurrent = p.price;
    let discount = Number(p.discount);
    let unitDiscount = "đ";
    if (discount) {
        if (parseInt(discount) == discount) priceCurrent = p.price - discount;
        else {
            priceCurrent == p.price - Math.round(p.price * p.discount);
            discount = discount * 100;
            unitDiscount = "%";
        }
    }
    return `<div class="col-6 col-lg-3">
				<div class="product ${discount == 0 ? "" : "sale"}" data-itemid=${p.id}>
                    <div class="product-img img-content">
                        <span class="product-sale"> - ${cvtIntToMoney(discount)} ${unitDiscount}</span>
						<div class="img">
							<img src="${p.image}" loading="lazy">
						</div>
						<a class="add-cart text-center d-none d-lg-block" href="#!">Add to Cart</a>
					</div>
					<a href="/san-pham/?id=${p.id}" class="product-content">
						<h4 class="pb-2 normal">${p.bandName} - ${p.name} - Dây ${p.wireName}</h4>
						<div class="text-center">
                            <p class="text-4 red bold d-block d-lg-inline-block m-0">
                            ${cvtIntToMoney(priceCurrent)} 
                            đ</p>
							<del class="normal">${cvtIntToMoney(p.price)} đ</del>
						</div>
					</a>
				</div>
			</div>`;
}

function updateViewCount() {
    $("#cart-items").text(orderObj.getCount());
}
/*=======?cart client======= */
function saveCookie() {
    let dt = new Date(Date.now() + 30 * 86400000);
    document.cookie = `basketshopping= ${JSON.stringify(
        orderObj.getData()
    )}; expires= ${dt.toString()}; samesite=strict; path=/; secure`;
}
function getCookie() {
    let cookie = document.cookie;
    let asset = cookie.split(";");
    let cart;
    for (let i = 0; i < asset.length; i++) {
        let data = asset[i].split("=");
        if (data[0].trimStart() == "basketshopping") {
            cart = data[1];
        }
    }
    if (cart) return JSON.parse(cart);
    return {
        items: [],
    };
}
/*=======?exec======= */
$(function () {
    orderObj.setData(getCookie());
    updateViewCount();

    visibleElement($("#menu"), $(".nav-mobile .btn-classic"), $(".nav-mobile"));
    visibleElement($("#search"), $(".searchbar .btn-classic"), $(".searchbar"));
    $("#search-sm").on("click", () => {
        $(".nav-mobile .btn-classic").click();
        $("#search").click();
    });
    //
    $(".search-input input").on("keydown", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $(this).parents("form").trigger("submit");
        }
    });
});
// cookie
window.addEventListener("beforeunload", saveCookie);
// orderObj.setData(getCookie());
updateViewCount();
