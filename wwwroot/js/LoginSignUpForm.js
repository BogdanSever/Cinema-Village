$(document).ready(function () {
    $.validator.unobtrusive.parse($("#formSignUp"));
    $.validator.unobtrusive.parse($("#formLogin"));
})

function getFormSignUpData() {

    if ($("#formSignUp").valid()) {

        var formData = $("#formSignLogin").serialize();

        $.ajax({
            type: "POST",
            url: "/Home/SubmitFormSignUp",
            data: formData,
            success: function (response) {
                console.log(response);
            },
            error: function (request, status, error) {
                console.log(request.responseText);
            }
        });
    }
};

function getFormLogInData() {

    if ($("#formLogin").valid()) {

        var formData = $("#formLogin").serialize();

        $.ajax({
            type: "POST",
            url: "/Home/SubmitFormLogIn",
            data: formData,
            success: function (response) {
                console.log(response);
            },
            error: function (request, status, error) {
                console.log(request.responseText);
            }
        });
    }
};