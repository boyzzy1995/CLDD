//确认修改车辆页面确认修改表单
function carmotify(guid){ 
 var _LicenseID=$("#LicenseID").val();
 var _DriverAccount=$("#DriverAccount").val();
 var _Sites=$("#Sites").val();
 var _Insurance=$("#Insurance").val();
 var _MOT=$("#MOT").val();
 var _Kilomoters=$("#_Kilomoters").val();
 var _form=document.getElementById("motifyform");
 if(_LicenseID==''){
   alert("车牌号不能为空!");
   return false;
 } 

 if(_DriverAccount==''){
   alert("司机姓名不能为空！");
   return false;
 }
 if(_Sites==''){
   alert("座位数不能为空");
   return false;
 }
 if(_Insurance==''){
   alert("保险时间不能为空!");
   return false;
 }
 if(_MOT==''){
   alert("年检时间不能为空!");
   return false;
 }
 if(_Kilomoters==''){
   alert("公里数不能为空!");
   return false;
 }
 var _url = "http://webservices.qgj.cn/cldd/car/carmotify.ashx?guid="+guid;
 _url=encodeURI(_url);
 var add = new AjaxForm(_form, "post", _url);
 showCover();
 add.response = function (_request) {
  hideCover();
  var _xml=new Xml(_request.responseXML); 
  var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
  if(_result=="1"){
    alert("修改成功");
    getlist("http://webservices.qgj.cn/cldd/car/cardetail.ashx?guid="+guid);
  }else{
    alert("操作出错，请与管理员联系"); 
  }
} 
add.submit();
}