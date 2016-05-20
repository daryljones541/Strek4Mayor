$().ready(function () {
    scrollbar = 0;
    $.ajax({
        type: "GET",
        url: "/Events/GetList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            numEvents = data.length;
            for (var i = 0; i < numEvents; i++) {
                var objDate = new Date(parseInt(data[i]["Date"].replace('/Date(', '')));
                data[i]["Date"] = objDate;
                calendar(data);
            }
        }
    });
});

$(window).resize(function () {
    var width = $('#events td').width();
    $('#events td').height(width);
});

function daysInMonth(calendarDay) {
    var dayCount = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    var thisYear = calendarDay.getFullYear();
    var thisMonth = calendarDay.getMonth();
    if (thisYear % 4 == 0) {
        if ((thisYear % 100 != 0 || (thisYear % 400 == 0))) {
            dayCount[1] = 29;
        }
    }
    return dayCount[thisMonth];
}

function renderCalender(monthYear) {
    var calendarDate = monthYear;
    var calendarDay = 0;
    var calendarMonth = calendarDate.getMonth();
    var calendarYear = calendarDate.getFullYear();
    var monthName = ["January", "February", "March", "April", "May", "June",
		"July", "August", "September", "October", "November", "December"];
    var calendar = '<table id="events"><thead><tr><th id="calendar-title" colspan="7"><img src="/Pictures/120px-Arrow_left.svg.png"  alt="Back one month" id="left-arrow" /> ' + monthName[calendarMonth] + ' ' + calendarYear + ' <img src="/Pictures/120px-Arrow_right.svg.png" alt="Forward one month" id="right-arrow" /></th></tr><tr id="weekdays"><th>Sun</th><th>Mon</th><th>Tue</th><th>Wed</th><th>Thu</th><th>Fri</th><th>Sat</th></tr></thead>';
    var dayOfMonth = new Date(calendarYear, calendarMonth, 1);
    var weekday = dayOfMonth.getDay();
    if (weekday > 0) {
        calendar += '<tbody><tr>';
        for (var i = 0; i < weekday ; i++) {
            calendar += '<td></td>';
        }
    }
    var days = daysInMonth(calendarDate);
    for (var i = 1; i <= days ; i++) {
        dayOfMonth.setDate(i);
        weekday = dayOfMonth.getDay();
        if (i == 1 && weekday == 0) calendar += '<tbody>';
        if (weekday == 0) calendar += '<tr>';
        var calendarBox= '<td class="calendar-box">' + i + '<br />';
        var eventTD = '';
        var eventID = '';
        for (var x = 0; x < events.length; x++) {
            calendarDate = new Date(events[x].Date);
            calendarYear = calendarDate.getFullYear();
            calendarMonth = calendarDate.getMonth();
            calendarDay = calendarDate.getDate();
            var eventDate = new Date(calendarYear, calendarMonth, calendarDay);
            if (dayOfMonth.getTime() === eventDate.getTime()) {
                var ampm = 'AM';
                var calendarHour = calendarDate.getHours();
                if (calendarHour > 12) {
                    calendarHour -= 12;
                    ampm = 'PM';
                }
                var calendarMinutes = calendarDate.getMinutes();
                if (calendarMinutes < 10) calendarMinutes = '0' + calendarMinutes;
                var eventTitle = events[x].Title;
                var time = calendarHour + ':' + calendarMinutes + ampm;
                var displayEvent = monthName[calendarMonth] + ' ' + calendarDay + ' at ' + time + '\n' + eventTitle + '\nLocation: ' + events[x].Location;
                eventLink = (time + ' ' + eventTitle).substring(0, 12);
                if (eventID != '') eventID += '<br />';
                eventID += displayEvent;
                if (eventTD != '') eventTD += '<br />';
                eventTD += eventLink;
            }
        }
        if (eventTD != '') {
            calendarBox = '<td class="calendar-box display-event" id="' + eventID + '">' + i + '<br /><img src="/Pictures/282px-Golden_star.svg.png" />';// + eventTD;
        }
        calendar += calendarBox + '</td>';
        if (weekday == 6) calendar += '</tr>';
    }
    if (weekday < 6) {
        for (var x = weekday; x<6; x++)
        {
            calendar += '<td></td>';
        }
    }
    calendar += '</tbody></table>';
    $('#calendar').html(calendar);
    var width = $('#events td').width() * .75;
    $('#events td').height(width);
    $(window).scrollTop(scrollbar);

    $('.calendar-box').hover(
        function () {
            $(this).css('border', '2px solid black');
        }, function () {
            $(this).css('border', '1px solid black');
        });

    $('.calendar-box').click(
        function () {
            $('#event-box').finish();
            if (typeof eventTimer != 'undefined') {
                clearTimeout(eventTimer);
            }
            $('#event-box').css('display', 'none');
        }
    );

    $('.display-event').mouseenter(
       function () {
           $('#event-box').finish();
           if (typeof eventTimer != 'undefined') {
               clearTimeout(eventTimer);
           }
           var eventText = $(this).attr('id');
           eventText = eventText.replace(/\n/g, "<br />");
           var pos = $(this).position();
           var positionLeft = pos.left;
           var positionTop = pos.top;
           var boxHeight = $(this).outerHeight();
           var boxWidth = $(this).outerWidth();
           $('#event-box').html(eventText);
           var eventWidth = $('#event-box').outerWidth();
           var eventHeight = $('#event-box').outerHeight();
           positionLeft += (boxWidth / 2) - (eventWidth / 2);
           positionTop += boxHeight / 2 - (eventHeight / 2);
           $('#event-box').css('top', positionTop);
           $('#event-box').css('left', positionLeft);
           $('#event-box').fadeIn();
       }
   ).mouseleave( 
        function () {
            $('#event-box').fadeOut(1200);
        }
    );

    $('#left-arrow').click(
        function () {
            $('#event-box').finish();
            if (typeof eventTimer != 'undefined') {
                clearTimeout(eventTimer);
            }
            $('#event-box').css('display', 'none');
            var month = monthYear.getMonth();
            var day = monthYear.getDate();
            var year = monthYear.getFullYear();
            month--;
            if (month < 0) {
                month = 11;
                year--;
            }
            monthYear = new Date(year, month, day);
            scrollbar = $(window).scrollTop();
            renderCalender(monthYear);
        }
    );

    $('#right-arrow').click(
        function () {
            $('#event-box').finish();
            if (typeof eventTimer != 'undefined') {
                clearTimeout(eventTimer);
            }
            $('#event-box').css('display', 'none');
            var month = monthYear.getMonth();
            var day = monthYear.getDate();
            var year = monthYear.getFullYear();
            month++;
            if (month > 11) {
                month = 0;
                year++;
            }
            monthYear = new Date(year, month, day);
            scrollbar = $(window).scrollTop();
            renderCalender(monthYear);
        }
    );
}

function calendar(eventList) {
    events = eventList;
    monthYear = new Date();
    renderCalender(monthYear);
}

