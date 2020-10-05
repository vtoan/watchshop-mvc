var order;
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
function addCart(elm, popup) {
    let id = $(elm).parents(".product").data("itemid");
    if (!id) id = $(elm).parents(".product-text").data("itemid");
    if (typeof id == "undefined") window.location.href = "/error";
    //add item
    addItemToCart(id);
    updateViewItemCart();
    if (!popup) return;
    UIPopup("Thêm vào giỏ hàng thành công", "Xem Giỏ hàng", "Không", "/gio-hang");
}

function addItemToCart(id) {
    let index = order.items.findIndex((item) => item.id == id);
    if (index >= 0) order.items[index].quantity++;
    else
        order.items.push({
            id: id,
            quantity: 1,
        });
}
/*=======?render product======= */
function renderProducts(items, target) {
    console.log("render " + window.scrollY);
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
    if (discount == 0) {
        priceCurrent =
            !parseInt(discount) == discount ? p.price - Math.round(p.price * p.discount) : p.price - discount;
    }

    return `<div class="col-6 col-lg-3">
				<div class="product ${discount == 0 ? "" : "sale"}" data-itemid=${p.id}>
                    <div class="product-img img-content">
                        <span class="product-sale"> - ${cvtIntToMoney(discount)} ${
        parseInt(discount) == discount ? "đ" : "%"
    }</span>
						<div class="img">
							<img src="/products/${p.image}" loading="lazy">
						</div>
						<a class="add-cart text-center d-none d-lg-block" href="#!">Add to Cart</a>
					</div>
					<a href="/san-pham/?id=${p.id}" class="product-content">
						<h4 class="pb-2 normal">${p.bandName} - ${p.name} - Dây ${p.wireName}</h4>
						<div class="text-center">
                            <p class="text-4 red bold d-block d-lg-inline-block m-0">
                            ${cvtIntToMoney(priceCurrent)} 
                            đ</p>
							<del class="normal text-sub">${cvtIntToMoney(p.price)} đ</del>
						</div>
					</a>
				</div>
			</div>`;
}

function updateViewItemCart() {
    if (order.length == 0) $("#cart-items").text(0);
    else $("#cart-items").text(order.items.reduce((t, item) => (t = t + item.quantity), 0));
}
/*=======?exec======= */
$(function () {
    visibleElement($("#menu"), $(".nav-mobile .btn-classic"), $(".nav-mobile"));
    visibleElement($("#search"), $(".searchbar .btn-classic"), $(".searchbar"));
    $("#search-sm").on("click", () => {
        $(".nav-mobile .btn-classic").click();
        $("#search").click();
    });
    $(".search-input input").on("keydown", function (e) {
        if (e.keyCode == 13) $(this).parents("form").trigger("submit");
    });
});
