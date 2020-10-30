$(function () {
    "use strict";
    /*=======?gallery======= */
    function Gallery(obj, nav) {
        let gallery = $(`${obj} .img`);
        let nav_gallery = $(nav);
        let widthItemNav = nav_gallery.children(":first-child").width();
        //add offset
        nav_gallery.children().each((idx, elm) => $(elm).attr("offset", idx));
        //handler
        function showImage(img) {
            gallery.css("background-image", `url(${img})`);
        }

        function changeImage(operation) {
            let itemCurr = nav_gallery.children(".active").removeClass("active");
            itemCurr = operation < 0 ? itemCurr.prev() : itemCurr.next();
            if (itemCurr.length == 0)
                itemCurr = operation < 0 ? nav_gallery.children(":last-child") : nav_gallery.children(":first-child");
            itemCurr.addClass("active");
            showImage(itemCurr.children().attr("src"));
            nav_gallery.get(0).scroll({
                left: itemCurr.attr("offset") * widthItemNav,
                behavior: "smooth",
            });
        }

        nav_gallery.children().on("mouseenter", function () {
            nav_gallery.children(".img.active").removeClass("active");
            showImage($(this).addClass("active").children().attr("src"));
        });

        $(`${obj} .arrow-prev`).on("click", function () {
            console.log("s");
            changeImage(-1);
        });
        $(`${obj} .arrow-next`).on("click", function () {
            changeImage(1);
        });
        changeImage(1);
    }
    /*=======?attach-event======= */
    $(".buy-now").on("click", function () {
        let id = $(this).parents(".product").data("itemid");
        if (!id) id = $(this).parents(".product-text").data("itemid");
        if (typeof id == "undefined") window.location.href = "/error";
        //add item
        orderObj.addItem(id);
        updateViewCount();
        window.location.href = "/gio-hang";
    });
    $(".to-cart").on("click", function () {
        addCartEvent(this);
    });
    $("#product-container-other").on("click", ".add-cart", function () {
        addCartEvent(this);
    });

    function showPriceProduct() {
        let priceElm = $("span[data-price]");
        let discountElm = $("del[data-discount]");
        //
        let priceVal = priceElm.data("price");
        let discountVal = discountElm.data("discount");
        //
        if (priceVal) priceElm.text(cvtIntToMoney(calDiscount(priceVal, discountVal)) + " đ");
        if (discountVal) discountElm.text(cvtIntToMoney(priceVal) + " đ");
        else discountElm.hide();
    }

    function calDiscount(price, val) {
        if (!price) return 0;
        if (!val) return price;
        return parseInt(val) == val ? price - val : price * (1 - val);
    }

    function hideCellEmpty() {
        $(".table tr td:nth-child(2)").each(function (key, val) {
            let elm = $(val);
            if (elm.text() == "") elm.parent().hide();
        });
    }
    /*=======?exec======= */
    showPriceProduct();
    Gallery("#gallery", "#gallery-nav");
    hideCellEmpty();

    reqListProducts("/Product/GetProductByCate", -1, (data) => renderProducts(data, "#product-container-other"), 4);
});
