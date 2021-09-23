jQuery(document).ready(function ($) {

    $(".button-confirm").each(function () {
        $(this).attr("data-bs-toggle", "modal");
        $(this).attr("data-bs-target", "#staticBackdrop");
    });


    $(".clickable-row").click(function () {
        window.location = $(this).data("href");
    });

    $(".button-confirm").click(function () {
        $("#staticBackdropLabel").html($(this).data("title"));
        $("#modal-body-content").html($(this).data("body"));
        $("#modal-confirm-button").attr("href",$(this).data("href"));
    });




});