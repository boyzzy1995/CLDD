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
            <!--内容-->   
            <div style="width:100%;height:100%;">
              <div class="words-div-review">
               <b>车辆维修列表</b>
               <div>
                 <!--更改车辆按钮参数为车辆申请ID，开始时间ID，天数ID,出行人数-->
                 <xsl:element name="a">
                  <xsl:attribute name="class">btn btn-default changeCarBtn</xsl:attribute>
                  <xsl:attribute name="href">#</xsl:attribute>
                  <xsl:attribute name="style">width:120px;</xsl:attribute>
                  <xsl:attribute name="id">changeCarBtn</xsl:attribute>
                  <xsl:attribute name="onclick">getfixlist('http://webservices.qgj.cn/cldd/car/addfixlist.ashx')</xsl:attribute>
                  添加维修记录
                </xsl:element>
              </div>
              <hr/>
            </div>
            <table class="table mytable-review">
             <thead>
               <tr>
                 <th>序号</th>
                 <th>开始时间</th>
                 <th>结束时间</th>
                 <th>车牌号</th>
                 <th>当前状态</th>               
                 <th>操作</th>
               </tr>
             </thead>
             <tbody>
              <xsl:if test="count(//SchemaTable)&lt;1">
                <tr><td colspan="10">当前无任何记录</td></tr>
              </xsl:if>
              <xsl:if test="count(//SchemaTable)&gt;=1">
                <xsl:for-each select="SchemaTable">
                 <tr>
                  <td><xsl:value-of select="position()"/></td>
                  <td><xsl:value-of select="substring-before(StartTime,' ')"/>
                  <p>
                    <xsl:if test="StartWeek='Monday'">星期一</xsl:if>
                    <xsl:if test="StartWeek='Tuesday'">星期二</xsl:if>
                    <xsl:if test="StartWeek='Wednesday'">星期三</xsl:if>
                    <xsl:if test="StartWeek='Thursday'">星期四</xsl:if>
                    <xsl:if test="StartWeek='Friday'">星期五</xsl:if>
                    <xsl:if test="StartWeek='Saturday'">星期六</xsl:if>
                    <xsl:if test="StartWeek='Sunday'">星期天</xsl:if>
                  </p></td>
                  <td><xsl:value-of select="substring-before(EndTime,' ')"/>
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
                  <td><xsl:value-of select="statue"/></td>

                  <td style="width:200px">

                     <xsl:element name="a">
                      <xsl:attribute name="href">#</xsl:attribute>
                      <xsl:attribute name="onclick">cancelfix('<xsl:value-of select="FixID"/>')</xsl:attribute>
                      取消维修
                    </xsl:element>

                  <xsl:if test="statue='开始'">
                     <xsl:element name="a">
                      <xsl:attribute name="href">#</xsl:attribute>
                      <xsl:attribute name="onclick">endfix('<xsl:value-of select="FixID"/>')</xsl:attribute>
                      结束维修
                    </xsl:element>
                  </xsl:if>
                </td>
              </tr>
            </xsl:for-each>
          </xsl:if>
        </tbody>
      </table>
    </div>
  </body>
</xsl:for-each>
</html>
</xsl:for-each>
</xsl:template>
</xsl:transform>