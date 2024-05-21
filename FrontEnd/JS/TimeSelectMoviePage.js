var btnToCheckOutElements = document.getElementsByClassName("btnToCheckOutMovie");
for (const btnToCheckOut of btnToCheckOutElements) {
    btnToCheckOut.addEventListener("click", () => {

        var date = btnToCheckOut.id.split("_")[2];
        var hour = btnToCheckOut.innerText + ":00";
        var movieId = btnToCheckOut.id.split("_")[1];
        var theatreId = document.getElementById("theatreID_" + date).innerText.split(" ").pop();

        console.log(btnToCheckOut)

        $.ajax({
            type: "GET",
            url: "/CheckOut",
            data: { date: date, hour: hour, movieID: movieId, theatreID: theatreId },
            success: function (response) {
                window.location.replace("/CheckOut?date=" + date + "&hour=" + hour + "&movieID=" + movieId + "&theatreID=" + theatreId);
            },
            error: function (response) {
                console.log("Error: " + response.Status + " |  " + response.Message + " " + response.url);
            }
        });
    })
}