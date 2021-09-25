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

    $breadcrumb - divider: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='8' height='8'%3E%3Cpath d='M2.5 0L1 1.5 3.5 4 1 6.5 2.5 8l4-4-4-4z' fill='currentColor'/%3E%3C/svg%3E");


});