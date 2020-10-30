$(function () {
    let dropDown = {};
    //========?request=========
    function reqProvince() {
        $.ajax({
            url: "/asset/province.json",
            method: "GET",
            dataType: "JSON",
        }).done((resp) => showProvince(resp));
    }

    function reqDistrict(id) {
        $.ajax({
            url: `/asset/district/${id}.json`,
            method: "GET",
            dataType: "JSON",
        }).done((resp) => showDistrict(resp));
    }

    function attachValidateForFrom() {
        $("#form-confirm").find(".validate > input").addClass("input-invalid");
    }
    //========================== validate for dropdown local ===============================
    function validateLocal() {
        if (checkLocal("local-province", "Chọn Tỉnh/TP") && checkLocal("local-district", "Chọn Quận/Huyện")) {
            let local = $("#bill .local").addClass("valid");
            if (local.hasClass("invalid")) local.removeClass("invalid");
            return true;
        }
        return false;
    }

    function checkLocal(target, errorText) {
        let obj = $(`#bill .local div[${target}] input`);
        let select = obj.prev();
        let local = obj.parents(".local");
        if (!obj.val()) {
            select.addClass("input-invalid");
            local.addClass("invalid");
            obj.parents(".local").find(".error").text(errorText);
            return false;
        } else {
            if (select.hasClass("input-invalid")) {
                obj.parents(".local").find(".error").text("");
                select.removeClass("input-invalid");
            }
            return true;
        }
    }
    //========================== local data ===============================
    function showProvince(province) {
        renderLocal("#bill .local div[local-province]", province);
        $("#bill .control-input div[local-district]").addClass("muted");
    }

    function showDistrict(district) {
        let target = $("#bill .local  div[local-district]");
        target.find(".select span").text("Chọn Quận/Huyện").addClass("placeholder");
        renderLocal(target, district);
        $("#bill .control-input div[local-district]").removeClass("muted");
        validateLocal();
    }
    //render local
    function renderLocal(target, data) {
        if (typeof target == "string") target = $(target);
        let container = target.find(".dropdown-menu").empty();
        Object.values(data).forEach((item) =>
            container.append(`<li data-code="${item.code}">${item.name_with_type}</li>`)
        );
    }
    //========================== attach event for form ===============================
    $("#bill .btn").on("click", () => $("form > button").trigger("click"));
    //begin validate
    $("#form-confirm input").on("click", () => {
        attachValidateForFrom();
        validateLocal();
    });
    //begin validate for dropdown
    $("#form-confirm .local").on("mouseout", function () {
        validateLocal();
        $(this).find(".error").parent().hide();
    });
    $("#form-confirm .local").on("mouseover", function () {
        $(this).find(".error").parent().show();
    });
    //submit form confirm order
    $("#form-confirm").on("submit", function (e) {
        if (!validateLocal() || orderObj.isEmpty()) {
            e.preventDefault();
            return;
        }
        let address = $("#CustomerAddress").val();
        let provinceID = $("#CustomerProvince").val();
        let districtID = $("#district").val();
        $("#CustomerProvince").val($(`div[local-province] .dropdown-menu li[data-code="${provinceID}"]`).text());
        $("#CustomerAddress").val(
            address + "," + $(`div[local-district] .dropdown-menu li[data-code="${districtID}"]`).text()
        );
        $(this).append(`<input name='items' type='hidden' value='${JSON.stringify(orderObj.getData().items)}'>`);
    });
    //submit form order
    $("#form").on("submit", function (e) {
        if (orderObj.isEmpty()) {
            e.preventDefault();
            return;
        }
        $(this).append(`<input name='items' type='hidden' value='${JSON.stringify(orderObj.getData().items)}'>`);
        orderObj.clear();
    });
    //========================== exec ===============================
    reqProvince();
    dropDown = new UIDropDown(function (idx) {
        reqDistrict(idx);
    });
    dropDown.attach();
    //========================== destroy ===============================
    $("#product-container").on("unload", function () {
        dropDown.detach();
    });
});
