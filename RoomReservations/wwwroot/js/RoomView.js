let formSubmitting = false;

$(function () {
    Init();    
});

function Init() {
    feather.replace();
}


document.addEventListener("DOMContentLoaded", function () {
    var buttons = document.querySelectorAll('button[type=submit]');

    // Enable all buttons once form is completly loaded
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