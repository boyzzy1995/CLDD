<?xml version="1.0" encoding="UTF-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:include href="http://webservices.qgj.cn/cldd/ui/apply/header.xsl"/>
  <xsl:include href="http://webservices.qgj.cn/cldd/ui/apply/footer.xsl"/>
  <xsl:output method="html" encoding="utf-8"/>
  <xsl:template name="page" match="/">
    <xsl:for-each select="application">
      <xsl:variable name="name" select="profile/SchemaTable/TokenName"/>
      <xsl:variable name="account" select="profile/SchemaTable/TokenAccount"/>
      <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
          <meta charset="UTF-8"/>
          <title>车辆调度系统 beta v1.0</title>
          <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
          <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/lib/bootstrap.min.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/lib/calender.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/style/head.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/apply/backstage_homepage/backstage_homepage.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/apply/getapplyreviewlist/getapplyreviewlist.css]]></xsl:attribute>
          </xsl:element>

          
          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/car/getcarlist/getcarlist.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/car/reqadd/reqadd.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/report/reqcar_mounth_report/reqcar_mounth_report.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/report/car_mounth_report/car_mounth_report.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="link">
            <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
            <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
            <xsl:attribute name="href"><![CDATA[/cldd/style/style/footer.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/prototype.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/jquery.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/bootstrap.min.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>  

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/calender.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>  

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/backstage_homepage/function.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/car/reqcarstate/reqcarstate.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/manapplycar/manapplycar.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>
          
          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/dispatcher/getlsdispatcher.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/getapplyreviewlist/getapplyreviewlist.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>
          
          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/getapply_waitlist/getapply_waitlist.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/car/reqcarmotify/reqcarmotify.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/car/reqadd/reqadd.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/car/getcarlist/getcarlist.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/driver/req_add_driver/req_add_driver.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/driver/reqmotifydriver/reqmotifydriver.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/driver/getdriverlist/getdriverlist.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/driver/driverdetail/driverdetail.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/report/car_mounth_report/car_mounth_report.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/report/print_car_mounth/print_car_mounth.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/report/reqcar_mounth_report/reqcar_mounth_report.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/car/getfixedlist/getfixedlist.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/car/addfixlist/addfixlist.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

        </head>
        <xsl:for-each select="response">
          <body>
           <span id="applyName" style="display:none"><xsl:value-of select="$name"/></span>
           <span id="applyAccount" style="display:none"><xsl:value-of select="$account"/></span>
           <span id="reasonguid" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
           <span id="de" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
           <span id="firstSize" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
           <span id="dateid" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
           <!--遮罩层-->
           <div class="wrap-cover" id="wrap-cover">
            <xsl:text><![CDATA[ ]]></xsl:text>
          </div>
          <!--加载的时候遮罩层-->
          <div class="cover" id="cover">
            <div class="cover-words center-vertical">
              请稍后....
            </div>
          </div>
          <!--更换车牌号的遮罩层-->
          <div id="changeCar" class="mask">
            <div class="mask-close">
              <a href="#" onclick="closeChangeMask();">关闭</a>
            </div>
            <div class="mask-content">
             <form name="reasonform" id="LicenseIDform">
              <label class="col-md-2 control-label" style="padding:0">车牌号:</label>
              <xsl:element name="select">
                <xsl:attribute name="id">mask-LicenseID</xsl:attribute>
                <xsl:attribute name="name">LicenseID</xsl:attribute>
                <xsl:attribute name="class">form-control</xsl:attribute>
                <xsl:text><![CDATA[ ]]></xsl:text>
              </xsl:element>
              <input type="button" class="btn btn-default mask-content-button" onclick="confirmChange()" value="确定"/>
            </form>
          </div>
        </div> 
        <!--申请延期的遮罩层-->
        <div id="delayDate" class="mask">
         <div class="mask-close">
           <a href="#" onclick="closeDelayMask();">关闭</a>
         </div>
         <div class="mask-content">
           <form name="reasonform" id="delayform">
             <label class="control-label">延期天数:</label>
             <input type="text" class="form-control" id="delayDateinput" name="delayDate"/>
             <input type="button" class="btn btn-default mask-content-button" onclick="confirmDelay()" value="确定"/>
           </form>
         </div>
       </div>  
       <!--填写拒绝理由的遮罩层-->
       <div id="mask" class="mask-refuse">
         <div class="mask-close">
           <a href="#" onclick="closeBg();">关闭</a>
         </div>
         <div class="mask-content">
           <form name="reasonform" id="reasonform">
             <label class="control-label">拒绝理由:</label>
             <textarea class="form-control" id="refuseReason" name="reason" rows="10"><xsl:text><![CDATA[ ]]></xsl:text></textarea>
             <input type="button" class="btn btn-default mask-content-button" onclick="confirmReason()" value="确定"/>
           </form>
         </div>
       </div>
       <!--填写具体时间的遮罩层--> 
       <div class="mask-time" id="mask-time">
        <!--关闭按钮-->
        <div class="mask-close">
          <a href="#" onclick="closeMaskTime();">关闭</a>
        </div>
        <div class="mask-content">
          <div class="row" style="text-align:center;">
            <span id="date" class="dataSpan" style="color:coral"><xsl:text><![CDATA[ ]]></xsl:text></span>
            <span id="datetime" class="dataSpan" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
          </div>
          <div class="form-group" style="text-align:center;padding-top:20px;margin-bottom: 30px;">
            <label for="place" class="col-sm-6 control-label" style="text-align: -webkit-center;">几点:</label>
            <div class="col-sm-5">
              <select id="hours" class="hourSelect">
                <xsl:text><![CDATA[ ]]></xsl:text>
              </select>
              <span class="hourSpan">时</span>
            </div>
          </div>
          <div class="form-group" style="text-align:center;padding-top:20px;">
            <label for="place" class="col-sm-6 control-label" style="text-align: -webkit-center;">几分:</label>
            <div class="col-sm-5">
              <select id="minutes" class="minuteSelect">
                <xsl:text><![CDATA[ ]]></xsl:text>
              </select>
              <span class="minuteSpan">分</span>
            </div>
          </div>
          <div class="btn-div" style="bottom: 10px;position: absolute;width: 74%;">
            <input type="button" class="btn btn-warning" onclick="confirmTime()" value="确定" style="width:100%"/>
          </div>
        </div>
      </div>
      <!--填写维修时间表单遮罩层-->
      <div id="repairTime" class="mask" >
        <div class="mask-close">
          <!-- <a href="#" onclick="closeformmask();">关闭</a> -->
          <xsl:text><![CDATA[ ]]></xsl:text>
        </div>
        <div class="mask-content">
         <form name="reasonform" id="reqapply-form" role="form">
          <div class="form-group">
            <label class="col-sm-2 control-label" style="margin-top: 5px;">开始时间:</label>
            <div class="col-sm-10">
              <input type="text" class="form-control" id="mask-starttime" name="Days"  />
            </div>
          </div>
          <div class="form-group">
            <label class="col-sm-2 control-label" style="margin-top:20px;">结束时间:</label>
            <div class="col-sm-10" style="margin-top:15px;">
              <input type="text" class="form-control" id="mask-endtime" name="TripNum"/>
            </div>
          </div>
          <input type="button" class="btn btn-default mask-content-button" style="margin-right: 15px;" onclick="confirmRepair()" value="确定"/>
        </form>
      </div>
    </div>
    <!--弹出更换车辆选择按钮-->
    <div id="selectChangeCar" class="mask" style="width:40%;left:30%">
      <div class="mask-close">
        <a href="#" onclick="closeChangeCarMask();">关闭</a>
        <xsl:text><![CDATA[ ]]></xsl:text>
      </div>
      <div class="mask-content" style="text-align:center;margin-top:40px;">
        <input type="button" class="btn btn-default" onclick="showchangefreecarbtn()" value="更换空闲车辆"/>
        <input type="button" class="btn btn-default" onclick="showChangeCheckbox()" value="与其他出车记录进行车辆互换" />
      </div>
    </div>
    <!--更换空闲车牌号的遮罩层-->
    <div id="changefreeCarMask" class="mask" style="height:180px;">
      <div class="mask-close">
        <a href="#" onclick="closechangefreeCarMask();">关闭</a>
      </div>
      <div class="mask-content" >
       <form name="reasonform" id="freeLicenseIDform">
        <label class="col-md-2 control-label" style="padding:0">车牌号:</label>
        <xsl:element name="select">
          <xsl:attribute name="id">mask-freecarLicenseID</xsl:attribute>
          <xsl:attribute name="name">LicenseID</xsl:attribute>
          <xsl:attribute name="class">form-control</xsl:attribute>
          <xsl:text><![CDATA[ ]]></xsl:text>
        </xsl:element>
        <input type="button" class="btn btn-default mask-content-button" onclick="confirmChangelicenseID()" value="确定"/>
      </form>
    </div>
  </div> 
  <div class="container testcontainer" id="testcontainer"> 
    <!--    头部 -->
    <div class="head">
     <div class="row row-head-row">
      <div class="col-md-12 mycol-md-12">
        <h3 style="height: 35px;"> 
          车辆调度系统
        </h3>
        <div class="top-words-content">
          <font class="top-words">浙江省钱塘江管理局勘测设计院</font>
        </div>
        <div style="float:right;z-index:500;position:absolute;right:25px;top:20px;">
         <a href="http://webservices.qgj.cn/cldd/apply/index.ashx" class="btn btn-default">
           <span class="glyphicon glyphicon-home"></span>主页
         </a>
       </div>
     </div>
   </div>
 </div>
 <!--内容-->
 <div class="hbody" id="hbody">
  <div class="hbody-row" id="hbody-row">
    <!--左边的导航栏-->
    <div class="col-xs-2 col-sm-2 col-md-2 col-lg-1 content-left" id="content-left">
     <h4>管理员:<xsl:value-of select="$name"/></h4>
     <ul class="myul">
       <li>
         <a href="#" onclick="show('#showId','#img1','#span1')"><span id="span1">预约管理<img id="img1" src="../Image/picon.png"/></span></a>
         <dl class="mydl" id="showId">
           <dd><a href="#" onclick="getlist('http://webservices.qgj.cn/cldd/apply/getapplyreviewlist.ashx');return false;">预约管理</a></dd>
           <dd><a href="#" onclick="getlist('http://webservices.qgj.cn/cldd/apply/manapplycar.ashx');return false;">人工分配车辆</a></dd>
           <dd><a href="#" onclick="getlist('http://webservices.qgj.cn/cldd/apply/getapply_waitlist.ashx');return false;">待出车列表</a></dd>
         </dl>
       </li>
       <li> 
         <a href="#" onclick="show('#showId1','#img2','#span2')"><span id="span2">车辆管理<img id="img2" src="../Image/picon.png" /></span></a>
         <dl class="mydl" id="showId1">
           <dd><a href="#" onclick="getcarlist('http://webservices.qgj.cn/cldd/car/getcarlist.ashx');return false;">管理车辆</a></dd>
           <dd><a href="#" onclick="toaddcar('http://webservices.qgj.cn/cldd/car/reqadd.ashx');return false;">添加车辆</a></dd>
           <dd><a href="#" onclick="getlist('http://webservices.qgj.cn/cldd/car/reqcarstate.ashx');return false;">查找状态</a></dd>
           <dd><a href="#" onclick="getlist('http://webservices.qgj.cn/cldd/car/getfixedlist.ashx');return false;">车辆维修列表</a></dd>
         </dl>
       </li>
       <li>
         <a href="#" onclick="show('#showId3','#img4','#span4')"><span id="span4">司机管理<img id="img4" src="../Image/picon.png" /></span></a>
         <dl class="mydl" id="showId3">
           <dd><a href="#" onclick="getdriverlist('http://webservices.qgj.cn/cldd/driver/getdriverlist.ashx');return false;">司机管理</a></dd>
           <dd><a href="#" onclick="toaddriver('http://webservices.qgj.cn/cldd/driver/req_add_driver.ashx');return false;">添加司机</a></dd>
         </dl>
       </li>
       <li>
         <a href="#" onclick="show('#showId2','#img3','#span3')"><span id="span3">报表<img id="img3" src="../Image/picon.png" /></span></a>
         <dl class="mydl" id="showId2">
           <dd><a href="#" onclick="toReport('http://webservices.qgj.cn/cldd/report/reqcar_mounth_report.ashx');return false;">车辆每月报表</a></dd>
         </dl>
       </li>
       <li>
         <a href="#" onclick="show('#showId4','#img5','#span5')"><span id="span5">设置<img id="img5" src="../Image/picon.png" /></span></a>
         <dl class="mydl" id="showId4">
           <dd><a href="#" onclick="toDispatcher('http://webservices.qgj.cn/cldd/dispatcher/getLsDispatcher.ashx');return false;">调度员管理</a></dd>
         </dl>
       </li>
     </ul>    
   </div>
   <!--ajax加载的右边内容-->
   <div class="col-xs-10 col-sm-10 col-md-10 col-lg-11 content-right" id="content-right">
     <div id="tableId" style="width:100%;height:100%;">
       <xsl:text><![CDATA[ ]]></xsl:text>
     </div>
   </div>
 </div>
</div>
<!--底部-->
<div class="footer">
  <div class="row myrow">
    <div class="col-sm-12 mycol-sm-12" >
      <font class="footer-words">本系统由xxx公司提供系统支持</font>
    </div>
  </div>
</div>
</div>
</body>
</xsl:for-each>
</html>
</xsl:for-each>
</xsl:template>
</xsl:transform>