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
            <xsl:attribute name="href"><![CDATA[/cldd/style/report/print_car_mounth/print_car_mounth.css]]></xsl:attribute>
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
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/backstage_homepage/function.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

	  <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/report/print_car_mounth/print_car_mounth.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>
          
        </head>
        <xsl:for-each select="response">

          <body>
           <center>
             <xsl:element name="input">
              <xsl:attribute name="type">button</xsl:attribute>
	      <xsl:attribute name="class">printBtn</xsl:attribute>
              <xsl:attribute name="value">打印</xsl:attribute>
              <xsl:attribute name="onclick">printform()</xsl:attribute>
            </xsl:element>

           <div class="warp-table" id="report-table">
             <table class="table" border="1" cellpadding="0" cellspacing="0" style="text-align:center;width:1000px"> 
               <thead>
                 <tr><th colspan="11"><span style="text-align:center;font-size:22px;font-weight:boild;height:30px;line-height:30px;"><xsl:value-of select="//response/@time"/>车牌为"<xsl:value-of select="//response/@licenseID"/>"的出行报表</span></th></tr>
                 <tr>
                   <th>序号</th>
                   <th>申请人</th>
                   <th>开始时间</th>
                   <th>结束时间</th>
		   <th>目的地</th>
                   <th>出行人数</th>
                   <th>出行人</th>
                   <th>车牌</th>
		   <th>司机姓名</th>
                   <th>申请理由</th>
                 </tr>
               </thead>
               <tbody>
                <xsl:for-each select="SchemaTable">
                 <tr>
                   <td><xsl:value-of select="position()"/></td>
                   <td><xsl:value-of select="CarAppliedName"/></td>
                   <td><xsl:value-of select="substring-before(StartTime,' ')"/></td>
                   <td><xsl:value-of select="substring-before(EndTime,' ')"/></td>
                   <td><xsl:value-of select="Destination"/></td>
                   <td><xsl:value-of select="TripNum"/></td>
                   <td><xsl:value-of select="TripMembers"/></td>
                   <td><xsl:value-of select="LicenseID"/></td>
                   <td><xsl:value-of select="CarOwner"/></td>
                   <td><xsl:value-of select="ApplieReson"/></td>
                 </tr>
               </xsl:for-each>
		 <tr>
		   <td colspan="10" style="text-align:right;">当前总公里数为:<xsl:value-of select="//response/@Kilometer"/>公里</td>
		 </tr>
               </tbody>
             </table>
           </div>
          </center>
         </body>

       </xsl:for-each>
     </html>
   </xsl:for-each>
 </xsl:template>
</xsl:transform>