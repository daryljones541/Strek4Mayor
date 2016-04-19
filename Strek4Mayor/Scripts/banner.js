$().ready(function () {
    measure();
});

$(window).resize(function () {
    measure();
});

function measure() {
    var donateWidth = $('#right-panel').width();
    $('#right-panel img').width(donateWidth);
    var panelHeight = $('#right-panel').outerHeight();
    $('#left-panel').height(panelHeight);
    $('#left-panel img').height(panelHeight);
    var navbarHeight = $('#navbar').outerHeight();
    var totalHeight = panelHeight + navbarHeight;
    $('#container').css('marginTop', totalHeight);
    
}