import AirDatepicker from 'https://cdn.skypack.dev/air-datepicker'
import airDatepickerLocaleEn from 'https://cdn.skypack.dev/air-datepicker/locale/en'

var noCalendar = 0;
var datesDict = {};
var disabledDates = [];

var btnAddNewRunDateTime = document.getElementById("addRunnigDateTimeBtn");
btnAddNewRunDateTime.addEventListener("click", () => {
    updateDisabledDates();

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

            addToDictionary(dateSelected, hourSelected);

            console.log(calendar);

            calendar.destroy();
            newCalendarDiv.removeChild(newCalendarDiv.firstChild);

            newCalendarDiv.innerText = dateSelected + ' ' + hourSelected;


            console.log(datesDict);


            btnAddNewRunDateTime.removeAttribute('disabled');
            btnAddNewRunDateTime.style.opacity = 1;
        }
    });

    btnAddNewRunDateTime.setAttribute('disabled', '');
    btnAddNewRunDateTime.style.opacity = 0.5;
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
        onSelect: ({ date, formattedDate, datepicker }) => {
            var timeSection = document.getElementsByClassName('air-datepicker--time')[0];
            timeSection.textContent = '';

            const allHours = ["12:00:00", "15:00:00", "18:00:00", "21:00:00"];

            var dateToKey = date.toLocaleString().split(',')[0];
            var hoursUnavailable = getValuesForKey(dateToKey);

            var availableHours = getAvailableHours(allHours, hoursUnavailable);
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

function addToDictionary(key, value) {

    if (datesDict.hasOwnProperty(key)) {
        datesDict[key].push(value);
    } else {
        datesDict[key] = [value];
    }
}

function getValuesForKey(key) {
    if (key in datesDict) {
        return Array.isArray(datesDict[key]) ? datesDict[key] : [datesDict[key]];
    } else {
        return [];
    }
}

function getAvailableHours(arr1, arr2) {
    return arr1.filter(item => !arr2.includes(item));
}

function updateDisabledDates() {
    for (const [key, value] of Object.entries(datesDict)) {
        if (!disabledDates.includes(key) && value.length == 4) {
            disabledDates.push(key);
        }
    }
}

function removeHour(hour, slider) {
    if (slider.value == hour - 3) { slider.value = slider.value + 3 }
    else if (slider.value == hour + 3) { slider.value = slider.value - 3 }
}

//no date selected at first
//add button that can add a new calendar and a button that confirms the selection => after confirmed and saved enable the add calendar button again
//add disabled dates and read from c# through a post the dates that are already selected see here: https://stackoverflow.com/questions/55203792/how-to-pass-data-from-a-c-sharp-to-javascript-function