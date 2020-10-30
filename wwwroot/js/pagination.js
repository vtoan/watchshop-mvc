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
