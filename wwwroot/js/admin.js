$(document).ready(function () {
    $.validator.unobtrusive.parse($("#formUpdateUser"));
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