$(function () {
    "use strict";
    var itemReander;
    //========?request=========
    function reqGetCartItems() {
        //Get all id of product in cart
        let itemIDs = order.items.reduce((str, currItem) => (str += currItem.id + ","), "");
        //request
        $.ajax({
            url: "/Product/GetProductsByIDs",
            data: {
                idString: itemIDs,
            },
            contentType: "text/plain",
            dataType: "JSON",
        }).done((data) => showData(data));
    }

    function reqGetBillPromotion() {
        let arrItems = JSON.stringify(order.items);
        $.ajax({
            url: "/Cart/GetBillPromotion",
            data: { items: arrItems },
            method: "GET",
            dataType: "JSON",
        }).done((resp) => updateAmountPay(resp));
    }

    function reqProvince() {
        $.ajax({
            url: "/asset/province.json",
            method: "GET",
            dataType: "JSON",
        }).done((resp) => showProvince(resp));
    }

    function reqDistrict(id) {
        $.ajax({
            url: `/asset/district/${id}.json`,
            method: "GET",
            dataType: "JSON",
        }).done((resp) => showDistrict(resp));
    }
    //========?list item =========
    function updateAmountPay(promValue) {
        updateViewCountItem();
        let total = 0;
        let prom = 1;
        let promText = "";
        //cal total amount of item
        let item = $("#cart-container .cart-item");
        item.each(function () {
            total = total + cvtMoneyToInt($(this).find("span[item-total]"));
        });
        //cal promotion bill
        let discount = Number(promValue);
        if (discount != NaN) {
            prom = parseInt(discount);
            promText = prom == discount ? cvtIntToMoney(discount) + " đ" : discount * 100 + " %";
            if (prom != discount) prom = Math.round(total * (1 - discount));
            showFee();
        } else {
            $("#" + promValue).text(0);
            promText = " đ";
        }
        //show result
        $("span[order-discount]").text("- " + promText);
        $("#bill span[order-total]").text(cvtIntToMoney(total));
        $("#bill span[order-pay]").text(cvtIntToMoney(total - prom + calFee(total)));
    }

    function updateAmountItem(trigger, operation) {
        let item = $(trigger).parents(".cart-item");
        let countElm = item.find("span[item-count]");
        let priceElm = item.find("span[item-price");
        let count = parseInt(countElm.text()) + (operation ? 1 : -1);
        if (!count) return; // if count == 0 => not thing
        countElm.text(count);
        item.find("span[item-total]").text(cvtIntToMoney(cvtMoneyToInt(priceElm) * count)); // update total amount of this item
        changeQuantityOrderItem(item.data("itemid"), operation); // update quantity in order object            reqGetBillPromotion();
        reqGetBillPromotion(); // check BillPromtion
    }

    function calFee(amount) {
        let totalFee = 0;
        let fees = $("#bill span[order-fee]");
        if (!fees || fees.length == 0) return 0;
        fees.each(function () {
            let val = $(this).text();
            let fe = cvtMoneyToInt($(this));
            if (val.endsWith("%")) totalFee += amount * (fe / 100);
            else totalFee += fe;
        });
        return totalFee;
    }

    function updateViewCountItem() {
        let orderItems = $("#cart-items");
        let count = parseInt(orderItems.text());
        $("#count-items").text(count);
        if (count == 0) emptyCart();
    }

    function emptyCart() {
        $("#cart-container")
            .empty()
            .append(
                '<p class="text-center" style="line-height: 180px;">Chưa có sản phẩm nào <a class="link blue" href="/khuyen-mai">MUA SẮM NGAY</a></p>'
            );
    }
    // defien-event
    function attachEventItem() {
        $(".cart-item .btn.add").on("click", function () {
            updateAmountItem(this, true);
        });

        $(".cart-item .btn.subtract").on("click", function () {
            updateAmountItem(this, false);
        });

        $(".cart-item .btn.remove").on("click", function () {
            let item = $(this).parents(".cart-item");
            removeOrderItem(item.data("itemid"));
            item.remove();
            if (isExistOrders()) reqGetBillPromotion();
            else {
                let ship = $("span[order-fee]");
                ship.text(ship.data("cost"));
                $("span[order-discount]").text("0 đ");
                $("#bill span[order-total]").text(0);
                $("#bill span[order-pay]").text(0);
            }
        });
    }
    function attachValidateForFrom() {
        $("#form-confirm").find(".validate > input").addClass("input-invalid");
    }
    //======== ?treatment with order object ==========
    function isExistOrders() {
        if (!order) return false;
        if (order.items.length == 0) return false;
        return true;
    }

    function changeQuantityOrderItem(id, operation) {
        if (!isExistOrders()) return;
        let indx = order.items.findIndex((i) => i.id == id);
        if (indx < 0) return;
        operation ? order.items[indx].quantity++ : order.items[indx].quantity--;
        updateViewItemCart();
    }

    function removeOrderItem(id) {
        if (!isExistOrders()) return;
        let indx = order.items.findIndex((i) => i.id == id);
        if (indx < 0) return;
        order.items.splice(indx, 1);
        updateViewItemCart();
    }
    //===========?render-view ===========
    function showData(data) {
        renderCartItem(data);
    }
    //cal fee;
    function showFee() {
        let ship = $("span[order-fee]");
        ship.text(cvtIntToMoney(ship.data("cost")));
    }

    function renderCartItem(items) {
        let container = $("#cart-container").empty();
        if (!items || items.length == 0) emptyCart();
        else {
            items.forEach((item) => container.append(itemReander(item)));
            reqGetBillPromotion();
            attachEventItem();
        }
    }

    function getCodePage() {
        let elm = $("#page");
        let code = elm.length == 0 ? -1 : Number(elm.data("code"));
        if (code == 0) attachValidateForFrom();
        itemReander = code > 0 ? crtCartItemConfirmElm : crtCartItemElm;
        elm.remove();
    }
    //========================== create element ===============================
    function crtCartItemElm(p) {
        let orderItem = order.items.find((i) => i.id == p.id);
        let priceCurrent = p.price;
        let discount = Number(p.discount);
        if (discount == 0) {
            priceCurrent =
                !parseInt(discount) == discount ? p.price - Math.round(p.price * p.discount) : p.price - discount;
        }
        return `<div class="cart-item mb-3" data-itemid=${p.id}>
                    <div class="row align-items-lg-center">
                        <div class="col-4 col-lg-2">
                            <div class="img"><img src="/products/${p.image}"></div>
                        </div>
                        <div class="col-6 col-lg-8">
                            <div class="row align-items-center">
                                <div class="col-12 col-lg-6 mb-2">
                                    <p>${p.bandName} ${p.name} - ${p.wireName}</p>
                                    <span item-price class="text-4 red bold">${cvtIntToMoney(priceCurrent)}</span>
                                    <span class="red">đ</span>
                                    ${
                                        p.discount == "0"
                                            ? ""
                                            : `<del class="normal text-sub">${cvtIntToMoney(p.price)} đ</del>`
                                    }
                                </div>
                                <div class="col-12 col-lg-3 mb-2">
                                    <div class="d-flex align-items-center">
                                        <a class="btn btn-sm add"><i class="las la-angle-up"></i></a>
                                        <span item-count class="px-3">${orderItem.quantity}</span>
                                        <a  class="btn btn-sm subtract"><i class="las la-angle-down"></i></a>
                                    </div>
                                </div>
                                <div class="col-12 col-lg-3 mb-2">
                                    <span >Tổng: </span>
                                    <span item-total class="text-4 bold">${cvtIntToMoney(
                                        orderItem.quantity * priceCurrent
                                    )}</span>
                                    <span class="bold">đ</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-2 text-right">
                            <a class="btn red remove" href="#!"><i class=" icon las la-times"></i></i></a>
                        </div>
                    </div>
                </div>`;
    }

    function crtCartItemConfirmElm(p) {
        let orderItem = order.items.find((i) => i.id == p.id);
        let priceCurr = Math.round(p.price - p.discount);
        return `<div class="cart-item mb-3" data-itemid=${p.id}>
                    <div class="row align-items-lg-center">
                        <div class="col-4 col-lg-2">
                            <div class="img">
                                <img src="/products/${p.image}">
                            </div>
                        </div>
                        <div class="col-8 col-lg-10">
                            <div class="row align-items-end">
                                <div class="col-12 col-lg-6 mb-2">
                                    <p>${p.bandName} ${p.name} - ${p.categoryName} - Dây ${p.wireName}</p>
                                    <span item-price class="text-4 red bold">${cvtIntToMoney(priceCurr)}</span>
                                    <span class="red">đ</span>
                                    ${p.discount == "0" ? "" : `<del class="normal">${cvtIntToMoney(p.price)} đ</del>`}
                                </div>
                                <div class="col-12 col-lg-3 mb-2">
                                    <span item-count >SL: ${orderItem.quantity}</span>                 
                                </div>
                                <div class="col-12 col-lg-3 mb-2">
                                    <span >Tổng: </span>
                                    <span item-total class="bold">${cvtIntToMoney(
                                        orderItem.quantity * priceCurr
                                    )}</span>
                                    <span class="bold">đ</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`;
    }
    //========================== validate for dropdown local ===============================
    function validateLocal() {
        if (checkLocal("local-province", "Chọn Tỉnh/TP") && checkLocal("local-district", "Chọn Quận/Huyện")) {
            let local = $("#bill .local").addClass("valid");
            if (local.hasClass("invalid")) local.removeClass("invalid");
            return true;
        }
        return false;
    }

    function checkLocal(target, errorText) {
        let obj = $(`#bill .local div[${target}] input`);
        let select = obj.prev();
        let local = obj.parents(".local");
        if (!obj.val()) {
            select.addClass("input-invalid");
            local.addClass("invalid");
            obj.parents(".local").find(".error").text(errorText);
            return false;
        } else {
            if (select.hasClass("input-invalid")) {
                obj.parents(".local").find(".error").text("");
                select.removeClass("input-invalid");
            }
            return true;
        }
    }
    //========================== local data ===============================
    function showProvince(province) {
        renderLocal("#bill .local div[local-province] .dropdown-menu", province);
        $("#bill .control-input div[local-district]").addClass("muted");
        onSelectedItemDropdown(reqDistrict);
    }

    function showDistrict(district) {
        renderLocal("#bill .local  div[local-district] .dropdown-menu", district);
        $("#bill .control-input div[local-district]").removeClass("muted");
        onSelectedItemDropdown();
        validateLocal();
    }
    //render local
    function renderLocal(target, data) {
        if (typeof target == "string") target = $(target);
        Object.values(data).forEach((item) =>
            target.append(`<li data-code="${item.code}">${item.name_with_type}</li>`)
        );
    }
    //========================== attach event for form ===============================
    $("#bill .btn").on("click", () => $("form > button").trigger("click"));
    //begin validate
    $("#form-confirm input").on("click", () => {
        attachValidateForFrom();
        validateLocal();
    });
    //begin validate for dropdown
    $("#form-confirm .local").on("mouseout", function () {
        validateLocal();
        $(this).find(".error").parent().hide();
    });
    $("#form-confirm .local").on("mouseover", function () {
        $(this).find(".error").parent().show();
    });
    //submit form confirm order
    $("#form-confirm").on("submit", function (e) {
        if (!validateLocal() || !isExistOrders()) {
            e.preventDefault();
            return;
        }
        let address = $("#CustomerAddress").val();
        let provinceID = $("#CustomerProvince").val();
        let districtID = $("#district").val();
        $("#CustomerProvince").val($(`div[local-province] .dropdown-menu li[data-code="${provinceID}"]`).text());
        $("#CustomerAddress").val(
            address + "," + $(`div[local-district] .dropdown-menu li[data-code="${districtID}"]`).text()
        );
    });
    //submit form order
    $("#form").on("submit", function (e) {
        if (!isExistOrders()) {
            e.preventDefault();
            return;
        }
        $(this).append(`<input name='items' type='hidden' value='${JSON.stringify(order.items).toString()}'>`);
    });
    //========================== exec ===============================
    UIDropDown();
    reqProvince();
    getCodePage();
    showFee();
    if (isExistOrders()) reqGetCartItems();
    else emptyCart();
});
