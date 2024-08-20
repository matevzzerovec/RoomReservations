$(function () {
    InitCreateRoom();
});

function InitCreateRoom() {
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

    // Register event for numbers/money only field
    $("#Price").ForceNumericOnly();

    // Prevent form resubmittion - redirect to empty Create view
    document.addEventListener("keydown", function (event) {
        if (event.key === "F5" || (event.ctrlKey && event.key === "r")) {
            event.preventDefault();

            window.location.href = '/RoomView/Create';
        }
    });
}
