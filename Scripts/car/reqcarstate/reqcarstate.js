function searchCarState(){
	var _year=$('#year').val();
	var _month=$('#month').val();
	var _day=$('#day').val();
	if(_year==''){
		alert('年不能为空!');
		return false;
	}
	if(_month==''){
		alert('月不能为空!');
		return false;
	}
	if(_day==''){
		alert('日不能为空!');
		return false;
	}
	var _url = "http://webservices.qgj.cn/cldd/car/getcarstatebyday.ashx";
	var _form=document.getElementById("searchCarState-form");
	showCover();
	var add = new AjaxForm(_form, "post", _url);
	add.response = function (_request) {
		hideCover();
		$('#content-right').css("height","auto");
		$('#hbody-row').css("height","auto");
		document.getElementById("searchCarState-tableId").innerHTML= _request.responseText;
		heightAuto("#content-right","#hbody-row",$("#content-right").height());
		heightAuto("body","#hbody-row",$("body").height()-140);
		heightAuto("#hbody-row","#content-right",$("#hbody-row").height());
		$("#year").attr("disabled",false); 
		$("#month").attr("disabled",false);
		$("#day").attr("disabled",false);
		$("#btnSearchCarState").attr("disabled",false);
		return false;
	}
	add.submit();
}