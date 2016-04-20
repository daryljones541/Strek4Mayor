$().ready(function () {
    measure();
});

$(window).resize(function () {
    measure();
});

function measure() {
    var windowWidth = $(window).width();
    var donateWidth = $('#right-panel').width();
    $('#right-panel img').width(donateWidth);
    var rightWidth = $('#right-panel').outerWidth();
    var leftWidth = .94 * (windowWidth - rightWidth);
    var panelHeight = $('#right-panel').outerHeight();
    $('#left-panel').width(leftWidth);
    $('#left-panel').height(panelHeight);
    $('#left-panel img').height(panelHeight);
    var navbarHeight = $('#navbar').outerHeight();
    var totalHeight = panelHeight + navbarHeight;
    $('#container').css('marginTop', totalHeight);
    
}