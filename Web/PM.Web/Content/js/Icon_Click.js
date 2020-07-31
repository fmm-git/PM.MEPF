
function ui_layout_toggler_click() {
    //gridList_scroll(3000);
    $(".ui-layout-center").css("overflow", "hidden");
    $(".ui-layout-toggler").click("bind", function () {
        //gridList_scroll(300);
    })
}

function gridList_scroll(minit) {
    setTimeout(function () {
        load_scroll();
    }, minit);
}
function load_scroll() {
    var width = $(".ui-layout-center").width();
    var height = $(".ui-layout-center").height();
    $("#gridList").setGridWidth(width);//jqgrid表格随窗口大小改变而改变
    var dom = $(".gridList");
    $.each(dom, function (i, item) {
        $(item).setGridWidth(width*0.98);
        $(item).setGridHeight(height * 0.65);
    });
}

