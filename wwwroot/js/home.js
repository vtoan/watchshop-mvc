$(function () {
    /*=======?exec======= */
    reqListProducts(-1, data => renderProducts(data, '#product-container-discount'), 8)
    reqListProducts(-2, data => renderProducts(data, '#product-container-seller'), 4)
    initSlider('.slider-container', '.arrow-prev', '.arrow-next', 500, 3000);
})