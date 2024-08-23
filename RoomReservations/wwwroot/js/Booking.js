$(function () {
    InitBooking();
});

function InitBooking() {
    // jQuery datepicker init and format
    $('#ArrivalDate').datepicker({
        format: 'dd.mm.yyyy',
        autoclose: true,
        todayHighlight: true
    });

    $('#DepartureDate').datepicker({
        format: 'dd.mm.yyyy',
        autoclose: true,
        todayHighlight: true 
    });

    // Dsiable the submit button before reCaptcha
    $("#btnBookRoom").prop("disabled", true);

    // Prevent form resubmittion - redirect to Index/Get
    document.addEventListener("keydown", function (event) {
        if (event.key === "F5" || (event.ctrlKey && event.key === "r")) {
            event.preventDefault();

            window.location.href = '/Booking/Index';
        }
    });
}

function RecaptchaCompleted() {
    $("#btnBookRoom").prop("disabled", false);
}