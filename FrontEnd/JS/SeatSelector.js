const selectedSeatsList = [];
let seatNumber = localStorage.getItem("totalTickets");
let totalTickets = document.getElementById("totalTickets");
let reserverBtn = document.getElementById("seatPage");

totalTickets.innerHTML = "Total tickets: " + seatNumber;

var seatsElements = document.getElementsByClassName("seat");
for (const seat of seatsElements) {
    seat.addEventListener("click", () => {
        selectSeat(seat);
    });
}

function selectSeat(seat) {
    var seatIdAndAvailability = seat.id;
    var seatId = seatIdAndAvailability.split('_')[0];
    var seatAvailability = seatIdAndAvailability.split('_')[1];

    if (seatAvailability === "True" && seatNumber > 0) {
        selectedSeatsList.push(seatId);

        seatNumber--;
        totalTickets.innerHTML = "Total tickets: " + seatNumber;

        seatAvailability = "False";
        seat.id = seatId + "_" + seatAvailability;

        document.getElementById(seat.id).classList.add("bg-mustard-100");

        if (seatNumber == 0) {
            document.getElementById("reserveDiv").classList.remove("hidden");
        }
    }
    else if (seatAvailability === "False" && selectedSeatsList.includes(seatId)) {
        const index = selectedSeatsList.indexOf(seatId);
        if (index !== -1) {
            selectedSeatsList.splice(index, 1);
        }

        seatNumber++;
        totalTickets.innerHTML = "Total tickets: " + seatNumber;

        seatAvailability = "True";
        seat.id = seatId + "_" + seatAvailability;

        document.getElementById(seat.id).classList.remove("bg-mustard-100");
        document.getElementById("reserveDiv").classList.add("hidden");
    }
}


//need date, hour, selected seats, moveid, theatreid
reserverBtn.addEventListener("click", () => {
    var data = getSeatsAvailable();

    console.log(window.dateOfMovie);
    console.log(window.hourOfMovie);
    console.log(window.movieId);
    console.log(window.theatreId);

    debugger;

    $.ajax({
        type: "POST",
        url: "/SeatSelection/UpdateSeatsState",
        data: { date: window.dateOfMovie, hour: window.hourOfMovie, seats: data, movieId: window.movieId, theatreId: window.theatreId, seatsBooked: selectedSeatsList },
        success: function (response) {
            alert("Seat(s) reserved succesfully!");
            window.location.replace("/");
        },
        error: function (response) {
            console.log("Error: " + response.Status + " |  " + response.Message);
            window.location.replace("/Error");
        }
    });
});

function getSeatsAvailable() {
    var seats = [];
    var seatsElement = document.getElementsByClassName("seat");


    for (const seat of seatsElement) {
        seats.push({
            seatId: seat.id.split("_")[0],
            available: seat.id.split("_")[1] === "True" ? true : false
        })
    }

    return seats;
}