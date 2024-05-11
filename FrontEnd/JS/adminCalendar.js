import AirDatepicker from 'https://cdn.skypack.dev/air-datepicker'
import airDatepickerLocaleEn from 'https://cdn.skypack.dev/air-datepicker/locale/en'

var noCalendar = 0;
var datesDict = {};
var unavailableDatesDict = {};
var disabledDates = [];

const queryParams = new Proxy(new URLSearchParams(window.location.search), {
    get: (searchParams, prop) => searchParams.get(prop),
});

$.ajax({
    type: "POST",
    url: "CurrentAvailableDates",
    data: { theatreID: queryParams.theatreID },
    success: function (response) {
        if (response.status === "OK") {
            for (const [key, value] of Object.entries(response.myDictionary)) {
                for (const val of value) {
                    addToDictionary(key, val, unavailableDatesDict);
                }
            }
            updateDisabledDates(unavailableDatesDict);
            console.log(unavailableDatesDict);
        }
        else {
            console.log(response.Status);
        }
    },
    error: function (response) {
        alert("Error: " + response.Status + " |  " + response.Message);
    }
})

var btnAddMovie = document.getElementById("addMovie");

var btnAddNewRunDateTime = document.getElementById("addRunnigDateTimeBtn");
btnAddNewRunDateTime.addEventListener("click", () => {

    btnAddNewRunDateTime.setAttribute('disabled', '');
    btnAddNewRunDateTime.style.opacity = 0.5;

    updateDisabledDates(datesDict);

    var calendarDivElement = document.getElementById("calendars");
    var newCalendarDiv = document.createElement("div");
    calendarDivElement.appendChild(newCalendarDiv);

    noCalendar = noCalendar + 1;
    newCalendarDiv.id = "calendar" + noCalendar;

    const calendar = createNewCalendar("#calendar" + noCalendar);
    calendar.show();

    var btnConfirmDate = createNewButton("confirmDateBtn", "Confirm date");
    newCalendarDiv.appendChild(btnConfirmDate);

    btnConfirmDate.addEventListener('click', () => {
        if (calendar.hasSelectedDates) {
            var splitedDate = calendar.selectedDates[0]
                .toLocaleString()
                .split(',');
            var dateSelected = splitedDate[0];

            const radioButtons = document.querySelectorAll('input[name=option]');
            let selectedRadioBtn = "";

            radioButtons.forEach(radioButton => {
                if (radioButton.checked) {
                    selectedRadioBtn = radioButton.id;
                }
            });

            var hourSelected = selectedRadioBtn.split('_')[1] + ":00:00";

            addToDictionary(dateSelected, hourSelected, datesDict);

            calendar.destroy();
            newCalendarDiv.removeChild(newCalendarDiv.firstChild);

            newCalendarDiv.innerText = dateSelected + ' ' + hourSelected;

            btnAddNewRunDateTime.removeAttribute('disabled');
            btnAddNewRunDateTime.style.opacity = 1;

            if (btnAddMovie.hasAttribute('disabled')) {
                btnAddMovie.removeAttribute('disabled');
                btnAddMovie.style.opacity = 1;
            }
        }
    });
});

function createNewCalendar(calendarId) {

    var nextDay = new Date();
    nextDay.setDate(nextDay.getDate() + 1);
    nextDay.setHours(12);

    var maxDayToBeSelected = new Date();
    maxDayToBeSelected.setDate(maxDayToBeSelected.getDate() + 30);

    var calendar = new AirDatepicker(calendarId, {
        locale: airDatepickerLocaleEn,
        timepicker: true,
        dateFormat: 'dd/MM/yyyy',
        minHours: 12,
        maxHours: 21,
        minMinutes: 0,
        maxMinutes: 0,
        minDate: nextDay,
        maxDate: maxDayToBeSelected,
        toggleSelected: false,
        onSelect: ({ date }) => {
            var timeSection = document.getElementsByClassName('air-datepicker--time')[0];
            timeSection.textContent = '';

            const allHours = ["12:00:00", "15:00:00", "18:00:00", "21:00:00"];

            var dateToKey = date.toLocaleString().split(',')[0];
            var hoursUnavailable = getValuesForKey(dateToKey);
            console.log(hoursUnavailable);
            var availableHours = allHours.filter(item => !hoursUnavailable.includes(item));
            console.log(availableHours);
            if (availableHours.length != 0) {
                for (const hour of availableHours) {
                    var hourBtnId = "hour_" + hour.split(':')[0] + "_btn";
                    createNewRadioButton(hourBtnId, hour, timeSection);
                }
            }
        },
        onShow: ({ }) => {
            var timeSection = document.getElementsByClassName('air-datepicker--time')[0];
            timeSection.textContent = '';
        },
        onRenderCell: ({ date }) => {
            var dateToKey = date.toLocaleString().split(',')[0];
            if (disabledDates.includes(dateToKey)) {
                return {
                    disabled: true
                }
            }
        }
    });

    return calendar;
}

function createNewButton(buttonId, btnText) {
    const btn = document.createElement('button');

    btn.id = buttonId;
    btn.innerText = btnText;

    return btn;
}

function createNewRadioButton(buttonId, btnText, parent) {
    const radioBtn = document.createElement("input");
    radioBtn.setAttribute("type", "radio");
    radioBtn.setAttribute("name", "option");
    radioBtn.setAttribute("id", buttonId);

    const label = document.createElement("label");
    label.setAttribute("for", buttonId);
    label.textContent = btnText;

    parent.appendChild(radioBtn);
    parent.appendChild(label);
    parent.appendChild(document.createElement("br"));
}

function addToDictionary(key, value, dictionary) {

    if (dictionary.hasOwnProperty(key)) {
        dictionary[key].push(value);
    } else {
        dictionary[key] = [value];
    }
}

function getValuesForKey(dateKey) {
    var hoursUnavailable = [];

    for (const [key, value] of Object.entries(datesDict)) {
        if (key === dateKey) {
            for (const val of value) {
                hoursUnavailable.push(val);
            }
        }
    }

    for (const [key, value] of Object.entries(unavailableDatesDict)) {
        if (key === dateKey) {
            for (const val of value) {
                hoursUnavailable.push(val);
            }
        }
    }

    console.log(hoursUnavailable);

    return hoursUnavailable;

}

function updateDisabledDates(dictionary) {
    for (const [key, value] of Object.entries(dictionary)) {
        if (!disabledDates.includes(key) && value.length == 4) {
            disabledDates.push(key);
        }
    }
}

btnAddMovie.addEventListener("click", () => {
    console.log(datesDict);

    var dataToSend = formatDataToSend();
    console.log(dataToSend);
    console.log(JSON.stringify(dataToSend));

    debugger;

    $.ajax({
        type: "POST",
        url: "SubmitMovieAdd",
        data: { json: dataToSend },
        success: function (response) {
            window.location.replace("/" + response);
        },
        error: function (request, status, error) {
            console.log(request.responseText);
        }
    });
});

function formatDataToSend() {
    var jsonToSend = [];
    var seatsAvailable = getSeatsAvailable();

    for (const [key, value] of Object.entries(datesDict)) {
        var hoursRunning = getHoursRunning(value, seatsAvailable);

        jsonToSend.push({
            date: key,
            hoursRunning: hoursRunning
        });
    }

    return jsonToSend;
}

function getSeatsAvailable() {
    var seats = [];
    for (var seatId = 1; seatId <= 40; seatId++) {
        seats.push({
            seatId: seatId,
            available: true
        })
    }

    return seats;
}

function getHoursRunning(hours, seatsAvailable) {
    var hoursToSend = []

    for (const hour of hours) {
        hoursToSend.push({
            hour: hour,
            seats: seatsAvailable
        });
    }

    return hoursToSend;
}

//no date selected at first - done
//add button that can add a new calendar and a button that confirms the selection => after confirmed and saved enable the add calendar button again - done
//add disabled dates - done
//send data through a post to the controller - done
//and create there the json file and add - done
//read from c# through a post the dates that are already selected see here: https://stackoverflow.com/questions/55203792/how-to-pass-data-from-a-c-sharp-to-javascript-function - done