$(function () {
    //Navigatte
    visibleElement($("#menu"), $(".nav-mobile .btn-classic"), $(".nav-mobile"));
    $(".nav-mobile .nav-item").on("click", function () {
        $(".nav-mobile .btn-classic").trigger("click");
    });
    //Search
    visibleElement($("#search"), $(".searchbar .btn-classic"), $(".searchbar"));
    $("#search-sm").on("click", () => {
        $(".nav-mobile .btn-classic").click();
        $("#search").click();
    });
    $(".search-input input").on("keydown", function (e) {
        if (e.keyCode == 13) $(this).parents("form").trigger("submit");
        e.preventDefault();
    });
});
