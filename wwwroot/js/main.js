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
function onSelectedItemDropdown(callBack) {
    $(".dropdown .dropdown-menu li").on("click", function () {
        let item = $(this);
        let dropdown = item.parents(".dropdown");
        let index = Number(item.data("code"));
        dropdown.find("input").attr("value", index);
        dropdown.find(".select span").removeClass("placeholder").text(item.text());
        if (callBack) callBack(index);
    });
}

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
/*=======?ui-elemetn======= */
function UIPopup(message, resloveName, rejectName, redirectPage, rejectCallback) {
    let popup = $(`<div class="popup show-popup">
						<div class="popup-content">
							<p>${message}</p>
							<div class="popup-control">
								<a control-resolve class="btn btn-sm bg-red white" href="#!">${resloveName}</a>
								<a control-reject class="btn btn-sm bg-black white" href="#!">${rejectName}</a>
							</div>
						</div>
					</div>`);
    popup.find("a[control-resolve]").on("click", function () {
        window.location.href = redirectPage;
        close();
    });
    popup.find("a[control-reject]").on("click", function () {
        rejectCallback;
        close();
    });

    function close() {
        // $('body').removeClass('no-overflow');
        popup.removeClass("show-popup").addClass("hide-popup");
        setTimeout(() => popup.remove(), 200);
    }
    $("body").append(popup);
}

function UIDropDown() {
    $(".dropdown").click(function () {
        $(this).attr("tabindex", 1).focus();
        $(this).toggleClass("active");
        $(this).find(".dropdown-menu").slideToggle(300);
    });
    $(".dropdown").focusout(function () {
        $(this).removeClass("active");
        $(this).find(".dropdown-menu").slideUp(300);
    });
}

function UILoader(target) {
    $(target).empty().append('<div class="loader"><div></div><div></div><div></div><div></div></div>');
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
/*=======?helpful======= */
function cvtMoneyToInt(target) {
    if (typeof target == "string") target = $(target);
    return Number(target.text().split(".").join(""));
}

function cvtIntToMoney(val) {
    return new Intl.NumberFormat("de-De").format(val);
}

function visibleElement(eOpen, eClose, eTarget) {
    try {
        eOpen.on("click", () => eTarget.removeClass("hide-item").addClass("show-item"));
        eClose.on("click", () => eTarget.removeClass("show-item").addClass("hide-item"));
    } catch {
        throw "Not is JQuery Element";
    }
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
