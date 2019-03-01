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
           <div class="top-words-carlist">
             <b>司机管理</b>
             <hr/>
           </div>
           <div class="Ttable" style="background-color:#f6f6f6;text-align:center;">
            <xsl:if test="count(//SchemaTable)&lt;1">
              <div style="margin-top:20%;">当前无任何司机记录</div>
            </xsl:if>
            <xsl:if test="count(//SchemaTable)&gt;=1">
             <ul class="ul">
               <xsl:for-each select="SchemaTable">
                <li class="lil" style="height:200px;">
                  <div class="panel panel-default Tpanel">
                    <table class="tb">
                      <tr>
                        <td rowspan="2"><img src="/cldd/image/person.png"/></td>
                        <td>姓名:</td>
                        <td><xsl:value-of select="DriverName"/></td>
                      </tr>
                      <tr>
                        <td>车牌:</td>
                        <td><xsl:value-of select="LicenseID"/></td>
                      </tr>
                      <tr>                            
                        <td colspan="2">
                          <xsl:element name="a">
                            <xsl:attribute name="href">#</xsl:attribute>
                            <xsl:attribute name="class">btn btn-default</xsl:attribute>
                            <xsl:attribute name="id"><xsl:value-of select="DriverID"/></xsl:attribute>
                            <xsl:attribute name="onclick">ToFullInformation('<xsl:value-of select="DriverID"/>')</xsl:attribute>
                            查看个人信息
                          </xsl:element>
                        </td>
                        <td>
                          <xsl:if test="LicenseID=' '">
                            <xsl:element name="input">
                              <xsl:attribute name="type">button</xsl:attribute>
                              <xsl:attribute name="class">btn btn-default</xsl:attribute>
                              <xsl:attribute name="id"><xsl:value-of select="position()"/></xsl:attribute>
                              <xsl:attribute name="value">删除</xsl:attribute>
                              <xsl:attribute name="onclick">ToDeleteDriver('<xsl:value-of select="DriverID"/>')</xsl:attribute>
                            </xsl:element>
                          </xsl:if>
                        </td>
                      </tr>
                    </table>
                  </div>
                </li>
              </xsl:for-each>
            </ul>
          </xsl:if>
          </div>
        </body>
      </xsl:for-each>
    </html>
  </xsl:for-each>
</xsl:template>
</xsl:transform>