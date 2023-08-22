// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//--------USER--------
$(document).ready(function () {
    GetUser();
})
function GetUser() {
    $(document).ready(function () {
        let table = new DataTable('#myTable', {
            ajax: {
                url: "https://localhost:7026/api/users",
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
                {
                    data: 'fullName',
                    render: function (data, type, row) {
                        return row.firstName + ' ' + row.lastName;
                    }
                },
                {
                    data: 'gender',
                    render: function (data, type, row) {
                        return data == 0 ? "Female" : "Male";
                    }
                },
                {
                    data: "birthDate",
                    render: function (data) {
                        return moment(data).format('DD-MM-YYYY');
                    }
                },
                { data: "email" },
                { data: "phoneNumber" },
                {
                    data: '',
                    render: function (data, type, row) {
                        return `<button onclick="ShowUpdateUser('${row.url}')" data-bs-toggle="modal" data-bs-target="#modalUpdate" class="btn btn-warning"><i class="fas fa-edit"></i> </button>
                    <button onclick="DeleteUser('${row.guid}')" data-bs-toggle="modal" data-bs-target="" class="btn btn-danger"><i class="fas fa-trash"></i> </button>`;
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

function InsertUser() {
    var user = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    user.FirstName = $("#firstname").val();
    user.LastName = $("#lastname").val();    
    user.Gender = parseInt($("#gender").val());
    user.BirthDate = $("#birthdate").val();
    user.Email = $("#email").val();
    user.PhoneNumber = $("#phonenumber").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:7026/api/users",
        type: "POST",
        //jika terkena 415 unsupported media type (tambahkan headertype Json & JSON.Stringify();)
        data: JSON.stringify(user),
        contentType: "application/json",
        dataType: "json"
    }).done((result) => {
        //buat alert pemberitahuan jika success
        Swal.fire({
            title: 'Success',
            text: 'User added successfully',
            icon: 'success'
        })
            location.reload();
        
    }).fail((error) => {
        //alert pemberitahuan jika gagal
        Swal.fire({
            title: 'Oooppss!',
            text: 'User failed added',
            icon: 'error'
        })
    })
}

function ShowUpdateUser(guid) {
    $.ajax({
        url: "https://localhost:7026/api/users" + guid,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        console.log(result)
        $("#guiduser").val(result.data.guid);
        $("#firstnameuser").val(result.data.firstName);
        $("#lastnameuser").val(result.data.lastName);
        $("#genderuser").val(result.data.gender);
        $("#birthdateuser").val(result.data.birthDate);
        $("#emailuser").val(result.data.email);
        $("#phonenumberuser").val(result.data.phoneNumber);
    }).fail((error) => {
        alert("Failed to fetch employee data. Please try again.");
        console.log(error)
    });

}

function UpdateUser() {
    let data = {
        guid: $("#guiduser").val(),
        firstName: $("#firstnameuser").val(),
        lastName: $("#lastnameuser").val(),
        gender: $("#genderuser").val(),
        birthDate: $("#birthdateuser").val(),
        email: $("#emailuser").val(),
        phoneNumber: $("#phonenumberuser").val(),
    };
    $.ajax({
        url: "https://localhost:7026/api/users",
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

function DeleteUser(guid) {
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
                url: "https://localhost:7026/api/users?guid=" + guid,
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


