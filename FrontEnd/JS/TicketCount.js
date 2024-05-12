const studentTicketPrice = 24;
const seniorTicketPrice = 28;
const adultTicketPrice = 30;
const childrenTicketPrice = 30;

const noOfTicketsAvailableToBuy = document.getElementById("noOfSeatsAvailable").innerText;

let studentIncrementButton = document.getElementById("studentIncrementTicket");
let studentDecrementButton = document.getElementById("studentDecrementTicket");
let incrementSeniorButton = document.getElementById("incrementSeniorTicket");
let decrementSeniorButton = document.getElementById("decrementSeniorTicket");
let incrementAdultButton = document.getElementById("incrementAdultTicket");
let decrementAdultButton = document.getElementById("decrementAdultTicket");
let incrementChildButton = document.getElementById("incrementChildTicket");
let decrementChildButton = document.getElementById("decrementChildTicket");
let nextPage = document.getElementById("seatPage");
let ticketAmount = 0;

function setAmountAndLeave() {
    var totalPrice = getTotalPrice();

    localStorage.setItem("totalTickets", ticketAmount);
    localStorage.setItem("totalPrice", totalPrice);
}

function incrementTicket(type) {
    let element = document.getElementById(type);
    if (ticketAmount < noOfTicketsAvailableToBuy) {
        element.innerHTML++
        ticketAmount++;
    }
    console.log(ticketAmount);
}

function decrementTicket(type) {
    let element = document.getElementById(type);
    if (element.innerHTML > 0) {
        element.innerHTML--;
        ticketAmount--;
        console.log(ticketAmount);
    }
}

studentIncrementButton.addEventListener("click", function () {
    incrementTicket("studentTicketAmount");
});

studentDecrementButton.addEventListener("click", function () {
    decrementTicket("studentTicketAmount");
});

incrementAdultButton.addEventListener("click", function () {
    incrementTicket("adultTicketAmount");
});

decrementAdultButton.addEventListener("click", function () {
    decrementTicket("adultTicketAmount");
});

incrementSeniorButton.addEventListener("click", function () {
    incrementTicket("seniorTicketAmount");
});

decrementSeniorButton.addEventListener("click", function () {
    decrementTicket("seniorTicketAmount");
});

incrementChildButton.addEventListener("click", function () {
    incrementTicket("childTicketAmount");
});

decrementChildButton.addEventListener("click", function () {
    decrementTicket("childTicketAmount");
});

nextPage.addEventListener("click", function () {
    setAmountAndLeave();
});

function getTotalPrice() {
    const noOfStudentTickets = document.getElementById("studentTicketAmount").innerText;
    const noOfAdultTickets = document.getElementById("adultTicketAmount").innerText;
    const noOfSeniorTickets = document.getElementById("seniorTicketAmount").innerText;
    const noOfChildTickets = document.getElementById("childTicketAmount").innerText;

    return noOfStudentTickets * studentTicketPrice +
        noOfAdultTickets * adultTicketPrice +
        noOfSeniorTickets * seniorTicketPrice +
        noOfChildTickets * childrenTicketPrice;
}