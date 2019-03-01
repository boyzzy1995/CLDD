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

    public class Login: HttpResponseProvider
    {
        public HttpContext context;
        private readonly string conn = "WEB";
        public Login(HttpContext _context) : base(_context) { this.context = _context; }
        //重写render方法
        public void Render(XmlDocument _result)
        {
            //设置权限
            try
            {
                string _account = _result.DocumentElement.SelectSingleNode("profile/SchemaTable/TokenAccount").InnerText.Trim();
                //判断管理员
                string _permit = getPermit(_account);
                if(_permit != "ADMIN" && _permit!="LSADMIN" )//非管理员
                {
                    string _player = isDriver(_account);
                    if (_player != "")
                        _account = _player; //司机
                    else
                        _account = getOUCode(_account);
                    _permit = getPermit(_account);
			
                }
                if(!string.IsNullOrEmpty(_permit))
                    _result.DocumentElement.SelectSingleNode("profile/SchemaTable/TokenPermit").InnerXml =  "<![CDATA["+ _permit+"]]>";
                else
                {
                    XmlElement _err = _result.CreateElement("errmsg");
                    _err.InnerXml = "<![CDATA[登录失败，登录用户非设计院人员]]>";
                    _result.DocumentElement.AppendChild(_err);
                }
            }
            catch(Exception _exc)
            {
                Provider.LogErr(_result, _exc);
            }
        }
        //判断是否为司机
        public string isDriver(string _account)
        {
            XmlDocument _result = XmlProvider.Document("data");
            string _sql = readXml("getDriver");
            //判断司机表是否存在该用户
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection =  SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _driverAccount = new SqlParameter("DriverAccount",SqlDbType.NVarChar,50);
                _driverAccount.Value = _account;
                _cmd.Parameters.Add(_driverAccount);
                _result = SQLProvider.GetData(_cmd);
            }
           if(_result.DocumentElement.SelectSingleNode("SchemaTable") != null)
           {
                return _result.DocumentElement.SelectSingleNode("SchemaTable/Player").InnerText.Trim();
           }
           else
           {
                return "";
           }
        }
        //获取权限
        public string getPermit(string _account )
        {
           XmlDocument _result = XmlProvider.Document("sqldata");
            string _sql = readXml("getPermit");
           using(SqlCommand _cmd = new SqlCommand())
           {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _permitAccount = new SqlParameter("PermitAccount",SqlDbType.NVarChar,50);
                _permitAccount.Value = _account;
                _cmd.Parameters.Add(_permitAccount);
                _result = SQLProvider.GetData(_cmd);
           }
           if(_result.DocumentElement.SelectSingleNode("SchemaTable") != null)
           {
                return _result.DocumentElement.SelectSingleNode("SchemaTable/ResourcePermit").InnerText.Trim();
           }
           else
           {
                return "";
           }
        }
        //获取组织编号
        //public string getDepartCode(string _account)
        //{
        //    string _departCode = "";
        //    XmlDocument _api = XmlProvider.Document("api");
        //    _api.Load("http://bpm.qgj.cn/api/bpmapi.asmx/GetUserDepartPath?profile=" + _account);
        //    foreach(XmlNode _section in _api.DocumentElement.SelectNodes("Member"))
        //    {
        //        string _ouPath = _section.SelectSingleNode("Code").InnerText;
        //        if (_ouPath.CompareTo("0002") == 1)
        //        {
        //            _departCode = "0002";
        //        }            
                
        //    }
        //    return _departCode;
        //}
        //钉钉接口获取组织架构
        public string getOUCode(string _account)
        {
            GetUserInfo user = new GetUserInfo(_account);
            if (user.getOUCode("0002").Contains("0002")){return "0002";}
            else if (user.getOUCode("0012").Contains("0012")){return "0012";}
            else{ return ""; }
        }
        public string readXml(string _type)
        {
             XmlDocument _config = XmlProvider.Document("sqlConfig");
            String _sql = "";
            //读取xml文件
            try
            {
                string _path = "/app_data/Login.xml";
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