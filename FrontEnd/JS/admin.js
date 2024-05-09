$(document).ready(function () {
    $.validator.unobtrusive.parse($("#formUpdateUser"));
    $.validator.unobtrusive.parse($("#formMovieAdd"));
})

function getFormUpdateUser() {

    if ($("#formUpdateUser").valid()) {

        var formData = $("#formUpdateUser").serialize();

        $.ajax({
            type: "POST",
            url: "/MyAdminDashBoard/SubmitFormUpdateUser",
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

function getFormMovieAdd() {
    if ($("#formMovieAdd").valid()) {

        var image = document.getElementById('movieImageInput').value;

        var formData = new FormData($("#formMovieAdd")[0]);

        $.ajax({
            type: "POST",
            url: "/MyAdminDashBoard/SubmitFormMovieAdd",
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                console.log(response);
            },
            error: function (request, status, error) {
                console.log(request.responseText);
            }
        });
    }
}