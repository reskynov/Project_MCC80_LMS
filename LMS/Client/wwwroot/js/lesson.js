
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function InsertLesson() {
    event.preventDefault();
    var lesson = new FormData(); // Gunakan FormData untuk mengirim berkas

    lesson.append("Name", $("#nameLesson").val());
    lesson.append("Description", $("#descriptionLesson").val());
    lesson.append("IsTask", $("#isTask").val());
    lesson.append("ClassroomGuid", $("#classroomGuidLesson").val());

    if ($("#isTask").val() == "true") {
        lesson.append("DeadlineDate", $("#deadlineDateLesson").val());
    }

    // Ambil file yang dipilih dalam input file
    var fileInput = document.getElementById("subjectAttachmentLesson");
    var file = fileInput.files[0]; // Ambil berkas pertama yang dipilih
    lesson.append("SubjectAttachmentFile", file);

    $.ajax({
        url: "/Dashboard/CreateLesson",
        type: "POST",
        processData: false, // Diperlukan agar FormData tidak diproses secara otomatis
        contentType: false, // Diperlukan agar FormData tidak diberikan tipe konten
        data: lesson, // Menggunakan FormData yang berisi data formulir dan berkas
        success: function (result) {
            Swal.fire({
                title: 'Success',
                text: 'Lesson added successfully',
                icon: 'success'
            })
            location.reload();
        },
        error: function (xhr, status, error) {
            Swal.fire({
                title: 'Oooppss!',
                text: 'Lesson failed added',
                icon: 'error'
            })
            console.log(error);
        }
    });
}


//function InsertLesson() {
//    var lesson = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
//    //ini ngambil value dari tiap inputan di form nya   
//    lesson.Name = $("#nameLesson").val();
//    lesson.Description = $("#descriptionLesson").val();
//    lesson.SubjectAttachment = $("#subjectAttachmentLesson").val();

//    if ($("#isTask").val() == "true") {
//        lesson.IsTask = true;
//        lesson.DeadlineDate = $("#deadlineDateLesson").val();
//    } else {
//        lesson.IsTask = false;
//        lesson.DeadlineDate = null;
//    }
//    lesson.ClassroomGuid = $("#classroomGuidLesson").val();
//    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
//    $.ajax({
//        url: "https://localhost:7026/api/lessons/task",
//        type: "POST",
//        //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
//        data: JSON.stringify(lesson),
//        contentType: "application/json",
//        dataType: "json"
//    }).done((result) => {
//        //buat alert pemberitahuan jika success
//        Swal.fire({
//            title: 'Success',
//            text: 'Lesson added successfully',
//            icon: 'success'
//        })
//        location.reload();

//    }).fail((error) => {
//        //alert pemberitahuan jika gagal
//        Swal.fire({
//            title: 'Oooppss!',
//            text: 'Lesson failed added',
//            icon: 'error'
//        })
//        console.log(error);
//    })
//}

//function DeleteLesson(guid) {
//    Swal.fire({
//        title: 'Are you sure?',
//        text: 'Changes cannot be reverted!',
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, Delete Data'
//    }).then(function (result) {
//        if (result.isConfirmed) {
//            $.ajax({
//                url: "https://localhost:7026/api/lessons?guid=" + guid,
//                type: "DELETE",
//            }).done(function (result) {
//                Swal.fire({
//                    title: 'Deleted',
//                    text: 'Data has been successfully deleted',
//                    icon: 'success'
//                }).then(function () {
//                    location.reload();
//                });
//            }).fail(function (error) {
//                alert("Failed to delete data. Please try again!");
//            });
//        }
//    });
//}

function RefreshCodeLesson(guid) {
    $.ajax({
        url: "https://localhost:7026/api/classrooms/new-code?guid=" + guid,
        type: "PUT",
    }).done(function () {
        console.log();
        location.reload();
    }).fail(function (error) {
        console.error("Error fetching new code:", error);
    });
}

function ShowUpdateLesson(guid) {
    $.ajax({
        url: "https://localhost:7026/api/lessons/task?guid=" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidUpdateLesson").val(result.data.lessonGuid);
        $("#nameUpdateLesson").val(result.data.name);
        $("#descriptionUpdateLesson").val(result.data.description);
        $("#subjectAttachmentUpdateLesson").val(result.data.subjectAttachment);
        //if (result.data.deadlineDate != null) {
        //    $("#deadlineDateContainer").css("display", "block");
        //    $("#deadlineDateUpdateLesson").val(result.data.deadlineDate);
        //}
        
        if (result.data.deadlineDate != null) {
            var success = document.getElementById("deadlineDateContainer");
            success.style.display = "block";
            checkDeadline = `<label for="deadlineDateUpdateLesson" class="form-label">Deadline</label>
                         <input type="datetime-local" class="form-control" id="deadlineDateUpdateLesson" name="deadlineDateUpdateLesson" value="${result.data.deadlineDate}" required>`;
            $("#deadlineDateContainer").html(checkDeadline);
            $("modalUpdateLesson").on("hidden.bs.modal", function () {
                $("#deadlineDateUpdateLesson").val(null);
                
            })

            var deadlineDate = document.getElementById("deadlineDateUpdateLesson");

            var now = new Date();
            var formattedNow = now.toISOString().substring(0, 16); // Format tanggal sesuai tipe datetime-local
            deadlineDate.setAttribute("min", formattedNow);
        } else {
            $("#deadlineDateUpdateLesson").val(null);
            var success = document.getElementById("deadlineDateContainer");
            success.style.display = "none";
        }
       

    }).fail((error) => {
        alert("Failed to fetch lesson data. Please try again.");
        console.log(error)
    });
}


function UpdateLesson(guid) {
    let data = {
        lessonGuid: $("#guidUpdateLesson").val(),
        name: $("#nameUpdateLesson").val(),
        description: $("#descriptionUpdateLesson").val(),
        subjectAttachment: $("#subjectAttachmentUpdateLesson").val(),
        classroomGuid: guid
    };
    if ($("#deadlineDateUpdateLesson").val() != null) {
        data.deadlineDate = $("#deadlineDateUpdateLesson").val()
    }
    $.ajax({
        url: "https://localhost:7026/api/lessons/task",
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(data)
    }).done((result) => {
        $("#deadlineDateContainer").css("display", "none");
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