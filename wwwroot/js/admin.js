function onClickItem(callBack) {
    $(".table-data tbody [item-nav]").on("click", function () {
        if (callBack) callBack($(this).parent().attr("item-id"));
    });
}

function onChangePropItem(callBack) {
    $(".table-data tbody [item-act]").on("change", function () {
        if (callBack) callBack($(this).parents("tr").attr("item-id"), $(this).prop("checked"));
    });
}

/*=======?exec======= */
onClickItem((id) => console.log("Nav item," + id));
onChangePropItem((id, val) => console.log("Change item," + id + " - " + val));
UIDropDown();
onSelectedItemDropdown((id) => console.log(id));
