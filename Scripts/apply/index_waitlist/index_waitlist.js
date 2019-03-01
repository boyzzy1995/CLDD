//让内容高度自适应方法，传递参数依次为基准高度，要改变的高度，基准高度的值。
function heightAuto(basicHeightId,changeHeightId,basicHeight){
	var _changeHeight=$(changeHeightId).height();
	if(parseInt(basicHeight)>parseInt(_changeHeight)){
		$(changeHeightId).css("height", basicHeight);
	}
}
window.onload=function(){
    //调用自适应方法
    heightAuto("#wrap-content","#content-row",$("#wrap-content").height());
    heightAuto("body","#mycontainer",$("#wrap-content").height()+140);
    heightAuto("#content-row","#wrap-content",$("#content-row").height());
    /*var _height=$("#wrap-content").height()+140;
    $("#mycontainer").css("height",_height);*/
    /*heightAuto("body","#hbody-row",$("body").height()-140);*/
}