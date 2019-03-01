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
             <b>添加车辆维修记录</b>
             <hr/>
           </div>
           <div class="wrap-words" style="height:90%;">
            <div class="wrap-content-words">
              <form name="report-form" id="fix-form" target="report-form">

                <b>车牌：</b>            
                <select size="1" id="LicenseID" name="LicenseID" class="form-control" style="margin-right:50px;width: 150px;padding-left: 20px;display:inline-block;" >
                  <xsl:for-each select="SchemaTable">
                    <option><xsl:value-of select="LicenseID"/></option>
                  </xsl:for-each>
                </select>

                <b>开始时间:</b>
                <input type="text" id="StartTime" class="form-control" name="StartTime" style="width: 150px;display:inline-block;" />

                <b>结束时间:</b>
                <input type="text" id="EndTime" class="form-control" name="EndTime" style="width: 150px;display:inline-block;"/>  

                <xsl:element name="input">
                  <xsl:attribute name="type">button</xsl:attribute>
                  <xsl:attribute name="id">btnbuildform</xsl:attribute>
                  <xsl:attribute name="class">btn btn-default button-sq</xsl:attribute>
                  <xsl:attribute name="value">添加</xsl:attribute>
                  <xsl:attribute name="onclick">addfixlist()</xsl:attribute>
                </xsl:element>
              </form>
            </div>
          </div>
        </body>
      </xsl:for-each>
    </html>
  </xsl:for-each>
</xsl:template>
</xsl:transform>