$(function () {
    $('#employ-check').change(function () {
        if ($(this).is(':checked')) {
            $(".employ-data").hide();
        }
        else {
            $(".employ-data").show();
        }
    });
});

$(function () {
    $('#5').click(function () {
        $('#amount-field').val("5.00");
        $('#amount-field').valid();
    });
});

$(function () {
    $('#10').click(function () {
        $('#amount-field').val("10.00");
        $('#amount-field').valid();
    });
});

$(function () {
    $('#20').click(function () {
        $('#amount-field').val("20.00");
        $('#amount-field').valid();
    });
});

$(function () {
    $('#50').click(function () {
        $('#amount-field').val("50.00");
        $('#amount-field').valid();
    });
});

$(function () {
    $('#100').click(function () {
        $('#amount-field').val("100.00");
        $('#amount-field').valid();
    });
});