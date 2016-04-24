$().ready(function () {
    var loaded = 1;
    //measure();
    $(".tile-1").click(function () {
        $.ajax({
            type: "GET",
            url: "/Home/Index/",
            contentType: "application/json; charset=utf-8",
            success: function (data) { $('#view-box').html(data); },
            error: errorFunc
        });
        //$("#view-box").load("/Home/Index");
    });
    $(".tile-2").click(function () {
        $.ajax({
            type: "GET",
            url: "/Home/About/",
            contentType: "application/json; charset=utf-8",
            success: function (data) { $('#view-box').html(data); },
            error: errorFunc
        });
        //$('#view-box').load("/Home/About");
    });
    $(".tile-3").click(function () {
        $.ajax({
            type: "GET",
            url: "/Issues/Index/",
            contentType: "application/json; charset=utf-8",
            success: function (data) { $('#view-box').html(data); },
            error: errorFunc
        });
        //$('.body-content').load("/Issues/Index");
    });
    $(".tile-4").click(function () {
        $.ajax({
            type: "GET",
            url: "/Donations/Index/",
            contentType: "application/json; charset=utf-8",
            success: function (data) { $('#view-box').html(data); },
            error: errorFunc
        });
        //$('.body-content').load("/Donations/Index");
    });
    $(".tile-5").click(function () {
        $.ajax({
            type: "GET",
            url: "/Events/List/",
            contentType: "application/json; charset=utf-8",
            success: function (data) { $('#view-box').html(data); },
            error: errorFunc
        });
        //$('.body-content').load("/Events/List");
    });
    $(".tile-6").click(function () {
        $.ajax({
            type: "GET",
            url: "/QandA/Index/",
            contentType: "application/json; charset=utf-8",
            success: function (data) { $('#view-box').html(data); },
            error: errorFunc
        });
        //$('.body-content').load("QandA/Index");
    });
});

//$(window).resize(function () {
 //   measure();
//});

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
    $('.container').css('paddingTop', totalHeight); 
}

function errorFunc() {
    alert("Cannot communicate with server.");
}