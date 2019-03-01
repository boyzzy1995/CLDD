<?xml version="1.0" encoding="UTF-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:include href="http://webservices.qgj.cn/cldd/ui/apply/header.xsl"/>
  <xsl:include href="http://webservices.qgj.cn/cldd/ui/apply/footer.xsl"/>
  <xsl:output method="html" encoding="utf-8"/>
  <xsl:template name="page" match="/">
    <xsl:for-each select="application">
      <xsl:variable name="permit" select="profile/SchemaTable/TokenPermit"/>
      <xsl:variable name="account" select="profile/SchemaTable/TokenAccount"/>
      <xsl:choose>
        <xsl:when test="$account!=''">
          <xsl:if test="$permit='ADMIN' or $permit='APPLY' or $permit='LSADMIN'">
            <html xmlns="http://www.w3.org/1999/xhtml">
              <head>
                <meta charset="UTF-8"/>
                <title>车辆调度系统 beta v1.0</title>
                <meta name="viewport" content="width=device-width, initial-scale=1.0"/>

                <xsl:element name="link">
                  <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                  <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                  <xsl:attribute name="href"><![CDATA[/cldd/style/lib/bootstrap.min.css]]></xsl:attribute>
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

                <xsl:element name="link">
                  <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                  <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                  <xsl:attribute name="href"><![CDATA[/cldd/style/apply/index/index.css]]></xsl:attribute>
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
                  <xsl:attribute name="href"><![CDATA[/cldd/style/style/footer.css]]></xsl:attribute>
                </xsl:element>

                <xsl:element name="script">
                  <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
                  <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
                  <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/prototype.js]]></xsl:attribute>
                  <xsl:text>   </xsl:text>
                </xsl:element>

                <xsl:element name="script">
                  <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
                  <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
                  <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/index/function.js]]></xsl:attribute>
                  <xsl:text>   </xsl:text>
                </xsl:element>

              </head>
              <xsl:for-each select="response">
                <body>
                  <span id="changeCarId" style="display:none"><xsl:text><![CDATA[ ]]></xsl:text></span>
                 <!--加载的时候遮罩层-->
                 <div class="wrap-mask" id="wrap-mask">
                  <xsl:text><![CDATA[ ]]></xsl:text>
                </div>
                <div class="mask" id="mask">
                  <div class="mask-words center-vertical">
                    请稍后....
                  </div>
                </div>
                 
                 <!--申请延期的遮罩层-->
                 <div id="delayDate" class="mask-delay">
                 <div class="mask-close">
                 <a href="#" onclick="closeDelayMask();">关闭</a>
                 </div>
                 <div class="mask-content">
                 <form name="reasonform" id="delayform">
                 <label class="control-label">延期天数:</label>
                 <input type="text" class="form-control" id="delayDateinput" name="delayDate"/>
                 <input type="button" class="btn btn-default mask-content-button" onclick="confirmDelay()" value="确定"/>
                 </form>
                 </div>
                 </div>  


                 <div class="container mycontainer">
                 <span id="account" style="display:none;"><xsl:value-of select="$account"/></span>
                 <span id="permit" style="display:none;"><xsl:value-of select="$permit"/></span>
                 <div class="row head-row">
                   <xsl:call-template name="header"></xsl:call-template>
                 </div>
                 
                 <div class="row content-row" style="top:70px;">
                  <div class="wrap-content">
                   <div class="media-row">
                     <div class="media-top-words">
                       <div class="media-top-left-words">
                        <b> 当前申请记录</b>
                      </div>
                      <div class="media-top-right-words" id="userwords">
                        
                       <xsl:element name="a">
                         <xsl:attribute name="href"><![CDATA[http://webservices.qgj.cn/cldd/apply/reqapply.ashx?]]></xsl:attribute>
                         车辆预约申请
                       </xsl:element>
                       
                       <xsl:if test="$permit='ADMIN' or $permit='LSADMIN'">
                         <a href="http://webservices.qgj.cn/cldd/apply/backstage_homepage.ashx">进入后台</a>
                       </xsl:if>
                     </div>
                   </div>
                   <div class="media-hr">
                     <hr/>
                   </div>
                   <div class="media-media-content">
                     <!-- <iframe src="" name="frameId" id="frameId"/> -->
                     <div id="tableId" class="inner-table">
                      <xsl:text><![CDATA[ ]]></xsl:text>
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
  </xsl:if>
  <xsl:if test="$permit='DRIVER'">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <meta charset="UTF-8"/>
        <title>主页</title>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>

        <xsl:element name="link">
          <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
          <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
          <xsl:attribute name="href"><![CDATA[/cldd/style/lib/bootstrap.min.css]]></xsl:attribute>
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

        <xsl:element name="link">
          <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
          <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
          <xsl:attribute name="href"><![CDATA[/cldd/style/apply/index/index.css]]></xsl:attribute>
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
          <xsl:attribute name="href"><![CDATA[/cldd/style/style/footer.css]]></xsl:attribute>
        </xsl:element>

        <xsl:element name="script">
          <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
          <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
          <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/prototype.js]]></xsl:attribute>
          <xsl:text>   </xsl:text>
        </xsl:element>

        <xsl:element name="script">
          <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
          <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
          <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/index/function.js]]></xsl:attribute>
          <xsl:text>   </xsl:text>
        </xsl:element>

        <xsl:element name="script">
          <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
          <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
          <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/index/getApplyList.js]]></xsl:attribute>
          <xsl:text>   </xsl:text>
        </xsl:element>

      </head>
      <xsl:for-each select="response">
        <body>
          <div class="wrap-mask" id="wrap-mask">
            <div class="mask" id="mask">
              <div class="mask-words center-vertical">
                请稍后....
              </div>
            </div>
          </div>
          <div class="container mycontainer">
           <span id="account" style="display:none;"><xsl:value-of select="$account"/></span>
           <span id="permit" style="display:none;"><xsl:value-of select="$permit"/></span>
           <span id="test" style="display:none;">qwe</span>
           <div class="row head-row">
             <xsl:call-template name="header"></xsl:call-template>
           </div>

           <div class="row content-row" style="top:70px;">
            <div class="wrap-content">
             <div class="media-row">
               <div class="media-top-words" id="topWords" >
                 <div class="media-top-left-words" style="margin-top:5px;">
                  <b> 当前申请记录</b>
                </div>
                <div id="rightWords" class="media-top-right-words" style="width:auto;float:right;margin-right:5%;
                  margin-left:-2%;margin-bottom:10px;">

                  <form id="motifykmform" name="motifykmform">
                   <b>当前行程:</b>               
                   <input id="km" type="text" name="km" disabled="disabled"><xsl:text><![CDATA[ ]]></xsl:text></input>
                   <input  class="btn-primary" type="button" value="修改" id="change" onclick="changebtn()" />
                 </form>
               </div>
             </div>
             <div class="media-hr">
               <hr/>
             </div>
             <div class="media-media-content">
               <div id="tableId" class="inner-table">

                <xsl:text><![CDATA[ ]]></xsl:text>
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
</xsl:if>
</xsl:when>

<xsl:otherwise>
  <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
      <title>
        <xsl:value-of select="response/title"/>
        <xsl:value-of select="response/@version"/>
      </title>
      <meta http-equiv="refresh" content="1;url=http://webservices.qgj.cn/cldd/index.html?uri=http://webservices.qgj.cn/cldd/apply/index.ashx"/>
    </head>
    <body>
      <center>请先登录，3秒后自动转向登录界面</center>
    </body>
  </html>
</xsl:otherwise>
</xsl:choose> 
</xsl:for-each>
</xsl:template>
</xsl:transform>