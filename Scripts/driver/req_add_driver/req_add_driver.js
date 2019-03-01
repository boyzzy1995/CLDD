//添加司机
function addDriver(){
  var _drivername=$('#DriverName').val();
  var _driveraccount=$('#DriverAccount').val();
  var _tel=$('#CarTelephone').val();
  var _licenseid=$('#LicenseID').val();
  if(_drivername==''){
    alert("司机姓名不能为空!");
    return false;
  }
  if(_driveraccount==''){
    alert("司机账号不能为空!");
    return false;s
  }
   if(_tel==''){
        alert("手机号不能为空!");  
        return false; 
       }
       if(isNaN(_tel)){ 
        alert("手机号码只能是数字(短号与手机号只能填一个!)");  
        return false; 
      } 
  if(_licenseid==''){
    alert("当前没有可用的车牌，不能添加司机!");
    return false;
  }
  var _form=document.getElementById("driveraddform");
  var _url = "http://webservices.qgj.cn/cldd/driver/add_driver.ashx";
  _url=encodeURI(_url);
  showCover();
  var add = new AjaxForm(_form, "post", _url);
  add.response = function (_request) {
    hideCover();
    var _xml=new Xml(_request.responseXML); 
    var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect");
    if(_result=="1"){
      alert("添加成功");
      getlist("http://webservices.qgj.cn/cldd/driver/getdriverlist.ashx");
    }else{
      alert("操作出错，请与管理员联系"); 
    }
  }
  add.submit();
}