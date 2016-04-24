$().ready(function () {
    var loaded = 1;
    //measure();
    $(".tile-1").click(function () {
        alert("1");
        $("#view-box").load("/Home/Index");
    });
    $(".tile-2").click(function () {
        alert("2");
        $('#view-box').load("/Home/About");
    });
    $(".tile-3").click(function () {
        alert("3");
        $('.body-content').load("/Issues/Index");
    });
    $(".tile-4").click(function () {
        alert("4");
        $('.body-content').load("/Donations/Index");
    });
    $(".tile-5").click(function () {
        alert("5");
        $('.body-content').load("/Events/List");
    });
    $(".tile-6").click(function () {
        alert("6");
        $('.body-content').load("QandA/Index");
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