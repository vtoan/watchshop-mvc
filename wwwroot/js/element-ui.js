/*=======?ui-elemetn======= */
function UIPopup(message, resolveName, rejectName, resolveCallBack, rejectCallback) {
    // element
    let popup = $(`<div class="popup show-popup">
						<div class="popup-content">
							<p>${message}</p>
							<div class="popup-control">
								<a control-resolve class="btn btn-sm bg-red white" href="javascript:void(0)">${resolveName}</a>
								<a control-reject class="btn btn-sm bg-black white" href="javascript:void(0)">${rejectName}</a>
							</div>
						</div>
                    </div>`);
    let resolveElm = popup.find("a[control-resolve]");
    let rejectElm = popup.find("a[control-reject]");
    //callback
    let resolveCb = function () {
        if (resolveCallBack) resolveCallBack();
        close();
    };
    let rejectCb = function () {
        if (rejectCallback) rejectCallback();
        close();
    };
    // attach - event
    !resolveName ? resolveElm.hide() : resolveElm.on("click", resolveCb);
    !rejectName ? rejectElm.hide() : rejectElm.on("click", rejectCb);
    // defin -event
    function close() {
        resolveElm.off("click", resolveCb);
        rejectElm.off("click", rejectCb);
        popup.removeClass("show-popup").addClass("hide-popup");
        setTimeout(() => popup.remove(), 200);
        $("body").off("keydown", closeWithKey);
    }

    function closeWithKey(e) {
        console.log("Close popup with key");
        e.preventDefault();
        if ((e.keyCode = 13)) resolveCb();
    }
    // exec
    $("body").append(popup);
    $("body").on("keydown", closeWithKey);
}
/*=======?drop down======= */
function UIDropDown(callBack) {
    this.callBack = callBack;
    let showDrop = function () {
        $(this).attr("tabindex", 1).focus();
        $(this).toggleClass("active");
        $(this).find(".dropdown-menu").slideToggle(300);
    };

    let hideDrop = function () {
        $(this).removeClass("active");
        $(this).find(".dropdown-menu").slideUp(300);
    };

    let onSelected = function () {
        let item = $(this);
        let dropdown = item.parents(".dropdown");
        let index = Number(item.data("code"));
        dropdown.find("input").attr("value", index);
        dropdown.find(".select span").removeClass("placeholder").text(item.text());
        if (callBack) callBack(index);
    };

    this.attach = function () {
        console.log("Attach Dropdown");
        $("body").on("click", ".dropdown", showDrop);
        $("body").on("focusout", ".dropdown", hideDrop);
        $(".dropdown .dropdown-menu").on("click", "li", onSelected);
    };

    this.detach = function () {
        console.log("Detach Dropdown");
        $("body").off("click", ".dropdown", showDrop);
        $("body").off("focusout", ".dropdown", hideDrop);
        $(".dropdown .dropdown-menu").off("click", "li", onSelected);
    };
}

function UILoader(target) {
    $(target).empty().append('<div class="loader"><div></div><div></div><div></div><div></div></div>');
}

/*=======?helpful======= */
function cvtMoneyToInt(target) {
    if (typeof target == "string") target = $(target);
    return Number(target.text().split(".").join(""));
}

function cvtIntToMoney(val) {
    return new Intl.NumberFormat("de-De").format(val);
}

function cvtDiscountDisplay(price, val) {
    return parseInt(val) == val ? cvtIntToMoney(val) + " đ" : val * 100 + " %";
}

function visibleElement(eOpen, eClose, eTarget) {
    try {
        eOpen.on("click", () => eTarget.removeClass("hide-item").addClass("show-item"));
        eClose.on("click", () => eTarget.removeClass("show-item").addClass("hide-item"));
    } catch {
        throw "Not is JQuery Element";
    }
}

/*=======?exec======= */
// $(function () {
// 	//Navigatte
// 	visibleElement($("#menu"), $(".nav-mobile .btn-classic"), $(".nav-mobile"));
// 	$(".nav-mobile .nav-item").on("click", function () {
// 		$(".nav-mobile .btn-classic").trigger("click");
// 	});
// 	//Search
// 	visibleElement($("#search"), $(".searchbar .btn-classic"), $(".searchbar"));
// 	$("#search-sm").on("click", () => {
// 		$(".nav-mobile .btn-classic").click();
// 		$("#search").click();
// 	});
// 	$(".search-input input").on("keydown", function (e) {
// 		if (e.keyCode == 13) $(this).parents("form").trigger("submit");
// 		e.preventDefault();
// 	});
// });
