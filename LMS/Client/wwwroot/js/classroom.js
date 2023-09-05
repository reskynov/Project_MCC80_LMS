// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function InsertClassroom() {
    var classroom = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    classroom.Name = $("#nameClassroom").val();
    classroom.Description = $("#descriptionClassroom").val();
    /*classroom.Description = tinymce.get("descriptionClassroom").getContent();*/
    classroom.TeacherGuid = $("#teacherGuidClassroom").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7026/api/classrooms",
        type: "POST",
        //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        data: JSON.stringify(classroom),
        contentType: "application/json",
        dataType: "json",
        headers: {
            'Authorization': 'Bearer ' + Token
        }
    }).done((result) => {
        //buat alert pemberitahuan jika success
        Swal.fire({
            title: 'Success',
            text: 'Classroom added successfully',
            icon: 'success'
        })
        location.reload();

    }).fail((error) => {
        //alert pemberitahuan jika gagal
        Swal.fire({
            title: 'Oooppss!',
            text: 'Classroom failed added',
            icon: 'error'
        })
    })
}

function ShowUpdateClassroom(guid) {
    $.ajax({
        url: "https://localhost:7026/api/classrooms/" + guid,
        type: "GET",
        dataType: "json",
        headers: {
            'Authorization': 'Bearer ' + Token
        }
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

function UpdateClassroom() {
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
            title: 'Oops...',
            text: 'Failed to update data! Please try again.'
        })
        console.log(error)
    })
}

function DeleteClassroom(guid) {
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
                url: "https://localhost:7026/api/classrooms?guid=" + guid,
                type: "DELETE",
                headers: {
                    'Authorization': 'Bearer ' + Token
                }
            }).done(function (result) {
                Swal.fire({
                    title: 'Deleted',
                    text: 'Data has been successfully deleted',
                    icon: 'success'
                }).then(function () {
                    location.reload();
                });
            }).fail(function (error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Failed to delete data! Please try again.'
                })
            });
        }
    });
}