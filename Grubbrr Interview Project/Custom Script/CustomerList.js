$(document).ready(function () {  

    var table = $('#customerListtable').DataTable({
        orderCellsTop: true,
        fixedHeader: true,
        order: [[1, 'asc']]
    });

});

function deleteCustomer(e, id) {
    $('.alert').show();
}