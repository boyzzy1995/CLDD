<?xml version="1.0" encoding="UTF-8"?>
<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8"/>
  <xsl:template name="header">
    <div class="row row-head-row">
      <div class="col-md-12 mycol-md-12">
        <h3 style="height: 40px;"> 
          车辆调度系统
        </h3>
        <div class="top-words-content">
          <font class="top-words">浙江省钱塘江管理局勘测设计院</font>
        </div>
        <div class="hrefdiv">
          <a href="http://webservices.qgj.cn/cldd/apply/index_waitlist.ashx" class="btn btn-default" style="margin-right:10px;">待出车列表</a>
          <!--<a href="#" class="btn btn-default" style="margin-right:10px;" onclick="delAllCookie()">退出</a>-->
          <a href="http://webservices.qgj.cn/cldd/apply/index.ashx" class="btn btn-default">
           <span class="glyphicon glyphicon-home "><xsl:text><![CDATA[ ]]></xsl:text></span>主页
          </a>
       </div>
     </div>
   </div>
 </xsl:template>
</xsl:transform>