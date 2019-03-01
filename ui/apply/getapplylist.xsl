<?xml version="1.0" encoding="utf-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8"/>
  <xsl:template name="page" match="/">
    <xsl:for-each select="application">
     <xsl:variable name="permit" select="profile/SchemaTable/TokenPermit"/>
     <xsl:variable name="account" select="profile/SchemaTable/TokenAccount"/>
     <xsl:if test="$permit='ADMIN' or $permit='APPLY'">
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
            <xsl:attribute name="href"><![CDATA[/cldd/style/style/fix.css]]></xsl:attribute>
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

        </head>
        <xsl:for-each select ="response">
          <body>
            <div class="container mycontainer" style="background-color:white;"> 
              <table class="table mytable">
                <thead>
                  <tr>
                    <th>序号</th>
                    <th>目的地</th> 
                    <th>开始时间</th>
                    <th>结束时间</th>
                    <th>司机</th>
                    <th>联系方式</th>                          
                    <th>申请状态</th>
                    <th style="width:225px;">操作</th>
                  </tr>
                </thead>
                <tbody>
                 <xsl:if test="count(//SchemaTable)&lt;1">
                  <tr><td colspan="9">当前无任何记录</td></tr>
                </xsl:if>
                <xsl:if test="count(//SchemaTable)&gt;=1">
                  <xsl:for-each select="SchemaTable">
                    <tr>
                      <td><xsl:value-of select="position()"/></td>
                      <td><xsl:value-of select="Destination"/></td>
                      <td><xsl:value-of select="StartTime"/></td>
                      <td><xsl:value-of select="EndTime"/></td>
                      <td><xsl:value-of select="CarOwner"/></td>
                      <td><xsl:value-of select="CarTelephone"/></td>
                      <td><xsl:value-of select="AppliedStatue"/></td>
                      <td>
                        <xsl:if test="AppliedStatue='待处理'">

                          <xsl:element name="a">
                            <xsl:attribute name="href">javascript:deleteItem('<xsl:value-of select="CarAppliedID" />');</xsl:attribute>
                            <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                            取消申请
                          </xsl:element>
                        </xsl:if>
                        
                        <xsl:if test="AppliedStatue='待出车'">

                          <xsl:element name="a">
                            <xsl:attribute name="href">javascript:deleteItem('<xsl:value-of select="CarAppliedID" />');</xsl:attribute>
                            <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                            取消申请
                          </xsl:element>

                          <xsl:element name="a">
                            <xsl:attribute name="href">javascript:startTrip('<xsl:value-of select="CarAppliedID" />');</xsl:attribute>
                            <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                            开始行程
                          </xsl:element>
                        </xsl:if>

                        <xsl:if test="AppliedStatue='开始'">
                          <xsl:element name="a">
                            <xsl:attribute name="href">javascript:endTrip('<xsl:value-of select="CarAppliedID" />');</xsl:attribute>
                            <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                            结束行程
                          </xsl:element>

                          <xsl:element name="a">
                            <xsl:attribute name="href">javascript:showDelayMask('<xsl:value-of select="CarAppliedID" />');</xsl:attribute>
                            <xsl:attribute name="style">margin-right:12px;</xsl:attribute>
                            申请延期
                          </xsl:element>

                        </xsl:if>

                      </td> 
                    </tr>
                    </xsl:for-each>
                  </xsl:if>
                </tbody>
              </table>
            </div>
          </body>
        </xsl:for-each>
      </html>
    </xsl:if>
    <xsl:if test="$permit='DRIVER'">
      <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
          <meta charset="UTF-8"/>
          <title>主页表格</title>
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
            <xsl:attribute name="href"><![CDATA[/cldd/style/style/fix.css]]></xsl:attribute>
          </xsl:element>

          <xsl:element name="script">
            <xsl:attribute name="type"><![CDATA[text/javascript]]></xsl:attribute>
            <xsl:attribute name="language"><![CDATA[javascript]]></xsl:attribute>
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/prototype.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

        </head>
        <xsl:for-each select ="response">
          <body>
            <div class="container mycontainer" style="background-color:white;">
              <table class="table mytable">
                <thead>
                  <tr id="driver">
                    <th>序号</th>
                    <th>申请人</th>
                    <th>出行人数</th>
                    <th>出行人员</th>
                    <th>联系方式</th>
                    <th>开始时间</th>
                    <th>结束时间</th>
                    <th>目的地</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:if test="count(//SchemaTable)&lt;1">
                    <tr><td colspan="8">当前无任何记录</td></tr>
                  </xsl:if>
                  <xsl:if test="count(//SchemaTable)&gt;=1">
                    <xsl:for-each select="SchemaTable">
                      <tr>
                        <td><xsl:value-of select="position()"/></td>
                        <td><xsl:value-of select="CarAppliedName"/></td>
                        <td><xsl:value-of select="TripNum"/></td>
                        <td><xsl:value-of select="TripMembers"/></td>
                        <td><xsl:value-of select="AppliedTel"/></td>
                        <td><xsl:value-of select="StartTime"/></td>
                        <td><xsl:value-of select="EndTime"/></td>
                        <td><xsl:value-of select="Destination"/></td>
                      </tr>
                    </xsl:for-each>
                  </xsl:if>
                </tbody>
              </table>
            </div>
          </body>
        </xsl:for-each>
      </html>
    </xsl:if>
  </xsl:for-each>
</xsl:template>
</xsl:transform>
