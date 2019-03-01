//确认拒绝理由
function confirmReason(){
 var _guid=$("#reasonguid").val();
 var _url = "http://webservices.qgj.cn/cldd/apply/apply_refuse.ashx?guid="+_guid;
 var _form=document.getElementById("reasonform");
 _url=encodeURI(_url);
 showCover();
 var add = new AjaxForm(_form, "post", _url);
 add.response = function (_request) {
   hideCover();
   var _xml=new Xml(_request.responseXML); 
   var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
   if(_result=="1"){
     closeBg();
     alert("已拒绝");
     getlist("http://webservices.qgj.cn/cldd/apply/getapplyreviewlist.ashx");
   }else{
    alert("操作出错，请与管理员联系"); 
  }
}
add.submit();
}
//确认通过
function confirmPass(guid){
  var _guid=guid;
  var _url = "http://webservices.qgj.cn/cldd/apply/apply_pass.ashx?guid="+_guid;
  var _process = new Process(_url, 'get');
  if(confirm("确认通过申请吗?")){
    showCover();
    _process.delegate = function (_request) {
     if (_request.status == 200) {
      hideCover();
      var _xml=new Xml(_request.responseXML); 
      var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
      if(_result=="1"){
        alert("已通过");
        getlist("http://webservices.qgj.cn/cldd/apply/getapplyreviewlist.ashx");
      }else{
        alert("操作出错，请与管理员联系"); 
      }
    }
  }
  Thread.append(_process);
}
}