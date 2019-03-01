<?xml version="1.0" encoding="UTF-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:include href="http://webservices.qgj.cn/cldd/ui/apply/header.xsl"/>
  <xsl:include href="http://webservices.qgj.cn/cldd/ui/apply/footer.xsl"/>
  <xsl:output method="html" encoding="utf-8"/>
  <xsl:template name="page" match="/">
    <xsl:for-each select="application">
      <xsl:variable name="name" select="profile/SchemaTable/TokenName"/>
      <xsl:variable name="account" select="profile/SchemaTable/TokenAccount"/>
      <html xmlns="http://webservices.w3.org/1999/xhtml">
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
            <xsl:attribute name="href"><![CDATA[/cldd/style/apply/reqapply/reqapply.css]]></xsl:attribute>
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
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/reqapply/reqapply.js]]></xsl:attribute>
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
            <!--透明遮罩层-->
          <div class="wrap-cover" id="wrap-cover">
            <xsl:text><![CDATA[ ]]></xsl:text>
          </div>
           <!--存放选中的时间-->
           <span id="sendTime" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
           <!-- 遮罩层 -->
           <div class="mask" id="mask">
            <!--关闭按钮-->
            <div class="mask-close">
              <a href="#" onclick="closeBg();">关闭</a>
            </div>
            <div class="mask-content">
                <div class="row" style="text-align:center;">
                    <span id="date" class="dataSpan" style="color:coral"><xsl:text><![CDATA[ ]]></xsl:text></span>
                </div>
                <div class="form-group" style="text-align:center;padding-top:20px;margin-bottom: 30px;">
                  <label for="place" class="col-sm-6 control-label">几点:</label>
                  <div class="col-sm-5">
                    <select id="hours" class="hourSelect">
                      <xsl:text><![CDATA[ ]]></xsl:text>
                    </select>
                    <span class="hourSpan">时</span>
                  </div>
                </div>
                <div class="form-group" style="text-align:center;padding-top:20px;">
                  <label for="place" class="col-sm-6 control-label">几分:</label>
                  <div class="col-sm-5">
                    <select id="minutes" class="minuteSelect">
                      <xsl:text><![CDATA[ ]]></xsl:text>
                    </select>
                    <span class="minuteSpan">分</span>
                  </div>
                </div>
                <div class="btn-div" style="bottom: 10px;position: absolute;width: 74%;">
                  <input type="button" class="btn btn-warning" onclick="confirmTime()" value="确定" style="width:100%"/>
                </div>
                        <!-- <div class="row">
                            <div class="col-md-6 col-xs-12 col-lg-5">
                                <span id="date" class="dataSpan"><xsl:text><![CDATA[ ]]></xsl:text></span>
                            </div>
                            <div class="col-md-3 col-xs-12 col-lg-3">
                                <label class="control-label">请选择几点:</label>
                                <select id="hours" class="hourSelect">
                                    <xsl:text><![CDATA[ ]]></xsl:text>
                                </select>
                                <span class="hourSpan">时</span>
                            </div>
                            <div class="col-md-3 col-xs-12 col-lg-3">
                                <label class="control-label">几分:</label>
                                <select id="minutes" class="minuteSelect">
                                    <xsl:text><![CDATA[ ]]></xsl:text>
                                </select>
                                <span class="minuteSpan">分</span>
                            </div>
                            <div class="col-md-1 col-xs-12 col-lg-1 btn-div">
                                <input type="button" class="btn btn-warning mask-content-button" onclick="confirmTime()" value="确定"/>
                            </div>
                          </div> -->
                      </div>
                    </div>
                    <!--主体内容-->
                    <div class="container mycontainer">
                     <div class="row head-row">
                       <xsl:call-template name="header"></xsl:call-template>
                     </div>
                     <div class="row content-row" style="top:70px;">
                      <div class="wrap-content">
                       <div class="mycont">
                         <div class="mycol">
                          <div class="top-words" >
                            <div class="words-div">
                              <b>车辆预约申请</b>
                            </div>
                            <hr/>
                          </div>
                          <div style="width:100%;text-align:center;height:90%">
                           <!--js生成日历-->
                           <div class="calendar center-vertical" id="calendar">
                             <xsl:text><![CDATA[ ]]></xsl:text>
                           </div>
                           <div class="instruction">
                             <div class="green-div"><xsl:text><![CDATA[ ]]></xsl:text></div>
                             <div class="instruction-words">绿色表示可预约</div>
                           </div>
                           <xsl:for-each select="SchemaTable">
                            <xsl:element name="span">
                              <xsl:attribute name="style">display:none</xsl:attribute>
                              <xsl:attribute name="id"><xsl:value-of select="position()"/></xsl:attribute>
                              <xsl:value-of select="statue"/>
                            </xsl:element>
                          </xsl:for-each>
                              <!-- <table style="width:78%;margin:0 auto;">
                                
                                 <tr>      
                                 <xsl:for-each select="SchemaTable">                             
                                    <td style="margin-left:10%;float:left;margin-top:8%;">
                                    <xsl:element name="a">
                                        <xsl:attribute name="href"><![CDATA[http://www.qgj.cn/cldd/apply/reqapplyform.ashx?starttime=]]><xsl:value-of select="substring-before(//SchemaTable/@date,' ')"/></xsl:attribute>
                                        <xsl:attribute name="class">btn btn-default btn-lg</xsl:attribute>
                                        <xsl:if test="statue='/@unable'">
                                            <xsl:attribute name="disabled">disabled</xsl:attribute>
                                        </xsl:if> 
                                        <xsl:value-of select="substring-before(//SchemaTable/@date,' ')"/>
                                    </xsl:element>
                                 </td>
                                 </xsl:for-each>
                                </tr>
                                
                              </table> -->
                            </div>
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