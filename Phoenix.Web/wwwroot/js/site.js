// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function convertDate(inputDate) {
    function pad(s) { return (s < 10 ? '0' + s : s); }
    var d = new Date(inputDate);
    return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('.');
}

$(function () {
    $("ul.dropdown-menu [data-toggle='dropdown']").on("click", function (event) {
        event.preventDefault();
        event.stopPropagation();

        $(this).siblings().toggleClass("show");

        if (!$(this).next().hasClass('show')) {
            $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
        }
        $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
            $('.dropdown-submenu .show').removeClass("show");
        });
    });
});

$.fn.bsAlert = function (message) {
    this.html(
        `<div class="alert alert-danger alert-dismissible fade show" role="alert">
            <p>`+message+`</p>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div >`
    );
};