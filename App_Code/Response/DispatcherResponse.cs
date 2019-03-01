namespace CLDD.Providers
{
    using System;
    using System.Xml;
    using System.Web;
    using System.Web.Security;
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections;
    using System.Text.RegularExpressions;
    using CS.Providers;
    using System.IO;
    using System.Net;

    public class DispatcherResponse : HttpResponseProvider
    {
        public HttpContext context;
        public XmlDocument config;
        public XmlDocument result;
        public string conn = "WEB";
        public DispatcherResponse(HttpContext _context) : base(_context) { this.context = _context; }
        public override void Render()
        {
            Provider.Invoke("CLDD.Providers.Login.Render", new object[] { this.context }, new object[] { base.result });
            base.Render();
        }
        public override XmlDocument Response()
        {
            this.result = XmlProvider.Document("pagedata");

            switch (this.PARAMS["guid"].ToString())
            {
                case "getlsdispatcher": getLsDispatcher(); break ;//查询临时调度员账号
                case "addlsdispatcher": addLsDispatcher(); return this.result;//添加临时调度员账号
                case "dellsdispatcher": delLsDispatcher(); return this.result;//删除临时调度员账号

            }
            //附加显示样式
            XmlElement _template = this.result.CreateElement("template");
            _template.SetAttribute("output", "html");
            if (File.Exists(Provider.Path("/ui/dispatcher/" + this.PARAMS["guid"].ToString() + ".xsl")))
            {
                _template.InnerXml = "/ui/dispatcher/" + this.PARAMS["guid"].ToString() + ".xsl";
            }
            else
            {
                _template.InnerXml = "/ui/pages/err.xsl";
            }
            this.result.DocumentElement.AppendChild(_template);
            return this.result;
        }

        //添加一个临时调度员账号
        public void addLsDispatcher()
        {
            this.result = XmlProvider.Document("sqldata");

            string _permitAccount = this.context.Request.Form["permitAccount"].ToString().Trim();

            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DispatcherDol.addLsDispatcher", new object[] { this.context }, new object[] { _permitAccount });
        }
        //查询当前临时调度员账号
        public void getLsDispatcher()
        {
            this.result = XmlProvider.Document("sqldata");

            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DispatcherDol.getLsDispatcher", new object[] { this.context },null);


        }
        //删除当前临时调度员账号
        public void delLsDispatcher()
        {
            this.result = XmlProvider.Document("sqldata");

            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DispatcherDol.delLsDispatcher", new object[] { this.context },null);
        }
    }
}