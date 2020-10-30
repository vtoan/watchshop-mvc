$(function () {
    let code = 0;
    let products = [];
    let temp = [];
    let scrollValue = 0;
    let itemPages = 16;
    let pageObj = new Pagination({
        container: ".pagnation",
        itemPages: itemPages,
        onPageChange: showDataOnPage,
    });
    let dropDown = {};
    // define event
    let orderbyEvent = function () {
        let elm = $(this);
        elm.parent().find(".nav-item.active").removeClass("active");
        elm.addClass("active");
        orderbyProduct(Number(elm.data("index")));
    };
    /*=======?handler======= */
    function showDataOnPage(index) {
        UILoader("#product-container");
        window.scrollTo({
            top: scrollValue,
            behavior: "smooth",
        });
        setTimeout(function () {
            if (index == pageObj.pages - 1) renderProducts(products.slice(index * itemPages), "#product-container");
            else renderProducts(products.slice(index * itemPages, (index + 1) * itemPages), "#product-container");
            pageObj.show();
        }, 300);
    }
    //get code
    function getPageCode() {
        let cate = $("#page");
        let code = cate.data("code");
        cate.remove();
        return code;
    }
    //show data
    function showData(data) {
        if (!data || data.length == 0) {
            renderProducts(null, "#product-container");
            return;
        }
        products = data;
        pageObj.init(products.length);
    }
    //filter
    function filterProduct(index) {
        if (index == 0) {
            showData([...temp]);
            temp = [];
        } else {
            if (temp.length == 0) temp = Array.from(products);
            showData(temp.filter((item) => item.typeWireID == index));
        }
    }
    //orderyby
    function orderbyProduct(index) {
        switch (index) {
            //Popular
            case 0:
                products.sort((a, b) => a.saleCount - b.saleCount);
                break;
            //High price
            case 1:
                products.sort((a, b) => b.price - b.discount - (a.price - a.discount));
                break;
            //Low price
            case 2:
                products.sort((a, b) => a.price - a.discount - (b.price - b.discount));
                break;
        }
        pageObj.showPageFirst();
    }
    //========== ?exec ==========
    $("#product-container").on("click", ".add-cart", function () {
        addCartEvent(this);
    });
    $("#orderby > a").on("click", orderbyEvent);
    //
    code = getPageCode();
    if (screen.width > 1024) scrollValue = code == -2 ? 100 : 525;
    else scrollValue = code == -2 ? 100 : 145;
    reqListProducts(code == -2 ? "/Product/GetProductResult" : "/Product/GetProductByCate", code, showData);
    dropDown = new UIDropDown(function (idx) {
        filterProduct(idx);
    });
    dropDown.attach();
    //========== ?destroy ==========
    $("#product-container").on("unload", function () {
        dropDown.detach();
        $("#product-container").off("click", ".add-cart", function () {
            addCartEvent(this);
        });
        $("#orderby > a").off("click", orderbyEvent);
    });
});
