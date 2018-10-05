function onExcelExport(e) {
    var columns = e.workbook.sheets[0].columns;
    columns.forEach(function (column) {
        delete column.width;
        column.autoWidth = true;
    });
}