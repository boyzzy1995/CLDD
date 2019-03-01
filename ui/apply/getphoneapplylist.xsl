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
            <xsl:attribute name="src"><![CDATA[/cldd/scripts/lib/prototype.js]]></xsl:attribute>
            <xsl:text>   </xsl:text>
          </xsl:element>

        </head>
        <xsl:for-each select ="response">
          <body>
            <div class="container mycontainer" style="background-color:white;">
              <div class="row" style="margin-left:0;margin-right:0;">
                <table class="table phone-index-table">
                  <thead>
                    <tr>
                      <th>目的地</th> 
                      <th>开始时间</th>
                      <th>司机</th>
                      <th>联系方式</th>                          
                    </tr>
                  </thead>
                  <tbody>
                    <xsl:for-each select="SchemaTable">
                      <span id="AppliedStatue" style="display:none;"><xsl:value-of select="AppliedStatue"/></span> 
                          <!--<xsl:element name="a">
			  <xsl:attribute name="href"><![CDATA[http://www.qgj.cn/cldd/apply/start_apply.ashx?guid=]]><xsl:value-of select="CarAppliedID" /></xsl:attribute>    
			  <xsl:element name="tr">
			    <xsl:element name="td"><xsl:value-of select="Destination"/></xsl:element>
			    <xsl:element name="td"><xsl:value-of select="StartTime"/></xsl:element>
			    <xsl:element name="td"><xsl:value-of select="CarOwner"/></xsl:element>
			    <xsl:element name="td"><xsl:value-of select="CarTelephone"/></xsl:element>
			  </xsl:element>                                        
     </xsl:element>  -->
     <xsl:element name="tr">
      <xsl:attribute name="onclick"> locationToHref('<xsl:value-of select="CarAppliedID" />')</xsl:attribute>
      <td><xsl:value-of select="Destination"/></td>
      <td><xsl:value-of select="StartTime"/></td>
      <td><xsl:value-of select="CarOwner"/></td>
      <td><xsl:value-of select="CarTelephone"/></td>
    </xsl:element>   
  </xsl:for-each>
</tbody>
</table>
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

      <!-- <xsl:element name="link">
        <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
        <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
        <xsl:attribute name="href"><![CDATA[/cldd/style/apply/index/index.css]]></xsl:attribute>
      </xsl:element>
      
      <xsl:element name="link">
        <xsl:attribute name="type"><![CDATA[text/css]]></xsl:attribute>
        <xsl:attribute name="rel"><![CDATA[stylesheet]]></xsl:attribute>
        <xsl:attribute name="href"><![CDATA[/cldd/style/style/fix.css]]></xsl:attribute>
      </xsl:element> -->

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
          <table class="table phone-index-table">
            <thead>
              <tr id="driver">
                <th>申请人</th>
                <th>开始时间</th>
                <th>结束时间</th>
                <th>目的地</th>
              </tr>
            </thead>
            <tbody>
              <xsl:for-each select="SchemaTable">
                <span id="AppliedStatue" style="display:none;"><xsl:value-of select="AppliedStatue"/></span>
                <tr>
                  <td><xsl:value-of select="CarAppliedName"/></td>
                  <td><xsl:value-of select="StartTime"/></td>
                  <td><xsl:value-of select="EndTime"/></td>
                  <td><xsl:value-of select="Destination"/></td>
                </tr>
              </xsl:for-each>
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
