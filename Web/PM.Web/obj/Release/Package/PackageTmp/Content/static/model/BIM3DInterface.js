/*
 * @Descripttion:
 * @version:
 * @Author: zp
 * @Date: 2019-10-22 09:22:12
 * @LastEditors: zp
 * @LastEditTime: 2020-07-02 17:45:04
 */
// import "./BIM3DViewer.js"; // es6标准用法
// import * as BIM from "./BIM3DViewer";    react里的用法
/**
 * @description:
 * @param {type} dom
 * @param {number} BMcubeSize  罗盘大小，默认35
 * @param {array} cubeImageUrl 罗盘图片路，数组，例["../../img/home.png", "../../img/up.png", "../../img/down.png"]；(不传时则不创建罗盘)
 * @param {object} options json对象。相关系统配置，参数见下面。
 * @param {number} options.near 近平面值，当该值不传时默认为0.01
 * @param {number} options.far 远平面值，当该值不传时默认为100000
 * @param {string} option.TopColor 背景顶部颜色，当该属性不传时默认为"#123456";
 * @param {string} option.BottomColor 背景顶部颜色，当该属性不传时默认为"#eeeeee";
 * @return:
 */
var BIM3DInterface = function (dom, BMcubeSize, cubeImageUrl, options) {
  this.viewer = new BIM.BimViewer(
    dom,
    BMcubeSize,
    cubeImageUrl,
    options
  );
  this.domElement = this.viewer.container;
};

BIM3DInterface.prototype = {
  /**
   * 销毁BIMViewer。
   */
  dispose: function () {
    this.viewer.dispose();
  },
  /**
   * 获取BIMViewer的dom对象。用来添加进浏览器展示。
   */
  getViewerDomElement: function () {
    return this.viewer.container;
  },
  /**
   * 重置窗口大小，调用getViewerDomElement()方法添加进对象里后调用此方法，浏览器窗口大小改变时应调用此方法。
   * @param {number} height 窗口高度，当为undefined时使用100%;
   * @param {number} width 窗口宽度，当为undefined时使用100%;
   */
  resize: function (height, width) {
    this.viewer.onWindowResize(height, width);
  },
  /**
   * 加载模型文件。
   * @param {*} filenames 文件的url地址。支持数组对象，例如["url1","url2"]。url需要是b3dz格式的文件路径
   * @param {*} isHide 是否隐藏，true隐藏加载的模型，false不隐藏。
   * @param {*} requestHeader {name:"",value:token};
   */
  appendFile: function (filenames, isHide, requestHeader) {
    this.viewer.appendFile(filenames, isHide, requestHeader);
  },
  /**
   * 移除模型。
   * @param {*} filenames 需要移除场景里模型的url地址。支持数组对象，例如["url1","url2"]。
   */
  removeFile: function (filenames) {
    this.viewer.removeFile(filenames);
  },
  /**
   * 清空场景。
   * @param {*} callback
   */
  clearScene: function () {
    this.viewer.removeScene();
  },
  /**
   * 添加模型加载完毕回调。当调用appendFile接口加载的模型文件加载完毕则调用改回调。。
   * @param {*} callback 文件加载成功后调用的回调方法，第一个参数为当前加载的文件url，第二个参数整数值，0失败，1成功。
   */
  addAppendFileCallBack: function (callback) {
    this.viewer.AddAppendFileCallBack(callback);
  },
  /**
   * 添加选中模型回调方法。
   * @param {*} callback 选中模型调用的方法。该方法需要定义一个函数参数。返回为数组对象，
   * 可用该数组对象调用接口来控制颜色，透明度等模型操作。可以使用该数组对象单个元素的name属性，该属性为字符串。为模型的名字id。
   */
  addSelectObjectCallBack: function (callback) {
    this.viewer.AddSelectObjectCallBack(callback);
  },
  /**
   * 设置场景裁剪。
   * @param {number} value 裁剪值，系统默认为0。
   */
  setOptionCullingThreshold: function (value) {
    this.viewer.setOptionCullingThreshold(value);
  },
  /**
   * 设置模型高亮。
   * @param {*} id string类型 需要高亮的模型id,多个则传数组。
   * @param {*} whetherAdd 是否增加到当前高亮对象。true则将传入id追加到当前高亮对象，false则取消当前场景高亮的对象，高亮当前传入对象。
   */
  setModelSelection: function (id, whetherAdd) {
    this.viewer.setModelSelection(id, whetherAdd);
  },
  /**
   * 设置模型显示隐藏。
   * @param {*} id 需要显示隐藏的模型id。
   * @param {*} visible 是否可见，true显示，false隐藏。
   * @param {*} option 操作类型，传入0表示对传入的对象进行操作。传入1表示对传入的对象之外的对象进行操作。
   */
  setModelVisible: function (id, visible, option) {
    this.viewer.setModelVisible(id, visible, option);
  },
  /**
   * 设置标准视图。
   * @param {*} type 视图的类型。0-主视图，1-前视图，2-后视图，3-左视图，4-右视图，5-顶视图，6-底视图。
   */
  setStandardView: function (type) {
    this.viewer.setStandardView(type);
  },
  /**
   * 根据RGB值设置模型颜色。
   * @param {*} ids 需要修改颜色的模型id。支持传入数组。
   * @param {*} red 红色值，取值范围0-255。
   * @param {*} green 绿色值，取值范围0-255。
   * @param {*} blue 蓝色值，取值范围0-255。
   * @param {*} option 操作类型，传入0表示对传入的对象进行操作。传入1表示对传入的对象之外的对象进行操作。
   */
  setModelColorByRGB: function (ids, red, green, blue, option) {
    this.viewer.setModelColorByRGB(ids, red, green, blue, option);
  },
  /**
   * 根据十六进制值设置模型颜色。
   * @param {*} ids 需要修改颜色的模型id。支持传入数组。
   * @param {*} hexColor 十六进制颜色值，例如红色0xff0000。
   * @param {*} option 操作类型，传入0表示对传入的对象进行操作。传入1表示对传入的对象之外的对象进行操作。
   */
  setModelColorByHex: function (ids, hexColor, option) {
    this.viewer.setModelColorByHex(ids, hexColor, option);
  },
  /**
   * 恢复模型颜色。
   * @param {*} ids 需要恢复颜色的模型id。支持传入数组。
   * @param {*} option 操作类型，传入0表示对传入的对象进行操作。传入1表示对传入的对象之外的对象进行操作。
   */
  restoreModelColor: function (ids, option) {
    this.viewer.restoreModelColor(ids, option);
  },
  /**
   * 设置模型透明度。
   * @param {*} id 需要设置透明度的模型id。
   * @param {*} alpha 透明度值。取值范围0到1。0-不透明，1透明。
   * @param {*} option 操作类型，传入0表示对传入的对象进行操作。传入1表示对传入的对象之外的对象进行操作。
   */
  setModelAlpha: function (id, alpha, option) {
    this.viewer.setModelAlpha(id, alpha, option);
  },
  /**
   * 恢复模型透明度。
   * @param {*} id 需要恢复透明度的模型id。
   */
  restoreModelAlpha: function (id) {
    this.viewer.restoreModelAlpha(id);
  },
  /**
   * 根据id创建文字标签。
   * @param {*} ids 需要创建文字标签的id。支持传入数组。
   * @param {*} height 文字线的高度。
   * @param {*} isWorldHeight 是否是世界坐标高度。
   * @param {*} text 文字标签内容。
   * @returns 返回数组对象，每个元素为html的a元素对象。可用来设置自定义风格以及添加事件。切记在调用dispose接口之前将返回的数组清空，否则回造成内存泄露。
   */
  setModelTextLabel: function (ids, height, isWorldHeight, text) {
    return this.viewer.setModelTextLabel(ids, height, isWorldHeight, text);
  },
  /**
   * 移除文本标签。
   * @param {Array?} ids 需要移除文字标签关联的id数组，当不传值时清除所有标签。
   */
  removeModelTextLabelByIDs: function (ids) {
    this.viewer.removeModelTextLabelByIDs(ids);
  },
  /**
   * 移除文本标签。
   * @param {Array?} divs 该参数由setModelTextLabel接口返回，当不传值时清除所有标签。
   */
  removeModelTextLabelByDivs: function (divs) {
    this.viewer.removeModelTextLabelByDivs(divs);
  },
  /**
   * 根据id创建图像标签。
   * @param {*} ids 需要创建图像标签的id。支持传入数组。
   * @param {*} image_url 图像url地址。
   * @param {*} image_width 图像标签宽度。
   * @param {*} image_height 图像标签高度。
   * @returns 返回数组对象，每个元素为html的img元素对象。可用来设置自定义风格以及添加事件。切记在调用dispose接口之前将返回的数组清空，否则回造成内存泄露。
   */
  setModelImageLabel: function (ids, image_url, image_width, image_height) {
    return this.viewer.setModelImageLabel(
      ids,
      image_url,
      image_width,
      image_height
    );
  },
  /**
   * 移除图像标签。
   * @param {Array?} ids 需要移除图像标签关联的id数组，当不传值时清除所有标签。
   */
  removeModelImageLabelByIDs: function (ids) {
    this.viewer.removeModelImageLabelByIDs(ids);
  },
  /**
   * 移除图像标签。
   * @param {Array?} divs 该参数由setModelImageLabel接口返回，当不传值时清除所有标签。
   */
  removeModelImageLabelByDivs: function (divs) {
    this.viewer.removeModelImageLabelByDivs(divs);
  },
  /**
   * 根据id创建任意html标签。
   * @param {*} ids 需要创建图像标签的id。支持传入数组。
   * @param {*} htmlValues html的字符串。
   * @returns 返回数组对象，每个元素为html的根元素对象。可用来设置自定义风格以及添加事件。切记在调用dispose接口之前将返回的数组清空，否则回造成内存泄露。
   */
  setModelAnyLabel: function (ids, htmlValues) {
    return this.viewer.setModelAnyLabel(ids, htmlValues);
  },
  /**
   * 移除html标签。
   * @param {Array?} ids 需要移除html标签关联的id数组，当不传值时清除所有标签。
   */
  removeModelAnyLabelByIDs: function (ids) {
    this.viewer.removeModelAnyLabelByIDs(ids);
  },
  /**
   * 移除html标签。
   * @param {Array?} divs 该参数由setModelAnyLabel接口返回，当不传值时清除所有标签。
   */
  removeModelAnyLabelByDivs: function (divs) {
    this.viewer.removeModelAnyLabelByDivs(divs);
  },
  /**
   * 设置轮廓线显示隐藏。
   * @param {bool} visible true为显示，false为隐藏。默认为false。在场景过大时建议关闭该效果。
   */
  setOutlineVisible: function (visible) {
    this.viewer.setOutlineVisible(visible);
  },
  /**
   * 设置轮廓线颜色
   * @param {*} hexColor 十六进制颜色值，例如红色0xff0000。
   */
  setOutlineColor: function (hexColor) {
    this.viewer.setOutlineColor(hexColor);
  },
  /**
   * 设置轮廓线的宽度。
   * @param {number}} width 轮廓线的宽度。默认为1
   */
  setOutlineWidth:function(width){
    this.viewer.setOutlineWidth(width);
  },
  /**
   * 开始拾取顶点，传入回调，且回调带一个参数，该参数返回json对象，格式为{x:拾取点x的坐标,y:拾取点y的坐标,z:拾取点z的坐标}。
   * 该json对象可用来使用在setImageLabelFromCoordinate接口参数里。
   * @param {*} callback 鼠标左键确认点以后调用的方法。参数为坐标的json对象。
   */
  startPickupPoint: function (callback) {
    this.viewer.startPickupPoint(callback);
  },
  /**
   * 结束拾取顶点操作。
   */
  endPickupPoint: function () {
    this.viewer.endPickupPoint();
  },
  /**
   * 清除所有根据坐标点创建的图标。
   */
  clearPickupPointImageLabel: function () {
    this.viewer.clearPickupPoint();
  },
  /**
   * 根据坐标点创建图标。
   * @param {*} coordinate 坐标。startPickupPoint返回的json对象。
   * @param {*} image_url 图像url地址。
   * @param {*} image_width 图像宽度。
   * @param {*} image_height 图像高度。
   * @returns 返回图像标签对象，dom对象。可用来自定义点击事件。
   */
  setImageLabelFromCoordinate: function (
    coordinate,
    image_url,
    image_width,
    image_height
  ) {
    return this.viewer.setImageLabelFromCoordinate(
      coordinate,
      image_url,
      image_width,
      image_height
    );
  },
  /**
   * 移除单个拾取点创建的图标。
   * @param {*} imagelabel 图像标签对象，传入setImageLabelFromCoordinate返回的对象。
   */
  removePickupPointImageLabel: function (imagelabel) {
    this.viewer.removePickupPointImageLabel(imagelabel);
  },
  /**
     * 获取摄像机信息。
     * @returns 返回当前摄像机信息，格式为json对象。例如
     * {
            cmPosition:{x:0.0,y:0.0,z:0.0},cmTarget:{x:10.0,y:10.0,z:10.0},cmUp:{x:0.0,y:0.0,z:1.0}
        };
     */
  getCamera: function () {
    return this.viewer.getCamera();
  },
  /**
   * 设置摄像机信息。
   * @param {*} cameraInfo 摄像机信息的json格式对象。
   */
  setCamera: function (cameraInfo) {
    this.viewer.setCamera(cameraInfo);
  },
  /**
   * 添加模型id进选择过滤管理器。在过滤管理器里的模型不能被选中。
   * @param {*} ids 不被选中的对象id，支持传入数组。
   */
  addSelectFilter: function (ids) {
    this.viewer.addSelectFilter(ids);
  },
  /**
   * 移除过滤管理器里的模型。使得对应的模型可以被选中。
   * @param {*} ids 模型对象id，支持传入数组。
   */
  removeSelectFilter: function (ids) {
    this.viewer.removeSelectFilter(ids);
  },
  /**
   * 清空过滤管理器里的模型。使得模型都可以被选中。
   */
  clearSelectFiler: function () {
    this.viewer.clearSelectFiler();
  },
  /**
   *指定的模型是否在过滤管理器里。
   * @param {*} id 需要查询的模型id。
   * @returns 返回bool类型变量，true表示在过滤管理器里，false表示没有在过滤管理器里。
   */
  isInSelectFilter: function (id) {
    return this.viewer.isInSelectFilter(id);
  },
  /**
   * 设置控件背景。
   * @param {*} topColor 顶部颜色值。十六进制的字符串。例如"#FF0000"红色。
   * @param {*} bottomColor 底部颜色值。十六进制的字符串。例如"#00FF00"绿色。
   */
  setBackground: function (topColor, bottomColor) {
    this.viewer.setBackground(1, topColor, bottomColor);
  },
  /**
   * 设置模型隔离。
   * @param {*} ids 需要隔离对象的id，支持数组。
   * @param {*} bQuarantine 是否隔离对象，true隔离，false取消隔离。
   * @param {*} option 操作类型，传入0表示对传入的对象进行操作。传入1表示对传入的对象之外的对象进行操作。
   * 注：当ids传入""时，bQuarantine传入false，option传入1时取消所有隔离。
   */
  setModelQuarantine: function (ids, bQuarantine, option) {
    this.viewer.setModelQuarantine(ids, bQuarantine, option);
  },
  /**
   * 根据字符串矩阵设置模型矩阵。
   * @param {*} ids 需要修改模型矩阵的模型id数组。
   * @param {*} strMatrix 字符串矩阵。
   */
  setModelMatrixByString: function (ids, strMatrix) {
    this.viewer.setModelMatrixByString(ids, strMatrix);
  },
  /**
   * 获取视点。
   * @param {*} moveSpeed
   * @param {*} rotateSpeed
   * @returns 返回json对象。
   */
  getViewpoint: function () {
    return this.viewer.getViewpoint();
  },
  /**
   * 恢复视点。
   * @param {*} viewpoint 视点信息，json对象。
   * @param {*} needsUpdate 是否需要恢复后可更改视点信息（标注的修改等）
   */
  setViewpoint: function (viewpoint, needsUpdate) {
    this.viewer.setViewpoint(viewpoint, needsUpdate);
  },
  /**
   * 箭头标注(有弹窗)
   * @param {function} con 用于获取返回值的回调函数。如 const con = a => {console.log(a)} a为返回值
   *
   */
  arrowMarkWithPanel: function (con) {
    this.viewer.arrowMark(con);
  },
  /**
   * 箭头标注(无弹窗)。
   * @param {*} color 颜色 #ffffff(str)
   * @param {*} width 线宽 线的粗细（单位像素）
   */
  arrowMark: function (color, width) {
    this.viewer._arrowMark(color, width);
  },
  /**
   * 圆形标注(有弹窗)
   * @param {function} con 用于获取返回值的回调函数。如 const con = a => {console.log(a)} a为返回值
   *
   */
  circleMarkWithPanel: function (con) {
    this.viewer.circleMark(con);
  },
  /**
   * 圆形标注(无弹窗)
   * @param {*} color 颜色 #ffffff(str)
   * @param {*} width 线宽 线的粗细（单位像素）
   */
  circleMark: function (color, width) {
    this.viewer._circleMark(color, width);
  },
  /**
   * 椭圆形标注(有弹窗)
   * @param {function} con 用于获取返回值的回调函数。如 const con = a => {console.log(a)} a为返回值
   *
   */
  ellipseMarkWithPanel: function (con) {
    this.viewer.ellipseMark(con);
  },
  /**
   * 椭圆形标注(无弹窗)
   * @param {*} color 颜色 #ffffff(str)
   * @param {*} width 线宽 线的粗细（单位像素）
   */
  ellipseMark: function (color, width) {
    this.viewer._ellipseMark(color, width);
  },
  /**
   * 矩形标注(有弹窗)
   * @param {function} con 用于获取返回值的回调函数。如 const con = a => {console.log(a)} a为返回值
   *
   */
  rectMarkWithPanel: function (con) {
    this.viewer.rectMark(con);
  },
  /**
   * 矩形标注(无弹窗)
   * @param {*} color 颜色 #ffffff(str)
   * @param {*} width 线宽 线的粗细（单位像素）
   */
  rectMark: function (color, width) {
    this.viewer._rectMark(color, width);
  },
  /**
   * 简单箭头标注(有弹窗)
   * @param {function} con 用于获取返回值的回调函数。如 const con = a => {console.log(a)} a为返回值
   *
   */
  simpleArrowMarkWithPanel: function (con) {
    this.viewer.simpleArrowMark(con);
  },
  /**
   * 简单箭头标注(无弹窗)
   * @param {*} color 颜色 #ffffff(str)
   * @param {*} width 线宽 线的粗细（单位像素）
   */
  simpleArrowMark: function (color, width) {
    this.viewer._simpleArrowMark(color, width);
  },
  /**
   * @description: 正多边形标注(有弹窗)
   * @param {type} con 用于获取返回值的回调函数。如 const con = a => {console.log(a)} a为返回值
   * @return:
   */
  regularPolygonMarkWithPanel: function (con) {
    this.viewer.regularPolygonMark(con);
  },
  /**
   * @description: 正多边形标注(无弹窗)
   * @param {*} color 颜色 #ffffff(str)
   * @param {*} width 线宽 线的粗细（单位像素）
   * @param {*} segments 边数 线的粗细（单位像素）
   * @return:
   */
  regularPolygonMark: function (color, width, segments) {
    this.viewer._regularPolygonMark(color, width, segments);
  },
  /**
   * @description: 任意多边形标注(有弹窗)
   * @param {type} con 用于获取返回值的回调函数。如 const con = a => {console.log(a)} a为返回值
   * @return:
   */
  polygonMarkWithPanel: function (con) {
    this.viewer.polygonMark(con);
  },
  /**
   * @description: 任意多边形标注(无弹窗)
   * @param {*} color 颜色 #ffffff(str)
   * @param {*} width 线宽 线的粗细（单位像素）
   * @return:
   */
  polygonMark: function (color, width) {
    this.viewer._polygonMark(color, width);
  },
  /**
   * 文字标注
   * @param {*} text 文字内容 str
   * @param {*} color 文字颜色 例如：#ffffff(str)
   * @param {*} bgColor 背景颜色 例如：#ffffff(str)
   * @param {*} fontSize 字体大小 int
   * @param {*} lineWidth 标注宽度（行宽）int
   */
  textMark: function (text, color, bgColor, fontSize, lineWidth) {
    this.viewer.textMark(text, color, bgColor, fontSize, lineWidth);
  },
  /**
   * 退出标注
   */
  endMark: function () {
    this.viewer.endMark();
  },
  /**
   * 获取选中标注信息
   */
  getSelectMarkInfo: function () {
    return this.viewer.bimMark.getSelectMarkInfo();
  },
  // ToDO
  /**
   * 设置标注 除文字标注外的其他所有标注
   * @param {*} color 颜色 #ffffff(str)
   * @param {*} width 线宽 线的粗细（单位像素）
   */
  setSelectMark: function (color, width) {
    this.viewer.bimMark.setSelectMark(color, width);
  },
  /**
   * 设置文字标注
   * @param {*} text 文字内容 str
   * @param {*} color 文字颜色 例如：#ffffff(str)
   * @param {*} bgColor 背景颜色 例如：#ffffff(str)
   * @param {*} fontSize 字体大小 number
   * @param {*} lineWidth 标注宽度（行宽） number
   */
  setSelectText: function (text, color, bgColor, fontSize, lineWidth) {
    this.viewer.bimMark.setSelectText(
      text,
      color,
      bgColor,
      fontSize,
      lineWidth
    );
  },
  /**
   * 获取面片
   */
  getModelInfo: function () {
    return this.viewer.getModelInfo();
  },
  /**
   * 炸开
   * @param {*} offset 偏移倍数 number 一般设为1
   * @param {*} time 时间 number 毫秒计 1000为1秒
   * @param {number} 系数取0-1的值。当time为0时候，该值有效。
   */
  explodeModels: function (offset, time, factor) {
    this.viewer.explodeModels(offset, time, factor);
  },
  /**
   * 炸开归位
   */
  backHomeModels: function () {
    this.viewer.backHomeModels();
  },
  /**
   * 剖切模型
   */
  sliceModel: function () {
    this.viewer.sliceModel();
  },
  /**
   * 沿着X方向加1
   */
  addCapX: function () {
    this.viewer.addCapX();
  },
  /**
   * 沿着X方向减1
   */
  subCapX: function () {
    this.viewer.subCapX();
  },
  /**
   * 沿着Y方向加1
   */
  addCapY: function () {
    this.viewer.addCapY();
  },
  /**
   * 沿着Y方向减1
   */
  subCapY: function () {
    this.viewer.subCapY();
  },
  /**
   * 沿着Z方向加1
   */
  addCapZ: function () {
    this.viewer.addCapZ();
  },
  /**
   * 沿着Z方向减1
   */
  subCapZ: function () {
    this.viewer.subCapZ();
  },
  /**
   * 退出剖切
   */
  cancleSliceModel: function () {
    this.viewer.cancleSliceModel();
  },
  /**
   * 设置模型的纹理
   * @param {*} object 需操作的模型对象
   * @param {*} path 纹理路径
   */
  setModelTexture: function (object, path) {
    this.viewer.setModelTexture(object, path);
  },
  /**
   * @param {*} object 需操作的模型对象
   * 恢复模型之前的纹理状态
   */
  resetModelTexture: function (object) {
    this.viewer.resetModelTexture(object);
  },
  /**
   * 设置模型是否闪烁
   * @param {*} object 需操作的模型对象，支持传入数组。
   * @param {*} color 颜色 0xffffff 十六进制
   * @param {*} time 间隔时间 number
   */
  setModelFlicker: function (object, color, time) {
    this.viewer.setModelFlicker(object, color, time);
  },
  /**
   * 取消闪烁
   * @param {*} object 需操作的模型对象,支持传入数组。传入''表示清除所有闪烁。
   * 注意：当调用removeFile文件时，请先调用stopModelFlicker接口，移除闪烁，否则会造成内存泄漏。
   */
  stopModelFlicker: function (object) {
    this.viewer.stopModelFlicker(object);
  },
  /**
   * 点测量 激活点测量状态 传入isOnlyZ true 为高程测量 undefined为点测量
   */
  measurePoint: function (isOnlyZ) {
    this.viewer.measurePoint(isOnlyZ);
  },
  /**
   * 点点测量 激活点点测量状态
   */
  measurePtop: function () {
    this.viewer.measurePtop();
  },
  /**
   * 边点测量 激活边点测量状态
   */
  measureLtop: function () {
    this.viewer.measureLtop();
  },
  /**
   * @description: 面点测量 激活面点测量状态
   * @param {type}
   * @return:
   */
  measureFtop: function () {
    this.viewer.measureFtop();
  },
  /**
   * @description: 边边测量 激活面点测量状态
   * @param {type}
   * @return:
   */
  measureLtol: function () {
    this.viewer.measureLtol();
  },
  /**
   * @description: 三点角度测量 激活三点角度测量
   * @param {type}
   * @return:
   */
  measureAngleByPoints: function () {
    this.viewer.measureAngleByPoints();
  },
  /**
   * @description: 体积测量。调用endMeasure结束。
   * @return: 无。
   */
  measureVolume: function () {
    this.viewer.measureVolume();
  },
  /**
   * @description: 面积测量 。调用endMeasure结束。
   * @return: 无。
   */
  measureArea: function () {
    this.viewer.measureArea();
  },
  /**
   * 计算体积。
   * @param {string} id 需要计算体积的模型id。
   */
  computeVolume: function (id) {
    return this.viewer.computeVolume(id);
  },
  /**
   * 退出测量状态
   */
  endMeasure: function () {
    this.viewer.endMeasure();
  },
  /**
    * @description: 配置测量
    * @param {type} Object类型 
    *                          {boardFill:[177,201,237],数值面板填充色
                               boardStroke:[221,221,221],数值面板描边色
                               textStroke:[255,255,255],数值颜色
                               hoverPointColor:[255,0,0],测量时顶点选中颜色
                               selectPointColor:[255,255,0],测量点确认后、测量时顶点未选中颜色
                               hoverLineColor:[255,0,0],测量边未选择颜色
                               selectLineColor:[255,255,0],测量边确认后颜色
                               lineColor:[255,255,255],虚线颜色
                               boardFont:'宋体',数值面板字体
       可传undefined,默认如上
    * @return: 
    */
  setMeasureColor: function (object) {
    this.viewer.setMeasureColor(object);
  },
  /**
   * 删除视点
   */
  deleteViewPoint: function () {
    this.viewer.deleteViewPoint();
  },
  /**
   * 删除选中标注
   */
  deleteSelectMark: function () {
    this.viewer.bimMark.deleteSelectMark();
  },

  /**
   * 设置背景及描边颜色（弹窗）
   * @param {function} con 用于获取返回值的回调函数。如 const con = a => {console.log(a)} a为返回值
   * @param {String} backgroundColorA 背景色上
   * @param {String} backgroundColorB 背景色下
   * @param {String} lineColor 描边高亮颜色
   * @param {String} faceColor 面高亮颜色
   * @param {Boolean} switchFace 是否高亮面
   * @param {Boolean} switchLight 是否有描边
   */

  setBackgroundAndOutLine: function (con) {
    this.viewer.setBackgroundAndOutLine(con);
  },
  /**
   * 初始化背景及描边颜色的弹窗
   * @param {Object} obj {backgroundColorA: "#ffc000", backgroundColorB: "#123456", lineColor: "#009789", faceColor: "#ffffff", switchFace:true, switchLight: false, switchAO : false, switchShadow : false}
   *
   */
  setInitBackAndOutlineColor: function (obj) {
    this.viewer.setInitBackAndOutlineColor(obj);
  },
  //清空标注
  disposeMark: function () {
    this.viewer.disposeMark();
  },
  /**
   * 设置环境光、色
   * @param {*} color 默认颜色0xffffff(不设置颜色传undefined)
   * @param {*} intensity 默认强度0.5
   */
  setAmbientLight: function (color, intensity) {
    this.viewer.setAmbientLight(color, intensity);
  },
  /**
   * 设置相机光、色
   * @param {*} color 默认颜色0xffffff(不设置颜色传undefined)
   * @param {*} intensity 默认强度0.25
   */
  setCameraLight: function (color, intensity) {
    this.viewer.setDirectionalLight(color, intensity);
  },
  /**
   * 根据时间设置光，该光照影响阴影。
   * @param {boolean} visible 根据开启太阳光
   * @param {number} time 时间值，取值范围为0-24。
   * @param {number} color 默认颜色0xffffff(不设置颜色传undefined)
   * @param {number} intensity 光照强度，默认强度为0.25
   */
  setSunLightByTime: function (visible, time, color, intensity) {
    this.viewer.setSunLightByTime(visible, time, color, intensity);
  },
  /**
   * 设置阴影，当enbale传true时会开启SunLight。
   * @param {*} enable 是否开启阴影，true为开启，false为关闭。当不传值时候关闭阴影。
   * @param {*} ignoreSetCast 是否忽略setModelCastShadow接口配置的值，true为忽略，false为不忽略。当不传该值时，setModelCastShadow接口配置有效。
   * @param {*} ingorSetReceive 是否忽略setModelReceiveShadow接口配置的值，true为忽略，false为不忽略。当不传该值时，setModelReceiveShadow接口配置有效。
   */
  setShadow: function (enable, ignoreSetCast, ingorSetReceive) {
    this.viewer.setShadow(enable, ignoreSetCast, ingorSetReceive);
  },
  /**
   * 设置模型是否能投射出阴影。显示模型阴影在别的模型上。
   * @param {*} ids 需要设置的模型id。支持传入数组。
   * @param {*} enable 是否开启，true为开启，false为不开启。
   * @param {*} option 操作类型，传入0表示对传入的对象进行操作。传入1表示对传入的对象之外的对象进行操作。
   */
  setModelCastShadow: function (ids, enable, option) {
    this.viewer.setModelCastShadow(ids, enable, option);
  },
  /**
   * 设置模型是否能接受别的模型阴影。在模型上显示别的模型的阴影。
   * @param {*} ids 需要设置的模型id。支持传入数组。
   * @param {*} enable 是否开启，true为开启，false为不开启。
   * @param {*} option 操作类型，传入0表示对传入的对象进行操作。传入1表示对传入的对象之外的对象进行操作。
   */
  setModelReceiveShadow: function (ids, enable, option) {
    this.viewer.setModelReceiveShadow(ids, enable, option);
  },
  /**
   * 是否使用遮蔽效果，该效果会使模型更有层次感，但是会对渲染效率有影响。
   * @param {boolean} enable true开启，false关闭。默认关闭。
   */
  setEffectOcclusion: function (enable) {
    this.viewer.setEffectOcclusion(enable);
  },
  /**
   * 根据id来zoomfit 不传参数就是重置视口
   * @param {*} id 模型对象id 可是string数组
   * @param {*} factor 距离因子
   */
  zoomFit: function (id, factor) {
    this.viewer.zoomFit(id, factor);
  },
  /**
   * 设置选中高光颜色和强度
   * @param {*} color 默认0x0000ff
   * @param {*} intensity 默认1
   */
  setSelectColorIntensity: function (color, intensity) {
    this.viewer.setSelectColorIntensity(color, intensity);
  },
  //更改描边颜色
  setOutLineColor: function (color) {
    this.viewer.setOutLineColor(color);
  },
  //开始直线法平面剖切
  startSliceByEdge: function () {
    this.viewer.startSliceByEdge();
  },
  //沿法线加1
  addSliceEdge: function () {
    this.viewer.addSliceEdge();
  },
  //沿法线减1
  subSliceEdge: function () {
    this.viewer.subSliceEdge();
  },
  // 关闭直线法平面剖切
  endSliceByEdge: function () {
    this.viewer.endSliceByEdge();
  },
  /**
   * @description: 开始自由剖切
   * @param {type}
   * @return:
   */
  startSliceByFace: function () {
    this.viewer.startSliceByFace();
  },
  /**
   * @description: 结束自由剖切
   * @param {type}
   * @return:
   */
  endSliceByFace: function () {
    this.viewer.endSliceByFace();
  },
  /**
   * 开始沿单轴剖切
   * @param {*} axis 0对应x轴即yz平面,1对应y轴即xz平面,2对应z轴即xy平面
   */
  startSliceModelByAxis: function (axis) {
    this.viewer.startSliceModelByAxis(axis);
  },
  /**
   * 结束沿单轴剖切
   * @param {*} axis 0对应x轴即yz平面,1对应y轴即xz平面,2对应z轴即xy平面
   */
  endSliceModelByAxis: function (axis) {
    this.viewer.endSliceModelByAxis(axis);
  },
  /**
   * 高亮边开关
   * @param {*} bool bool变量 true打开，false关闭
   */
  setHighlightedLine: function (bool) {
    this.viewer.setHighlightedLine(bool);
  },
  /**
   * 高亮面开关
   * @param {*} bool bool变量 true打开，false关闭
   */
  setHighlightedFace: function (bool) {
    this.viewer.setHighlightedFace(bool);
  },
  /**
   * 设置剖切面移动步长 绝对
   * @param {*} unit number 默认为1 单位为m
   */
  setCapMoveStepByUnit: function (unit) {
    this.viewer.setCapMoveStepByUnit(unit);
  },
  /**
   * @description: 设置点测量偏移点 默认为[0,0,0] 顺序对应xyz
   * @param {type} Array
   * @return:
   */
  setOffsetPoint: function (arr) {
    this.viewer.setOffsetPoint(arr);
  },
  /**
   * @description: 反转剖切方向
   * @param {type}
   * @return:
   */
  inverseCap: function () {
    this.viewer.inverseCap();
  },
  /**
   * @description: 是否隐藏剖切参考面 （参考面默认是显示状态）
   * @param {type} bool true隐藏 false显示 必须处于剖切状态才可调用
   * @return:
   */
  hiddenCap: function (bool) {
    this.viewer.hiddenCap(bool);
  },
  /**
   * @description: 打开剖切盒
   * @param {type}
   * @return:
   */
  startBoxClip: function () {
    this.viewer.startBoxClip();
  },
  /**
   * @description: 关闭剖切盒
   * @param {type}
   * @return:
   */
  endBoxClip: function () {
    this.viewer.endBoxClip();
  },
  /**
   * @description: 是否隐藏剖切参考面操作轴 （参考面操作轴默认是显示状态）
   * @param {type} bool true隐藏 false显示 必须处于剖切状态才可调用
   * @return:
   */
  hiddenCapGizmo: function (bool) {
    this.viewer.hiddenCapGizmo(bool);
  },
  /**
   * @description: 碰撞检测
   * @param {type} Array id数组 collisionDetectionByIds(['idString',...],['idString',...]) 如果idArr2为undefined 则在idArr1内互相检测
   * @return: Array 相互碰撞的对象数组[{id1:idString,id2:idString},...]
   */
  collisionDetectionByIds: function (idArr1, idArr2) {
    return this.viewer.collisionDetectionByIds(idArr1, idArr2);
  },
  /**
   * @description: 碰撞检测异步执行
   * @param {type} Array id数组 collisionDetectionByIds(['idString',...],['idString',...]) 如果idArr2为undefined 则在idArr1内互相检测 callback 回调
   * @return:
   */
  collisionDetectionByIdsAsync: function (idArr1, idArr2, callback) {
    this.viewer.collisionDetectionByIdsAsync(idArr1, idArr2, callback);
  },
  /**
   * @description: 设置碰撞显示效果 即颜色（注：全选）
   * @param {type} color 0xffffff 十六进制 number
   * @return:
   */
  setCollisionDetectionColor: function (color1, color2) {
    this.viewer.setCollisionDetectionColor(color1, color2);
  },
  /**
   * @description: 设置碰撞显示效果 即颜色（注：根据id）  如果是全部设置 使用setCollisionDetectionColor以节约开销
   * @param {type} ids 可以是单个id字符串 也可以是多个id字符串数组 color 0xffffff 十六进制 number
   * @return:
   */
  setCollisionDetectionColorByIds: function (ids, color) {
    this.viewer.setCollisionDetectionColorByIds(ids, color);
  },
  /**
   * @description: 清除碰撞显示效果 即颜色
   * @param {type}
   * @return:
   */
  clearCollisionDetectionColor: function () {
    this.viewer.clearCollisionDetectionColor();
  },
  /**
   * @description: 使用worker 进行一组碰撞检测
   * @param {type} idArr id数组
   * @param {type} callback(result) 回调 碰撞检测结束后执行 返回[{id1:id1,id2:id2},...]互相碰撞的id对
   * @param {type} progress(progress) 回调 碰撞检测进行时执行  进度回调
   * @return:
   */
  collisionDetectionByIdsWorker: function (idArr, callback, progress) {
    this.viewer.collisionDetectionByIdsWorker(idArr, callback, progress);
  },
  /**
   * @description: 使用worker 进行两组碰撞检测
   * @param {type} idArr id数组
   * @param {type} callback(result) 回调 碰撞检测结束后执行 返回[{id1:id1,id2:id2},...]互相碰撞的id对
   * @param {type} progress(progress) 回调 碰撞检测进行时执行  进度回调
   * @return:
   */
  collisionDetectionByIdsWorker2: function (
    idArr1,
    idArr2,
    callback,
    progress
  ) {
    this.viewer.collisionDetectionByIdsWorker2(
      idArr1,
      idArr2,
      callback,
      progress
    );
  },
  /**
   * @description: 传入bool值来控制是否更新
   * @param {type} 布尔
   * @return:
   */
  enableRender: function (bool) {
    this.viewer.enableRender(bool);
  },
  /**
   * 设置摄像机进入模型内部的最短距离。
   * @param {number} distance 穿入模型的最短距离。大场景建议0.5，小场景建议0.005即可。根据用户自定义适配。
   */
  setMinEnterDistance: function (distance) {
    this.viewer.setMinEnterDistance(distance);
  },
  /**
   * @description: 开始删除标注 此时可通过鼠标点击标注实现删除点击到的标注功能
   * @param {type}
   * @return:
   */
  startDeleteMark: function () {
    this.viewer.startDeleteMark();
  },
  /**
   * @description: 结束删除标注
   * @param {type}
   * @return:
   */
  endDeleteMark: function () {
    this.viewer.endDeleteMark();
  },
  /**
   * @description: 开始面点测量（使用拓展平面）
   * @param {type}
   * @return:
   */
  measureFtop2: function () {
    this.viewer.measureFtop2();
  },
  /**
   * @description: 开始面线测量（使用拓展平面）
   * @param {type}
   * @return:
   */
  measureFtoL2: function () {
    this.viewer.measureFtoL2();
  },
  /**
   * @description: 开始面面测量（使用拓展平面）
   * @param {type}
   * @return:
   */
  measureFtoF2: function () {
    this.viewer.measureFtoF2();
  },
  /**
   * @description: 开始半径测量
   * @param {type}
   * @return:
   */
  measureRadius: function () {
    this.viewer.measureRadius();
  },
  /**
   * 截图功能，返回包含图片展示的data URI。数据头为data:image/png;
   */
  screenshots: function () {
    return this.viewer.screenshots();
  },
  /**
   * @description: 设置删除标注橡皮擦颜色
   * @param {type} str 默认'#ff0'
   * @return:
   */
  setDeleteMarkMouseColor: function (str) {
    this.viewer.setDeleteMarkMouseColor(str);
  },
  /**
   * @description: 设置隔离对象的透明度
   * @param {type} number 0-1之间，越小越透明，例如interface.setQuarantineItemsOpacity(0.5)
   * @return:
   */
  setQuarantineItemsOpacity: function (value) {
    this.viewer.setQuarantineItemsOpacity(value);
  },
  /**
   * @description: 剖切盖子开关 默认是打开状态
   * @param {type}bool true/false
   * @return:
   */
  enableCap: function (bool) {
    this.viewer.enableCap(bool);
  },
  /**
   * @description: 剖切盖子颜色设置
   * @param {type}color css颜色字符串 例如"#f00" 条纹颜色
   * @param {type}bgColor css颜色字符串 例如"#f00" 背景颜色
   * @return:
   */
  setCapColor: function (color, bgColor) {
    this.viewer.setCapColor(color, bgColor);
  },
  /**
   * @description: 控制等距的边测量
   * @param {Boolean} bool true/false 默认为true状态 即显示等距的测量边
   * @return:
   */
  setMeasureEqualEdge: function (bool) {
    this.viewer.setMeasureEqualEdge(bool);
  },
};

// export { BIM3DInterface };
