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

    public class DriverResponse : HttpResponseProvider
    {
        public HttpContext context;
        public XmlDocument config;
        public XmlDocument result;
        public string conn = "WEB";
        public DriverResponse(HttpContext _context) : base(_context) { this.context = _context; }
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
                case "req_add_driver": reqAddDriver(); break;   //请求添加司机
                case "add_driver": addDriver(); return this.result; //添加司机
                case "del_driver": delDriver(); return this.result;   //删除司机
                case "getdriverlist": getDriverList(); break;//获取司机列表
                case "driverdetail": getDriverDetail(); break;//获取司机详情
                case "reqmotifydriver": getDriverDetail(); break;    //请求修改页面
                case "motifydriver": motifyDriver(); return this.result;    //请求修改页面
                case "unbound": unBound(); return this.result;//解除司机和车辆的绑定
            }
            //附加显示样式
            XmlElement _template = this.result.CreateElement("template");
            _template.SetAttribute("output", "html");
            if (File.Exists(Provider.Path("/ui/driver/" + this.PARAMS["guid"].ToString() + ".xsl")))
            {
                _template.InnerXml = "/ui/driver/" + this.PARAMS["guid"].ToString() + ".xsl";
            }
            else
            {
                _template.InnerXml = "/ui/pages/err.xsl";
            }
            this.result.DocumentElement.AppendChild(_template);
            return this.result;
        }
        //解除司机和车辆绑定
        public void unBound()
        {
            this.result = XmlProvider.Document("sqldata");
            string _driverid = this.context.Request.QueryString["driverid"];
            //通过ID查找信息
            XmlDocument _driverDom = (XmlDocument)Provider.Invoke("CLDD.Providers.DriverDol.getDriverDetail", new object[] { this.context }, new object[] { _driverid });
            string _licenseid = _driverDom.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText;
            //清除司机列表里的车牌
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DriverDol.upLicense", new object[] { this.context }, new object[] { _driverid, " " });
            if (this.result.DocumentElement.Attributes["affect"].Value == "1")
            {
                //清除车辆表里的司机账号
                this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.unBound", new object[] { this.context }, new object[] { _licenseid });
            }
        }
        //获取司机详情
        public void getDriverDetail()
        {
            string _driverid = this.context.Request.QueryString["driverid"];
            //通过ID查找信息
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DriverDol.getDriverDetail", new object[] { this.context }, new object[] { _driverid });
            //未分配车辆列表返回
            XmlDocument _carlist = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getCarUnallocated", new object[] { this.context }, null);
            foreach (XmlElement _section in _carlist.DocumentElement.SelectNodes("SchemaTable"))
            {
                //string  _licenseEl = _section.SelectSingleNode("LicenseID").InnerText;
                XmlElement _schEl = this.result.CreateElement("Lin");
                this.result.DocumentElement.AppendChild(_schEl);
                XmlElement _licenseEl = this.result.CreateElement("LicenseID");
                _licenseEl.InnerXml = "<![CDATA[" + _section.SelectSingleNode("LicenseID").InnerText + "]]>";
                _schEl.AppendChild(_licenseEl);

            }
            // this.result = _carlist;
        }
        //获取司机列表
        public void getDriverList()
        {
            this.result = XmlProvider.Document("sqldata");
            //车辆列表返回
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DriverDol.getDriverList", new object[] { this.context }, null);
        }
        //请求添加司机
        public void reqAddDriver()
        {
            this.result = XmlProvider.Document("sqldata");
            //未分配车辆列表返回
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getCarUnallocated", new object[] { this.context }, null);

        }

        //添加司机
        public void addDriver()
        {
            this.result = XmlProvider.Document("sqldata");
            string _driverID = Provider.GUID;
            string _driverName = this.context.Request.Form["DriverName"].ToString().Trim();
            string _driverAccount = this.context.Request.Form["DriverAccount"].ToString().Trim();
            string _carTelephone = this.context.Request.Form["CarTelephone"].ToString().Trim();
            string _licenseID = this.context.Request.Form["LicenseID"].ToString().Trim();

            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DriverDol.addDriver", new object[] { this.context }, new object[] { _driverID, _driverName, _driverAccount, _carTelephone, _licenseID });
            //绑定车辆
            if (this.result.DocumentElement.Attributes["affect"].Value == "1")
            {
                this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.Bound", new object[] { this.context }, new object[] { _licenseID, _driverAccount });

            }

        }

        //修改司机
        public void motifyDriver()
        {
            this.result = XmlProvider.Document("sqldata");
            string _driverID = this.context.Request.QueryString["driverid"];
            string _driverName = this.context.Request.Form["driverName"].ToString().Trim();
            string _driverAccount = this.context.Request.Form["driverAccount"].ToString().Trim();
            string _carTelephone = this.context.Request.Form["carTelephone"].ToString().Trim();
            string _licenseID = this.context.Request.Form["licenseID"].ToString().Trim();
            //修改司机
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DriverDol.motifyDriver", new object[] { this.context }, new object[] { _driverID, _driverName, _driverAccount, _carTelephone, _licenseID });
            if (_licenseID != "")
            {
                this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.Bound", new object[] { this.context }, new object[] { _licenseID, _driverAccount });
            }
        }

        //删除司机
        public void delDriver()
        {
            this.result = XmlProvider.Document("sqldata");

            string _driverID = this.context.Request.QueryString["driverid"].ToString().Trim();

            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DriverDol.delDriver", new object[] { this.context }, new object[] { _driverID });
        }


    }
}