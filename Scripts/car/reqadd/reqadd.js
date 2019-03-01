//添加车辆
function addcar(){
 var _licenseID=$("#licenseID").val();
 var _sites=$("#sites").val();
 var _insurance=$("#insurance").val();
 var _mot=$("#mot").val();
 var _kilometers=$("#kilometers").val();
 var _form=document.getElementById("caraddform");
 if(_licenseID==''){
   alert("车牌号不能为空!");
   return false;
 } 

 if(_sites==''){
   alert("座位数不能为空");
   return false;
 }
 if(_insurance==''){
   alert("保险时间不能为空!");
   return false;
 }
 if(_mot==''){
   alert("年检时间不能为空!");
   return false;
 }
 if(_kilometers==''){
   alert("公里数不能为空!");
   return false;
 }
 var _url = "http://webservices.qgj.cn/cldd/car/add_car.ashx";
 _url=encodeURI(_url);
 var add = new AjaxForm(_form, "post", _url);
 showCover();
 add.response = function (_request) {
  hideCover();
  var _xml=new Xml(_request.responseXML); 
  var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect");
  if(_result=="1"){
    alert("添加成功");
    getlist("http://webservices.qgj.cn/cldd/car/getcarlist.ashx");
  }else{
    alert("操作出错，请与管理员联系"); 
  }
}
add.submit();
}