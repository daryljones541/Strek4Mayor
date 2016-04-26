﻿$().ready(function () {
    //measure();
    $(".tile-1").click(function () {
        $('.navlink').removeClass('highlighted');
        $('.tile-1').addClass('highlighted');
        $('#ajax-box').html('<img src="/Pictures/Loading_2_transparent.gif" style="width:10%;height:10%;margin-left:45%;" />');
        $.ajax({
            type: "GET",
            url: "/Home/AjaxIndex/",
            dataType: "html",
            success: function (data) {
                $('#ajax-box').html(data);
                document.title = 'Home';
            },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-2").click(function () {
        $('.navlink').removeClass('highlighted');
        $('.tile-2').addClass('highlighted');
        $('#ajax-box').html('<img src="/Pictures/Loading_2_transparent.gif" style="width:10%;height:10%;margin-left:45%;" />');
        $.ajax({
            type: "GET",
            url: "/Home/AjaxAbout/",
            dataType: "html",
            success: function (data) {
                $('#ajax-box').html(data);
                document.title = 'About';
            },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-3").click(function () {
        $('.navlink').removeClass('highlighted');
        $('.tile-3').addClass('highlighted');
        $('#ajax-box').html('<img src="/Pictures/Loading_2_transparent.gif" style="width:10%;height:10%;margin-left:45%;" />');
        $.ajax({
            type: "GET",
            url: "/Issues/AjaxIndex/",
            dataType: "html",
            success: function (data) {
                $('#ajax-box').html(data);
                document.title = 'Issues';
                
            },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-4").click(function () {
        $('.navlink').removeClass('highlighted');
        $('.tile-4').addClass('highlighted');
        $('#ajax-box').html('<img src="/Pictures/Loading_2_transparent.gif" style="width:10%;height:10%;margin-left:45%;" />');
        $.ajax({
            type: "GET",
            url: "/Donations/AjaxIndex/",
            dataType: "html",
            success: function (data) {
                $('#ajax-box').html(data);
                document.title = 'Donate';
            },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-5").click(function () {
        $('.navlink').removeClass('highlighted');
        $('.tile-5').addClass('highlighted');
        $('#ajax-box').html('<img src="/Pictures/Loading_2_transparent.gif" style="width:10%;height:10%;margin-left:45%;" />');
        $.ajax({
            type: "GET",
            url: "/Events/AjaxList/",
            dataType: "html",
            success: function (data) {
                $('#ajax-box').html(data);
                document.title = 'Calendar';
            },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-6").click(function () {
        $('.navlink').removeClass('highlighted');
        $('.tile-6').addClass('highlighted');
        $('#ajax-box').html('<img src="/Pictures/Loading_2_transparent.gif" style="width:10%;height:10%;margin-left:45%;" />');
        $.ajax({
            type: "GET",
            url: "/QandA/AjaxIndex/",
            dataType: "html",
            success: function (data) {
                $('#ajax-box').html(data);
                document.title = 'Q and A';
            },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
});

$(window).resize(function () {
    //measure();
});

function measure() {
    var panelHeight=$('#banner').outerHeight();
    var navbarHeight = $('#navbar').outerHeight();
    var totalHeight = (panelHeight + navbarHeight);
    $('body').css('paddingTop', totalHeight); 
}
