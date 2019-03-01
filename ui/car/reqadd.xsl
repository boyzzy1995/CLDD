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
                  <form class="form-horizontal myform" name="caraddform" role="form" id="caraddform">
                    <div class="top-words" >
                      <div class="content-words">
                        <div class="words-div">
                          <b>添加车辆</b>
                        </div>
                      </div>
                      <hr/>
                    </div>
                    <div class="form-group">
                      <label for="place" class="col-sm-2 control-label">车牌号:</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" name="licenseID" id="licenseID" />
                      </div>
                    </div>

                    <div class="form-group">
                      <label class="col-sm-2 control-label">座位数:</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" id="sites" name="sites"  />
                      </div>
                    </div>
                    <div class="form-group">
                      <label class="col-sm-2 control-label">保险时间:</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" id="mot" name="mot"/>
                      </div>
                    </div>
                    <div class="form-group">
                      <label for="who" class="col-sm-2 control-label">年检时间:</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" id="insurance" name="insurance"/>
                      </div>
                    </div>
                    <div class="form-group">
                      <label for="who" class="col-sm-2 control-label">总公里数:</label>
                      <div class="col-sm-10">
                        <input type="text" class="form-control" id="kilometers" name="kilometers"/>
                      </div>
                    </div>
                    <div class="bottom-btn">
                      <div class="bottom-btn-inside">
                        <span id="maxsites" display="none"><xsl:text><![CDATA[ ]]></xsl:text></span>
                        <xsl:element name="input">
                          <xsl:attribute name="type">button</xsl:attribute>
                          <xsl:attribute name="class">btn btn-default</xsl:attribute>
                          <xsl:attribute name="value">确认添加</xsl:attribute>
                          <xsl:attribute name="onclick">addcar()</xsl:attribute>
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