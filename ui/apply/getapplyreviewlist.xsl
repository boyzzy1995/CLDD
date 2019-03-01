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
      <xsl:for-each select="response">
          <body>
            <div style="width:100%;height:100%;">
                <div class="words-div-review">
                   <b>预约申请</b>
                   <hr/>
               </div>
               <table class="table mytable-review">
                   <thead>
                     <tr>
                       <th>序号</th>
                       <th>申请人</th>
                       <th>申请时间</th>
                       <th>天数</th>
                       <th>目的地</th>
                       <th>人数</th>
                       <th>司机</th>
                       <th>出行人</th>
                       <th>申请理由</th>
                       <th>操作</th>
                   </tr>
               </thead>
               <tbody>
                   <xsl:if test="count(//SchemaTable)&lt;1">
                      <tr><td colspan="10">当前无任何记录</td></tr>
                   </xsl:if>
                  <xsl:if test="count(//SchemaTable)&gt;=1">
                  <xsl:for-each select="SchemaTable">
                     <tr>
                       <td><xsl:value-of select="position()"/></td>
                       <td><xsl:value-of select="CarAppliedName"/></td>
                       <td><xsl:value-of select="StartTime"/></td>
                       <td><xsl:value-of select="Days"/></td>
                       <td><xsl:value-of select="Destination"/></td>
                       <td><xsl:value-of select="TripNum"/></td>
                       <td><xsl:value-of select="CarOwner"/></td>
                       <td><xsl:value-of select="TripMembers"/></td>
                       <td><xsl:value-of select="ApplieReson"/></td>
                       <td>
                       <xsl:element name="a">
                          <xsl:attribute name="href">#</xsl:attribute>
                          <xsl:attribute name="id"><xsl:value-of select="Guid"/></xsl:attribute>
                          <xsl:attribute name="onclick">confirmPass('<xsl:value-of select="CarAppliedID"/>')</xsl:attribute>
                          通过
                       </xsl:element>
                        <xsl:element name="a">
                          <xsl:attribute name="href">#</xsl:attribute>
                          <xsl:attribute name="id"><xsl:value-of select="Guid"/></xsl:attribute>
                          <xsl:attribute name="onclick">showMask('<xsl:value-of select="CarAppliedID"/>')</xsl:attribute>
                          拒绝
                       </xsl:element>
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
</xsl:for-each>
</xsl:template>
</xsl:transform>