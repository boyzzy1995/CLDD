/*JS通用类*/
String.prototype.format=function(){
	var _args=arguments;
	return this.replace(/\{(\d+)\}/g,
		function(m,i){
			return _args[i];
		});
};
var $=function(_id){
	return document.getElementById(_id);
};
var $$=function(_name){
	return document.getElementsByName(_name);
};
var $t=function(_tag){
	return document.getElementsByTagName(_tag);
};

var $b={
	n:function(){
		var _n=navigator.appName.toLowerCase();
		if(_n=='microsoft internet explorer'){
			_n='ie';
		};
		if(window.ActiveXObject || "ActiveXObject" in window){
			_n = 'ie';
		}
		return _n;
	}(),
	v:function(){
		var _v=-1;
		try{
			if(navigator.appName.toLowerCase()=='microsoft internet explorer'){
				_v=navigator.appVersion.match(/MSIE ([/\.\d]+)/i)[1];
			}else{
				if(window.ActiveXObject || "ActiveXObject" in window){
					_v = 11;
				}
			};
			_v=parseFloat(_v);
		}catch(_exc){
			_v=-1;
		};
		return _v;
	}()
};
/*if($b.n=='ie'&&$b.v<8){alert('请升级您的IE浏览器至8.0以上，或停止兼容模式');};*/
var $e=function(_obj){
	var _type=_obj==null?'null':typeof(_obj);
	if(_type=='object'){
		_type=Object.prototype.toString.apply(_obj);
		_type=_type.substring(7,_type.length-1).toLowerCase();
		if(_type=='object'&&_obj.constructor!=Object){
			if(typeof(_obj.prototype.constructor)=='string'&&'classname' in _obj.prototype.constructor){
				_type=_obj.constructor.prototype.classname;
			};
		};
	};
	return _type;
};
if(typeof(XMLDocument)!='undefined'&& $b.n!='ie'){
	XMLDocument.prototype.selectNodes=function(_path,_node){
		if(!_node){
			_node=this;
		};
		var _ns=this.createNSResolver(this.documentElement);
		var _items=this.evaluate(_path,_node,_ns,XPathResult.ORDERED_NODE_SNAPSHOT_TYPE,null);
		var _results=[];
		for(var i=0;i<_items.snapshotLength;i++){
			_results[i]=_items.snapshotItem(i);
		};
		return _results;
	};
	XMLDocument.prototype.selectSingleNode=function(_path,_node){
		if(!_node){
			_node=this;
		};
		var _items=this.selectNodes(_path,_node);
		if(_items.length>0){
			return _items[0];
		}else{
			return null;
		};
	};
	XMLDocument.prototype.transformNode=function(_xslt){
		var _result=null;
		try{
			if(typeof(XSLTProcessor)!='undefined'){
				var _xsl=new XSLTProcessor();
				_xsl.importStylesheet(_xslt);
				_result=_xsl.transformToFragment(this,document);
			};
		}catch(_exc){
			alert(_exc.message);
			_result=null;
		};
		return _result;
	};
};
Element.prototype.selectNodes=function(_path){
	if(this.ownerDocument.selectNodes){
		return this.ownerDocument.selectNodes(_path,this);
	};
};
Element.prototype.selectSingleNode=function(_path){
	if(this.ownerDocument.selectSingleNode){
		return this.ownerDocument.selectSingleNode(_path,this);
	};
};
Element.prototype.addevent=function(_method,_arg,_flag){
	if(typeof(this.addEventListener)!='undefined'){
		this.addEventListener(_method,_arg,_flag);
	}else{
		this.attachEvent('on'+_method,_arg);
	};
};
Element.prototype.delevent=function(_method,_arg,_flag){
	if(typeof(this.removeEventListener)!='undefined'){
		this.removeEventListener(_method,_arg,_flag);
	}else{
		this.detachEvent('on'+_method,_arg);
	};
};

Element.prototype.clear=function(){
	while(this.childNodes.Length>0){
		this.removeChild(this.childNodes[0]);
	}
	this.innerHTML='';
};
Element.prototype.append=function(_html){
	if(typeof(_html)=='string'){
		this.innerHTML+=_html;
	}else{
		this.appendChild(_html);
	};
};
Element.prototype.html=function(_html){
	this.clear();this.append(_html);
};
	//收集表单数据
Element.prototype.init=function(_arg,_chk){
	if(this.tagName.toUpperCase()=='FORM'){
		var _this=this;this.addevent('submit',function(){
			if(_chk!=null){
				_chk();
			}
			FormSub(_this,_arg);
			return false;
		},false);
	};
};
//为防止用户误操作，防止用户多次操作
Element.prototype.lock=function(_bool){
	_bool=_bool==null?true:_bool;
	for(var i=0;i<this.childNodes.length;i++){
		if(this.childNodes[i].tagName&&this.childNodes[i].tagName.match(/input|select|textarea/i)){
			this.childNodes[i].disabled=_bool;
		};
	};
	this.disabled=_bool;
};
if(typeof(XMLHttpRequest)=='undefined'){
	try{
		XMLHttpRequest=function(){
		return new ActiveXObject('Microsoft.XMLHTTP');
		}
	}catch(_exc){
		alert('未发现可用AJAX');
	};
};
if(typeof(DOMParser)=='undefined'){
	try{
		DOMParser=function(){};
		DOMParser.prototype.parseFromString=function(_str,_type){
			var _xml=new ActiveXObject('Msxml2.DOMDocument.5.0');
			_xml.loadXML(_str);
			return _xml;
		};
	}catch(_exc){
		alert('未发现可用XML');
	};
};
if(typeof(XMLSerializer)=='undefined'){
	try{
		XMLSerializer=function(){};
		XMLSerializer.prototype.serializeToString=function(_dom){
			return _dom.xml
		};
	}catch(_exc){
		alert('未发现可用的XML序列对象');
	};
};
var Guid=function(){
	var _hex=['0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f'];
	var _guid='';for(var i=0;i<32;i++){
		if(i==8||i==12||i==16||i==20){
			_guid+='-';
		};
		_guid+=_hex[Math.floor(Math.random()*16)];
	};
	return _guid;
};
var FormSub=function(_tag,_arg,_method,_handler){
	var _form=new AjaxForm(_tag,_method,_handler);
	_form.response=_arg;
	_form.submit();
};
/*XMLDOM*/
var Xml=function(_data){
	if(_data==null){
		this.dom=null;
		this.documentElement=null;
	}else{
		this.dom=_data;
		this.documentElement=this.dom.documentElement
	};
};
Xml.prototype.load=function(_url,_cache){
	_cache=_cache==null?true:_cache;
	var _request=new XMLHttpRequest();
	_request.open('get',_url,false);
	if($b.n=='ie'){
		_request.responseType='msxml-document';
	};
	if(!_cache){
		_request.setRequestHeader('If-Modified-Since','0');
		_request.setRequestHeader('Cache-Control','no-cache');
	};
	_request.send(null);
	if($b.n=='ie'&&$b.v<10){
		this.loadXML(_request.responseText);
	}else{
		this.dom=_request.responseXML;
		this.documentElement=this.dom.documentElement;
	};
};
//用xsl把xml解析成html
Xml.prototype.transForm=function(_xslt){
	var _result=null;
	if(typeof(this.dom.transformNode)=='unknown'){
		var _xsl=new ActiveXObject('Msxml2.FreeThreadedDOMDocument');
		_xsl.loadXML((new XMLSerializer()).serializeToString(_xslt.dom));
		var _pro=new ActiveXObject('Msxml2.XSLTemplate');
		_pro.stylesheet=_xsl;
		var _xpro=_pro.createProcessor();
		_xpro.input=this.dom;
		_xpro.transform();
		_result=_xpro.output;
	}else{
		_result=this.dom.transformNode(_xslt.dom);
	};
	return _result;
};
Xml.prototype.loadXML=function(_txt){
	this.dom=(new DOMParser()).parseFromString(_txt,'text/xml');
	this.documentElement=this.dom.documentElement;
};
Xml.prototype.newDocument=function(){
	this.load('/dom.xml');
};
/*PROCESS*/
var Process=function(_url,_method,_query){
	this.url=_url,this.method=_method,this.query=_query
};
Process.prototype.delegate=function(){};
var Thread=new function(){
	this.pool=new Array();
	this.request=null
};
Thread.append=function(_process){
	this.pool.push(_process);
	this.run();
};
Thread.run=function(){
	if(this.request==null&&this.pool.length>0){
		this.request=new XMLHttpRequest();
		this.request.open(this.pool[0].method,this.pool[0].url,true);
		this.request.setRequestHeader('CONTENT-TYPE','application/x-www-form-urlencoded');
		if($b.n=='ie'){
			this.request.responseType='msxml-document';
		};
		this.request.send(this.pool[0].query);
		this.request.onreadystatechange=function(){
			if(this.readyState==4){
				try{
					Thread.pool[0].delegate(this);
				}catch(_exc){}
				finally{
					Thread.pool.splice(0,1);
					Thread.request=null;
					Thread.run();
				};
			};
		};
	};
};
/*FORM*/
var AjaxForm=function(_tag,_method,_handler){
	this.form=_tag;
	this.method=_method==null?this.form.getAttribute('method'):_method;
	this.handler=_handler==null?this.form.getAttribute('action'):_handler;
	this.query='';
	this.data=new Xml();
	this.data.newDocument();
	var _query=this.data.dom.createElement('query');
	var _value=this.data.dom.createElement('value');
	this.data.documentElement.appendChild(_query);
	this.data.documentElement.appendChild(_value);
};
AjaxForm.prototype.addQuery=function(_name,_value,_exp){
	var _flag=_exp==null?true:_value.match(new RegExp(_exp))!=null;
	if(_flag){
		if(this.data.documentElement.selectSingleNode('query/'+_name)==null){
			var _param=this.data.dom.createElement(_name);
			this.data.documentElement.selectSingleNode('query').appendChild(_param);
		};
		var _query=this.data.dom.createElement(_name);
		var _txt=this.data.dom.createCDATASection(encodeURIComponent(_value));
		_query.appendChild(_txt);
		this.data.documentElement.selectSingleNode('value').appendChild(_query);
	};
	return _flag;
};
AjaxForm.prototype.init=function(){
	var _flag=true;
	var _inputs=this.form.getElementsByTagName('INPUT');
	for(var i=0;i<_inputs.length;i++){
		if(_inputs[i].type.match(/submit|button|file|image|reset/i)){continue;};
		if(_inputs[i].type.match(/radio|checkbox/i)&&_inputs[i].checked==false){continue;};
		if(_inputs[i].getAttribute('name')==null){continue;}
		_flag=this.addQuery(_inputs[i].getAttribute('name'),_inputs[i].value,_inputs[i].getAttribute('accept'));
		if(!_flag){
			alert(_inputs[i].getAttribute('title'));
			break;
		};
	};
	if(_flag){
		var _selects=this.form.getElementsByTagName('SELECT');
		for(var i=0;i<_selects.length;i++){
			_flag=this.addQuery(_selects[i].getAttribute('name'),_selects[i][_selects[i].selectedIndex].value,_selects[i].getAttribute('accept'));
			if(!_flag){
				alert(_selects[i].getAttribute('title'));
				break;
			};
		};
	};
	if(_flag){
		var _textareas=this.form.getElementsByTagName('TEXTAREA');
		for(var i=0;i<_textareas.length;i++){
			_flag=this.addQuery(_textareas[i].getAttribute('name'),_textareas[i].value,_textareas[i].getAttribute('accept'));
			if(!_flag){
				alert(_textareas[i].getAttribute('title'));
				break;
			};
		};
	};
	if(_flag){
		for(var i=0;i<this.data.documentElement.selectSingleNode('query').childNodes.length;i++){
			var _name=this.data.documentElement.selectSingleNode('query').childNodes[i].nodeName;
			if(i>0){
				this.query+='&';
			};
			this.query+=_name+'=';
			for(var j=0;j<this.data.documentElement.selectNodes('value/'+_name+'/text()').length;j++){
				if(j>0){
					this.query+=',';
				};
				this.query+=this.data.documentElement.selectNodes('value/'+_name+'/text()')[j].nodeValue;
			};
		};
	};
	return _flag;
};
AjaxForm.prototype.submit=function(){
	var _flag=this.init();
	if(_flag){
		if(this.method=='get'){
			this.handler+='?'+this.query;this.query=null;
		};
		this.form.lock(true);
		var _process=new Process(this.handler,this.method,this.query);
		_process.delegate=this.response;Thread.append(_process);
	}
};


//事件响应，针对服务器返回的数据进行响应
AjaxForm.prototype.response=function(_request){};