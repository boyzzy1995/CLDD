//删除司机
function ToDeleteDriver(driverId){
  var _url = "http://webservices.qgj.cn/cldd/driver/del_driver.ashx?driverId="+driverId;
  var _process = new Process(_url, 'get');
  if(confirm("确定要删除吗?")){
    showCover();
    _process.delegate = function (_request) {
     if (_request.status == 200) {
      hideCover();
      var _xml=new Xml(_request.responseXML); 
      var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
      if(_result=="1"){
        alert("删除成功");
        getlist("http://webservices.qgj.cn/cldd/driver/getdriverlist.ashx");
      }else{
        alert("操作出错，请与管理员联系"); 
      }
    }
  }
  Thread.append(_process);
}
}