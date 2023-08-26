// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$(document).ready(function () {
//    $("#enrollNow").click(function () {
//        Enroll();

//        $("#enrollModal").modal("hide");
//    });
//});

$(document).ready(function () {
    $("#enrollCheck").click(function () {
        GetEnroll(classroomCode);
    })
   
    $("#enrollNow").click(function () {
        Enroll();
        $("#enrollModal").modal("hide");
    });

    $("#enrollModal").on("hidden.bs.modal", function () {
        // Hapus data yang diperoleh dari GetEnroll
        // Misalnya, mengosongkan elemen-elemen HTML atau variabel-variabel yang menyimpan data tersebut
        $("#classroomGuidEnroll").val("");
        $("#classroomNameEnroll").html("");
        $("#teacherNameEnroll").html("");
        $("#peopleCountEnroll").val("");

        var success = document.getElementById('getEnrollView');
        success.style.display = 'none'; // Menyembunyikan elemen
    });
});



function Enroll() {
    var obj = {
        userGuid: $("#userGuid").val(),
        classroomCode: $("#classroomCode").val()
    };
    
    console.log(obj);

    $.ajax({
        url: "https://localhost:7026/api/classrooms/enroll",
        type: "POST",
        data: JSON.stringify(obj), // Konversi object menjadi JSON string
        contentType: "application/json; charset=utf-8", // Set content type
    }).done((result) => {
        console.log(result);
        Swal.fire({
            icon: 'success',
            title: result.message,
            showConfirmButton: false,
            timer: 1500
        }).then(() => {
            location.reload();
        });

    }).fail((error) => {
        console.log(error);
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: error.responseJSON.message
        })    
    })
}

function GetEnroll(enrollCode) {
    var enrollCode = document.getElementById('classroomCode').value;
    $.ajax({
        url: `https://localhost:7026/api/classrooms/enroll?classCode=${enrollCode}`,
        type: "GET",
        dataType: "json"
    }).done((result) => {
        $("#classroomGuidEnroll").val(result.data.classroomGuid);
        $("#classroomNameEnroll").html(result.data.classroomName);
        $("#teacherNameEnroll").html(result.data.teacherName);
        $("#peopleCountEnroll").val(result.datapeopleCount);

        //var success = document.getElementById('getEnrollView');
        //success.style.display = 'block';

        var success = document.getElementById('getEnrollView');
        success.style.display = 'block';
        success.classList.add('slide-top-card'); // Menambahkan class animasi

    }).fail((error) => {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: error.responseJSON.message
        });
    })
}

//async function Unenroll() {
//    const result = await Swal.fire({
//        title: 'Are you sure?',
//        text: "You won't be able to revert this!",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Yes, delete it!'
//    });

//    if (result.isConfirmed) {
//        const form = document.getElementById("UnenrollForm");
//        await Swal.fire({
//            icon: 'success',
//            title: 'You have successfully unenrolled from the class.',
//            showConfirmButton: false,
//            timer: 1500
//        });

//        // Menunggu pesan Swal selesai ditampilkan, kemudian submit formulir
//        setTimeout(() => {
//            form.submit();
//        }, 1000); // Tunggu selama 1,5 detik sebelum pengalihan halaman
//    }
//}


function Unenroll(className) {
    Swal.fire({
        title: 'Are you sure you want to unenroll from the ' + className +"?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            document.getElementById("UnenrollForm").submit(); // Submit the form
        }
    })
}
