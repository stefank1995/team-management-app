$(document).ready(function () {
    $('#myTable').DataTable({
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true
    });
});

$(document).ready(function () {
    $('#filesTable').DataTable({
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true,
        "order": [[4, 'desc']]
    });
});