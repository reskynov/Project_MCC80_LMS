// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification

$(document).ready(function () {
    $(".editable-toggle").click(function () {
        if (this.value == "Enable") {
            this.value = "Disable";
            $("#edit-profile :input").prop("disabled", false);
            $(".editable-toggle").css("display", "none");
            $("#cancel-save-container :button").css("display", "inline-block");
        }
    });

    $(".cancel-toggle").click(function () {
        $(".editable-toggle").val('Enable');
        $("#edit-profile :input").prop("disabled", true);
        $(".editable-toggle").css("display", "inline-block");
        $("#cancel-save-container :button").css("display", "none");
    });
});

function SaveProfileConfirmation() {
    Swal.fire({
        title: 'Update Profile?',
        text: "Are you sure you want to change profile information?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById("edit-profile").submit(); // Submit the form
        }
    });
}

function ChangePasswordConfirmation() {
    Swal.fire({
        title: 'Update Profile?',
        text: "You need to login again if you want to change profile",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById("profile-change-password").submit(); // Submit the form
        }
    });
}

