﻿//车辆页面按钮交换
function changebtn(btnid,spanid,selectid,guid){
  var _url;
  if($("#"+btnid).val()=='修改'){
    $("#"+spanid).css("display","none");
    $("#"+selectid).css("display","inline");
    $("#"+btnid).val("更新");
    return false;
  }
  if($("#"+btnid).val()=='更新'){
   var _statue=$('#'+selectid).val();
	alert(_statue);
   _url = "http://webservices.qgj.cn/cldd/car/disable_car.ashx?guid="+guid+"&statue="+ escape(_statue);
   showCover();
   var _process = new Process(_url, 'get');
   _process.delegate = function (_request) {
     if (_request.status == 200) {
      hideCover();
      var _xml=new Xml(_request.responseXML); 
      var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
      if(_result=="1"){
        alert("更新成功");
        getcarlist("http://webservices.qgj.cn/cldd/car/getcarlist.ashx");
        return false;
      }
    }
  }
  Thread.append(_process);
}
}
//修改可操作车辆数
function changefreecar(){
  if($('#btnchange').val()=='修改'){
   $('#txtnum').attr("disabled",false);
   $('#btnchange').val('更新');
   return false;
 }
 if($('#btnchange').val()=='更新'){
   var _txtnum=$('#txtnum').val();
   if(_txtnum==''){
    alert("车辆数不能为空!");
   }
   var _url="http://webservices.qgj.cn/cldd/car/motify_controlnum.ashx?num="+_txtnum;
   var _process = new Process(_url, 'get');
   showCover();
   _process.delegate = function (_request) {
     if (_request.status == 200) {
      hideCover();
      var _xml=new Xml(_request.responseXML); 
      var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
      if(_result=="1"){
        alert("修改成功");
        $('#btnchange').val('修改');
        $('#txtnum').attr("disabled",true);
        return false;
      }else{
        alert("操作出错，请与管理员联系"); 
      }
    }
  }
  Thread.append(_process);
}
}