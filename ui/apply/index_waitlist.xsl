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

                <xsl:element name="link">
                  <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                  <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                  <xsl:attribute name="href"><![CDATA[/cldd/style/apply/getapplyreviewlist/getapplyreviewlist.css]]></xsl:attribute>
                </xsl:element>
                
                <xsl:element name="link">
                  <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
                  <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
                  <xsl:attribute name="href"><![CDATA[/cldd/style/apply/index_waitlist/index_waitlist.css]]></xsl:attribute>
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
                  <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/jquery.js]]></xsl:attribute>
                  <xsl:text>   </xsl:text>
                </xsl:element>

                <xsl:element name="script">
                  <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
                  <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
                  <xsl:attribute name="src"><![CDATA[/cldd/scripts/apply/index_waitlist/index_waitlist.js]]></xsl:attribute>
                  <xsl:text>   </xsl:text>
                </xsl:element>

              </head>
              <xsl:for-each select="response">
                <body>
                  <div class="container mycontainer" style="height:auto;" id="mycontainer">
                   <div class="row head-row">
                     <xsl:call-template name="header"></xsl:call-template>
                   </div>
                   <div class="row content-row" style="top:70px;height:auto;" id="content-row">
                    <div class="wrap-content" style="height:auto;" id="wrap-content">
                      <div class="media-row" style="height:auto;">
                       <!--内容-->   
                       <div style="width:100%;height:100%;">
                        <div class="words-div-review">
                         <b>待出车列表</b>
                         <hr/>
                       </div>
                       <table class="table mytable-review">
                         <thead>
                           <tr>
                             <th><xsl:text><![CDATA[ ]]></xsl:text></th>
                             <th>序号</th>
                             <th>申请人</th>
                             <th>出车时间</th>
                             <th>天数</th>
                             <th>结束时间</th>
                             <th>车牌号</th>
                             <th>目的地</th>
                             <th>司机</th>             
                           </tr>
                         </thead>
                         <tbody>
                          <xsl:if test="count(//SchemaTable)&lt;1">
                            <tr><td colspan="10">当前无任何记录</td></tr>
                          </xsl:if>
                          <xsl:if test="count(//SchemaTable)&gt;=1">
                            <xsl:for-each select="SchemaTable">
                             <tr>
                               <td>
                                <xsl:element name="input">
                                  <xsl:attribute name="name">carNumbox</xsl:attribute>
                                  <xsl:attribute name="type">checkbox</xsl:attribute>
                                  <xsl:attribute name="data-type">checkbox</xsl:attribute>
                                  <xsl:attribute name="data-num">num<xsl:value-of select="position()"/></xsl:attribute>
                                  <xsl:attribute name="value"><xsl:value-of select="CarOwner"/>,<xsl:value-of select="CarAppliedID"/></xsl:attribute>
                                  <xsl:attribute name="data-value"><xsl:value-of select="CarOwner"/></xsl:attribute>
                                  <xsl:attribute name="id">checkbox<xsl:value-of select="position()"/></xsl:attribute>
                                  <xsl:attribute name="style">display:none;float:right</xsl:attribute>
                                </xsl:element>
                              </td>
                              <td><xsl:value-of select="position()"/></td>
                              <td><xsl:value-of select="CarAppliedName"/></td>
                              <td><xsl:value-of select="StartTime"/>
                              <p>
                                <xsl:if test="StartWeek='Monday'">星期一</xsl:if>
                                <xsl:if test="StartWeek='Tuesday'">星期二</xsl:if>
                                <xsl:if test="StartWeek='Wednesday'">星期三</xsl:if>
                                <xsl:if test="StartWeek='Thursday'">星期四</xsl:if>
                                <xsl:if test="StartWeek='Friday'">星期五</xsl:if>
                                <xsl:if test="StartWeek='Saturday'">星期六</xsl:if>
                                <xsl:if test="StartWeek='Sunday'">星期天</xsl:if>
                              </p>
                            </td>
                            <td><xsl:value-of select="Days"/></td>
                            <td>
                              <xsl:value-of select="EndTime"/>
                              <p>
                                <xsl:if test="EndWeek='Monday'">星期一</xsl:if>
                                <xsl:if test="EndWeek='Tuesday'">星期二</xsl:if>
                                <xsl:if test="EndWeek='Wednesday'">星期三</xsl:if>
                                <xsl:if test="EndWeek='Thursday'">星期四</xsl:if>
                                <xsl:if test="EndWeek='Friday'">星期五</xsl:if>
                                <xsl:if test="EndWeek='Saturday'">星期六</xsl:if>
                                <xsl:if test="EndWeek='Sunday'">星期天</xsl:if>
                              </p>
                            </td>
                            <td><xsl:value-of select="LicenseID"/></td>
                            <td><xsl:value-of select="Destination"/></td>
                            <td><xsl:value-of select="CarOwner"/></td>
                          </tr>
                        </xsl:for-each>
                      </xsl:if>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          </div>

          <div class="row footer-row" style="background-color: #2a3542;"> 
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
      <meta http-equiv="refresh" content="3;url=http://webservices.qgj.cn/cldd/index.html?uri=http://webservices.qgj.cn/cldd/apply/index_waitlist.ashx"/>
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