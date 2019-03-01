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

    public class ReportResponse : HttpResponseProvider
    {
        public HttpContext context;
        public XmlDocument config;
        public XmlDocument result;
        public string conn = "WEB";
        public ReportResponse(HttpContext _context) : base(_context) { this.context = _context; }
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
                case "car_mounth_report":reportCarDetail();break;
                case "reqcar_mounth_report":reqCarMounthReport();break;
		case "print_car_mounth" : printCarMounth();break;

            }
            //附加显示样式
            XmlElement _template = this.result.CreateElement("template");
            _template.SetAttribute("output", "html");
            if (File.Exists(Provider.Path("/ui/report/" + this.PARAMS["guid"].ToString() + ".xsl")))
            {
                _template.InnerXml = "/ui/report/" + this.PARAMS["guid"].ToString() + ".xsl";
            }
            else
            {
                _template.InnerXml = "/ui/pages/err.xsl";
            }
            this.result.DocumentElement.AppendChild(_template);
            return this.result;
        }
        //请求报表
        public void reqCarMounthReport()
        {
            //获取所有车辆信息
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getCarList", new object[] { this.context }, null);
        }
        //生成车辆每月记录--打印
        public void printCarMounth()
        {
            this.result = XmlProvider.Document("data");
            string _licenseid = this.context.Request.QueryString["licenseid"];
            int _year = Convert.ToInt32(this.context.Request.QueryString["year"]);
            int _mounth = Convert.ToInt32(this.context.Request.QueryString["mounth"]);
            //string　_date = _year + "-" + _mounth + "-" + "01 00:00:000";//查询报表的时间
            //string _nextDate = _year + "-" + (Convert.ToInt32(_mounth) + 1) + "-" + "01 00:00:000";
            
           
            string _date = Convert.ToString(_year) + "-" + Convert.ToString(_mounth) +"-01 00:00:000";
             //如果是12月，下个月就是第二年1月
            /*if (_mounth == 12)
            {
                _mounth = 1;
                _year = _year + 1;  //下一年的一月
            }
            else
            {
                _mounth = _mounth + 1;
            }*/
            string _nextDate = Convert.ToString(_year) + "-" + Convert.ToString(_mounth) +"-01 00:00:000";

            //获取列表
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ReportDol.getMounthReport", new object[] { this.context }, new object[] { _date, _nextDate, _licenseid });
            this.result.DocumentElement.SetAttribute("nextMounth",_nextDate);
            this.result.DocumentElement.SetAttribute("licenseID",_licenseid);
            //获取车辆当前总公里数
            XmlDocument _kmDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getKmByLicense",new object[] { this.context},new object[] { _licenseid});
            if (_kmDom.DocumentElement.SelectSingleNode("SchemaTable") != null)
            {
                this.result.DocumentElement.SetAttribute("Kilometer",_kmDom.DocumentElement.SelectSingleNode("SchemaTable/Kilometers").InnerText.ToString().Trim());
            }
            else
            {
                 this.result.DocumentElement.SetAttribute("Kilometer","出现错误，请于管理员联系");
            }
	    this.result.DocumentElement.SetAttribute("time",Convert.ToString(_year) + "年" + Convert.ToString(_mounth)+"月");
	    this.result.DocumentElement.SetAttribute("licenseID",_licenseid);
        }
	 //生成车辆每月记录
        public void reportCarDetail()
        {
            this.result = XmlProvider.Document("data");
            string _licenseid = this.context.Request.Form["licenseid"];
            int _year = Convert.ToInt32(this.context.Request.Form["year"]);
            int _mounth = Convert.ToInt32(this.context.Request.Form["mounth"]);
            //string　_date = _year + "-" + _mounth + "-" + "01 00:00:000";//查询报表的时间
            //string _nextDate = _year + "-" + (Convert.ToInt32(_mounth) + 1) + "-" + "01 00:00:000";
            
           
            string _date = Convert.ToString(_year) + "-" + Convert.ToString(_mounth) +"-01 00:00:000";
             //如果是12月，下个月就是第二年1月
            if (_mounth == 12)
            {
                _mounth = 1;
                _year = _year + 1;  //下一年的一月
            }
            else
            {
                _mounth = _mounth + 1;
            }
            string _nextDate = Convert.ToString(_year) + "-" + Convert.ToString(_mounth) +"-01 00:00:000";

            //获取列表
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ReportDol.getMounthReport", new object[] { this.context }, new object[] { _date, _nextDate, _licenseid });
            this.result.DocumentElement.SetAttribute("nextMounth",_nextDate);
            this.result.DocumentElement.SetAttribute("licenseID",_licenseid);
            //获取车辆当前总公里数
            XmlDocument _kmDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getKmByLicense",new object[] { this.context},new object[] { _licenseid});
            if (_kmDom.DocumentElement.SelectSingleNode("SchemaTable") != null)
            {
                this.result.DocumentElement.SetAttribute("Kilometer",_kmDom.DocumentElement.SelectSingleNode("SchemaTable/Kilometers").InnerText.ToString().Trim());
            }
            else
            {
                 this.result.DocumentElement.SetAttribute("Kilometer","出现错误，请于管理员联系");
            }
        }
    }
}