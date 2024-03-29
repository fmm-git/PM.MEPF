﻿
function ui_layout_toggler_click() {
    $(".ui-layout-center").css("overflow", "hidden");
    load_scroll();
}

function gridList_scroll(minit) {
    setTimeout(function () {
        load_scroll();
    }, minit);
}
function load_scroll() {
    var width = $("#ui-layout-center").width();
    var height = $("#ui-layout-center").height();
    $("#gridList").setGridWidth(width); 
    var dom = $(".gridList");
    $.each(dom, function (i, item) {
        $(item).setGridWidth(width);
        $(item).setGridHeight(height-130);
    });
    doResize_left();
    doResize_right();
    if (typeof (resize3DModelWindow) == 'function') {
        resize3DModelWindow();
    }
}
//左侧机构
function doResize_left() {
    var domleft = $(".gridListLeft");
    $.each(domleft, function (i, item) {
        $(item).setGridWidth($(".ui-layout-west").width());
        $(item).setGridHeight($(".ui-layout-west").height() - 40);
    });
}
//右侧统计图
function doResize_right() {
    $('.charbox').each(function (index, item) {
        $(item).highcharts().reflow();
    });
}
//打开时
function onopen_end() {
    $(".gridPanelReport").hide();
}
//关闭时
function onclose_end() {
    $(".gridPanelReport").show();
}
