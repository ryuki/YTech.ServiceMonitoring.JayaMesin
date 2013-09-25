/// <reference path="_Layout.js" />

var Navigation = {}
Navigation.__namespace = true;

Navigation.displayForum = function (e) {
    /// <summary>Display the list of forum posts for the selected forum.</summary>

    Index.displayItem();
    var rssItemGrid = $('#rssItemGrid').data('kendoGrid');
    rssItemGrid.dataSource.transport.options.read.url = '/Home/GetItems/' + e.id;
    rssItemGrid.dataSource.read();
}