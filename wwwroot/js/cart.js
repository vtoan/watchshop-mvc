$(function () {
    "use strict";
    var itemRender;
    let promotion = [];
    //
    let totalOrderElm = $("#bill span[order-total]");
    let disountOrderElm = $("span[order-discount]");
    let payOrderElm = $("#bill span[order-pay]");
    //========?request=========
    function reqGetCartItems() {
        //Get all id of product in cart
        let itemIDs = orderObj.getData().items.reduce((str, currItem) => (str += currItem.productId + ","), "");
        //request
        $.ajax({
            url: "/Product/GetProductsByIDs",
            data: {
                idString: itemIDs,
            },
            contentType: "text/plain",
            dataType: "JSON",
        }).done((data) => renderCartItem(data));
    }

    function reqGetBillPromotion() {
        $.ajax({
            url: "/Cart/GetBillPromotion",
            method: "GET",
            dataType: "JSON",
        }).done((resp) => {
            promotion = resp;
            updateAmountTotal();
        });
    }

    function reqGetOrderItems(idOrder) {
        $.ajax({
            url: "/Cart/GetOrderItems",
            method: "GET",
            dataType: "JSON",
            data: {
                id: idOrder,
            },
        }).done((resp) => {
            let s = JSON.parse(resp);
            console.log(s);
            console.log(typeof s);
            let obj = { items: s };
            orderObj.setData(obj);
        });
    }

    //========?list item =========
    let updateAmountTotal = function () {
        let totalItem = orderObj.getCount();
        $("#count-items").text(totalItem);
        updateViewCount();
        if (totalItem == 0) {
            emptyCart();
            return;
        }
        //
        let totalAmount = 0;
        let discount = 0;
        let unitDiscount = "0 đ";
        let totalFee = 0;
        let totalPay = 0;
        //cal total amount of item
        let item = $("#cart-container .cart-item");
        item.each(function () {
            totalAmount = totalAmount + cvtMoneyToInt($(this).find("span[item-total]"));
        });
        //cal promotion bill
        if (promotion) {
            let lenProm = promotion.length;
            if (promotion.length > 0) {
                for (let index = 0; index < lenProm; index++) {
                    let prom = promotion[index];
                    if (
                        (prom.conditionAmount && totalAmount >= prom.conditionAmount) ||
                        (prom.conditionItem && prom.conditionItem <= totalItem)
                    ) {
                        if (parseInt(prom.discount) == prom.discount) {
                            discount = prom.discount;
                            unitDiscount = discount + unitDiscount;
                        } else {
                            discount = Math.round(totalAmount * (1 - prom.discount));
                            unitDiscount = prom.discount * 100 + " %";
                        }
                    }
                }
            }
        }
        totalPay = totalAmount - discount;
        //call fee
        let fees = $("#bill span[order-fee]");
        if (fees || fees.length > 0) {
            fees.each(function () {
                let val = $(this).data("cost");
                totalFee += parseInt(val) == val ? val : Math.round(totalPay * val);
            });
        }
        //show result
        totalOrderElm.text(cvtIntToMoney(totalAmount));
        disountOrderElm.text("- " + unitDiscount);
        payOrderElm.text(cvtIntToMoney(totalPay + totalFee));
    };

    let updateAmountItem = function (trigger, operation) {
        let item = $(trigger).parents(".cart-item");
        let countElm = item.find("span[item-count]");
        let priceElm = item.find("span[item-price");
        let count = parseInt(countElm.text()) + (operation ? 1 : -1);
        if (!count) return; // if count == 0 => not thing
        //update on view
        countElm.text(count);
        item.find("span[item-total]").text(cvtIntToMoney(cvtMoneyToInt(priceElm) * count)); // update total amount of this item
        //update in object
        orderObj.changeQuantityItem(item.data("itemid"), operation);
        //
        updateAmountTotal();
    };

    function emptyCart() {
        $("#cart-container")
            .empty()
            .append(
                '<p class="text-center" style="line-height: 180px;">Chưa có sản phẩm nào <a class="link blue" href="/khuyen-mai">MUA SẮM NGAY</a></p>'
            );
        totalOrderElm.text(cvtIntToMoney(0));
        disountOrderElm.text("- 0 đ");
        payOrderElm.text(cvtIntToMoney(0));
    }

    let removeOrderItem = function (target) {
        let item = $(target).parents(".cart-item");
        orderObj.removeItem(item.data("itemid"));
        item.remove();
        //
        updateAmountTotal();
        if (orderObj.isEmpty()) emptyCart();
    };
    // defien-event
    function attachEventItem() {
        $(".cart-item .btn.add").on("click", function () {
            updateAmountItem(this, true);
        });
        $(".cart-item .btn.subtract").on("click", function () {
            updateAmountItem(this, false);
        });
        $(".cart-item .btn.remove").on("click", function () {
            removeOrderItem(this);
        });
    }
    //===========?render-view ===========
    //cal fee;
    function showFee() {
        let fees = $("#bill span[order-fee]");
        if (!fees || fees.length == 0) return 0;
        fees.each(function () {
            let val = $(this).data("cost");
            let unitCost = " đ";
            if (parseInt(val) == val) $(this).text(cvtIntToMoney(val) + unitCost);
            else $(this).text(val * 100 + " %");
        });
    }

    function renderCartItem(items) {
        let container = $("#cart-container").empty();
        if (!items || items.length == 0) emptyCart();
        else {
            items.forEach((item) => container.append(itemRender(item)));
            attachEventItem();
        }
        reqGetBillPromotion();
    }

    function getCodePage() {
        let elm = $("#page");
        let code = elm.length == 0 ? -1 : Number(elm.data("code"));
        // if (code == 0) attachValidateForFrom();
        itemRender = code > 0 ? crtCartItemConfirmElm : crtCartItemElm;
        elm.remove();
    }
    //========================== create element ===============================
    function crtCartItemElm(p) {
        let orderItem = orderObj.getItem(p.id);
        let priceCurrent = p.price;
        let discount = Number(p.discount);
        if (discount) {
            priceCurrent =
                !parseInt(discount) == discount ? p.price - Math.round(p.price * p.discount) : p.price - discount;
        }
        return `<div class="cart-item mb-3" data-itemid=${p.id}>
                    <div class="row align-items-lg-center">
                        <div class="col-4 col-lg-2">
                            <div class="img"><img src="${p.image}"></div>
                        </div>
                        <div class="col-6 col-lg-8">
                            <div class="row align-items-center">
                                <div class="col-12 col-lg-6 mb-2">
                                    <p>${p.bandName} ${p.name} - ${p.wireName}</p>
                                    <span item-price class="text-4 red bold">${cvtIntToMoney(priceCurrent)}</span>
                                    <span class="red">đ</span>
                                    ${
                                        discount == 0
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
        let orderItem = orderObj.getItem(p.id);
        let priceCurrent = p.price;
        let discount = Number(p.discount);
        if (discount) {
            priceCurrent =
                !parseInt(discount) == discount ? p.price - Math.round(p.price * p.discount) : p.price - discount;
        }
        return `<div class="cart-item mb-3" data-itemid=${p.id}>
                    <div class="row align-items-lg-center">
                        <div class="col-4 col-lg-2">
                            <div class="img">
                                <img src="${p.image}">
                            </div>
                        </div>
                        <div class="col-8 col-lg-10">
                            <div class="row align-items-end">
                                <div class="col-12 col-lg-6 mb-2">
                                    <p>${p.bandName} ${p.name} - ${p.categoryName} - Dây ${p.wireName}</p>
                                    <span item-price class="text-4 red bold">${cvtIntToMoney(priceCurrent)}</span>
                                    <span class="red">đ</span>
                                    ${discount == 0 ? "" : `<del class="normal">${cvtIntToMoney(p.price)} đ</del>`}
                                </div>
                                <div class="col-12 col-lg-3 mb-2">
                                    <span item-count >SL: ${orderItem.quantity}</span>                 
                                </div>
                                <div class="col-12 col-lg-3 mb-2">
                                    <span >Tổng: </span>
                                    <span item-total class="bold">${cvtIntToMoney(
                                        orderItem.quantity * priceCurrent
                                    )}</span>
                                    <span class="bold">đ</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`;
    }
    //========================== exec ===============================
    getCodePage();
    showFee();
    let idOrd = $("#bill").data("id");
    if (idOrd) {
        orderObj.clear();
        reqGetOrderItems(idOrd);
    }

    if (!orderObj.isEmpty()) {
        reqGetBillPromotion();
        reqGetCartItems();
    } else emptyCart();

    //========== ?destroy ==========
    $("#product-container").on("unload", function () {
        $(".cart-item .btn.add").off("click", function () {
            updateAmountItem(this, true);
        });
        $(".cart-item .btn.subtract").off("click", function () {
            updateAmountItem(this, false);
        });
        $(".cart-item .btn.remove").off("click", function () {
            removeOrderItem(this);
        });
    });
});
