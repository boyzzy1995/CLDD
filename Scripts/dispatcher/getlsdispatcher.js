//添加调度员
function addDispatcher(){
  var _account=$('#input-permitAccount').val();
  if(_account==''){
    alert("调度员账号不能为空!");
    return false;
  }
  var _url="http://webservices.qgj.cn/cldd/dispatcher/addlsdispatcher.ashx";
  var _form=document.getElementById("dispatcherform");
  _url=encodeURI(_url);
  showCover();
  var add = new AjaxForm(_form, "post", _url);
  add.response = function (_request) {
   hideCover();
   var _xml=new Xml(_request.responseXML); 
   var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
   if(_result=="1"){
     alert("添加成功");
     toDispatcher("http://webservices.qgj.cn/cldd/dispatcher/getlsdispatcher.ashx");
   }else{
    alert("操作出错，请与管理员联系"); 
  }
}
add.submit();
}
//删除调度员
function deleteDispatcher(){
  var _permitAccount=$('#PermitAccount').val();
  var _url="http://webservices.qgj.cn/cldd/dispatcher/dellsdispatcher.ashx";
  var _process = new Process(_url, 'get');
  showCover();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     var _xml=new Xml(_request.responseXML); 
     var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
     if(_result=="1"){
       alert("删除成功");
       toDispatcher("http://webservices.qgj.cn/cldd/dispatcher/getlsdispatcher.ashx");
     }else{
      alert("操作出错，请与管理员联系"); 
    }
  }
}
Thread.append(_process);
}