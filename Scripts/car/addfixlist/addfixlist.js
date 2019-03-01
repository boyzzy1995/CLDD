function getfixlist(url){
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
     var myDate3 = new Calender({id:'StartTime'});
     var myDate4 = new Calender({id:'EndTime',isSelect:!0});
     return false;
 }
}
Thread.append(_process);
}
function addfixlist(){
	var _st=$("#StartTime").val();
	var _et=$("#EndTime").val();
	if(_st==''){
		alert('开始时间不能为空!');
		return false;
	}
	if(_et==''){
		alert('结束时间不能为空!');
		return false;
	}
	var _url="http://webservices.qgj.cn/cldd/car/add_fix.ashx";
	var _form=document.getElementById("fix-form");
	_url=encodeURI(_url);
	var add = new AjaxForm(_form, "post", _url);
	showCover();
	add.response = function (_request) {
		hideCover();
		$("#btnbuildform").attr("disabled",false); 
		$("#StartTime").attr("disabled",false); 
		$("#EndTime").attr("disabled",false); 
		$("#LicenseID").attr("disabled",false); 
       var _xml = new Xml(_request.responseXML);
		var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
		if (_result == "1" ) {
			alert("添加成功!");
			getlist("http://webservices.qgj.cn/cldd/car/getfixedlist.ashx");
		}
		else {
			alert("系统异常！请联系管理员。");
			getlist("http://webservices.qgj.cn/cldd/car/getfixedlist.ashx");
		}
	}
	add.submit();
}