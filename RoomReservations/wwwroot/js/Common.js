let formSubmitting = false;

$(function () {
    Init();
});

function Init() {
    // data-feather icons init
    feather.replace();
}

document.addEventListener("DOMContentLoaded", function () {
    var buttons = document.querySelectorAll('button[type=submit]');

    // Enable all buttons once form is completely loaded
    buttons.forEach(button => {
        button.disabled = false;
    });
});

window.addEventListener("beforeunload", function () {
    var buttons = document.querySelectorAll('button[type=submit]');

    // Disable all buttons when form is posted
    buttons.forEach(button => {
        button.disabled = true;
    });
});

// https://stackoverflow.com/questions/995183/how-to-allow-only-numeric-0-9-in-html-inputbox-using-jquery?page=1&tab=scoredesc#tab-top
// Numeric only control handler
jQuery.fn.ForceNumericOnly =
    function () {
        return this.each(function () { // Iterate through all jQuery object this is called on
            $(this).keydown(function (e) {
                var key = e.charCode || e.keyCode || 0;

                // Allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
                // home, end, period, and numpad decimal
                return (
                    key == 8 ||
                    key == 9 ||
                    key == 13 ||
                    key == 46 ||
                    key == 110 ||
                    key == 188 ||
                    (key >= 35 && key <= 40) ||
                    (key >= 48 && key <= 57) ||
                    (key >= 96 && key <= 105));
            });
        });
    };