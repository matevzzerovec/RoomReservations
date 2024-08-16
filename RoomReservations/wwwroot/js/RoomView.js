﻿let formSubmitting = false;

$(function () {
    Init();
});

function Init() {
    // data-feather icons init
    feather.replace();

    // jQuery datepicker init and format
    $('#BookingVm_ArrivalDate').datepicker({
        format: 'dd.mm.yyyy',
        autoclose: true,
        todayHighlight: true
    });

    $('#BookingVm_DepartureDate').datepicker({
        format: 'dd.mm.yyyy',
        autoclose: true,
        todayHighlight: true 
    });
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