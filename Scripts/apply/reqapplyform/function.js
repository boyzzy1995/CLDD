
window.onload = function() {
    //正则表达式显示参数
    function GetQueryString(name) {
      var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
      var r = window.location.search.substr(1).match(reg);
      if (r != null) return unescape(r[2]);
      return null;
    }
    //获得地址栏的预约时间
    function getStartTime() {
      var _date = GetQueryString("date");
      var _time = GetQueryString("time");
      $("time").value = _date + " " + _time;
    }
    getStartTime();
    showformmask();
  }
  //判断当前选择条件是否存在车辆
   function Trim(str)
     { 
             return str.replace(/(^\s*)|(\s*$)/g, ""); 
     }
  function CheckCarNum() {
    var _personnum = $("mask-peronNum").value;
    var _starttime = $("time").value;
    var _days = $("mask-days").value;
    if (_starttime == '') {
      alert("开始时间不能为空！");
      return false;
    }
    if (_days == '') {
      alert("用车天数不能为空");
      return false;
    }
    if (_personnum == '') {
      alert("用车人数不能为空!");
      return false;
    }
    var _url = "http://webservices.qgj.cn/cldd/apply/req_isapply.ashx?starttime=" + _starttime + "&days=" + _days + "&tripnum=" + _personnum;
    showMask();
    closeformmaskT();
    var _process = new Process(_url, 'get');
    _process.delegate = function(_request) {
      hideMask();
      showformmask();
      var _xml = new Xml(_request.responseXML);
      var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect");
      var _title = _xml.documentElement.selectSingleNode('response').getAttribute("title");
      if (_result == "0") {
        closeformmask();
        alert(_title);
        showformmask();
        $("availableCar").innerHTML = 'yes';
        $("peronNum").value = _personnum;
        $("day").value = _days;
      } else if (_result == "1") {
        closeformmask();
        alert(_title);
        $("availableCar").innerHTML = 'yes';
        $("peronNum").value = _personnum;
        $("day").value = _days;
      } else if (_result == "-1") {
        alert(_title);
        showformmask();
        $("availableCar").innerHTML = 'no';
      }
    }
    Thread.append(_process);
  }
//遮罩层判断用车人数是否符合标准
function checkPerson() {
  var _personnum = $("mask-peronNum").value;
  var _starttime = $("time").value;
  var _url = "http://webservices.qgj.cn/cldd/apply/reqsites.ashx?sites=" + _personnum + "&starttime=" + _starttime;
  var _process = new Process(_url, 'get');
  showMask();
  closeformmaskT();
  _process.delegate = function(_request) {
    if (_request.status == 200) {
      hideMask();
      showformmask();
      var _xml = new Xml(_request.responseXML);
      var _carnum = _xml.documentElement.selectSingleNode('response').getAttribute("CarNum");
      var _sites = _xml.documentElement.selectSingleNode('response').getAttribute("sites");
      var _MaxSites = _xml.documentElement.selectSingleNode('response').getAttribute("MaxSites");
      $("maxsites").value = _MaxSites;
      if (_sites == 'false') {
        if (_personnum > parseInt(_MaxSites)) {
          if (_carnum == parseInt('1')) {
            alert("当前最多出行人数为：" + _MaxSites + "请删减用车人数!");
          } else {
            alert("当前最多出行人数为：" + _MaxSites + "请删减用车人数并再申请一辆车!");
          }
        }
      } else {
        CheckCarNum();
      }
    }
  }
  Thread.append(_process);
}
//判断用车人数是否符合标准
function RecheckPerson() {
  var _personnum = $("peronNum").value;
  var _starttime = $("time").value;
  var _url = "http://webservices.qgj.cn/cldd/apply/reqsites.ashx?sites=" + _personnum + "&starttime=" + _starttime;
  var _process = new Process(_url, 'get');
  showMask();
  _process.delegate = function(_request) {
    if (_request.status == 200) {
      hideMask();
      var _xml = new Xml(_request.responseXML);
      var _carnum = _xml.documentElement.selectSingleNode('response').getAttribute("CarNum");
      var _sites = _xml.documentElement.selectSingleNode('response').getAttribute("sites");
      var _MaxSites = _xml.documentElement.selectSingleNode('response').getAttribute("MaxSites");
      $("maxsites").value = _MaxSites;
      if (_sites == 'false') {
        if (_personnum > parseInt(_MaxSites)) {
          if (_carnum == parseInt('1')) {
            alert("当前最多出行人数为：" + _MaxSites + "请删减用车人数!");
          } else {
            alert("当前最多出行人数为：" + _MaxSites + "请删减用车人数并再申请一辆车!");
          }
        }
      }
    }
  }
  Thread.append(_process);
}
//提交按钮
function ReCheckNum() {
  var _destination = $("destination").value;
  var substr = "杭州";
  var _starttime = $("time").value;
  var _day = $("day").value;
  var _personnum = $("peronNum").value;
  var _applytel = $("AppliedTel").value;
  var _availablecar = $("availableCar").value;
  if (_destination == '') {
    alert("目的地不能为空!");
    return false;
  }
  //检查是否包含杭州字符串
  function isContains(str, substr) {
    return str.indexOf(substr) >= 0;
  }
  if (isContains(_destination, substr)) {
    alert('!输入的目的地不能为杭州市内');
    return false;
  };
  if (_starttime == '') {
    alert("开始时间不能为空！");
    return false;
  }
  if (_day == '') {
    alert("用车天数不能为空");
    return false;
  }
  if (_personnum == '') {
    alert("用车人数不能为空!");
    return false;
  }
  if ($("who").value == '') {
    alert("用车人姓名不能为空!");
    return false;
  }
  if (_applytel == '') {
    alert("手机号不能为空!");
    return false;
  }
  if (isNaN(_applytel)) {
    alert("手机号码只能是数字(短号与手机号只能填一个!)");
    return false;
  }
  if (Trim($("reason").value )== '') {
    alert("申请理由不能为空!");
    return false;
  }
  if (_availablecar == 'null') {
    alert("请先查询是否有可用车辆!");
    return false;
  }
  if (_availablecar == 'no') {
    alert("当前无可用车辆不能申请!");
    return false;
  }
  $("btnComfirmApply").disabled = true;
  var _name = $("applyName").innerHTML;
  var _account = $("applyAccount").innerHTML;
  var _form = $("addform");
  var _url = "http://webservices.qgj.cn/cldd/apply/applyreview.ashx?account=" + _account + "&name=" + _name;
  _url = encodeURI(_url);
  var add = new AjaxForm(_form, "post", _url);
  showMask();
  add.response = function(_request) {
    hideMask();
    var _xml = new Xml(_request.responseXML);
    var _result = _xml.documentElement.selectSingleNode('response').getAttribute("affect")
    var _title = _xml.documentElement.selectSingleNode('response').getAttribute("title")
    if (_result == "0") {
      alert(_title);
      window.location.href = "http://webservices.qgj.cn/cldd/apply/index.ashx";
    } else if (_result == "1") {
      alert(_title);
      window.location.href = "http://webservices.qgj.cn/cldd/apply/index.ashx";
    } else if (_result == "-1") {
      alert(_title);
      $("btnComfirmApply").disabled = false;
      return false;
    }
  }
  if ($("peronNum").value > $("maxsites").value) {
    hideMask();
    alert("请删减用车人数!");
  } else {
    add.submit();
  }
}
//显示遮罩层
function showMask() {
  $("cover").style.cssText = 'display: block';
  $("wrap-cover").style.cssText = 'display: block';
  $("wrap-cover").style.opacity = "0.6";
}

function hideMask() {
  $("cover").style.cssText = 'display: none';
  $("wrap-cover").style.cssText = 'display: none';
  $("wrap-cover").style.opacity = "1";
}
//显示预约表单遮罩层
function showformmask() {
  $("reqapplydiv").style.cssText = 'display: block';
  $("wrap-cover").style.cssText = 'display: block';
  $("wrap-cover").style.opacity = "0.6";
}

function closeformmask() {
  $("reqapplydiv").style.cssText = 'display: none';
  $("wrap-cover").style.cssText = 'display: none';
  $("wrap-cover").style.opacity = "1";
}
//在查询车辆时的遮罩层
function showformmaskT() {
  $("reqapplydiv").style.cssText = 'display: block';
  $("wrap-cover").style.cssText = 'display: block';
  $("wrap-cover").style.opacity = "0.6";
}

function closeformmaskT() {
  $("reqapplydiv").style.cssText = 'display: none';
  $("wrap-cover").style.cssText = 'display: block';
  $("wrap-cover").style.opacity = "0.6";
}