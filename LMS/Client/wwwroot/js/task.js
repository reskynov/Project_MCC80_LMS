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
        buttons: ['colvis',
            //{ extend: 'copy', exportOptions: { columns: ':visible' } },
            //{ extend: 'csv', exportOptions: { columns: ':visible' } },
            //{ extend: 'excel', exportOptions: { columns: ':visible' } },
            { extend: 'pdf', exportOptions: { columns: ':visible' } },
            { extend: 'print', exportOptions: { columns: ':visible' } }
        ]
    });
});


$(document).ready(function () {
    $('#teacherTaskTable').DataTable({
        "paging": false,
        "searching": false,
        "info": false,
        "order": [],
        "columnDefs": [
            { "orderable": false, "targets": 0 } // Disable sorting for the first column
        ]
    });

    // Show/hide child rows
    $('#teacherTaskTable tbody').on('click', 'tr.parent-row', function () {
        var currentRow = $(this).closest('tr');
        var nextRow = currentRow.next('tr.child-row');
        nextRow.toggle();
    });
});
