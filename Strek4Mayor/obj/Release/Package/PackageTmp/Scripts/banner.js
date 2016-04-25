$().ready(function () {
    measure();
    $(".tile-1").click(function () {
        $.ajax({
            type: "GET",
            url: "/Home/AjaxIndex/",
            dataType: "html",
            success: function (data) { $('#ajax-box').html(data); },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-2").click(function () {
        $.ajax({
            type: "GET",
            url: "/Home/AjaxAbout/",
            dataType: "html",
            success: function (data) { $('#ajax-box').html(data); },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-3").click(function () {
        $.ajax({
            type: "GET",
            url: "/Issues/AjaxIndex/",
            dataType: "html",
            success: function (data) { $('#ajax-box').html(data); },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-4").click(function () {
        $.ajax({
            type: "GET",
            url: "/Donations/AjaxIndex/",
            dataType: "html",
            success: function (data) { $('#ajax-box').html(data); },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-5").click(function () {
        $.ajax({
            type: "GET",
            url: "/Events/AjaxList/",
            dataType: "html",
            success: function (data) { $('#ajax-box').html(data); },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-6").click(function () {
        $.ajax({
            type: "GET",
            url: "/QandA/AjaxIndex/",
            dataType: "html",
            success: function (data) { $('#ajax-box').html(data); },
            error: function (data) { $('#ajax-box').html("Unable to retrieve page from server."); }
        });
    });
});

$(window).resize(function () {
    measure();
});

function measure() {
    //var windowWidth = $(window).width();
    //var donateWidth = $('#right-panel').width();
    //$('#right-panel img').width(donateWidth);
    //var rightWidth = $('#right-panel').outerWidth();
    //var leftWidth = .94 * (windowWidth - rightWidth);
    //var panelHeight = $('#right-panel').outerHeight();
    //$('#left-panel').width(leftWidth);
    //$('#left-panel').height(panelHeight);
    //$('#left-panel img').height(panelHeight);
    var panelHeight=$('#left-panel').outerHeight();
    var navbarHeight = $('#navbar').outerHeight();
    var totalHeight = (panelHeight + navbarHeight);
    $('body').css('paddingTop', totalHeight); 
}
