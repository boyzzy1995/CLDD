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
             <b>查找车辆状态</b>
             <hr/>
           </div>
           <div class="wrap-words">
            <div class="wrap-content-words">
              <form name="searchCarState-form" id="searchCarState-form" target="searchCarState-form">
               <b>请输入年份:</b>
               <input type="text" id="year" class="form-control" name="year" style="width: 100px;display:inline-block;" />
               <b>请输入月份:</b>
               <input type="text" id="month" class="form-control" name="month" style="width: 70px;display:inline-block;"/>
               <b>请输入几号:</b>
               <input type="text" id="day" class="form-control" name="day" style="width: 70px;display:inline-block;"/>           
               <xsl:element name="input">
                <xsl:attribute name="type">button</xsl:attribute>
                <xsl:attribute name="id">btnSearchCarState</xsl:attribute>
                <xsl:attribute name="class">btn btn-default button-sq</xsl:attribute>
                <xsl:attribute name="value">查找</xsl:attribute>
                <xsl:attribute name="onclick">searchCarState()</xsl:attribute>
              </xsl:element>
            </form>
          </div>
          <div id="searchCarState-tableId" class="searchCarState-tableId">
           <xsl:text><![CDATA[ ]]></xsl:text>
         </div>
       </div>
     </body>
   </xsl:for-each>
 </html>
</xsl:for-each>
</xsl:template>
</xsl:transform>