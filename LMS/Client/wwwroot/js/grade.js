// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//--------GRADE--------
$(document).ready(function () {
    GetGrade();
})
function GetGrade() {
    $(document).ready(function () {
        let table = new DataTable('#myTableGrade', {
            ajax: {
                url: "https://localhost:7026/api/grades",
                dataSrc: "data",
                dataType: "JSON"
            },

            columns: [
                {
                    data: null,
                    render: function (data, type, row, no) {
                        return no.row + 1;
                    }
                },
                { data: "value" },
                { data: "userguid" },
                { data: "taskguid" },
                {
                    data: '',
                    render: function (data, type, row) {
                        return `<button onclick="ShowUpdateClassroom('${row.url}')" data-bs-toggle="modal" data-bs-target="#modalUpdate" class="btn btn-warning"><i class="fas fa-edit"></i> </button>
                    <button onclick="DeleteClassroom('${row.guid}')" data-bs-toggle="modal" data-bs-target="" class="btn btn-danger"><i class="fas fa-trash"></i> </button>`;
                    }
                }
            ],
            dom: 'Blfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print',
                {
                    extend: 'colvis',
                    title: 'Colvis',
                    text: 'Column Visibility'
                }
            ]
        });
    });
}
function InsertGrade() {
    var grade = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    grade.Value = $("#valueGrade").val();
    grade.UserGuid = $("#userGuidGrade").val();
    grade.TaskGuid = $("#taskGuidGrade").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7026/api/grades",
        type: "POST",
        //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        data: JSON.stringify(grade),
        contentType: "application/json",
        dataType: "json"
    }).done((result) => {
        //buat alert pemberitahuan jika success
        Swal.fire({
            title: 'Success',
            text: 'Grade added successfully',
            icon: 'success'
        })
        location.reload();

    }).fail((error) => {
        //alert pemberitahuan jika gagal
        Swal.fire({
            title: 'Oooppss!',
            text: 'Grade failed added',
            icon: 'error'
        })
    })
}

function ShowUpdateGrade(guid) {
    $.ajax({
        url: "https://localhost:7026/api/grades" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guidClassroom").val(result.data.guid);
        $("#valueGrade").val(result.data.value);
        $("#userGuidGrade").val(result.data.userGuid);
        $("#taskGuidGrade").val(result.data.valueGuid);
    }).fail((error) => {
        alert("Failed to fetch employee data. Please try again.");
        console.log(error)
    });

}

function UpdateClassroom() {
    let data = {
        guid: $("#guidClassroom").val(),
        value: $("#valueGrade").val(),
        userGuid: $("#userGuidGrade").val(),
        taskGuid: $("#taskGuidGrade").val(),
    };
    $.ajax({
        url: "https://localhost:7026/api/grades",
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
            text: 'Failed to insert data! Please try again.'
        })
        console.log(error)
    })
}

function DeleteGrade(guid) {
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
                url: "https://localhost:7026/api/grades?guid=" + guid,
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


