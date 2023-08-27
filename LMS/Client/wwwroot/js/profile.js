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

