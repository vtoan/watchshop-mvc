var Pagination = function (_config) {
    let container = $(_config.container);
    this.pages = 0;
    this.init = function (len) {
        container.empty().hide();
        if (len == 0) return;
        pages = Math.ceil(len / _config.itemPages);
        for (let i = 0; i < pages; i++) container.append(`<span class="btn page-item" data-index=${i}>${i + 1}</span>`);
        //attach-event
        container.on("click", "span", function () {
            let elm = $(this);
            elm.parent().find(".muted").removeClass("muted");
            elm.addClass("muted");
            _config.onPageChange(Number(elm.data("index")));
        });
        //call func show first page
        this.showPageFirst();
    };

    this.showPageFirst = function () {
        $(".page-item:first-child").trigger("click");
    };

    this.hidden = function () {
        container.hide();
    };

    this.show = function () {
        container.show();
    };
};

$(function () {
    let products = [];
    let temp = [];
    let scrollValue = 0;
    let itemPages = 16;
    let pageObj = new Pagination({
        container: ".pagnation",
        itemPages: itemPages,
        onPageChange: showDataOnPage,
    });
    /*=======?handler======= */
    function showDataOnPage(index) {
        window.scrollTo({
            top: scrollValue,
            behavior: "smooth",
        });
        setTimeout(function () {
            UILoader("#product-container");
            if (index == pageObj.pages - 1) renderProducts(products.slice(index * itemPages), "#product-container");
            else renderProducts(products.slice(index * itemPages, (index + 1) * itemPages), "#product-container");
            pageObj.show();
        }, 400);
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
        if (!data) renderProducts(null, "#product-container");
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
    //========== ?exce ==========
    $("#product-container").on("click", ".add-cart", function () {
        addCart(this, true);
    });
    $("#orderby > a").on("click", function () {
        let elm = $(this);
        elm.parent().find(".nav-item.active").removeClass("active");
        elm.addClass("active");
        orderbyProduct(Number(elm.data("index")));
    });
    let code = getPageCode();
    scrollValue = code == -2 ? 100 : 525;
    reqListProducts(code == -2 ? "/Product/GetProductResult" : "/Product/GetProductByCate", code, showData);
    UIDropDown();
    onSelectedItemDropdown(filterProduct);
});
