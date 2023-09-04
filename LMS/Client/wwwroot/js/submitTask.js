// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//document.getElementById("listCardLesson").addEventListener("click", function{
//    window.location.href = "Dashboard/LessonDetail?lessonGuid=@item.LessonGuid";
//})

function deleteTaskConfirmation() {
    Swal.fire({
        title: 'Are you sure?',
        text: "This File will be deleted!",
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


//$(document).ready(function () {
//    $("#buttonSubmitTask").click(function () {
//        // Memeriksa apakah input memiliki nilai
//        var attachmentValue = $("#uploadAttachmentFile").val();
//        console.log(attachmentValue)
//        if (attachmentValue === "") {
//            Swal.fire({
//                icon: 'error',
//                title: 'Oops...',
//                text: 'Attachment field is required.'
//            })
//        }
//    });
//});

//    $("#editButtonAttachment").click(function () {
        
//        var success = document.getElementById('buttonEditTask');
//        success.style.display = 'block';
//        var inputAttacment = document.getElementById('attacmentEditTask');
//        inputAttacment.readOnly = false;

//        $("#buttonEditTask").click(function () {
//            // Memeriksa apakah input memiliki nilai
//            var attachmentValue = $("#attacmentEditTask").val();
//            if (attachmentValue.trim() !== "") {
//                // Jika input memiliki nilai, maka jalankan submitTask()
//                EditSubmitTask();
//            } else {
//                Swal.fire({
//                    icon: 'error',
//                    title: 'Oops...',
//                    text: 'Attachment field is required.'
//                })
//            }
//        })
//    })
//})


//function submitTask() {
//    var obj = {
//        attachment: $("#attacmentSubmitTask").val(),
//        userGuid: $("#userGuidSubmitTask").val(),
//        lessonGuid: $("#lessonGuidSubmitTask").val()
//    }

//    $.ajax({
//        url: "https://localhost:7026/api/user-tasks/submit-task",
//        type: "POST",
//        data: JSON.stringify(obj), // Konversi object menjadi JSON string
//        contentType: "application/json; charset=utf-8", // Set content type
//    }).done((result) => {
//        console.log(result);
//        Swal.fire({
//            icon: 'success',
//            title: result.message,
//            timer: 1500
//        }).then(() => {
//            location.reload();
//        });
//    }).fail((error) => {
//        console.log(error);
//        Swal.fire({
//            icon: 'error',
//            title: 'Oops...',
//            text: error.responseJSON.message
//        })
//    })
//}

//function EditSubmitTask() {
//    var obj = {
//        attachment: $("#attacmentEditTask").val(),
//        userGuid: $("#userGuidEditTask").val(),
//        lessonGuid: $("#lessonGuidEditTask").val()
//    }

//    $.ajax({
//        url: "https://localhost:7026/api/user-tasks/submit-task",
//        type: "PUT",
//        contentType: "application/json",
//        data: JSON.stringify(obj)
//    }).done((result) => {
//        Swal.fire({
//            icon: 'success',
//            title: result.message,
//            timer: 1500
//        }).then(() => {
//            location.reload();
//        });
//    }).fail((error) => {
//        Swal.fire({
//            icon: 'error',
//            title: 'Oops...',
//            text: 'Failed to update data! Please try again.'
//        })
//        console.log(error)
//    })
//}

////function GetSubmitTask() {
////    var userGuidSubmitTask = document.getElementById('userGuidSubmitTask').value;
////    var lessonGuidSubmitTask = document.getElementById('lessonGuidSubmitTask').value;

////    $.ajax({
////        url: `https://localhost:7026/api/user-tasks/submit-task?UserGuid=${userGuidSubmitTask}&LessonGuid=2bcc90f5-a6b7-4eaa-7ca5-08dba45b6cce${lessonGuidSubmitTask}`,
////        type: "GET",
////        dataType: "json"
////    }).done((result) => {
////        $("#userTaskGuid").val(result.data.guid);
////        $("#attacmentSubmitTask").html(result.data.attachment);
////        $("#gradeTask").html(result.data.grade);
////        $("#userGuid").val(result.data.userGuid);
////        $("#taskGuid").val(result.data.taskGuid);

////    }).fail((error) => {
////        Swal.fire({
////            icon: 'error',
////            title: 'Oops...',
////            text: error.responseJSON.message
////        });
////    })

////}