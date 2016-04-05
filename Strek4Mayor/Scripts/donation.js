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