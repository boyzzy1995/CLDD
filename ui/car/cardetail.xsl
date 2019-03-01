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
                          <b>车辆详情</b>
                        </div>
                      </div>
                      <hr/>
                    </div>
                    <div class="form-group">
                      <label for="place" class="col-sm-6 control-label">车牌号:</label>
                      <div class="col-sm-6">
                        <span><xsl:value-of select="SchemaTable/LicenseID"/></span>
                      </div>
                    </div>

                    <div class="form-group">
                      <label for="time" class="col-sm-6 control-label">司机姓名:</label>
                      <div class="col-sm-6">
                       <span><xsl:value-of select="SchemaTable/DriverName"/></span>
                     </div>
                   </div>

                   <div class="form-group">
                    <label class="col-sm-6 control-label">座位数:</label>
                    <div class="col-sm-6">
                     <span><xsl:value-of select="SchemaTable/Sites"/></span>
                   </div>
                 </div>
                 <div class="form-group">
                  <label class="col-sm-6 control-label">保险时间:</label>
                  <div class="col-sm-6">
                   <span><xsl:value-of select="SchemaTable/Insurance"/></span>
                 </div>
               </div>
               <div class="form-group">
                <label class="col-sm-6 control-label">年检时间:</label>
                <div class="col-sm-6">
                 <span><xsl:value-of select="SchemaTable/MOT"/></span>
               </div>
             </div>
             <div class="form-group">
              <label class="col-sm-6 control-label">总公里数:</label>
              <div class="col-sm-6">
               <span><xsl:value-of select="SchemaTable/Kilometers"/></span>
             </div>
           </div>

           <div class="form-group">
            <label class="col-sm-6 control-label"><xsl:text><![CDATA[ ]]></xsl:text></label>
            <div class="col-sm-6">
             <div class="bottom-btn">
              <div class="bottom-btn-inside">
                <span id="maxsites" display="none"><xsl:text><![CDATA[ ]]></xsl:text></span>
                <xsl:element name="input">
                  <xsl:attribute name="type">button</xsl:attribute>
                  <xsl:attribute name="class">btn btn-default</xsl:attribute>
                  <xsl:attribute name="id"><xsl:value-of select="Guid"/></xsl:attribute>
                  <xsl:attribute name="value">修改</xsl:attribute>
                  <xsl:attribute name="onclick">HrefToMotify('<xsl:value-of select="SchemaTable/Guid"/>')</xsl:attribute>
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