﻿<?xml version="1.0" encoding="UTF-8"?>
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
            <div class="wrap-content"><!-- 控制外部灰色块div-->
              <div class="mycont"><!-- 控制外部灰色块div-->
                <div class="mycol"><!-- 控制外部灰色块div-->
                  <!--表单form-->
                  <form class="form-horizontal myform" name="addform" role="form" id="driveraddform">
                    <div class="top-words" >
                      <div class="content-words">
                        <div class="words-div">
                          <b>添加司机</b>
                        </div>
                      </div>
                      <hr/>
                    </div>
                    <div class="form-group">
                      <label for="place" class="col-sm-2 control-label">司机姓名:</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" name="DriverName" id="DriverName" />
                      </div>
                    </div>

                    <div class="form-group">
                      <label class="col-sm-2 control-label">司机账号:</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" name="DriverAccount" id="DriverAccount"/>
                      </div>
                    </div>
                    <div class="form-group">
                      <label class="col-sm-2 control-label">司机联系方式:</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" id="CarTelephone" name="CarTelephone"/>
                      </div>
                    </div>
                    <div class="form-group">
                      <label for="who" class="col-sm-2 control-label">车牌:</label>
                      <div class="col-sm-10">
                         <xsl:element name="select">
                            <xsl:attribute name="id">LicenseID</xsl:attribute>
                            <xsl:attribute name="name">LicenseID</xsl:attribute>
                            <xsl:attribute name="class">form-control</xsl:attribute>
			    <option value="">请选择车牌</option>
                            <xsl:for-each select="SchemaTable">
                            <xsl:element name="option">
                            <xsl:attribute name="value"><xsl:value-of select="LicenseID"/></xsl:attribute>
                            <xsl:value-of select="LicenseID"/>
                            </xsl:element>  
                            </xsl:for-each>       
                           </xsl:element>
                      </div>
                    </div>

                    <div class="bottom-btn">
                      <div class="bottom-btn-inside">
                        <span id="maxsites" display="none"><xsl:text><![CDATA[ ]]></xsl:text></span>
                        <xsl:element name="input">
                          <xsl:attribute name="type">button</xsl:attribute>
                          <xsl:attribute name="class">btn btn-default</xsl:attribute>
                          <xsl:attribute name="style">margin-top:10px</xsl:attribute>
                          <xsl:attribute name="value">确认添加</xsl:attribute>
                          <xsl:attribute name="onclick">addDriver()</xsl:attribute>
                        </xsl:element>
                      </div>
                    </div>

                  </form>
                </div>
              </div>
            </div>
          </body>
        </xsl:for-each>
      </html>
    </xsl:for-each>
  </xsl:template>
</xsl:transform>