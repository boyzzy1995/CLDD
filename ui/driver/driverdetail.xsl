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
            <div class="wrap-content"><!-- 控制外部灰色块div-->
              <div class="mycont"><!-- 控制外部灰色块div-->
                <div class="mycol"><!-- 控制外部灰色块div-->
                  <!--表单form-->
                  <form class="form-horizontal myform" role="form" id="addform">
                    <div class="top-words" >
                      <div class="content-words">
                        <div class="words-div">
                          <b>司机详情</b>
                        </div>
                      </div>
                      <hr/>
                    </div>
                    <xsl:for-each select="SchemaTable"> 

                      <div class="form-group">
                        <label for="time" class="col-sm-6 control-label">司机姓名:</label>
                        <div class="col-sm-6">
                         <span><xsl:value-of select="DriverName"/></span>
                       </div>
                     </div>

                     <div class="form-group">
                      <label class="col-sm-6 control-label">司机账号:</label>
                      <div class="col-sm-6">
                       <span><xsl:value-of select="DriverAccount"/></span>
                     </div>
                   </div>
                   <div class="form-group">
                    <label class="col-sm-6 control-label">联系方式:</label>
                    <div class="col-sm-6">
                     <span><xsl:value-of select="CarTelephone"/></span>
                   </div>
                 </div>    
                 <div class="form-group">
                  <label for="place" class="col-sm-6 control-label">车牌号:</label>
                  <div class="col-sm-6">
                    <span><xsl:value-of select="LicenseID"/></span>
                    <xsl:if test="LicenseID!=' '">
                      <xsl:element name="input">
                        <xsl:attribute name="type">button</xsl:attribute>
                        <xsl:attribute name="class">btn btn-default</xsl:attribute>
                        <xsl:attribute name="id"><xsl:value-of select="DriverID"/></xsl:attribute>
                        <xsl:attribute name="value">解除绑定</xsl:attribute>
                        <xsl:attribute name="onclick">unlockLicense('<xsl:value-of select="DriverID"/>')</xsl:attribute>
                      </xsl:element>  
                    </xsl:if>
                  </div>
                </div>
              </xsl:for-each>
              <div class="form-group">
                <label class="col-sm-6 control-label"><xsl:text><![CDATA[ ]]></xsl:text></label>
                <div class="col-sm-6">
                 <!-- <span><xsl:value-of select="CarTelephone"/></span> -->
                 <div class="bottom-btn">
                  <div class="bottom-btn-inside">
                    <span id="maxsites" display="none"><xsl:text><![CDATA[ ]]></xsl:text></span>
                    <xsl:element name="input">
                      <xsl:attribute name="type">button</xsl:attribute>
                      <xsl:attribute name="class">btn btn-default</xsl:attribute>
                      <xsl:attribute name="id"><xsl:value-of select="DriverID"/></xsl:attribute>
                      <xsl:attribute name="value">修改</xsl:attribute>
                      <xsl:attribute name="onclick">HrefToMotifyDriver('<xsl:value-of select="SchemaTable/DriverID"/>')</xsl:attribute>
                    </xsl:element>  
                  </div>
                </div>
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