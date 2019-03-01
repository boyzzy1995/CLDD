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
        </head>
        <xsl:for-each select="response">
        <body>
               未找到页面，请确认是通过电脑端登录本系统。若非手机端登录，请联系管理员。
        
        </body>
      </xsl:for-each>
</html>
</xsl:for-each>
</xsl:template>
</xsl:transform>