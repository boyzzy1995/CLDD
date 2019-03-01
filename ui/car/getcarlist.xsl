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
             <b>车辆管理</b>
             <div style="float:right;margin-top:-10px;">
               操作空闲车量数:
               <xsl:element name="input">
                <xsl:attribute name="type">text</xsl:attribute>
                <xsl:attribute name="id">txtnum</xsl:attribute>
                <xsl:attribute name="disabled">disabled</xsl:attribute>
                <xsl:attribute name="value"><xsl:value-of select="//response/@ControlNum"/></xsl:attribute>
              </xsl:element>
              <xsl:element name="input">
                <xsl:attribute name="type">button</xsl:attribute>
                <xsl:attribute name="id">btnchange</xsl:attribute>
                <xsl:attribute name="class">btn btn-default</xsl:attribute>
                <xsl:attribute name="value">修改</xsl:attribute>
                <xsl:attribute name="onclick">changefreecar()</xsl:attribute>
              </xsl:element>
            </div>
            <hr/>
          </div>
          <div class="Ttable" style="background-color:#f6f6f6;text-align:center;">
           <xsl:if test="count(//SchemaTable)&lt;1">
            <div style="margin-top:20%;">当前无任何车辆记录</div>
          </xsl:if>
          <xsl:if test="count(//SchemaTable)&gt;=1">
           <ul class="ul">
             <xsl:for-each select="SchemaTable">
              <li class="lil">
                <div class="panel panel-default Tpanel">
                  <table class="tb">
                    <tr>
                      <td style="text-align:center;"><img src="/cldd/image/car.png"/></td>
                    </tr>
                    <tr>
                      <td>车牌:<xsl:value-of select="LicenseID"/></td>
                    </tr>
                    <tr>
                      <td>所属司机:<xsl:value-of select="DriverName"/></td>
                    </tr>
                    <tr>
                      <td>

                        状态:

                        <xsl:element name="span">
                          <xsl:attribute name="id">GuidSpan<xsl:value-of select="Guid"/><xsl:value-of select="position()"/></xsl:attribute>
                          <xsl:value-of select="CarStatue"/>
                        </xsl:element>

                        <xsl:element name="select">
                          <xsl:attribute name="id"><![CDATA[select]]><xsl:value-of select="position()"/></xsl:attribute>
                          <xsl:attribute name="class">Tpanel-select</xsl:attribute>     
                          <option>空闲</option>   
			                    <option>出车</option>
                          <option>维修</option>
                          <option>禁用</option>             
                        </xsl:element>
                        
                        <xsl:if test="CarStatue='未分配'">
                          <xsl:element name="input">
                          <xsl:attribute name="type">button</xsl:attribute>
                          <xsl:attribute name="class">btn btn-default</xsl:attribute>
                          <xsl:attribute name="disabled">disabled</xsl:attribute>
                          <xsl:attribute name="id">btn<xsl:value-of select="Guid"/></xsl:attribute>
                          <xsl:attribute name="value">修改</xsl:attribute>
                          <xsl:attribute name="onclick">changebtn('btn<xsl:value-of select="Guid"/>','GuidSpan<xsl:value-of select="Guid"/><xsl:value-of select="position()"/>','select<xsl:value-of select="position()"/>','<xsl:value-of select="Guid"/>')</xsl:attribute>
                        </xsl:element>
                        </xsl:if>
                        <xsl:if test="CarStatue!='未分配'">
                        <xsl:element name="input">
                          <xsl:attribute name="type">button</xsl:attribute>
                          <xsl:attribute name="class">btn btn-default</xsl:attribute>
                          <xsl:attribute name="id">btn<xsl:value-of select="Guid"/></xsl:attribute>
                          <xsl:attribute name="value">修改</xsl:attribute>
                          <xsl:attribute name="onclick">changebtn('btn<xsl:value-of select="Guid"/>','GuidSpan<xsl:value-of select="Guid"/><xsl:value-of select="position()"/>','select<xsl:value-of select="position()"/>','<xsl:value-of select="Guid"/>')</xsl:attribute>
                        </xsl:element>
                        </xsl:if>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <xsl:element name="input">
                          <xsl:attribute name="type">button</xsl:attribute>
                          <xsl:attribute name="class">btn btn-default</xsl:attribute>
                          <xsl:attribute name="id">detail<xsl:value-of select="Guid"/></xsl:attribute>
                          <xsl:attribute name="value">查看详情</xsl:attribute>
                          <xsl:attribute name="onclick">locationToHref('<xsl:value-of select="Guid"/>')</xsl:attribute>
                        </xsl:element>
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