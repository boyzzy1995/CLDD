judgeIndexDecive(); 
window.onload=function(){
   //显示遮罩层
   function showMask(){
    $("mask").style.cssText = 'display: block';
    $("wrap-mask").style.cssText = 'display: block';
    $("wrap-mask").style.opacity="0.3";
  }
  function hideMask(){
   $("mask").style.cssText = 'display: none';
   $("wrap-mask").style.cssText = 'display: none';
   $("wrap-mask").style.opacity="1";
 }
   //判断是要获得什么端的列表
   function getApplyList() {
    var _account=$("account").innerHTML;
    var _permit=$("permit").innerHTML;
    var _url;
    var sUserAgent= navigator.userAgent.toLowerCase(); 
    var bIsIpad= sUserAgent.match(/ipad/i) == "ipad"; 
    var bIsIphoneOs= sUserAgent.match(/iphone os/i) == "iphone os"; 
    var bIsMidp= sUserAgent.match(/midp/i) == "midp"; 
    var bIsUc7= sUserAgent.match(/rv:1.2.3.4/i) == "rv:1.2.3.4"; 
    var bIsUc= sUserAgent.match(/ucweb/i) == "ucweb"; 
    var bIsAndroid= sUserAgent.match(/android/i) == "android"; 
    var bIsCE= sUserAgent.match(/windows ce/i) == "windows ce"; 
    var bIsWM= sUserAgent.match(/windows mobile/i) == "windows mobile"; 

    if (bIsIpad || bIsIphoneOs || bIsMidp || bIsUc7 || bIsUc || bIsAndroid || bIsCE || bIsWM) {             
     _url = "http://webservices.qgj.cn/cldd/apply/getphoneapplylist.ashx?account=" + _account + "&permit=" + _permit;
   }else{
     _url = "http://webservices.qgj.cn/cldd/apply/getapplylist.ashx?account=" + _account + "&permit=" + _permit;
   }
   /*$("frameId").src=_url;*/
   var _process = new Process(_url, 'get');
   showMask();
   _process.delegate = function (_request) {
    if (_request.status == 200) {
      hideMask();
      $("tableId").innerHTML = _request.responseText;
    }
  }
  Thread.append(_process);
}

  //获得公里
  function getDriverkm(){
    var _account=$("account").innerHTML;
    var _url = "http://webservices.qgj.cn/cldd/car/getkm.ashx?account="+_account;
    var _process = new Process(_url, 'get');
    showMask();
    _process.delegate = function (_request) {
      if (_request.status == 200) {
        hideMask();
        var _xml = new Xml(_request.responseXML);
        var _result = _xml.documentElement.selectSingleNode('response/SchemaTable/Kilometers/text()').nodeValue;
        $("km").value=_result;
      }
    }
    Thread.append(_process);
  }
   //手机屏幕移除按钮
   function checkScreenDriver(){
    var sUserAgent= navigator.userAgent.toLowerCase(); 
    var bIsIpad= sUserAgent.match(/ipad/i) == "ipad"; 
    var bIsIphoneOs= sUserAgent.match(/iphone os/i) == "iphone os"; 
    var bIsMidp= sUserAgent.match(/midp/i) == "midp"; 
    var bIsUc7= sUserAgent.match(/rv:1.2.3.4/i) == "rv:1.2.3.4"; 
    var bIsUc= sUserAgent.match(/ucweb/i) == "ucweb"; 
    var bIsAndroid= sUserAgent.match(/android/i) == "android"; 
    var bIsCE= sUserAgent.match(/windows ce/i) == "windows ce"; 
    var bIsWM= sUserAgent.match(/windows mobile/i) == "windows mobile"; 

    if (bIsIpad || bIsIphoneOs || bIsMidp || bIsUc7 || bIsUc || bIsAndroid || bIsCE || bIsWM) { 
     if($("rightWords")){
       $("rightWords").remove(); 
     }else if($("userwords")){
       $("userwords").remove();
     }
   }
 }
 getApplyList();
 checkScreenDriver();
 getDriverkm();
}
//显示遮罩层
function showMask(){
  $("mask").style.cssText = 'display: block';
  $("wrap-mask").style.cssText = 'display: block';
  $("wrap-mask").style.opacity="0.3";
}
function hideMask(){
 $("mask").style.cssText = 'display: none';
 $("wrap-mask").style.cssText = 'display: none';
 $("wrap-mask").style.opacity="1";
}
function changebtn(){
  $("km").removeAttribute("disabled"); 
  $("change").value="更新";
  $("change").removeAttribute("onclick");
  $("change").setAttribute("onclick","resetbtn()");
}
//显示申请延期的遮罩层
function showDelayMask(guid){
  $('delayDate').style.cssText="display:block";
  $('wrap-mask').style.cssText="display:block";
  $("wrap-mask").style.opacity="0.6";
  $("changeCarId").value=guid;
}
//关闭申请延期的遮罩层
function closeDelayMask(){
  $("delayDate").style.cssText="display:none";
  $("wrap-mask").style.opacity="1";
  $('wrap-mask').style.cssText="display:none";
}
//司机修改行程
function resetbtn(){
  var _km=$('km').value;
  var _account=$('account').innerHTML;
  var _url = "http://webservices.qgj.cn/cldd/car/motifykm.ashx?account=" + _account; 
  var _form = $("motifykmform");
  var add = new AjaxForm(_form, "post", _url);
  showMask();
  add.response = function (_request) {
    hideMask();
    var _xml = new Xml(_request.responseXML);
    var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
    if (_result == "1") alert("修改成功!");
    else alert("修改失败!");
    location.reload();
  } 
  add.submit();
} 
//管理员取消申请按钮
function deleteItem(_CarAppliedID) {
  _CarAppliedID = escape(_CarAppliedID);
  var _url = "http://webservices.qgj.cn/cldd/apply/cancal_apply.ashx?guid=" + _CarAppliedID;
  if (confirm("确认取消该申请吗?")) {
    var _process = new Process(_url, 'get');
    showMask();
    _process.delegate = function (_request) {
      if (_request.status == 200) {
        hideMask();
        var _xml = new Xml(_request.responseXML);
        var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
        if (_result == "1") alert("取消成功!");
        else alert("取消失败!");
        location.reload();
      }
    }
    Thread.append(_process);
  }
}

function locationToHref(_CarAppliedID){
 window.location.href="http://webservices.qgj.cn/cldd/apply/phone_detail.ashx?guid="+_CarAppliedID; 
}
 //判断是什么设备进入主页
 function judgeIndexDecive(){
  var _url;
  var sUserAgent= navigator.userAgent.toLowerCase(); 
  var bIsIpad= sUserAgent.match(/ipad/i) == "ipad"; 
  var bIsIphoneOs= sUserAgent.match(/iphone os/i) == "iphone os"; 
  var bIsMidp= sUserAgent.match(/midp/i) == "midp"; 
  var bIsUc7= sUserAgent.match(/rv:1.2.3.4/i) == "rv:1.2.3.4"; 
  var bIsUc= sUserAgent.match(/ucweb/i) == "ucweb"; 
  var bIsAndroid= sUserAgent.match(/android/i) == "android"; 
  var bIsCE= sUserAgent.match(/windows ce/i) == "windows ce"; 
  var bIsWM= sUserAgent.match(/windows mobile/i) == "windows mobile"; 

  if (bIsIpad || bIsIphoneOs || bIsMidp || bIsUc7 || bIsUc || bIsAndroid || bIsCE || bIsWM) {             
   window.location.href="http://webservices.qgj.cn/cldd/apply/phone_index.ashx";
   return false; 
 }
}
//待出车列表开始行程
function startTrip(guid){
  var _guid=guid;
  var _url = "http://webservices.qgj.cn/cldd/apply/start_apply.ashx?guid="+_guid;
  if(confirm("确定要开始行程吗?")){
    var _process = new Process(_url, 'get');
    showMask();
    _process.delegate = function (_request) {
     if (_request.status == 200) {
       hideMask();
       var _xml = new Xml(_request.responseXML);
       var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
       if (_result == "1" ) {
        alert("行程已经开始!");
        location.reload();
      }
      else {
        alert("系统异常！请联系管理员。");
      }
    }
  }
}
Thread.append(_process);
}
//结束行程
function endTrip(guid){
  var _guid=guid;
  var _url = "http://webservices.qgj.cn/cldd/apply/end_apply.ashx?guid="+_guid;
  if(confirm("确定要结束行程吗?")){
    var _process = new Process(_url, 'get');
    showMask();
    _process.delegate = function (_request) {
     if (_request.status == 200) {
       hideMask();
       var _xml = new Xml(_request.responseXML);
       var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
       if (_result == "1" ) {
        alert("行程已经结束!");
        location.reload();
      }
      else {
        alert("系统异常！请联系管理员。");
      }
    }
  }
}
Thread.append(_process);
}
//获得待出车列表
function getwaitlist(){
  var _url="http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx";
  var _process = new Process(_url, 'get');
   showMask();
   _process.delegate = function (_request) {
    if (_request.status == 200) {
      hideMask();
      $("tableId").innerHTML = _request.responseText;
    }
  }
  Thread.append(_process);
}
//确定申请延期
function confirmDelay(){
  var _days=$("delayDateinput").value;
  if(_days==''){
    alert("申请延期的天数不能为空!");
    return false;
  }
  var _guid=$("changeCarId").value;
  var _url = "http://webservices.qgj.cn/cldd/apply/dely_apply.ashx?applyid="+_guid+"&days="+_days;
  var _process = new Process(_url, 'get');
  closeDelayMask();
  showMask();
  _process.delegate = function (_request) {
   if (_request.status == 200) {
     hideMask();
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

function delAllCookie(){    
    var keys = document.cookie.match(/[^ =;]+(?=\=)/g);  
                if(keys) {  
                    for(var i = keys.length; i--;)  
                        document.cookie = keys[i] + '=0;expires=' + new Date(0).toUTCString()  
                }  
   window.location.href="http://webservices.qgj.cn/cldd/index.html";    
                          
}