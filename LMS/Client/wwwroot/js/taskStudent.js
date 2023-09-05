// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//document.getElementById("listCardLesson").addEventListener("click", function{
//    window.location.href = "Dashboard/LessonDetail?lessonGuid=@item.LessonGuid";
//})
$(document).ready(function () {
    $('#studentTaskTable').DataTable({
        dom: 'Bfrtip',
        lengthMenu: [
            [10, 25, 50, -1],
            ['10 rows', '25 rows', '50 rows', 'Show all']
        ],
        buttons: [
            {
                extend: 'pdf',
                text: 'Download',
                style: 'background-color: #5e72e4; color:#fff',
                className: 'btn btn-primary',
                exportOptions: { columns: ':visible' }
            }
        ]
    });

    var dataTableButtons = $('.dt-button');
    dataTableButtons.removeClass('dt-button');
});



