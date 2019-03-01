//让内容高度自适应方法，传递参数依次为基准高度，要改变的高度，基准高度的值。
function heightAuto(basicHeightId,changeHeightId,basicHeight){
  var _changeHeight=$(changeHeightId).height();
  if(parseInt(basicHeight)>parseInt(_changeHeight)){
    $(changeHeightId).css("height", basicHeight);
}
}
//获得手机端的调度员列表
function getApplyList() {           
    var _url="http://webservices.qgj.cn/cldd/dispatcher/getlsdispatcher.ashx";
    var _process = new Process(_url, 'get');
    _process.delegate = function (_request) {
        if (_request.status == 200) {
            document.getElementById("tableId").innerHTML = _request.responseText;
            heightAuto("body","#body-row",$("body").height()-57);
        }
    }
    Thread.append(_process);
}

function locationToHref(_CarAppliedID){
 window.location.href="http://webservices.qgj.cn/cldd/apply/phone_detail.ashx?guid="+_CarAppliedID; 
}

window.onload=function(){
    getApplyList();
    //调用自适应方法
}
//删除调度员
function deleteDispatcher(){
  var _permitAccount=$('#PermitAccount').val();
  var _url="http://webservices.qgj.cn/cldd/dispatcher/dellsdispatcher.ashx";
  var _process = new Process(_url, 'get');
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     var _xml=new Xml(_request.responseXML); 
     var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
     if(_result=="1"){
       alert("删除成功");
       getApplyList();
     }else{
      alert("操作出错，请与管理员联系"); 
    }
  }
}
Thread.append(_process);
}
//显示添加调度员的表单
function showAddDispatcher(){
  $('#firstShow').css("display","none");
  $('#clickShow').css("display","block");
}
//关闭添加调度员表单
function cancelDispatcher(){
  $('#firstShow').css("display","block");
  $('#clickShow').css("display","none");
}
//添加调度员
function addDispatcher(){
  var _url="http://webservices.qgj.cn/cldd/dispatcher/addlsdispatcher.ashx";
  var _form=document.getElementById("dispatcherform");
  _url=encodeURI(_url);
  var add = new AjaxForm(_form, "post", _url);
  add.response = function (_request) {
   var _xml=new Xml(_request.responseXML); 
   var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
   if(_result=="1"){
     alert("添加成功");
     getApplyList();
   }else{
    alert("操作出错，请与管理员联系"); 
  }
}
add.submit();
}

