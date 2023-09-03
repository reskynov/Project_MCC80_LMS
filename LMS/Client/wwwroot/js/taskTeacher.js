// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    // Tambahkan event click pada setiap baris parent
    $('.parent-row').click(function () {
        // Toggle (sembunyikan/tampilkan) baris child yang sesuai
        $(this).next('.child-row').slideToggle();
    });
});



//$(document).ready(function () {
//    let table = $('#teacherTaskTable');

//    table.DataTable({
//    });

//    table.on('click', 'td.details-control', function () {
//        var tr = $(this).closest('tr');
//        var row = table.DataTable().row(tr);

//        if (row.child.isShown()) {
//            // This row is already open - close it
//            row.child.hide();
//            tr.removeClass('shown');
//        } else {
//            // Open this row
//            row.child(formatChildRow(row.data())).show();
//            tr.addClass('shown');
//        }
//    })
//})

//function formatChildRow(data) {
//    // 'data' adalah data dari baris utama yang diklik
//    var childTable = '<table class="table" id="childRowTeacher">';
//    childTable += '<thead><tr><th>Lesson</th><th>Deadline Date</th></tr></thead>';
//    childTable += '<tbody>';

//    // Isi tabel anak dengan data yang sesuai
//    for (var i = 0; i < data[2].length; i++) {
//        childTable += '<tr>';
//        childTable += '<td style="max-width:100px; text-overflow: ellipsis; overflow: hidden; white-space: nowrap;">' + data[2][i].LessonName + '</td>';
//        childTable += '<td>' + data[2][i].DeadlineDate + '</td>';
//        childTable += '</tr>';
//    }

//    childTable += '</tbody></table>';

//    return childTable;
//}

/*let table = new DataTable('#teacherTaskTable');*/

function format(value) {
    return '<div>Hidden Value: ' + value + '</div>';
}

$(document).ready(function () {
    //let dataSet = $('#teacherTaskTable td').map(function () {
    //    return $(this).text();
    //}).get();

    //var table = $('#teacherTaskTable').DataTable({
    //    columns: [
    //        { title: 'Name' },
    //        { title: 'Position' },
    //        { title: 'Office' },
    //        { title: 'Extn.' },
    //        { title: 'Start date' },
    //        { title: 'Salary' }
    //    ],
    //    data: dataSet
    //});

    //var table = $('#example').DataTable({});

    //// Add event listener for opening and closing details
    //$('#example').on('click', 'td.details-control', function () {
    //    var tr = $(this).closest('tr');
    //    var row = table.row(tr);

    //    if (row.child.isShown()) {
    //        // This row is already open - close it
    //        row.child.hide();
    //        tr.removeClass('shown');
    //    } else {
    //        // Open this row
    //        row.child(format(tr.data('.child-value'))).show();
    //        tr.addClass('shown');
    //    }
    //});
});