$(function () {
    "use strict";
    /*=======?attach-event======= */
    $("body").on("click", ".add-cart", function () {
        addCartEvent(this);
    });
    $(".to-cart").on("click", function () {
        addCartEvent(this);
    });
    /*=======?exec======= */
    reqListProducts("/Product/GetProductByCate", 0, (data) => renderProducts(data, "#product-container-discount"), 8);
    reqListProducts("/Product/GetProductByCate", -1, (data) => renderProducts(data, "#product-container-seller"), 4);
    initSlider(".slider-container", ".arrow-prev", ".arrow-next", 500, 3000);
});
