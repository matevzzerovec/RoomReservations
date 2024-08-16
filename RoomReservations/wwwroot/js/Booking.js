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
}