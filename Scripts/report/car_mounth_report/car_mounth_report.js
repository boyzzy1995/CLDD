//进入打印车牌界面
function toprintform(){
  var _year=$("#year").val();
  var _mounth=$("#mounth").val();
  var _licenseID=$("#LicenseID").val();
  window.open("http://webservices.qgj.cn/cldd/report/print_car_mounth.ashx?licenseId="+_licenseID+"&year="+_year+"&mounth="+_mounth); 
}