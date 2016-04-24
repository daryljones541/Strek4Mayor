$().ready(function () {
    measure();
    $(".tile-1").click(function () {
        $.ajax({
            type: "GET",
            url: "/Home/Index/",
            dataType: "html",
            success: function (data) { $('#view-box').html(data); },
            error: function (data) { $('#view-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-2").click(function () {
        $.ajax({
            type: "GET",
            url: "/Home/About/",
            dataType: "html",
            success: function (data) { $('#view-box').html(data); },
            error: function (data) { $('#view-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-3").click(function () {
        $.ajax({
            type: "GET",
            url: "/Issues/Index/",
            dataType: "html",
            success: function (data) { $('#view-box').html(data); },
            error: function (data) { $('#view-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-4").click(function () {
        $.ajax({
            type: "GET",
            url: "/Donations/Index/",
            dataType: "html",
            success: function (data) { $('#view-box').html(data); },
            error: function (data) { $('#view-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-5").click(function () {
        $.ajax({
            type: "GET",
            url: "/Events/List/",
            dataType: "html",
            success: function (data) { $('#view-box').html(data); },
            error: function (data) { $('#view-box').html("Unable to retrieve page from server."); }
        });
    });
    $(".tile-6").click(function () {
        $.ajax({
            type: "GET",
            url: "/QandA/Index/",
            dataType: "html",
            success: function (data) { $('#view-box').html(data); },
            error: function (data) { $('#view-box').html("Unable to retrieve page from server."); }
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
