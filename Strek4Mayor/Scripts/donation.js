$().ready(function () {
    selectAmount();
    if ($('#employ-check').is(':checked')) {
        $(".employ-data").hide();
    }
    else {
        $(".employ-data").show();
    }
});

function selectAmount() {
    var amount = $('#amount-field').val();
    var number = parseInt(amount);
    if (number == 100) { $('#100').addClass('highlight'); }
    else if (number == 50) { $('#50').addClass('highlight'); }
    else if (number == 20) { $('#20').addClass('highlight'); }
    else if (number == 10) { $('#10').addClass('highlight'); }
    else if (number == 5) { $('#5').addClass('highlight'); }
}

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
        $(this).css('background-color', '#F0E68C');
    });
});

$('#amount-field').keyup(function () {
    $('.dollar').removeClass('highlight');
    selectAmount();
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