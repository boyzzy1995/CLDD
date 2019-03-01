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

    public class ReportDol : HttpResponseProvider
    {
        public XmlDocument result;
        public HttpContext context;
        public string conn = "WEB";
        public ReportDol(HttpContext _context) : base(_context) { this.context = _context; }
       

        //生成每月车辆报表
        public XmlDocument getMounthReport(string _date, string _nextDate,string _lincenseID)
        {
            this.result = XmlProvider.Document("data");
            result = XmlProvider.Document("configdata");
            string _sql = readXml("CarMounthReport");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter licenseid = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseid.Value = _lincenseID;
                _cmd.Parameters.Add(licenseid);
                SqlParameter mounth = new SqlParameter("ThisMounth", SqlDbType.NVarChar, 50);
                mounth.Value = _date;
                _cmd.Parameters.Add(mounth);
                SqlParameter nextMounth = new SqlParameter("NextMounth", SqlDbType.NVarChar, 50);
                nextMounth.Value = _nextDate;
                _cmd.Parameters.Add(nextMounth);
                result = SQLProvider.GetData(_cmd);
            }
            return result;
        }

        //读取XML
        public string readXml(string _type)
        {
            XmlDocument _config = XmlProvider.Document("sqlConfig");
            String _sql = "";
            //读取xml文件
            try
            {
                string _path = "/app_data/report.xml";
                _config.Load(Provider.Path(_path));
                foreach (XmlNode _section in _config.DocumentElement.SelectNodes("section"))
                {
                    if (((XmlElement)_section).Attributes["type"].Value.ToString().Trim() == _type)
                    {
                        _sql = _section.SelectSingleNode("sql").InnerText.ToString().Trim();
                        break;
                    }
                }
            }
            catch (Exception _exc)
            {
                Provider.LogErr(this.result, _exc);
            }
            return _sql;
        }
    }
}