//人工分配车辆页面确定时间
function confirmTime(){
  var _hours=$('#hours').val();
  var _minutes=$('#minutes').val();
  if(_minutes<10){
    _minutes="0"+_minutes;
  }
  var _date=document.getElementById('datetime').innerHTML;
  closeMaskTime();
  $('#manStarttime').val(_date+" "+_hours+":"+_minutes);
}
//人工申请页面查找车牌
function SearchLicenseID(){
  var _starttime=$('#manStarttime').val();
  var _days=$('#day').val();
  var _personnum=$('#personNum').val();
  var _url="http://webservices.qgj.cn/cldd/apply/getcar_freelist.ashx?starttime="+_starttime+"&days="+_days+"&tripnum="+_personnum;
  if(_starttime==''){
    alert("请先选择开始时间!");
    return false;
  }
  if(_days==''){
    alert("请先输入用车天数!");
    return false;
  }
  if(_personnum==''){
    alert("请先输入用车人数!");
    return false;
  }
  showCover();
  var _process = new Process(_url, 'get');
  var _select=document.getElementById("apply-LicenseID");
  _select.options.length=0
  _process.delegate = function (_request) {
    if (_request.status == 200) {
      hideCover();
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
//判断开始时间是否选中
function checkStarttime(){
  var _starttime=$("#manStarttime").val();
  if(_starttime==''){
    alert("请先选择开始时间!");
    $("#manStarttime").focus();
    return false;
  }
}
//判断用车人数是否符合标准
function checkPerson(){
  var _personnum=$("#personNum").val();
  var _starttime=$("#manStarttime").val();
  var _url = "http://webservices.qgj.cn/cldd/apply/reqsites.ashx?sites=" + _personnum + "&starttime=" + _starttime;
  var _process = new Process(_url, 'get');
  showCover();
  _process.delegate = function (_request) {
    if (_request.status == 200) {
     hideCover();
     var _xml = new Xml(_request.responseXML);  
     var _carnum = _xml.documentElement.selectSingleNode('response').getAttribute("CarNum");
     var _sites = _xml.documentElement.selectSingleNode('response').getAttribute("sites");
     var _MaxSites=_xml.documentElement.selectSingleNode('response').getAttribute("MaxSites");
     $("#maxsites").val(_MaxSites);
     if(_sites=='false'){
       if(_personnum>parseInt(_MaxSites)){
        if(_carnum==parseInt('1')){
          alert("当前最多出行人数为："+_MaxSites+"请删减用车人数!");
        }else{
          alert("当前最多出行人数为："+_MaxSites+"请删减用车人数并再申请一辆车!");
        }
      }
    }
  }
}
Thread.append(_process);  
}

//提交按钮
function ReCheckNum(){   
 var _destination=$("#destination").val();
 var substr = "杭州";
 var _starttime=$("#manStarttime").val();
 var _day=$("#day").val();
 var _personnum=$("#personNum").val();
 var _applytel=$("#AppliedTel").val();
 var _LicenseID=$("#apply-LicenseID").val();
 var _applyAccount=$("#ApplyAccount").val();
 if(_destination==''){
   alert("目的地不能为空!");
   return false;
 } 
       //检查是否包含杭州字符串
       function isContains(str, substr) {
         return str.indexOf(substr) >= 0;
       }
       if(isContains(_destination, substr)){
         alert('输入的目的地不能为杭州市内!');
         return false;
       };
       if(_starttime==''){
         alert("开始时间不能为空！");
         return false;
       }
       if(_day==''){
         alert("用车天数不能为空");
         return false;
       }
       if(_personnum==''){
         alert("用车人数不能为空!");
         return false;
       }
       if(_applyAccount==''){
        alert("申请人不能为空!");
        return false;
       }
       if($("#who").val()==''){
         alert("用车人姓名不能为空!");
         return false;
       }
       if(isNaN(_applytel)){ 
        alert("手机号码只能是数字!");  
        return false; 
      } 
      if(_applytel==''){
        alert("手机号不能为空!");  
        return false; 
       }
       if(isNaN(_applytel)){ 
        alert("手机号码只能是数字(短号与手机号只能填一个!)");  
        return false; 
      } 
      if(_LicenseID=="当前无可用车辆"){
        alert("当前无可用车辆,不能申请用车");
        return false;
      }
      if($("#reason").val()==''){
       alert("申请理由不能为空!");
       return false;
     }
     var _name = $("#applyName").text();
     var _account = $("#applyAccount").text();
     var _form = document.getElementById("addform");
     var _url = "http://webservices.qgj.cn/cldd/apply/req_manapply.ashx?account="+_account+"&name="+_name;
     _url=encodeURI(_url);
     var add = new AjaxForm(_form, "post", _url);
     showCover();
     add.response = function (_request) {
      hideCover();
      var _xml=new Xml(_request.responseXML); 
      var _result=_xml.documentElement.selectSingleNode('response').getAttribute("affect")
      if(_result=="1"){
        alert("添加成功");
        getlist("http://webservices.qgj.cn/cldd/apply/getapplyreviewlist.ashx"); 
      }else{
         alert("添加失败!请与管理员联系");
         return false;
      }/* if(_result=="1"){
        alert("添加成功");
        getlist("http://webservices.qgj.cn/cldd/apply/getapplyreviewlist.ashx");
      }else if(_result=="-1"){
        alert(_title);
        return false;
      }*/
    } 
    if($("#peronNum").val()>$("#maxsites").val()){
      hideCover(); 
      alert("请删减用车人数!");
    }else{
      add.submit();
    } 
  }