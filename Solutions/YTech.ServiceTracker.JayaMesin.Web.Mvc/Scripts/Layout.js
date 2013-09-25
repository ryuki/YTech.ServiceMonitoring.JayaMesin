var Layout = {}
Layout.__namespace = true;

Layout._height = null;

Layout.init = function () {
    /// <summary>Initialize the layout page.</summary>

    $($("#mainSplitter").children("div.k-pane")[0]).css("overflow-x", "hidden");
    setInterval(function () { Layout.resize(); }, 500);
}

Layout.resize = function () {
    /// <summary>Resize the splitter to fit the window.</summary>

    var height = $(window).height();
    if (height != Layout._height) {
        Layout._height = height;
        var headSplitter = $("#headSplitter");
        headSplitter.height(height - 10);
        headSplitter.resize();

        var mainSplitter = $("#mainSplitter");
        mainSplitter.height(height - 95);
        mainSplitter.resize();

        var contentSplitter = $("#contentSplitter");
        contentSplitter.height(height - 120);
        contentSplitter.resize();
    }
}