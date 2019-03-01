namespace CLDD.Providers{
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

    public class ToolDol: HttpResponseProvider
    {
        public XmlDocument result;
        public HttpContext context;
        public string conn = "WEB";
        public ToolDol(HttpContext _context) : base(_context) { this.context = _context; }
        //获取部门领导账号
        public string getDepartAccount(string _departID)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getDepartLeader");
            using(SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter departID = new SqlParameter("DepartID",SqlDbType.NVarChar,50);
                departID.Value = _departID;
                _cmd.Parameters.Add(departID);
                result = SQLProvider.GetData(_cmd);
            }
            if (result.DocumentElement.SelectNodes("SchemaTable").Count>0)
            {
                 return result.DocumentElement.SelectSingleNode("SchemaTable/DepartLeaderAccount").InnerText.ToString().Trim();
            }
            else
            {
                return "";
            }
           
        }
        //获取调度员账号
        public string getSysAccount()
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("getSysAccount");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                result = SQLProvider.GetData(_cmd);
            }
            return result.DocumentElement.SelectSingleNode("SchemaTable/PermitAccount").InnerText.ToString().Trim();
        }

      
        //读取XML
         public string readXml(string _type)
        {
             XmlDocument _config = XmlProvider.Document("sqlConfig");
            String _sql = "";
            //读取xml文件
            try
            {
                string _path = "/app_data/tool.xml";
                _config.Load(Provider.Path(_path));
                foreach(XmlNode _section in _config.DocumentElement.SelectNodes("section"))
                {
                    if (((XmlElement)_section).Attributes["type"].Value.ToString().Trim() == _type)
                    {
                        _sql = _section.SelectSingleNode("sql").InnerText.ToString().Trim();
                        break;
                    }
                }
            }
            catch(Exception _exc)
            {
                Provider.LogErr(this.result, _exc);
            }
            return _sql;
        }
    }
}