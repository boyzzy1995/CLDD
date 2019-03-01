function cancelfix(fixID){
	if(confirm("确定要取消吗?")){
		_url = "http://webservices.qgj.cn/cldd/car/cancel_fix_statue.ashx?fixID="+fixID;
		showCover();
		var _process = new Process(_url, 'get');
		_process.delegate = function (_request) {
			if (_request.status == 200) {
				hideCover();
				var _xml=new Xml(_request.responseXML); 
				var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
				if(_result=="1"){
					alert("取消成功！");
					getcarlist("http://webservices.qgj.cn/cldd/car/getfixedlist.ashx");
					return false;
				}else{
					alert("取消失败！");
					return false;
				}
			}
		}
		Thread.append(_process);
	}
}

function endfix(fixID){
	if(confirm("确定要结束吗?")){
		_url = "http://webservices.qgj.cn/cldd/car/end_fix_statue.ashx?fixID="+fixID;
		showCover();
		var _process = new Process(_url, 'get');
		_process.delegate = function (_request) {
			if (_request.status == 200) {
				hideCover();
				var _xml=new Xml(_request.responseXML); 
				var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
				if(_result=="1"){
					alert("结束成功！");
					getcarlist("http://webservices.qgj.cn/cldd/car/getfixedlist.ashx");
					return false;
				}else{
					alert("结束失败！");
					return false;
				}
			}
		}
		Thread.append(_process);
	}
	
}

