<?xml version="1.0" encoding="UTF-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8"/>
  <xsl:template name="page" match="/">
    <xsl:for-each select="application">
      <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
          <meta charset="UTF-8"/>
          <title>车辆调度系统 beta v1.0</title>
          <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
          <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
          
        </head>
        <xsl:for-each select="response">
          <body>
            <!--隐藏的span储存值-->
            <span id="carapplyid" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
            <span id="guid" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span> 
            <span id="startTimeId" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
            <span id="daysId" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
            <span id="TripNumId" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
            <span id="changeCarId" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
            <span id="maskchangeCarId" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
            <!--内容-->   
            <div style="width:100%;height:100%;">
              <div class="words-div-review">
               <b>待出车列表</b>
               <div>
                 <!--更改车辆按钮参数为车辆申请ID，开始时间ID，天数ID,出行人数-->
                 <xsl:element name="a">
                  <xsl:attribute name="class">btn btn-default changeCarBtn</xsl:attribute>
                  <xsl:attribute name="href">#</xsl:attribute>
                  <xsl:attribute name="id">changeCarBtn</xsl:attribute>
                  <xsl:attribute name="onclick">showChangeCarMask()</xsl:attribute>
                  更换车辆
                </xsl:element>
              </div>
              <hr/>
            </div>
            <table class="table mytable-review">
             <thead>
               <tr>
                 <th><xsl:text><![CDATA[ ]]></xsl:text></th>
                 <th>序号</th>
                 <th>申请人</th>
                 <th>出车时间</th>
                 <th>天数</th>
                 <th>结束时间</th>
                 <th>车牌号</th>
                 <th>目的地</th>
                 <th>司机</th>
                 <th>出行人数</th>                 
                 <th style="width:250px;">操作</th>
               </tr>
             </thead>
             <tbody>
              <xsl:if test="count(//SchemaTable)&lt;1">
                <tr><td colspan="10">当前无任何记录</td></tr>
              </xsl:if>
              <xsl:if test="count(//SchemaTable)&gt;=1">
                <xsl:for-each select="SchemaTable">
                 <tr>
                   <td>
                    <xsl:element name="input">
                      <xsl:attribute name="name">carNumbox</xsl:attribute>
                      <xsl:attribute name="type">checkbox</xsl:attribute>
                      <xsl:attribute name="data-type">checkbox</xsl:attribute>
                      <xsl:attribute name="data-num">num<xsl:value-of select="position()"/></xsl:attribute>
                      <xsl:attribute name="value"><xsl:value-of select="CarOwner"/>,<xsl:value-of select="CarAppliedID"/></xsl:attribute>
                      <xsl:attribute name="data-value"><xsl:value-of select="CarOwner"/></xsl:attribute>
                      <xsl:attribute name="id">checkbox<xsl:value-of select="position()"/></xsl:attribute>
                      <xsl:attribute name="style">display:none;float:right</xsl:attribute>
                    </xsl:element>
                  </td>
                  <td><xsl:value-of select="position()"/></td>
                  <td><xsl:value-of select="CarAppliedName"/></td>
                  <td><xsl:value-of select="StartTime"/>
                  <p>
                    <xsl:if test="StartWeek='Monday'">星期一</xsl:if>
                    <xsl:if test="StartWeek='Tuesday'">星期二</xsl:if>
                    <xsl:if test="StartWeek='Wednesday'">星期三</xsl:if>
                    <xsl:if test="StartWeek='Thursday'">星期四</xsl:if>
                    <xsl:if test="StartWeek='Friday'">星期五</xsl:if>
                    <xsl:if test="StartWeek='Saturday'">星期六</xsl:if>
                    <xsl:if test="StartWeek='Sunday'">星期天</xsl:if>
                  </p></td>
                  <td><xsl:value-of select="Days"/></td>
                  <td><xsl:value-of select="EndTime"/>
                  <p>
                    <xsl:if test="EndWeek='Monday'">星期一</xsl:if>
                    <xsl:if test="EndWeek='Tuesday'">星期二</xsl:if>
                    <xsl:if test="EndWeek='Wednesday'">星期三</xsl:if>
                    <xsl:if test="EndWeek='Thursday'">星期四</xsl:if>
                    <xsl:if test="EndWeek='Friday'">星期五</xsl:if>
                    <xsl:if test="EndWeek='Saturday'">星期六</xsl:if>
                    <xsl:if test="EndWeek='Sunday'">星期天</xsl:if>
                  </p></td>
                  <td><xsl:value-of select="LicenseID"/></td>
                  <td><xsl:value-of select="Destination"/></td>
                  <td><xsl:value-of select="CarOwner"/></td>
                  <td><xsl:value-of select="TripNum"/></td>
                  <td style="width:200px">
                   <xsl:if test="AppliedStatue='待出车'">
                    <xsl:element name="a">
                      <xsl:attribute name="href">#</xsl:attribute>
                      <xsl:attribute name="id">btn-startTrip</xsl:attribute>
                      <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                      <xsl:attribute name="onclick">startTrip('<xsl:value-of select="CarAppliedID"/>')</xsl:attribute>
                      开始行程 
                    </xsl:element>
                    <xsl:element name="a">
                      <xsl:attribute name="href">#</xsl:attribute>
                      <xsl:attribute name="id">btn-cancelTrip</xsl:attribute>
                      <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                      <xsl:attribute name="onclick">cancelTrip('<xsl:value-of select="CarAppliedID"/>')</xsl:attribute>
                      取消申请 
                    </xsl:element>
                     <xsl:element name="a">
                      <xsl:attribute name="href">#</xsl:attribute>
                      <xsl:attribute name="class">changefreecarclass</xsl:attribute>
                      <xsl:attribute name="style">margin-right:12px;display:none;</xsl:attribute>
                      <xsl:attribute name="onclick">showchangefreecar('<xsl:value-of select="CarAppliedID"/>')</xsl:attribute>
                      更换车辆 
                    </xsl:element>
                  </xsl:if>
                  <xsl:if test="AppliedStatue='待处理'">
                    <xsl:element name="a">
                      <xsl:attribute name="href">#</xsl:attribute>
                      <xsl:attribute name="id">btn-cancelTrip</xsl:attribute>
                      <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                      <xsl:attribute name="onclick">cancelTrip('<xsl:value-of select="CarAppliedID"/>')</xsl:attribute>
                      取消申请 
                    </xsl:element>
                  </xsl:if>
                  <xsl:if test="AppliedStatue='开始'">
                    <xsl:element name="a">
                      <xsl:attribute name="href">#</xsl:attribute>
                      <xsl:attribute name="id">2</xsl:attribute>
                      <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                      <xsl:attribute name="onclick">endTrip('<xsl:value-of select="CarAppliedID"/>')</xsl:attribute>
                      结束行程
                    </xsl:element>
                    <xsl:element name="a">
                      <xsl:attribute name="href">#</xsl:attribute>
                      <xsl:attribute name="id">3</xsl:attribute>
                      <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                      <xsl:attribute name="onclick">showDelayMask('<xsl:value-of select="CarAppliedID"/>')</xsl:attribute>
                      申请延期
                    </xsl:element>
                  </xsl:if>
                </td>
              </tr>
            </xsl:for-each>
          </xsl:if>
        </tbody>
      </table>
      <div style="width:94%;margin:0 auto;">
        <span id="driverOne" style="display:none;">null</span>
        <span id="driverTwo" style="display:none;">null</span>
        <span id="driverOneId" style="display:none;">null</span>
        <span id="driverTwoId" style="display:none;">null</span>
        <input type="button" class="btn btn-default" id="btnCancel" value="取消" style="display:none;float:right;" onclick="boxcancel()"/>
        <input type="button" class="btn btn-default" id="btnConfirm" value="确定" style="display:none;float:right;margin-right:15px;" onclick="comfirmHandup()"/>
      </div>
    </div>
  </body>
</xsl:for-each>
</html>
</xsl:for-each>
</xsl:template>
</xsl:transform>