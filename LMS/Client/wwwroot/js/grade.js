// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowUserTask(guid) {
    $.ajax({
        url: "https://localhost:7026/api/user-tasks/student-to-grade?guidUserTask=" + guid,
        type: "GET",
        dataType: "json",
        headers: {
            'Authorization': 'Bearer ' + Token
        }
    }).done((result) => {
        console.log(result)
        $("#guidLessonUserTask").val(result.data.userTaskGuid);
        /*        $("#submittedTaskUserTask").val(result.data.attachment);*/
                $("#submittedTaskUserTask").val(result.data.studentName);
        $("#gradeusertask").val(result.data.grade);
    }).fail((error) => {
        alert("Failed to fetch data. Please try again.");
        console.log(error)
    });
}

    function UpdateGrade() {
        let data = {
            userTaskGuid: $("#guidLessonUserTask").val(),
            grade: $("#gradeusertask").val(),
        };
        $.ajax({
            url: "https://localhost:7026/api/user-tasks/grade-task",
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(data),
            headers: {
                'Authorization': 'Bearer ' + Token
            }
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
                title: 'Failed to update data!',
                text: 'Grade value must be between 0-100. Please try again.'
            })
            console.log(error)
        })
    }