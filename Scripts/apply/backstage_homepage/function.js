//让内容高度自适应方法，传递参数依次为基准高度，要改变的高度，基准高度的值。
function heightAuto(basicHeightId,changeHeightId,basicHeight){
  var _changeHeight=$(changeHeightId).height();
  if(parseInt(basicHeight)>parseInt(_changeHeight)){
    $(changeHeightId).css("height", basicHeight);
  }
}
window.onload=function(){
    //调用自适应方法
    heightAuto("#content-right","#hbody-row",$("#content-right").height());
    heightAuto("body","#hbody-row",$("body").height()-140);
    $('#firstSize').val($("#hbody-row").height());
    $(".myul a").hover(function() {
      $(this).css("background-color","#4E5465");
      $(this).css("font-size","16px");
      $(".mydl a").css("font-size","14px");
    },function(){
      $(this).css("background-color","#35404d");
      $(this).css("font-size","14px");
    });
    //人工分配页面为selection增加内容
    function buildOption(){
      for(var i=0;i<24;i++){
        var _hours=document.getElementById("hours");
        var _option=document.createElement("option");
        _hours.appendChild(_option);
        _option.text=i;
        _option.value=i;
      }
      for(var y=0;y<60;y++){
        var _hours=document.getElementById("minutes");
        var _option=document.createElement("option");
        _hours.appendChild(_option);
        _option.text=y;
        _option.value=y;
      }
    }
    buildOption();
  }
//导航栏点击展开和关闭事件
function show(showId,imgId,spanId){
  if($(showId).is(':hidden')){　　 //如果node是隐藏的则显示node元素，否则隐藏
    $(showId).show(100);
    $(imgId).attr("src","../Image/cicon.png");
    $(spanId).css("background-color","#4E5465");
    $(spanId).css("font-size","16px");
  }else{
    $(showId).hide(100);
    $(imgId).attr("src","../Image/picon.png");
    $(spanId).css("background-color","#35404d");
    $(spanId).css("font-size","14px");
  }
}
//获得预约申请列表
function getlist(url){
 var _url = url;
 var _process = new Process(_url, 'get');
 showCover();
 _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     //显示日历
     var myDate1 = new Calender({id:'manStarttime'});
     /*var myDate2 = new Calender({id:'MOT',isSelect:!0});*/
     return false;
   }
 }
 Thread.append(_process);
}    

//进入调度员界面
function toDispatcher(url){
  var _url = url;
  var _process = new Process(_url, 'get');
  showCover();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     return false;
   }
 }
 Thread.append(_process);
}
//获得车辆列表
function getcarlist(url){
 var _url = url;
 var _process = new Process(_url, 'get');
 showCover();
 _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover(); 
     document.getElementById("tableId").innerHTML= _request.responseText; 
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     return false;
   }
 }
 Thread.append(_process);
}
//进入添加车辆页面
function toaddcar(url){
 var _url = url;
 var _process = new Process(_url, 'get');
 showCover();
 _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     var myDate1 = new Calender({id:'insurance'});
     var myDate2 = new Calender({id:'mot',isSelect:!0});
     return false;
   }
 }
 Thread.append(_process);
}    
//进入车辆详情页面
function locationToHref(_guid){
 var _url="http://webservices.qgj.cn/cldd/car/cardetail.ashx?guid="+_guid;
 var _process = new Process(_url, 'get');
 showCover();
 _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     return false;
   }
 }
 Thread.append(_process);

}
//进入车辆修改页面
function HrefToMotify(guid){
  var _guid=guid;
  var _url="http://webservices.qgj.cn/cldd/car/reqcarmotify.ashx?guid="+_guid;
  var _process = new Process(_url, 'get');
  showCover();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     var myDate1 = new Calender({id:'Insurance'});
     var myDate2 = new Calender({id:'MOT',isSelect:!0});
     return false;
   }
 }
 Thread.append(_process);
}
//进入司机修改页面
function HrefToMotifyDriver(guid){
  var _guid=guid;
  var _url="http://webservices.qgj.cn/cldd/driver/reqmotifydriver.ashx?driverId="+_guid;
  var _process = new Process(_url, 'get');
  showCover();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     return false;
   }
 }
 Thread.append(_process);

}
//进入司机详情页面
function ToFullInformation(guid){
  var _url="http://webservices.qgj.cn/cldd/driver/driverdetail.ashx?driverid="+guid;
  var _process = new Process(_url, 'get');
  showCover();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     return false;
   }
 }
 Thread.append(_process);
}
//获得司机列表页面
function getdriverlist(_url){
  var _process = new Process(_url, 'get');
  showCover();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     return false;
   }
 }
 Thread.append(_process);
}
//获得司机添加页面
function toaddriver(_url){
 var _process = new Process(_url, 'get');
 showCover();
 _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     return false;
   }
 }
 Thread.append(_process);
}
//进入报表页面
function toReport(_url){
  var _process = new Process(_url, 'get');
  showCover();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideCover();
     $('#content-right').css("height","auto");
     $('#hbody-row').css("height","auto");
     document.getElementById("tableId").innerHTML= _request.responseText;
     heightAuto("#content-right","#hbody-row",$("#content-right").height());
     heightAuto("body","#hbody-row",$("body").height()-140);
     heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
     return false;
   }
 }
 Thread.append(_process);
}
//弹出遮罩层
function showMask(guid){
  $("#mask").css("display","block");
  $("#wrap-cover").css("display","block");
  $("#wrap-cover").css("opacity","0.6");
  $("#reasonguid").val(guid);
}
//关闭遮罩层
function closeBg(){
  $("#mask").css("display","none");
  $("#wrap-cover").css("display","none");
  $("#wrap-cover").css("opacity","1");
}
//显示遮罩层
function showCover(){
  $("#cover").css('display','block');
  $("#wrap-cover").css('display','block');
  $("#wrap-cover").css('opacity','0.3');
}
function hideCover(){
 $("#cover").css('display','none');
 $("#wrap-cover").css('display','none');
 $("#wrap-cover").css('opacity','1');
}
//显示申请延期的遮罩层
function showDelayMask(guid){
  $('#delayDate').css("display","block");
  $('#wrap-cover').css("display","block");
  $("#wrap-cover").css("opacity","0.6");
  $("#changeCarId").val(guid);
}
//关闭申请延期的遮罩层
function closeDelayMask(){
  $("#delayDate").css("display","none");
  $("#wrap-cover").css("opacity","1");
  $('#wrap-cover').css("display","none");
}
function closeMaskTime(){
  $("#mask-time").css("display","none");
  $("#wrap-cover").css("opacity","1");
  $('#wrap-cover').css("display","none");
  $('#manStarttime').val("");
}
//显示添加调度员的表单
function showAddDispatcher(){
  $('#firstShow').css("display","none");
  $('#clickShow').css("display","block");
}
//关闭添加调度员表单
function cancelDispatcher(){
  $('#firstShow').css("display","block");
  $('#clickShow').css("display","none");
}
