$(function () {
    $.ajax({
        url: "/Admin/Order/Detail?id=12",
        method: "GET",
        dataType: "JSON",
    })
        .done((response) => {
            console.log(response);
        })
        .fail((er) => {
            console.log(er);
        });
});
