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

    public class DispatcherDol : HttpResponseProvider
    {
        public XmlDocument result;
        public HttpContext context;
        public string conn = "WEB";
        public DispatcherDol(HttpContext _context) : base(_context) { this.context = _context; }



        /// <summary>
        /// 查询临时调度员账号
        /// </summary>
        /// <returns></returns>
        public XmlDocument getLsDispatcher()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getLsDispather");//Dispather.xml中的获取临时调度员账号的标签的type为getLsDispather
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter resourceID = new SqlParameter("ResourceID", SqlDbType.NVarChar, 50);
                resourceID.Value = "LSSYSTEM";
                _cmd.Parameters.Add(resourceID);
                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }

        /// <summary>
        /// 添加一个临时调度员
        /// </summary>
        /// <param name="_permitAccount">临时调度员账号</param>
        /// <returns></returns>
        public XmlDocument addLsDispatcher(string _permitAccount)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("addLsDispather");//Dispather.xml中的添加临时调度员的标签的type为addLsDispather

            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                
                SqlParameter resourceID = new SqlParameter("ResourceID", SqlDbType.NVarChar, 50);
                resourceID.Value = "LSSYSTEM";
                _cmd.Parameters.Add(resourceID);

                SqlParameter resourcePermit = new SqlParameter("ResourcePermit", SqlDbType.NVarChar, 50);
                resourcePermit.Value = "LSADMIN";
                _cmd.Parameters.Add(resourcePermit);

                SqlParameter resourcePermitLev = new SqlParameter("ResourcePermitLev", SqlDbType.Int);
                resourcePermitLev.Value = Convert.ToInt32(1);
                _cmd.Parameters.Add(resourcePermitLev);

                //临时调度员账号的账号名称
                SqlParameter permitAccount = new SqlParameter("PermitAccount", SqlDbType.NVarChar, 50);
                permitAccount.Value = _permitAccount;
                _cmd.Parameters.Add(permitAccount);
                
                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }


        /// <summary>
        /// 删除临时调度员账号
        /// </summary>
        /// <returns></returns>
        public XmlDocument delLsDispatcher()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("delLsDispather");//Dispatcher.xml中的删除临时调度员账号的标签的type为delLsDispather

            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter resourceID = new SqlParameter("ResourceID", SqlDbType.NVarChar, 50);
                resourceID.Value = "LSSYSTEM";
                _cmd.Parameters.Add(resourceID);

                result = SQLProvider.Transcation(_cmd);
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
                string _path = "/app_data/Dispatcher.xml";
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