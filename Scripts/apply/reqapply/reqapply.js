function showMask(date){
	var _date=date.toString();
	document.getElementById("wrap-cover").style.display="block";
	document.getElementById("wrap-cover").style.opacity="0.6";
	document.getElementById("mask").style.display="block";
	$('date').innerHTML="你选择的日期是:"+_date.substr(0,4)+"年"+_date.substr(4,2)+"月"+_date.substr(6,2)+"号";
	$('sendTime').innerHTML=_date.substr(0,4)+"-"+_date.substr(4,2)+"-"+_date.substr(6,2);
}
function closeBg(){
	document.getElementById("mask").style.display="none";
	document.getElementById("wrap-cover").style.display="none";
	document.getElementById("wrap-cover").style.opacity="1";
}
function confirmTime(){
	var _hours=$('hours').value;;
	var _minutes=$('minutes').value;
	if(_minutes<10){
		_minutes="0"+_minutes;
	}
	if(confirm("确定预约当前时间"+$('sendTime').innerHTML+unescape("%20")+_hours+":"+_minutes+"吗？")){
		/*var _sendTime=$('sendTime').innerHTML+unescape("%20")+_hours+":"+_minutes;*/
		var _date=$('sendTime').innerHTML;
		var _time=_hours+":"+_minutes;
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
			var _url = "http://webservices.qgj.cn/cldd/apply/phone_reqapplyform.ashx?date="+_date+"&time="+_time;
		}else{
			var _url = "http://webservices.qgj.cn/cldd/apply/reqapplyform.ashx?date="+_date+"&time="+_time;
		}
		window.location.href=_url; 
	}
}
//加载的时候遮罩层
function showCover(){
	$("#cover").css("display","block");
}
//关闭加载的时候遮罩层
function hideCover(){
	$("#cover").css("display","none");
}
window.onload=function(){

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

	　　　　　　　　(function(){
  /*
   * 用于记录日期，显示的时候，根据dateObj中的日期的年月显示
   */
  var dateObj = (function(){
    var _date = new Date();    // 默认为当前系统时间
    return {
	      getDate : function(){
		        return _date;
	      },
	      setDate : function(date) {
		        _date = date;
	      }
    };
  })();
 
  // 设置calendar div中的html部分
  renderHtml();
  // 表格中显示日期
  showCalendarData();
  // 绑定事件
  bindEvent();
  /**
   * 渲染html结构
   */
function getCountDays() {
	var curDate = new Date();
	/* 获取当前月份 */
	var curMonth = curDate.getMonth();
	/*  生成实际的月份: 由于curMonth会比实际月份小1, 故需加1 */
	curDate.setMonth(curMonth + 1);
	/* 将日期设置为0, 这里为什么要这样设置, 我不知道原因, 这是从网上学来的 */
	curDate.setDate(0);
	/* 返回当月的天数 */
	return curDate.getDate();
}

 //点击下个月时隐藏下个月按钮 显示上个月按钮 	
 function hideNextMonth(){
 	document.getElementById("nextMonth").style.display="none";
 	document.getElementById("prevMonth").style.display="block";
 }
  //点击上个月时隐藏上个月按钮 显示下个月按钮
  function hidePrevMonth(){
  	document.getElementById("prevMonth").style.display="none";
  	document.getElementById("nextMonth").style.display="block";
  }
    function renderHtml() {
  	var calendar = document.getElementById("calendar");
    var titleBox = document.createElement("div");  // 标题盒子 设置上一月 下一月 标题
    var bodyBox = document.createElement("div");  // 表格区 显示数据
 
    // 设置标题盒子中的html
    titleBox.className = 'calendar-title-box';
if(getCountDays()-parseInt(getDateStr(new Date()).substr(6,2))>=14){
	titleBox.innerHTML = "<span class='prev-month' id='prevMonth' style='position: absolute;top:25px;left: 0px;display: inline-block;width: 0px;height: 0px;border-left: 0px;border-top: 6px solid transparent;border-right: 8px solid #999;border-bottom: 6px solid transparent;cursor: pointer;'></span>" +
	      "<span class='calendar-title' id='calendarTitle' style='font-size:20px;font-family:微软雅黑'></span>" +
      "<span id='nextMonth' class='next-month'></span>";calendar.appendChild(titleBox);    // 添加到calendar div中
document.getElementById("prevMonth").style.display="none";
}else{
	titleBox.innerHTML = "<span class='prev-month' id='prevMonth' style='position: absolute;top:25px;left: 0px;display: inline-block;width: 0px;height: 0px;border-left: 0px;border-top: 6px solid transparent;border-right: 8px solid #999;border-bottom: 6px solid transparent;cursor: pointer;'></span>" +
	      "<span class='calendar-title' id='calendarTitle' style='font-size:20px;font-family:微软雅黑'></span>" +
      "<span id='nextMonth' style='position: absolute;top: 25px;right: 0px;display: inline-block;width: 0px;height: 0px;border-right: 0px;border-top: 6px solid transparent;border-left: 8px solid #999;border-bottom: 6px solid transparent;cursor: pointer;' class='next-month'></span>";calendar.appendChild(titleBox);    // 添加到calendar div中
document.getElementById("prevMonth").style.display="none";
}
    /*titleBox.innerHTML = "<span class='prev-month' style='position: absolute;top:12px;left: 0px;display: inline-block;width: 0px;height: 0px;border-left: 0px;border-top: 6px solid transparent;border-right: 8px solid #999;border-bottom: 6px solid transparent;cursor: pointer;' id='prevMonth'></span>" +
      "<span class='calendar-title' id='calendarTitle'></span>" +
      "<span id='nextMonth' style='position: absolute;top: 12px;right: 0px;display: inline-block;width: 0px;height: 0px;border-right: 0px;border-top: 6px solid transparent;border-left: 8px solid #999;border-bottom: 6px solid transparent;cursor: pointer;' class='next-month'></span>";calendar.appendChild(titleBox);    // 添加到calendar div中
 */
    // 设置表格区的html结构
    bodyBox.className = 'calendar-body-box';
    var _headHtml = "<tr style='height:30px;line-height:30px;background-color: gainsboro;'>" + 
              "<th>日</th>" +
              "<th>一</th>" +
              "<th>二</th>" +
              "<th>三</th>" +
              "<th>四</th>" +
              "<th>五</th>" +
              "<th>六</th>" +
            "</tr>";
    var _bodyHtml = "";  
    // 一个月最多31天，所以一个月最多占6行表格
    for(var i = 0; i < 6; i++) {  
	      _bodyHtml += "<tr style='height:30px;line-height:30px;'>" +
	              "<td></td>" +
	              "<td></td>" +
	              "<td></td>" +
	              "<td></td>" +
	              "<td></td>" +
	              "<td></td>" +
	              "<td></td>" + 
	            "</tr>";
    }
    bodyBox.innerHTML = "<table id='calendarTable' class='calendar-table' style='width: 100%;border-collapse: collapse;text-align:center;height:80%;'>" +
              _headHtml + _bodyHtml +
              "</table>";
    // 添加到calendar div中
    calendar.appendChild(bodyBox);
  }
 
  /**
   * 表格中显示数据，并设置类名
   */
  function showCalendarData() {
	    var _year = dateObj.getDate().getFullYear();
	    var _month = dateObj.getDate().getMonth() + 1;
	    var _dateStr = getDateStr(dateObj.getDate());
	 
    // 设置顶部标题栏中的 年、月信息
    var calendarTitle = document.getElementById("calendarTitle");
    var titleStr = _dateStr.substr(0, 4) + "年" + _dateStr.substr(4,2) + "月";
    calendarTitle.innerText = titleStr;
 
    // 设置表格中的日期数据
    var _table = document.getElementById("calendarTable");
    var _tds = _table.getElementsByTagName("td");
    var _firstDay = new Date(_year, _month - 1, 1);  // 当前月第一天
    var _todayMonth=_month;
    //_firstDay.getDay()知道第一天是星期几
        for(var i = 0; i < _tds.length; i++) {
    	  var _thisDay = new Date(_year, _month - 1, i + 1 - _firstDay.getDay());
	      var _thisDayStr = getDateStr(_thisDay);//截成字符串的形式
	      _tds[i].innerText = _thisDay.getDate();
      //_tds[i].data = _thisDayStr;
      _tds[i].setAttribute('data', _thisDayStr);
      if(_thisDayStr == getDateStr(new Date())) {    // 当前天
	     if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
	     	 _tds[i].className = 'otherMonth';
	     }else if($('1').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a1' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	       var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a1' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	  
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+1))){//对应参数分别是年月日
	      if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
	      	 _tds[i].className = 'otherMonth';
	      }else if($('2').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a1' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a2' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+2))){//对应参数分别是年月日
      	 if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      	 	 _tds[i].className = 'otherMonth';
      	 }else if($('3').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a1' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a3' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+3))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('4').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a1' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a4' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+4))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('5').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a5' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a5' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+5))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('6').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a6' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a6' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+6))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('7').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a7' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a7' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+7))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('8').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a8' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a8' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+8))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('9').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a9' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a9' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+9))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('10').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a10' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a10' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+10))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('11').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a11' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a11' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+11))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('12').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a12' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a12' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+12))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('13').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a13' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a13' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+13))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('14').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a14' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a14' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
      }else if(_thisDayStr == getDateStr(new Date(parseInt(getDateStr(new Date()).substr(0,4)),parseInt(getDateStr(new Date()).substr(4,2))-1,parseInt(getDateStr(new Date()).substr(6,2))+14))){//对应参数分别是年月日
      	if(_thisDayStr.substr(4,2)!=_todayMonth){//判断是不是这个月
      		 _tds[i].className = 'otherMonth';
      	}else if($('15').innerHTML!='disable'){
          _tds[i].className = 'currentDay';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<a href='#' onclick='showMask("+_thisDayStr+")' id='a15' style='display: inline-block;width: 100%;'><span style='display:block'>"+_day+"</span></a>";//当前td插入连接
	  }else{
	      _tds[i].className = 'currentMonth';//设置样式
	      var _day=parseInt(_thisDayStr.substr(6,2));//获得当前几号
	      _tds[i].innerHTML="<span id='a15' style='display:block'>"+_day+"</span>";//当前td插入连接
	  }	
	}else if(_thisDayStr.substr(0, 6) == getDateStr(_firstDay).substr(0, 6)) {
        _tds[i].className = 'currentMonth';  // 当前月
      }else {    // 其他月
	        _tds[i].className = 'otherMonth';
      } 
    }
  }
 
  /**
   * 绑定上个月下个月事件
   */
  function bindEvent() {
	var prevMonth = document.getElementById("prevMonth");
	var nextMonth = document.getElementById("nextMonth");
	addEvent(prevMonth, 'click', toPrevMonth);
	addEvent(nextMonth, 'click', toNextMonth);
  }
 
  /**
   * 绑定事件
   */
  function addEvent(dom, eType, func) {
    if(dom.addEventListener) {  // DOM 2.0
	      dom.addEventListener(eType, function(e){
		        func(e);
	      });
    } else if(dom.attachEvent){  // IE5+
	      dom.attachEvent('on' + eType, function(e){
		        func(e);
	      });
    } else {  // DOM 0
	      dom['on' + eType] = function(e) {
		        func(e);
	      }
    }
  }
 
  /**
   * 点击上个月图标触发
   */
  function toPrevMonth() {
	    var date = dateObj.getDate();
	    dateObj.setDate(new Date(date.getFullYear(), date.getMonth() - 1, 1));
	    showCalendarData();
	hidePrevMonth();

  }
 
  /**
   * 点击下个月图标触发
   */
  function toNextMonth() {
	    var date = dateObj.getDate();
	    dateObj.setDate(new Date(date.getFullYear(), date.getMonth() + 1, 1));
	    showCalendarData();
	hideNextMonth();
  }
 
  /**
   * 日期转化为字符串， 4位年+2位月+2位日
   */
  function getDateStr(date) {
	var _year = date.getFullYear();
    var _month = date.getMonth() + 1;    // 月从0开始计数
    var _d = date.getDate();
     
    _month = (_month > 9) ? ("" + _month) : ("0" + _month);
    _d = (_d > 9) ? ("" + _d) : ("0" + _d);
    return _year + _month + _d;
  }
})();　　　　　　　　　　　　　　　　　　　//将link元素节点添加到head元素子节点下　　　　　　　　
　}
