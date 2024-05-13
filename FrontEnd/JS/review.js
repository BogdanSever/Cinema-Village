const submitReviewBtn = document.getElementById("submitReviewBtn");
const reviewElement = document.getElementById("reviewInput");

const ratingStars = [...document.getElementsByClassName("rating__star")];
const starClassActive = "rating__star fas fa-star";
const starClassInactive = "rating__star far fa-star";

function executeRating(stars) {
    const starsLength = stars.length;
    let i;
    
    stars.map((star) => {
        star.onclick = () => {
            i = stars.indexOf(star);

            if (star.className === starClassInactive) {
                for (i; i >= 0; --i) stars[i].className = starClassActive;
            } else {
                for (i; i < starsLength; ++i) stars[i].className = starClassInactive;
            }

            var noOfStars = document.getElementsByClassName(starClassActive).length;
            if (noOfStars !== 0) {
                starsError = document.getElementById("starsError");
                starsError.innerText = "";
            }
        };
    });
}

executeRating(ratingStars);

submitReviewBtn.addEventListener("click", () => {
    var reviewInput = document.getElementById("reviewInput").value;
    var noOfStars = document.getElementsByClassName(starClassActive).length;

    if (reviewInput !== "" && noOfStars !== 0) {

        var urlParams = window.location.pathname.split("/");
        var movieId = urlParams[urlParams.length - 1];
        console.log(movieId);

        $.ajax({
            type: "POST",
            url: "/Review/SubmitReview",
            data: { review: reviewInput, noOfStars: noOfStars, movieId: movieId },
            success: function (response) {
                if (response === "OK") {
                    window.location.replace("/");
                }
            },
            error: function (response) {
                console.log(response);
            }
        });
    }
    else {
        if (reviewInput === "") {
            spanReviewError = document.getElementById("reviewError");
            spanReviewError.innerText = "Please write a review..";
        }

        if (noOfStars == 0) {
            starsError = document.getElementById("starsError");
            starsError.innerText = "Please select a number of stars!";
        }
    }
});

reviewElement.addEventListener("change", () => {
    spanReviewError = document.getElementById("reviewError");
    spanReviewError.innerText = "";
});