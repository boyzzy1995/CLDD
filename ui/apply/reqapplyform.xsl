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
                <xsl:element name="link">
                    <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                    <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                    <xsl:attribute name="href"><![CDATA[/cldd/style/lib/bootstrap.min.css]]></xsl:attribute>
                </xsl:element>

                <xsl:element name="link">
                    <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                    <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                    <xsl:attribute name="href"><![CDATA[/cldd/style/lib/bootstrap-select.min.css]]></xsl:attribute>
                </xsl:element>

                <xsl:element name="link">
                    <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                    <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                    <xsl:attribute name="href"><![CDATA[/cldd/style/style/head.css]]></xsl:attribute>
                </xsl:element>

                <xsl:element name="link">
                    <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                    <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                    <xsl:attribute name="href"><![CDATA[/cldd/style/style/fix.css]]></xsl:attribute>
                </xsl:element>

                <xsl:element name="link">
                    <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                    <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                    <xsl:attribute name="href"><![CDATA[/cldd/style/apply/reqapplyform/reqapplyform.css]]></xsl:attribute>
                </xsl:element>

                <xsl:element name="link">
                    <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                    <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                    <xsl:attribute name="href"><![CDATA[/cldd/style/style/footer.css]]></xsl:attribute>
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
                    <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/bootstrap.min.js]]></xsl:attribute>
                    <xsl:text>   </xsl:text>
                </xsl:element>

                <xsl:element name="script">
                    <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
                    <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
                    <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/bootstrap-select.min.js]]></xsl:attribute>
                    <xsl:text>   </xsl:text>
                </xsl:element>

                <xsl:element name="script">
                    <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
                    <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
                    <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/reqapplyform/function.js]]></xsl:attribute>
                    <xsl:text>   </xsl:text>
                </xsl:element>

                <xsl:element name="script">
                    <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
                    <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
                    <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/prototype.js]]></xsl:attribute>
                    <xsl:text>   </xsl:text>
                </xsl:element>

            </head>
            <xsl:for-each select="response">
                <body>
                   <!--加载的时候遮罩层-->
                   <div class="wrap-cover" id="wrap-cover">
                    <xsl:text><![CDATA[ ]]></xsl:text>
                </div>
                <!--填写预约表单遮罩层-->
                <div id="reqapplydiv" class="mask">
                    <div class="mask-close">
                      <!-- <a href="#" onclick="closeformmask();">关闭</a> -->
                      <xsl:text><![CDATA[ ]]></xsl:text>
                  </div>
                  <div class="mask-content">
                   <form name="reasonform" id="reqapply-form" role="form">
                      <div class="form-group">
                        <label class="col-sm-2 control-label" style="margin-top: 5px;">使用天数</label>
                        <div class="col-sm-10">
                            <input type="text" class="form-control" id="mask-days" name="Days"  />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label" style="margin-top:20px;">用车人数</label>
                        <div class="col-sm-10" style="margin-top:15px;">
                            <input type="text" class="form-control" id="mask-peronNum" name="TripNum"/>
                       </div>
                   </div>
                   <input type="button" class="btn btn-default mask-content-button" onclick="checkPerson()" value="确定"/>
               </form>
           </div>
       </div> 
       <div class="cover" id="cover">
          <div class="cover-words center-vertical">
            请稍后....
        </div>
    </div>
    <div class="container mycontainer">
     <span id="applyName"><xsl:value-of select="$name"/></span>
     <span id="applyAccount"><xsl:value-of select="$account"/></span>
     <div class="row head-row">
         <xsl:call-template name="header"></xsl:call-template>
     </div>

     <div class="row content-row" style="top:70px;">
        <div class="wrap-content" id="turnTodark">
         <div class="mycont">
           <div class="mycol">
               <form class="form-horizontal myform" role="form" id="addform">
                <div class="top-words" >
                  <div class="words-div">
                      <b>车辆预约申请</b>
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
                    <input type="text" disabled="disabled" class="form-control" id="time" name="Starttime"/>
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
                   <input type="text" class="form-control" id="peronNum" name="TripNum" onblur="RecheckPerson()"/>
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
            <label for="text" class="col-sm-2 control-label">申请理由</label>
            <div class="col-sm-10">
                <textarea class="form-control" id="reason" name="ApplieReson" rows="10"><xsl:text><![CDATA[ ]]></xsl:text></textarea>
            </div>
        </div>
        <div class="bottom-btn">
           <div class="bottom-btn-inside">
            <!-- <span id="tipwords" style="color:gray;margin-right:20px">在提交申请前先请查询合适车辆</span> -->
            <span id="availableCar" style="display:none;">null</span>
            <span id="maxsites" display="none"><xsl:text><![CDATA[ ]]></xsl:text></span>
            <input type="button" id="btnComfirmApply" class="btn btn-default" onclick="ReCheckNum()" value="确认申请" ><xsl:text><![CDATA[ ]]></xsl:text></input>
        </div>
    </div>

</form>
</div>
</div>
</div>
</div>

<div class="row footer-row"> 
    <xsl:call-template name="footer"></xsl:call-template>
</div>

</div>
</body>
</xsl:for-each>
</html>
</xsl:for-each>
</xsl:template>
</xsl:transform>