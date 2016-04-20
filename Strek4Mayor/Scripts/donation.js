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
    $('.dollar').mouseenter(function () {
        $(this).css('background-color', 'greenyellow');
    });
});

$(function () {
    $('.dollar').mouseleave(function () {
        $(this).css('background-color', 'transparent');
    });
});

$('#amount-field').keyup(function () {
    $('.dollar').removeClass('highlight');
})

$(function () {
    $('#5').click(function () {
        $('#amount-field').val("5.00");
        $('.dollar').removeClass('highlight');
        $('#5').addClass('highlight');
        $('#amount-field').valid();
    });
});

$(function () {
    $('#10').click(function () {
        $('#amount-field').val("10.00");
        $('.dollar').removeClass('highlight');
        $(this).addClass('highlight');
        $('#amount-field').valid();
    });
});

$(function () {
    $('#20').click(function () {
        $('#amount-field').val("20.00");
        $('.dollar').removeClass('highlight');
        $(this).addClass('highlight');
        $('#amount-field').valid();
    });
});

$(function () {
    $('#50').click(function () {
        $('#amount-field').val("50.00");
        $('.dollar').removeClass('highlight');
        $(this).addClass('highlight');
        $('#amount-field').valid();
    });
});

$(function () {
    $('#100').click(function () {
        $('#amount-field').val("100.00");
        $('.dollar').removeClass('highlight');
        $(this).addClass('highlight');
        $('#amount-field').valid();
    });
});