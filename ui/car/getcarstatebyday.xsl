<?xml version="1.0" encoding="UTF-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:include href="http://webservices.qgj.cn/cldd/ui/apply/header.xsl"/>
  <xsl:include href="http://webservices.qgj.cn/cldd/ui/apply/footer.xsl"/>
  <xsl:output method="html" encoding="utf-8"/>
  <xsl:template name="page" match="/">
    <xsl:for-each select="application">
      <xsl:variable name="name" select="profile/SchemaTable/TokenName"/>
      <xsl:variable name="account" select="profile/SchemaTable/TokenAccount"/>
      <xsl:variable name="datanum" select="//response/Row/DayNums"/>
      <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
          <meta charset="UTF-8"/>
          <title>车辆调度系统 beta v1.0</title>
          <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
          <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
        </head>
        <xsl:for-each select="response">
          <body>
            <div class="warp-table">
              <table class="table report-table" style="width:auto;margin:0 auto"> 
               <thead>
                 <tr>
                   <th style="width:130px"><xsl:text><![CDATA[ ]]></xsl:text></th>
                   <xsl:for-each select="//response/Row/Content/OneDay">
                    <xsl:if test="position()&lt;=$datanum">
                     <th style="width:130px">
                      <p><xsl:value-of select='substring-before(Day," ")'/></p>
                      <xsl:text><![CDATA[ ]]></xsl:text><xsl:text><![CDATA[ ]]></xsl:text>
                      <xsl:if test="Week='Monday'">
                      星期一
                      </xsl:if>
                      <xsl:if test="Week='Tuesday'">
                      星期二
                      </xsl:if>
                      <xsl:if test="Week='Wednesday'">
                      星期三
                      </xsl:if>
                      <xsl:if test="Week='Thursday'">
                      星期四
                      </xsl:if>
                      <xsl:if test="Week='Friday'">
                      星期五
                      </xsl:if>
                      <xsl:if test="Week='Saturday'">
                      星期六
                      </xsl:if>
                      <xsl:if test="Week='Sunday'">
                      星期日
                      </xsl:if>
                    </th>
                  </xsl:if>
                </xsl:for-each>
              </tr>
            </thead>
            <tbody>
              <xsl:for-each select="//response/Row">
               <tr>
                 <td><xsl:value-of select="LicenseID"/></td>
                 <xsl:for-each select="Content/OneDay">
                  <xsl:if test="position()&lt;=$datanum">
                   <td><xsl:value-of select="State"/></td>
                 </xsl:if>
               </xsl:for-each>
             </tr>
           </xsl:for-each>
         </tbody>
       </table>
     </div>
   </body>
 </xsl:for-each>
</html>
</xsl:for-each>
</xsl:template>
</xsl:transform>