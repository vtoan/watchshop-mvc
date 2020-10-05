/*=======?ui-elemetn======= */
function UIPopup(message, resloveName, rejectName, redirectPage, rejectCallback) {
    let popup = $(`<div class="popup show-popup">
						<div class="popup-content">
							<p>${message}</p>
							<div class="popup-control">
								<a control-resolve class="btn btn-sm bg-red white" href="#!">${resloveName}</a>
								<a control-reject class="btn btn-sm bg-black white" href="#!">${rejectName}</a>
							</div>
						</div>
					</div>`);
    popup.find("a[control-resolve]").on("click", function () {
        window.location.href = redirectPage;
        close();
    });
    popup.find("a[control-reject]").on("click", function () {
        rejectCallback;
        close();
    });

    function close() {
        // $('body').removeClass('no-overflow');
        popup.removeClass("show-popup").addClass("hide-popup");
        setTimeout(() => popup.remove(), 200);
    }
    $("body").append(popup);
}

function UIDropDown() {
    $(".dropdown").click(function () {
        $(this).attr("tabindex", 1).focus();
        $(this).toggleClass("active");
        $(this).find(".dropdown-menu").slideToggle(300);
    });
    $(".dropdown").focusout(function () {
        $(this).removeClass("active");
        $(this).find(".dropdown-menu").slideUp(300);
    });
}

function UILoader(target) {
    $(target).empty().append('<div class="loader"><div></div><div></div><div></div><div></div></div>');
}
/*=======?event======= */
function onSelectedItemDropdown(callBack) {
    $(".dropdown .dropdown-menu li").on("click", function () {
        let item = $(this);
        let dropdown = item.parents(".dropdown");
        let index = Number(item.data("code"));
        dropdown.find("input").attr("value", index);
        dropdown.find(".select span").removeClass("placeholder").text(item.text());
        if (callBack) callBack(index);
    });
}

/*=======?helpful======= */
function cvtMoneyToInt(target) {
    if (typeof target == "string") target = $(target);
    return Number(target.text().split(".").join(""));
}

function cvtIntToMoney(val) {
    return new Intl.NumberFormat("de-De").format(val);
}

function visibleElement(eOpen, eClose, eTarget) {
    try {
        eOpen.on("click", () => eTarget.removeClass("hide-item").addClass("show-item"));
        eClose.on("click", () => eTarget.removeClass("show-item").addClass("hide-item"));
    } catch {
        throw "Not is JQuery Element";
    }
}
