// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowUserTask(guid) {
    $.ajax({
        url: "https://localhost:7026/api/user-tasks/to-grade?guidLesson=" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidLessonUserTask").val(result.data.lessonGuid);
        $("#submittedTaskUserTask").val(result.data.submittedTask);
        $("#gradeUserTask").val(result.data.grade);
    }).fail((error) => {
        alert("Failed to fetch data. Please try again.");
        console.log(error)
    });
}

    function UpdateGrade() {
        let data = {
            lessonGuid: $("#guidLessonUserTask").val(),
            submittedTask: $("#submittedTaskUserTask").val(),
            grade: $("#gradeUserTask").val(),
        };
        $.ajax({
            url: "https://localhost:7026/api/user-tasks/grade-task",
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(data)
        }).done((result) => {
            Swal.fire(
                'Data has been successfully updated!',
                'success'
            ).then(() => {
                location.reload();
            });
        }).fail((error) => {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Failed to update data! Please try again.'
            })
            console.log(error)
        })
    }