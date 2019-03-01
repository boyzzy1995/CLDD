//确认修改司机页面
function motifyDriver(_driverid){
  var _drivername=$('#DriverName').val();
  var _driveraccount=$('#DriverAccount').val();
  var _tel=$('#CarTelephone').val();
  if(_drivername==''){
    alert("司机姓名不能为空!");
    return false;
  }
  if(_driveraccount==''){
    alert("司机账号不能为空!");
    return false;
  }
   if(_tel==''){
        alert("手机号不能为空!");  
        return false; 
       }
       if(isNaN(_tel)){ 
        alert("手机号码只能是数字(短号与手机号只能填一个!)");  
        return false; 
      } 
  var _form=document.getElementById("motifyform");
  var _url = "http://webservices.qgj.cn/cldd/driver/motifydriver.ashx?driverid="+_driverid;
  _url=encodeURI(_url);
  var add = new AjaxForm(_form, "post", _url);
  showCover();
  add.response = function (_request) {
    hideCover();
    var _xml=new Xml(_request.responseXML); 
    var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
    if(_result=="1"){
      alert("修改成功");
      getlist("http://webservices.qgj.cn/cldd/driver/driverdetail.ashx?driverid="+_driverid);
    }else{
      alert("操作出错，请与管理员联系"); 
    }
  } 
  add.submit();
}