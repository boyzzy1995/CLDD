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
        </head>
        <xsl:for-each select="response">
          <body>

            <div class="words-copy">
             <xsl:element name="input">
              <xsl:attribute name="type">button</xsl:attribute>
              <xsl:attribute name="class">button-dy</xsl:attribute>
              <xsl:attribute name="value">打印</xsl:attribute>
              <xsl:attribute name="onclick">toprintform()</xsl:attribute>
            </xsl:element>

           </div>
           <div class="warp-table">
             <table class="table report-table"> 
               <thead>
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
         </body>
       </xsl:for-each>
     </html>
   </xsl:for-each>
 </xsl:template>
</xsl:transform>