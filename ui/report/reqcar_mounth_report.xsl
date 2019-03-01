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
           <div class="words-div-report">
             <b>报表</b>
             <hr/>
           </div>
           <div class="wrap-words" style="height:90%;">
            <div class="wrap-content-words">
              <form name="report-form" id="report-form" target="report-form">
               <b>请输入年份:</b>
               <input type="text" id="year" class="form-control" name="year" style="width: 100px;display:inline-block;" />
               <b>请输入月份:</b>
               <input type="text" id="mounth" class="form-control" name="mounth" style="width: 70px;display:inline-block;"/>  
               <b style="margin-left: 10%">车牌：</b>            
               <select size="1" id="LicenseID" name="licenseID" class="form-control" style="width: 150px;padding-left: 20px;display:inline-block;" >
                <xsl:for-each select="SchemaTable">
                  <option><xsl:value-of select="LicenseID"/></option>
                </xsl:for-each>
              </select>
              <xsl:element name="input">
                <xsl:attribute name="type">button</xsl:attribute>
                <xsl:attribute name="id">btnbuildform</xsl:attribute>
                <xsl:attribute name="class">btn btn-default button-sq</xsl:attribute>
                <xsl:attribute name="value">生成</xsl:attribute>
                <xsl:attribute name="onclick">buildreport()</xsl:attribute>
              </xsl:element>
            </form>
            <!-- <button type="button"  class="button-sq">
             <abbr style="color: #0a6999">生成</abbr>
           </button> -->
         </div>
         <div id="report-tableId" class="reportTable">
           <xsl:text><![CDATA[ ]]></xsl:text>
         </div>
       </div>
     </body>
   </xsl:for-each>
 </html>
</xsl:for-each>
</xsl:template>
</xsl:transform>