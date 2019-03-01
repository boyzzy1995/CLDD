namespace CLDD.Providers
{
    using System;
    using System.Xml;
    using System.Web;
    using System.Data;
    using System.Data.SqlClient;
    using CS.Providers;

    public class DriverDol : HttpResponseProvider
    {
        public XmlDocument result;
        public HttpContext context;
        public string conn = "WEB";
        public DriverDol(HttpContext _context) : base(_context) { this.context = _context; }

        //查找非分配车辆司机
        public XmlDocument getFreeDriver()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getFreeDriver");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }
        //根据账号查找司机信息
        public XmlDocument getSingleDetailByAccount(string _account)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getByAccount");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter account = new SqlParameter("DriverAccount", SqlDbType.NVarChar, 50);
                account.Value = _account;
                _cmd.Parameters.Add(account);
                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }
        //获取司机列表
        public XmlDocument getDriverList()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getDriverList");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }
        //获取司机详情
        public XmlDocument getDriverDetail(string _driverid)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getDriverDetail");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter driverid = new SqlParameter("DriverID", SqlDbType.NVarChar, 50);
                driverid.Value = _driverid;
                _cmd.Parameters.Add(driverid);
                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }
        //更新车牌
        public XmlDocument upLicense(string _driverid, string _licenseid)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("upLicense");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter driverid = new SqlParameter("DriverID", SqlDbType.NVarChar, 50);
                driverid.Value = _driverid;
                _cmd.Parameters.Add(driverid);
                SqlParameter licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseID.Value = _licenseid;
                _cmd.Parameters.Add(licenseID);
                result = SQLProvider.Transcation(_cmd);
            }
            return this.result;
        }
        /// <summary>
        /// 添加司机
        /// </summary>
        /// <param name="_driverID"></param>
        /// <param name="_driverName"></param>
        /// <param name="_driverAccount"></param>
        /// <param name="_carTelephone"></param>
        /// <param name="_licenseID"></param>
        /// <returns></returns>
        public XmlDocument addDriver(string _driverID, string _driverName, string _driverAccount, string _carTelephone, string _licenseID)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("addDriver");//driver.xml中的添加车辆的标签的type为addDriver

            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter driverID = new SqlParameter("DriverID", SqlDbType.NVarChar, 50);
                driverID.Value = _driverID;
                _cmd.Parameters.Add(driverID);

                SqlParameter driverName = new SqlParameter("DriverName", SqlDbType.NVarChar, 50);
                driverName.Value = _driverName;
                _cmd.Parameters.Add(driverName);

                SqlParameter driverAccount = new SqlParameter("DriverAccount", SqlDbType.NVarChar, 50);
                driverAccount.Value = _driverAccount;
                _cmd.Parameters.Add(driverAccount);

                SqlParameter carTelephone = new SqlParameter("CarTelephone", SqlDbType.NVarChar, 50);
                carTelephone.Value = _carTelephone;
                _cmd.Parameters.Add(carTelephone);

                SqlParameter licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseID.Value = _licenseID;
                _cmd.Parameters.Add(licenseID);


                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        //修改司机
         public XmlDocument motifyDriver(string _driverID, string _driverName, string _driverAccount, string _carTelephone, string _licenseID)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("motifyDriver");

            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter driverID = new SqlParameter("DriverID", SqlDbType.NVarChar, 50);
                driverID.Value = _driverID;
                _cmd.Parameters.Add(driverID);

                SqlParameter driverName = new SqlParameter("DriverName", SqlDbType.NVarChar, 50);
                driverName.Value = _driverName;
                _cmd.Parameters.Add(driverName);

                SqlParameter driverAccount = new SqlParameter("DriverAccount", SqlDbType.NVarChar, 50);
                driverAccount.Value = _driverAccount;
                _cmd.Parameters.Add(driverAccount);

                SqlParameter carTelephone = new SqlParameter("CarTelephone", SqlDbType.NVarChar, 50);
                carTelephone.Value = _carTelephone;
                _cmd.Parameters.Add(carTelephone);

                SqlParameter licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseID.Value = _licenseID;
                _cmd.Parameters.Add(licenseID);


                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        /// <summary>
        /// 删除司机
        /// </summary>
        /// <param name="_driverID"></param>
        /// <returns></returns>
        public XmlDocument delDriver(string _driverID)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("delDriver");//driver.xml中的添加车辆的标签的type为addDriver

            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter driverID = new SqlParameter("DriverID", SqlDbType.NVarChar, 50);
                driverID.Value = _driverID;
                _cmd.Parameters.Add(driverID);

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
                string _path = "/app_data/driver.xml";
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