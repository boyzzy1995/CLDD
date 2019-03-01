//生成报表
function buildreport(){
  var _year=$("#year").val();
  var _mounth=$("#mounth").val();
  if(_year==''){
    alert('年不能为空!');
    return false;
  }
  if(_mounth==''){
    alert('月不能为空!');
    return false;
  }
  var _licenseID=$("#LicenseID").val();
  var _url="http://webservices.qgj.cn/cldd/report/car_mounth_report.ashx";
  var _form=document.getElementById("report-form");
  _url=encodeURI(_url);
  var add = new AjaxForm(_form, "post", _url);
  showCover();
  add.response = function (_request) {
    hideCover();
    $('#content-right').css("height","auto");
    $('#hbody-row').css("height","auto");
    document.getElementById("report-tableId").innerHTML=_request.responseText;
    heightAuto("#content-right","#hbody-row",$("#content-right").height());
    heightAuto("body","#hbody-row",$("body").height()-140);
    heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
    $("#btnbuildform").attr("disabled",false); 
    $("#year").attr("disabled",false); 
    $("#mounth").attr("disabled",false); 
    $("#LicenseID").attr("disabled",false); 
  }
  add.submit();
}