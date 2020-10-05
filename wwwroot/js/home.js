$(function () {
    "use strict";
    /*=======?attach-event======= */
    $("body").on("click", ".add-cart", function () {
        addCart(this, true);
    });
    $(".to-cart").on("click", function () {
        addCart(this, true);
    });
    /*=======?exec======= */
    reqListProducts("/Product/GetProductByCate", 0, (data) => renderProducts(data, "#product-container-discount"), 8);
    reqListProducts("/Product/GetProductByCate", -1, (data) => renderProducts(data, "#product-container-seller"), 4);
    initSlider(".slider-container", ".arrow-prev", ".arrow-next", 500, 3000);
});
