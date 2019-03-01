<?xml version="1.0" encoding="UTF-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8"/>
  <xsl:template name="page" match="/">
    <xsl:for-each select="application">
      <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
          <meta charset="UTF-8"/>
          <title>车辆调度系统 beta v1.0</title>
          <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
          <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
        </head>
        <xsl:element name="script">
          <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
          <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
          <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/prototype.js]]></xsl:attribute>
          <xsl:text>   </xsl:text>
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
        <xsl:for-each select="response">
          <body>
            <div class="wrap-content" id="turnTodark">
             <div class="mycont">
               <div class="mycol">
                 <form class="form-horizontal myform" role="form" id="addform">
                  <div class="top-words">
                    <div class="words-div">
                      <b>人工分配车辆</b>
                    </div>
                    <hr/>
                  </div>
                  <div class="form-group">
                    <label for="place" class="col-sm-2 control-label">目的地</label>
                    <div class="col-sm-10">
                      <input type="text" class="form-control" name="Destination" id="destination" />
                    </div>
                  </div>

                  <div class="form-group">
                    <label for="time" class="col-sm-2 control-label">开始时间</label>
                    <div class="col-sm-10">
                      <input type="text" id="manStarttime" class="form-control" name="Starttime"/>
                    </div>
                  </div>

                  <div class="form-group">
                    <label class="col-sm-2 control-label">使用天数</label>
                    <div class="col-sm-10">
                      <input type="text" class="form-control" id="day" name="Days"  />
                    </div>
                  </div>

                  <div class="form-group">
                    <label class="col-sm-2 control-label">用车人数</label>
                    <div class="col-sm-10">
                     <input type="text" class="form-control" id="personNum" name="TripNum" onfocus="checkStarttime()"/>
                   </div>
                 </div>

                 <div class="form-group">
                  <label class="col-sm-2 control-label">申请人账号</label>
                  <div class="col-sm-10">
                   <input type="text" class="form-control" id="ApplyAccount" name="ApplyAccount"/>
                 </div>
               </div>

               <div class="form-group">
                <label for="who" class="col-sm-2 control-label">出行人</label>
                <div class="col-sm-10">
                  <input type="text" class="form-control" id="who" name="TripMembers"/>
                </div>
              </div>

              <div class="form-group">
                <label for="text" class="col-sm-2 control-label">手机号</label>
                <div class="col-sm-10">
                  <input type="text" class="form-control" id="AppliedTel" name="AppliedTel"/>
                </div>
              </div>

              <div class="form-group">
                <label for="text" class="col-sm-2 control-label">车牌号</label>
                <div class="col-sm-10">
                  <xsl:element name="select">
                    <xsl:attribute name="id">apply-LicenseID</xsl:attribute>
                    <xsl:attribute name="name">LicenseID</xsl:attribute>
                    <xsl:attribute name="class">form-control</xsl:attribute>
                    <xsl:element name="option">
                      <xsl:attribute name="value">请先查找车牌</xsl:attribute>
                      请先查找车牌
                    </xsl:element>
                  </xsl:element>
                </div>
              </div>

              <div class="form-group">
                <label for="text" class="col-sm-2 control-label">申请理由</label>
                <div class="col-sm-10">
                  <textarea class="form-control" id="reason" name="ApplieReson" rows="10"><xsl:text><![CDATA[ ]]></xsl:text></textarea>
                </div>
              </div>

              <div class="bottom-btn">
               <div class="bottom-btn-inside">
                <span id="maxsites" display="none"><xsl:text><![CDATA[ ]]></xsl:text></span>
                <input type="button" class="btn btn-default" onclick="SearchLicenseID()" value="查找车牌" ><xsl:text><![CDATA[ ]]></xsl:text></input>
                <input type="button" class="btn btn-default" onclick="ReCheckNum()" value="确认申请" ><xsl:text><![CDATA[ ]]></xsl:text></input>
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