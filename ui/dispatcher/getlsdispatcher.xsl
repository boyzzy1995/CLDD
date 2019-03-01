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
           <div class="words-div-report">
             <b>调度员管理</b>
             <hr/>
           </div>
           <div class="wrap-words" style="height:85%">
            <!--展示账号-->
            <div class="wrap-content-words center-vertical" id="firstShow">
              <xsl:if test="count(//response/SchemaTable)&gt;0">
                <xsl:for-each select="SchemaTable">
                  <div class="form-group center-vertical">
                    <label for="place" class="col-xs-5 col-sm-6 control-label"><span style="float:right;margin-top: 6px;">账号:</span></label>
                    <div class="col-xs-7 col-sm-2">
                      <span id="PermitAccount" style="font-weight:bold;color:#FF7F00;"><xsl:value-of select="PermitAccount"/></span>
                      <input type="button" class="btn btn-default" onclick="deleteDispatcher()" value="删除" style="margin-left:20px;"></input>
                    </div>
                  </div>
                </xsl:for-each>
              </xsl:if>
              <xsl:if test="count(//response/SchemaTable)&lt;1"> 
                <div class="form-group center-vertical" style="width: 60%;margin: 0 auto;">
                  <label for="place" class="col-xs-12 col-sm-6 control-label"><span class="dispatcherSpan">当前无调度员账号</span></label>
                  <div class="col-xs-12 col-sm-2">
                   <input type="button" class="btn btn-default" onclick="showAddDispatcher()" value="添加"></input>
                 </div>
               </div>
             </xsl:if>
             <xsl:text><![CDATA[ ]]></xsl:text>
           </div>
           <!--调度员表单--> 
           <div class="wrap-content-words center-vertical" id="clickShow" style="display:none;">     
             <form name="dispatcher-form" id="dispatcherform" target="dispatcher-form" role="form">
              <div class="form-group" style="padding-top:30px">
                <label for="place" class="col-xs-3 col-sm-5 control-label"><span style="float:right;margin-top: 6px;">账号:</span></label>
                <div class="col-xs-9 col-sm-2">
                 <input type="text" class="form-control inputAccount" id="input-permitAccount" name="permitAccount"/>
                 <xsl:text><![CDATA[ ]]></xsl:text>
               </div>
             </div>
             <div class="form-group" style="padding-top:30px">
              <div class="col-xs-7 col-sm-6">
                <xsl:element name="input">
                 <xsl:attribute name="type">button</xsl:attribute>
                 <xsl:attribute name="id">btnaddDis</xsl:attribute>
                 <xsl:attribute name="class">btn btn-default</xsl:attribute>
                 <xsl:attribute name="style">float:right</xsl:attribute>
                 <xsl:attribute name="value">添加</xsl:attribute>
                 <xsl:attribute name="onclick">addDispatcher()</xsl:attribute>
               </xsl:element>
             </div>
             <div class="col-xs-3 col-sm-2">
              <xsl:element name="input">
               <xsl:attribute name="type">button</xsl:attribute>
               <xsl:attribute name="id">btncancelDis</xsl:attribute>
               <xsl:attribute name="class">btn btn-default</xsl:attribute>
               <xsl:attribute name="value">取消</xsl:attribute>
               <xsl:attribute name="onclick">cancelDispatcher()</xsl:attribute>
             </xsl:element>
           </div>
         </div>
       </form>
     </div>
   </div>
 </body>
</xsl:for-each>
</html>
</xsl:for-each>
</xsl:template>
</xsl:transform>