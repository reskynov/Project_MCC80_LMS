
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//--------LESSON--------
//$(document).ready(function () {
//    let table = new DataTable('#myTableLesson', {
//        ajax: {
//            url: "https://localhost:7026/api/lessons",
//            dataSrc: "data",
//            dataType: "JSON"
//        },

//        columns: [
//            {
//                data: null,
//                render: function (data, type, row, no) {
//                    return no.row + 1;
//                }
//            },
//            { data: "name" },
//            { data: "description" },
//            { data: "subjectAttachment" },
//            { data: "LessonGuid" },
//            {
//                data: '',
//                render: function (data, type, row) {
//                    return `<button onclick="ShowUpdateLesson('${row.url}')" data-bs-toggle="modal" data-bs-target="#modalUpdate" class="btn btn-warning"><i class="fas fa-edit"></i> </button>
//                    <button onclick="DeleteLesson('${row.guid}')" data-bs-toggle="modal" data-bs-target="" class="btn btn-danger"><i class="fas fa-trash"></i> </button>`;
//                }
//            }
//        ],
//        dom: 'Blfrtip',
//        buttons: [
//            'copy', 'csv', 'excel', 'pdf', 'print',
//            {
//                extend: 'colvis',
//                title: 'Colvis',
//                text: 'Column Visibility'
//            }
//        ]
//    });
//});

//$.ajax({
//    url: "https://localhost:7026/api/Lessons"
//}).done(function (result) {
//    // Assuming the API response contains a property named "totalLesson"
//    let getLesson = ""
//    $.each(result.data, (key, val) => {
//        console.log(result)
//        getLesson += ` <option value="${val.guid}">${val.guid}</option>`
//    })
//    $('.selectLesson').html(getLesson)
//}).fail(function () {
//    $(".selectLesson").text("Failed to fetch data");
//});


function InsertLesson() {
    var lesson = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya   
    lesson.Name = $("#nameLesson").val();
    lesson.Description = $("#descriptionLesson").val();
    lesson.SubjectAttachment = $("#subjectAttachmentLesson").val();
    
    if ($("#isTask").val() == "true") {
        lesson.IsTask = true;
        lesson.DeadlineDate = $("#deadlineDateLesson").val();
    } else {
        lesson.IsTask = false;
        lesson.DeadlineDate = null;
    }
    lesson.ClassroomGuid = $("#classroomGuidLesson").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7026/api/lessons/task",
        type: "POST",
        //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        data: JSON.stringify(lesson),
        contentType: "application/json",
        dataType: "json"
    }).done((result) => {
        //buat alert pemberitahuan jika success
        Swal.fire({
            title: 'Success',
            text: 'Lesson added successfully',
            icon: 'success'
        })
        location.reload();

    }).fail((error) => {
        //alert pemberitahuan jika gagal
        Swal.fire({
            title: 'Oooppss!',
            text: 'Lesson failed added',
            icon: 'error'
        })
        console.log(error);
    })
}

function DeleteLesson(guid) {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Changes cannot be reverted!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Delete Data'
    }).then(function (result) {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:7026/api/lessons?guid=" + guid,
                type: "DELETE",
            }).done(function (result) {
                Swal.fire({
                    title: 'Deleted',
                    text: 'Data has been successfully deleted',
                    icon: 'success'
                }).then(function () {
                    location.reload();
                });
            }).fail(function (error) {
                alert("Failed to delete data. Please try again!");
            });
        }
    });
}

//function RefreshCodeLesson(guid) {
//    $.ajax({
//        url: "https://localhost:7026/api/classrooms/new-code?guid=" + guid,
//        success: function (data) {
//            var newCode = data;
//            document.getElementById("codeContainer").innerHTML = newCode;
//            window.location.reload();
//        },
//        error: function (xhr, status, error) {
//            console.error("Error fetching new code:", error);
//        }
//    });
//}

//function ShowCodeLesson(guid) {
//    $.ajax({
//        url: "https://localhost:7026/api/classrooms/" + guid,
//        type: "GET",
//        dataType: "json"
//    }).done((result) => {
//        console.log(result)
//        $("#guidClassroomCode").val(result.data.guid);
//    }).fail((error) => {
//        alert("Failed to fetch classroom data. Please try again.");
//        console.log(error)
//    });
//}

function ShowUpdateLesson(guid) {
    $.ajax({
        url: "https://localhost:7026/api/classrooms/" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpdateClassroom").val(result.data.guid);
        $("#codeUpdateClassroom").val(result.data.code);
        $("#nameUpdateClassroom").val(result.data.name);
        $("#descriptionUpdateClassroom").val(result.data.description);
        $("#teacherGuidUpdateClassroom").val(result.data.teacherGuid);
        //const teacherGuid = result.data.teacherGuid;
        //const code = result.data.code;
        //UpdateClassroom(result.data)
    }).fail((error) => {
        alert("Failed to fetch classroom data. Please try again.");
        console.log(error)
    });
}

function UpdateLesson() {
    let data = {
        guid: $("#guidUpdateClassroom").val(),
        code: $("#codeUpdateClassroom").val(),
        name: $("#nameUpdateClassroom").val(),
        description: $("#descriptionUpdateClassroom").val(),
        teacherGuid: $("#teacherGuidUpdateClassroom").val(),
    };
    $.ajax({
        url: "https://localhost:7026/api/classrooms",
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
function RefreshCodeLesson(guid) {
    $.ajax({
        url: "https://localhost:7026/api/classrooms/new-code?guid=" + guid,
        type: "PUT",
    }).done(function () {
        //var newCode = data;
        //document.getElementById("codeContainer").innerHTML = newCode;
        /*.then(function () {*/
        console.log();
        location.reload();

        //});
    }).fail(function (error) {
        console.error("Error fetching new code:", error);
    });
}

//$(document).ready(function () {
//    $("#refreshCode").click(function () {
//        RefreshCodeLesson(guid);
//    })
//})