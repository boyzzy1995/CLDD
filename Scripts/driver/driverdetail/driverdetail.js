//解除绑定的司机
function unlockLicense(driverId){
 var _url = "http://webservices.qgj.cn/cldd/driver/unbound.ashx?driverId="+driverId;
 var _process = new Process(_url, 'get');
 if(confirm("确定要解除绑定吗?")){
   showCover();
   _process.delegate = function (_request) {
     if (_request.status == 200) {
      hideCover();
      var _xml=new Xml(_request.responseXML); 
      var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
      if(_result=="1"){
        alert("解除成功");
        getlist("http://webservices.qgj.cn/cldd/driver/driverdetail.ashx?driverId="+driverId);
      }else{
        alert("操作出错，请与管理员联系"); 
      }
    }
  }
  Thread.append(_process);
}
}