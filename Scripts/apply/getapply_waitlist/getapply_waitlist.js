
//待出车列表开始行程
function startTrip(guid){
  var _guid=guid;
  var _url = "http://webservices.qgj.cn/cldd/apply/start_apply.ashx?guid="+_guid;
  if(confirm("确定要开始行程吗?")){
    var _process = new Process(_url, 'get');
    showCover();
    _process.delegate = function (_request) {
     if (_request.status == 200) {
       hideCover();
       var _xml = new Xml(_request.responseXML);
       var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
       if (_result == "1" ) {
        alert("行程已经开始!");
        getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
      }
      else {
        alert("系统异常！请联系管理员。");
      }
    }
  }
}
Thread.append(_process);
}
//待出车列表结束行程
function endTrip(guid){
  var _guid=guid;
  var _url = "http://webservices.qgj.cn/cldd/apply/end_apply.ashx?guid="+_guid;
  if(confirm("确定要结束行程吗?")){
    var _process = new Process(_url, 'get');
    showCover();
    _process.delegate = function (_request) {
     if (_request.status == 200) {
       hideCover();
       var _xml = new Xml(_request.responseXML);
       var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
       if (_result == "1" ) {
        alert("行程已经结束!");
        getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
      }
      else {
        alert("系统异常！请联系管理员。");
      }
    }
  }
}
Thread.append(_process);
}
//待出车列表更改车牌的遮罩层
function showChangeMask(carapplyid,starttime,days,tripnum){
  showCover();
  var _url="http://webservices.qgj.cn/cldd/apply/getcar_freelist.ashx?starttime="+starttime+"&days="+days+"&tripnum="+tripnum;
  var _process = new Process(_url, 'get');
  var _select=document.getElementById("mask-LicenseID");
  _select.options.length=0
  _process.delegate = function (_request) {
    if (_request.status == 200) {
      hideCover();
      $('#changeCar').css("display","block");
      $('#wrap-cover').css("display","block");
      $("#wrap-cover").css("opacity","0.6");
      $("#carapplyid").val(carapplyid);
      $("#startTimeId").val(starttime);
      $("#daysId").val(days);
      $("#tripNumId").val(tripnum);
      var _xml = new Xml(_request.responseXML);
      var _root = _xml.documentElement.selectSingleNode("response");
      var _conpeny = new Array();
      _conpeny = _root.selectNodes("SchemaTable");  //所有的部门节点
      if(_conpeny.length<=0){
        var _apartment = _conpeny[i];
      var _optgroup = document.createElement("option"); //创建当前部门的optgroup
      _optgroup.setAttribute("value", "当前无可用车辆");
      _optgroup.setAttribute("label", "当前无可用车辆");
      _select.appendChild(_optgroup);
    }else{
      for (var i = 0; i < _conpeny.length; i++) {
        var _apartment = _conpeny[i];
      var _optgroup = document.createElement("option"); //创建当前部门的optgroup
      _optgroup.setAttribute("value", _apartment.selectSingleNode("Guid/text()").nodeValue);
      _optgroup.setAttribute("label", _apartment.selectSingleNode("LicenseID/text()").nodeValue);
      _select.appendChild(_optgroup);
    }

  }
}
}
Thread.append(_process);
}
//弹出选择更换车辆遮罩层
function showChangeCarMask(){
 $("#wrap-cover").css("display","block");
 $("#wrap-cover").css("opacity","0.6");
 $('#selectChangeCar').css("display","block");

}
//关闭选择更换车辆遮罩层
function closeChangeCarMask(){
 $("#wrap-cover").css("display","none");
 $("#wrap-cover").css("opacity","1");
 $('#selectChangeCar').css("display","none");
}
//待出车列表关闭改变牌遮罩层
function closeChangeMask(){
  $("#changeCar").css("display","none");
  $("#wrap-cover").css("opacity","1");
  $('#wrap-cover').css("display","none");
}
//确定更改车牌号
function confirmChange(){
 var _applyId=$("#carapplyid").val();
 var _guid=document.getElementById("mask-LicenseID").value;
 if(_guid=='当前无可用车辆'){
   alert("当前无可用车辆，不可更改");
   return false;
 }
 var _url = "http://webservices.qgj.cn/cldd/apply/edit_car.ashx?carid="+_guid+"&applyid="+_applyId;
 var _process = new Process(_url, 'get');
 showCover();
 closeChangeMask();
 _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     var _xml = new Xml(_request.responseXML);
     var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
     if (_result == "1" ) {
      alert("更改成功");
      closeChangeMask();
      getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
    }
    else {
      alert("系统异常！请联系管理员。");
    }
  }
}
Thread.append(_process);
}
//确定申请延期
function confirmDelay(){
  var _days=$("#delayDateinput").val();
  if(_days==''){
    alert("申请延期的天数不能为空!");
    return false;
  }
  var _guid=$("#changeCarId").val();
  var _url = "http://webservices.qgj.cn/cldd/apply/dely_apply.ashx?applyid="+_guid+"&days="+_days;
  var _process = new Process(_url, 'get');
  closeDelayMask();
  showCover();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     var _xml = new Xml(_request.responseXML);
     var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
     if (_result == "1" ) {
      alert("申请成功");
      closeDelayMask();
      getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
    }
    else {
      alert("系统异常！请联系管理员。");
      showDelayMask();
    }
  }
}
Thread.append(_process);
}
//遮罩层中更换空闲车辆按钮
function showchangefreecarbtn(){
  $(".changefreecarclass").css("display","inline-block");
  closeChangeCarMask();
}
//操作中更换空闲车辆按钮
function showchangefreecar(guid){
  $("#maskchangeCarId").html(guid);
  showCover();
  var _url="http://webservices.qgj.cn/cldd/apply/getchangecar.ashx?guid="+guid;
  var _process = new Process(_url, 'get');
  var _select=document.getElementById("mask-freecarLicenseID");
  _select.options.length=0
  _process.delegate = function (_request) {
    if (_request.status == 200) {
      hideCover();
      $("#wrap-cover").css("display","block");
      $("#wrap-cover").css("opacity","0.6");
      $('#changefreeCarMask').css("display","block");
      var _xml = new Xml(_request.responseXML);
      var _root = _xml.documentElement.selectSingleNode("response");
      var _conpeny = new Array();
      _conpeny = _root.selectNodes("SchemaTable/LicenseID");  //所有的部门节点
      if(_conpeny.length<=0){
      
      var _optgroup = document.createElement("option"); //创建当前部门的optgroup
      _optgroup.setAttribute("value", "当前无可用车辆");
      _optgroup.setAttribute("label", "当前无可用车辆");
      _select.appendChild(_optgroup);
    }else{
      for (var i = 0; i < _conpeny.length; i++) {
        var _apartment = _conpeny[i];
      var _optgroup = document.createElement("option"); //创建当前部门的optgroup
      _optgroup.setAttribute("value", _apartment.selectSingleNode("text()").nodeValue);
      _optgroup.setAttribute("label", _apartment.selectSingleNode("text()").nodeValue);
      _select.appendChild(_optgroup);
    }
  }
}
}
Thread.append(_process);
}
//遮罩层中选择车辆之后的确定事件
function confirmChangelicenseID(){
  $('#changefreeCarMask').css("display","none");
  showCover();
  var _appliedId=$("#maskchangeCarId").text();
  var _licenseId=$("#mask-freecarLicenseID").val();
  var _url="http://webservices.qgj.cn/cldd/apply/changefreecar.ashx?appliedID="+_appliedId+"&licenseID="+_licenseId;
  var _process = new Process(_url, 'get');
  _process.delegate = function (_request) {
    if (_request.status == 200) {
     hideCover();
     $('#changefreeCarMask').css("display","block");
     var _xml = new Xml(_request.responseXML);
     var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
     if (_result == "1" ) {
      alert("交换车辆成功");
      $('#changefreeCarMask').css("display","none");
      getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
    }
    else {
      alert("系统异常！请联系管理员。");
      $('#changefreeCarMask').css("display","none");
      getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
    }
  }
}
Thread.append(_process);
}
function closechangefreeCarMask(){
  $("#wrap-cover").css("display","none");
  $("#wrap-cover").css("opacity","1");
  $('#changefreeCarMask').css("display","none");
}
//显示复选框
function showChangeCheckbox(){
  $("[name='carNumbox']").css("display","block");
  $("#btnConfirm").css("display","block");
  $("#btnCancel").css("display","block");
  closeChangeCarMask();
}
function boxcancel(){
  $("[name='carNumbox']").css("display","none");
  $("#btnConfirm").css("display","none");
  $("#btnCancel").css("display","none");
}
//复选框事件
$("document").ready(function(){ 
  $("body").on('click','[data-type="checkbox"]',function(){
    if($(this).prop("checked")){
      var _driverone=$('#driverOne').text();
      if(_driverone=='null'){
        $('#driverOne').text($(this).attr('data-value'));
        $('#driverOneId').text($(this).attr('value').split(",")[1]);
        /*$('#number').text($(this).attr('data-num'));*/
      }else if($('#driverTwo').text()!='null'){
       alert("只能选择两条记录!");
       $(this).removeAttr('checked');
     }else{
      var _drivertwo=$(this).attr('data-value');
      var _drivertwoId=$(this).attr('value');
      if($('#driverOne').text()==_drivertwo){
        alert("车辆不能相同!");
        $(this).removeAttr('checked');
      }else{
        $('#driverTwo').text(_drivertwo);
        $('#driverTwoId').text(_drivertwoId.split(",")[1]);
      }
    }        
  }else{
    var count=0;
    var checkArry = document.getElementsByName("carNumbox");
    for (var i = 0; i < checkArry.length; i++) { 
      if(checkArry[i].checked == true){
        count++;
      } 
    }
    if(count==0){
      $('#driverOne').text("null");
      $('#driverTwo').text("null");
      $('#driverOneId').text("null");
      $('#driverTwoId').text("null");
    }
    if(count==1){
      $('#driverTwo').text("null"); 
      $('#driverOne').text("null");
      $('#driverTwoId').text("null"); 
      $('#driverOneId').text("null");
      for (var i = 0; i < checkArry.length; i++) { 
        if(checkArry[i].checked == true){
          var _str=checkArry[i].value;
          $('#driverOne').text(_str.split(",")[0]);
          $('#driverOneId').text(_str.split(',')[1]);
        } 
      }
    }
  }
});
});
//交换车辆提交事件
function comfirmHandup(){
  _driveroneid=$('#driverOneId').text();
  _drivertwoid=$('#driverTwoId').text();
  if(_driveroneid=='null'){
    alert("请先勾选要交换的记录(至少两条)");
    return false;
  }
  if(_drivertwoid=='null'){
    alert("请先勾选要交换的记录(至少两条)");
    return false;
  }
  var _url = "http://webservices.qgj.cn/cldd/apply/edit_car.ashx?applyida="+_driveroneid+"&applyidb="+_drivertwoid+"&type=ischange";
  var _process = new Process(_url, 'get');
  showCover();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     var _xml = new Xml(_request.responseXML);
     var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
     var _title = _xml.documentElement.selectSingleNode('response').getAttribute("title");

     if(_result=="0"){
      if(confirm(_title)){
       _url = "http://webservices.qgj.cn/cldd/apply/edit_car.ashx?applyida="+_driveroneid+"&applyidb="+_drivertwoid+"&type=change";
       var _process = new Process(_url, 'get');
       showCover();
       _process.delegate = function (_request) {
         if (_request.status == 200) {
           hideCover();
           var _xml = new Xml(_request.responseXML);
           var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
           var _title = _xml.documentElement.selectSingleNode('response').getAttribute("title");
           if(_result=="1"){
            alert(_title);
            getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
          }
          if(_result=="-1"){
            alert(_title);
            getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
          }
        }
      }
      Thread.append(_process);
    }
  }
  if(_result=="1"){
    alert(_title);
    getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
  }
  if(_result=="-1"){
    alert(_title);
    return false;
  }
}
}
Thread.append(_process);
}
//取消申请按钮
function cancelTrip(_CarAppliedID) {
  _CarAppliedID = escape(_CarAppliedID);
  var _url = "http://webservices.qgj.cn/cldd/apply/cancal_apply.ashx?guid=" + _CarAppliedID;
  if (confirm("确认取消该申请吗?")) {
    var _process = new Process(_url, 'get');
    showCover();
    _process.delegate = function (_request) {
      if (_request.status == 200) {
        hideCover();
        var _xml = new Xml(_request.responseXML);
        var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
        if (_result == "1") alert("取消成功!");
        else alert("取消失败!");
        getlist("http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx");
      }
    }
    Thread.append(_process);
  }
}
