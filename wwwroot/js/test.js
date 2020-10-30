$(function () {
    $.ajax({
        url: "/Admin/Promotion/AddForProduct",
        data: { name: "Helelo" },
        method: "POST",
        dataType: "JSON",
    })
        .done((response) => {
            console.log(response);
        })
        .fail((er) => {
            console.log(er);
        });
});
