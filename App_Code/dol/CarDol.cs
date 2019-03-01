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

    public class CarDol : HttpResponseProvider
    {
        public XmlDocument result;
        public HttpContext context;
        public string conn = "WEB";
        public CarDol(HttpContext _context) : base(_context) { this.context = _context; }




        /// <summary>
        /// 查询所有车辆状态为出行、维修、空闲的车辆的车牌集合
        /// </summary>
        /// <returns></returns>
        public XmlDocument getEnableCarLicense()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getEnableCarLicense");//Car.xml中的获取所有车辆状态为出行、维修、空闲的车辆的车牌的标签的type为 getEnableCarLicense
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                this.result = SQLProvider.GetData(_cmd);
            }
            return result;
        }
        /// <summary>
        /// 查询某车牌的车辆在某天的车辆状态
        /// </summary>
        /// <param name="_LicenseID"></param>
        /// <param name="_Day"></param>
        /// <returns></returns>
        public string getCarStateByLicenseIDAndDay(string _LicenseID, string _Day)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getCarStateByLicenseIDAndDay");//Car.xml 中的 查询某车牌的车辆在某天的车辆状态 的标签的 type 为 getCarStateByLicenseIDAndDay
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _dChangeDateTime = new SqlParameter("ChangeDateTime", SqlDbType.NVarChar, 50);
                _dChangeDateTime.Value = _Day;
                _cmd.Parameters.Add(_dChangeDateTime);

                SqlParameter _dLicenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                _dLicenseID.Value = _LicenseID;
                _cmd.Parameters.Add(_dLicenseID);

                result = SQLProvider.GetData(_cmd);
            }
            if (this.result.DocumentElement.SelectSingleNode("SchemaTable") == null)
            {
                return "";
            }
            else
            {
                return this.result.DocumentElement.SelectSingleNode("SchemaTable/NewCarState").InnerText.ToString().Trim();//车辆状态
            }

        }
        //获取总公里数--账号
        public XmlDocument getKm(string _account)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getKm");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _dAccount = new SqlParameter("DriverAccount", SqlDbType.NVarChar, 50);
                _dAccount.Value = _account;
                _cmd.Parameters.Add(_dAccount);
                result = SQLProvider.GetData(_cmd);
            }
            return result;
        }
        //获取总公里数
        public XmlDocument getKmByLicense(string _licenseid)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getKmByLicense");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter licenseid = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseid.Value = _licenseid;
                _cmd.Parameters.Add(licenseid);
                result = SQLProvider.GetData(_cmd);
            }
            return result;
        }
        //获取未分配车辆
        public XmlDocument getCarUnallocated()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getCarUnallocated");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                result = SQLProvider.GetData(_cmd);
            }
            return result;
        }
        //获取可用车牌
        public XmlDocument getLicenseIDList()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getLicenseIDList");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                result = SQLProvider.GetData(_cmd);
            }
            return result;
        }
        //更新总公里数
        public XmlDocument upKm(string _km, string _account)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("upKm");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _diAccount = new SqlParameter("DriverAccount", SqlDbType.NVarChar, 50);
                _diAccount.Value = _account;
                _cmd.Parameters.Add(_diAccount);
                SqlParameter _kmLength = new SqlParameter("Kilometers", SqlDbType.NVarChar, 50);
                _kmLength.Value = _km;
                _cmd.Parameters.Add(_kmLength);
                result = SQLProvider.Transcation(_cmd);

            }
            return result;
        }
        /// <summary>
        /// 更新车辆状态
        /// </summary>
        /// <param name="_licenseID">车牌</param>
        /// <returns></returns>
        public XmlDocument upCarStatue(string _guid, string _carStatue)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("upCarStatue");//Car.xml中的更新车辆状态的标签的type为upCarStatue
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter carStatue = new SqlParameter("CarStatue", SqlDbType.NVarChar, 50);
                carStatue.Value = _carStatue;
                _cmd.Parameters.Add(carStatue);
                SqlParameter guid = new SqlParameter("Guid", SqlDbType.NVarChar, 50);
                guid.Value = _guid;
                _cmd.Parameters.Add(guid);
                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        public XmlDocument upCarStatueByLicenseID(string _licenseID, string _carStatue)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("upCarStatueByLicenseID");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter carStatue = new SqlParameter("CarStatue", SqlDbType.NVarChar, 50);
                carStatue.Value = _carStatue;
                _cmd.Parameters.Add(carStatue);
                SqlParameter guid = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                guid.Value = _licenseID;
                _cmd.Parameters.Add(guid);
                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        /// <summary>
        /// 获取车辆状态
        /// </summary>
        /// <param name="_reqLicenseID"></param>
        /// <returns></returns>
        public string getStatue(string _reqLicenseID)
        {
            this.result = XmlProvider.Document("sqldata");
            string _sql = readXml("getCarStatue");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseID.Value = _reqLicenseID;
                _cmd.Parameters.Add(licenseID);
                result = SQLProvider.GetData(_cmd);
            }

            return this.result.DocumentElement.SelectSingleNode("SchemaTable/CarStatue").InnerText.ToString().Trim();//车辆状态
        }
        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <param name="_licenseID"></param>
        /// <param name="_driverAccount">若司机为空，则车辆状态为未分配</param>
        /// <param name="_sites"></param>
        /// <param name="_mot"></param>
        /// <param name="_insurance"></param>
        /// <param name="_kilometers"></param>
        /// <returns></returns>
        public XmlDocument addCar(string _guid, string _licenseID, string _sites, string _mot, string _insurance, string _kilometers)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("addCar");//Car.xml中的添加车辆的标签的type为addCar
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter guid = new SqlParameter("Guid", SqlDbType.NVarChar, 50);
                guid.Value = _guid;
                _cmd.Parameters.Add(guid);
                SqlParameter licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseID.Value = _licenseID;
                _cmd.Parameters.Add(licenseID);
                SqlParameter sites = new SqlParameter("Sites", SqlDbType.Int);
                sites.Value = Convert.ToInt32(_sites);
                _cmd.Parameters.Add(sites);
                SqlParameter mot = new SqlParameter("MOT", SqlDbType.NVarChar, 50);
                mot.Value = _mot;
                _cmd.Parameters.Add(mot);
                SqlParameter insurance = new SqlParameter("Insurance", SqlDbType.NVarChar, 50);
                insurance.Value = _insurance;
                _cmd.Parameters.Add(insurance);
                SqlParameter kilometers = new SqlParameter("Kilometers", SqlDbType.NVarChar, 50);
                kilometers.Value = _kilometers;
                _cmd.Parameters.Add(kilometers);
                SqlParameter carStatue = new SqlParameter("CarStatue", SqlDbType.NVarChar, 50);
                carStatue.Value = "未分配";
                _cmd.Parameters.Add(carStatue);

                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        /// <summary>
        /// 修改车辆
        /// </summary>
        public XmlDocument motifyCar(string _guid, string _licenseID, string _driverAccount, string _sites, string _mot, string _insurance, string _kilometers)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("motifyCar");

            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter guid = new SqlParameter("Guid", SqlDbType.NVarChar, 50);
                guid.Value = _guid;
                _cmd.Parameters.Add(guid);
                SqlParameter licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseID.Value = _licenseID;
                _cmd.Parameters.Add(licenseID);
                SqlParameter driverAccount = new SqlParameter("DriverAccount", SqlDbType.NVarChar, 50);
                driverAccount.Value = _driverAccount;
                _cmd.Parameters.Add(driverAccount);
                SqlParameter sites = new SqlParameter("Sites", SqlDbType.Int);
                sites.Value = Convert.ToInt32(_sites);
                _cmd.Parameters.Add(sites);
                SqlParameter mot = new SqlParameter("MOT", SqlDbType.NVarChar, 50);
                mot.Value = _mot;
                _cmd.Parameters.Add(mot);
                SqlParameter insurance = new SqlParameter("Insurance", SqlDbType.NVarChar, 50);
                insurance.Value = _insurance;
                _cmd.Parameters.Add(insurance);
                SqlParameter kilometers = new SqlParameter("Kilometers", SqlDbType.NVarChar, 50);
                kilometers.Value = _kilometers;
                _cmd.Parameters.Add(kilometers);

                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        //获取所有可用车辆
        public XmlDocument getUseCars()
        {
            result = XmlProvider.Document("data");
            string _sql = readXml("getUseCars");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                result = SQLProvider.GetData(_cmd);
            }
            return result;
        }
        //解除司机与车辆绑定
        public XmlDocument unBound(string _licenseID)
        {
            result = XmlProvider.Document("data");
            string _sql = readXml("unBound");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseID.Value = _licenseID;
                _cmd.Parameters.Add(licenseID);
                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        //司机与车辆绑定
        public XmlDocument Bound(string _licenseID, string _account)
        {
            result = XmlProvider.Document("data");
            string _sql = readXml("Bound");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseID.Value = _licenseID;
                _cmd.Parameters.Add(licenseID);
                SqlParameter account = new SqlParameter("DriverAccount", SqlDbType.NVarChar, 50);
                account.Value = _account;
                _cmd.Parameters.Add(account);
                XmlDocument _api = XmlProvider.Document("api");
                _api.Load("http://api.qgj.cn/webapi/profile.asmx/GetUser?User=" + _account);
                string _name = _api.DocumentElement.SelectSingleNode("ITEM/name").InnerText;
                SqlParameter name = new SqlParameter("DriverName", SqlDbType.NVarChar, 50);
                name.Value = _name;
                _cmd.Parameters.Add(name);
                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        //获取需要调度员控制的车辆数量
        public int getControlNum()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getControlCarNum");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                result = SQLProvider.GetData(_cmd);
            }

            return Convert.ToInt32(result.DocumentElement.SelectSingleNode("SchemaTable/SetValue").InnerText.ToString().Trim());
        }
        //更新需要调度员控制的车辆数量
        public XmlDocument upControlNum(int _num)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("upControlCarNum");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter SetValue = new SqlParameter("SetValue", SqlDbType.NVarChar, 50);
                SetValue.Value = _num;
                _cmd.Parameters.Add(SetValue);
                result = SQLProvider.Transcation(_cmd);
            }

            return result;
        }
        //获取所有车辆
        public XmlDocument getCarList()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getCarList");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }
        public XmlDocument getSingleCar(string _guid)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getSingleCar");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter guid = new SqlParameter("Guid", SqlDbType.NVarChar, 50);
                guid.Value = _guid;
                _cmd.Parameters.Add(guid);
                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }
        //获取待联系方式的车辆记录
        public XmlDocument getSingleCarAndCarTel(string _guid)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getSingleCarAndCarTel");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter guid = new SqlParameter("Guid", SqlDbType.NVarChar, 50);
                guid.Value = _guid;
                _cmd.Parameters.Add(guid);
                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }


        //获取待联系方式的车辆记录ByLicenseID
        public XmlDocument getSingleCarByLicenseID(string _licenseID)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getSingleCarByLicenseID");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                licenseID.Value = _licenseID;
                _cmd.Parameters.Add(licenseID);
                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }



        /// <summary>
        /// 插入车辆状态更改记录
        /// </summary>
        /// <param name="_parGuid"></param>
        /// <param name="_parLicenseID"></param>
        /// <param name="_parState"></param>
        /// <returns></returns>
        public XmlDocument insertChangeState(string _parLicenseID, string _parState)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("insertChangeState");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                SqlParameter _guid = new SqlParameter("Guid", SqlDbType.NVarChar, 50);
                _guid.Value = Provider.GUID;
                _cmd.Parameters.Add(_guid);
                SqlParameter _licenseid = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                _licenseid.Value = _parLicenseID;
                _cmd.Parameters.Add(_licenseid);
                SqlParameter _state = new SqlParameter("NewCarState", SqlDbType.NVarChar, 50);
                _state.Value = _parState;
                _cmd.Parameters.Add(_state);
                SqlParameter _datetime = new SqlParameter("ChangeDatetime", SqlDbType.NVarChar, 50);
                _datetime.Value = DateTime.Now;
                _cmd.Parameters.Add(_datetime);
                result = SQLProvider.Transcation(_cmd);
            }
            return this.result;
        }
        /// <summary>
        /// 车辆维修列表
        /// </summary>
        /// <returns></returns>
        public XmlDocument getFixedList()
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getFixdList");
            try
            {
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = _sql;

                    result = SQLProvider.GetData(_cmd);
                }
            }
            catch (Exception _exc)
            {
                Provider.LogErr(result, _exc);
            }
            return result;
        }
        /// <summary>
        /// 添加维修记录
        /// </summary>
        /// <param name="_reqLisenceID">车牌</param>
        /// <param name="_reqStartTime">开始时间</param>
        /// <param name="_reqEndTime">结束时间</param>
        /// <returns></returns>
        public XmlDocument addFixed(string _reqLicenseID, string _reqStartTime, string _reqEndTime)
        {
	   DateTime _reqEndTimeDay = Convert.ToDateTime(_reqEndTime);
	    _reqEndTime = _reqEndTimeDay.Year + "-" + _reqEndTimeDay.Month + "-" + _reqEndTimeDay.Day + " 23:59:59";
            result = XmlProvider.Document("configdata");
            string _sql = readXml("addFixed");
            try
            {
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = _sql;
                    SqlParameter _guid = new SqlParameter("FixID", SqlDbType.NVarChar, 50);
                    _guid.Value = Provider.GUID;
                    _cmd.Parameters.Add(_guid);
                    SqlParameter _licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                    _licenseID.Value = _reqLicenseID;
                    _cmd.Parameters.Add(_licenseID);
                    SqlParameter _startTime = new SqlParameter("StartTime", SqlDbType.NVarChar, 50);
                    _startTime.Value = Convert.ToDateTime(_reqStartTime);
                    _cmd.Parameters.Add(_startTime);
                    SqlParameter _endTime = new SqlParameter("EndTime", SqlDbType.NVarChar, 50);
                    _endTime.Value = Convert.ToDateTime(_reqEndTime);
                    _cmd.Parameters.Add(_endTime);
                    SqlParameter _statue = new SqlParameter("Statue", SqlDbType.NVarChar, 10);
                    _statue.Value = "未开始";
                    _cmd.Parameters.Add(_statue);

                    result = SQLProvider.Transcation(_cmd);
                }
            }
            catch (Exception _exc)
            {
                Provider.LogErr(result, _exc);
            }
            return result;
        }
	 /// <summary>
        /// 更改维修记录
        /// </summary>
        /// <param name="_reqFixID"></param>
        /// <param name="_reqStartTime"></param>
        /// <param name="_reqEndTime"></param>
        /// <returns></returns>
        public XmlDocument editFixed(string _reqFixID, DateTime _reqStartTime, DateTime _reqEndTime)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("editFixed");
            try
            {
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = _sql;
                    SqlParameter _guid = new SqlParameter("FixID", SqlDbType.NVarChar, 50);
                    _guid.Value = _reqFixID;
                    _cmd.Parameters.Add(_guid);;
                    SqlParameter _startTime = new SqlParameter("StartTime", SqlDbType.NVarChar, 50);
                    _startTime.Value = Convert.ToDateTime(_reqStartTime);
                    _cmd.Parameters.Add(_startTime);
                    SqlParameter _endTime = new SqlParameter("EndTime", SqlDbType.NVarChar, 50);
                    _endTime.Value = Convert.ToDateTime(_reqEndTime);
                    _cmd.Parameters.Add(_endTime);

                    result = SQLProvider.Transcation(_cmd);
                }
            }
            catch (Exception _exc)
            {
                Provider.LogErr(result, _exc);
            }
            return result;
        }
        /// <summary>
        /// 取消维修
        /// </summary>
        /// <param name="_reqFixID"></param>
        /// <returns></returns>
        public XmlDocument cancelFixedStatue(string _reqFixID)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("cancelFixedStatue");
            try
            {
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = _sql;
                    SqlParameter _guid = new SqlParameter("FixID", SqlDbType.NVarChar, 50);
                    _guid.Value = _reqFixID;
                    _cmd.Parameters.Add(_guid);

                    result = SQLProvider.Transcation(_cmd);
                }
            }
            catch (Exception _exc)
            {
                Provider.LogErr(result, _exc);
            }
            return this.result;
        }
        /// <summary>
        /// 结束维修
        /// </summary>
        /// <param name="_reqFixID"></param>
        /// <returns></returns>
        public XmlDocument endFixedStatue(string _reqFixID)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("endFixedStatue");
            try
            {
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = _sql;
                    SqlParameter _guid = new SqlParameter("FixID", SqlDbType.NVarChar, 50);
                    _guid.Value = _reqFixID;
                    _cmd.Parameters.Add(_guid);
                    SqlParameter _endTime = new SqlParameter("EndTime", SqlDbType.NVarChar, 50);
                    _endTime.Value = DateTime.Now.ToString();
                    _cmd.Parameters.Add(_endTime);

                    result = SQLProvider.Transcation(_cmd);
                }
            }
            catch (Exception _exc)
            {
                Provider.LogErr(result, _exc);
            }
            return this.result;
        }
        /// <summary>
        /// 查询某日某车是否有维修记录
        /// </summary>
        /// <param name="_reqLicenseID">车牌</param>
        /// <param name="_reqDate">日期</param>
        /// <returns>维修记录</returns>
        public XmlDocument getDayFixedByLicense(string _reqLicenseID,string _reqDate)
        {
            result = XmlProvider.Document("configdata");
            string _sql = readXml("getDayFixedByLicense");
            try
            {
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = _sql;
                    SqlParameter _licenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                    _licenseID.Value = _reqLicenseID;
                    _cmd.Parameters.Add(_licenseID);
                    SqlParameter _startTime = new SqlParameter("StartTime", SqlDbType.NVarChar, 50);
                    _startTime.Value = Convert.ToDateTime(_reqDate);
                    _cmd.Parameters.Add(_startTime);

                    result = SQLProvider.GetData(_cmd);
                }
            }
            catch (Exception _exc)
            {
                Provider.LogErr(result, _exc);
            }
            return this.result;
        }
        //读取XML
        public string readXml(string _type)
        {
            XmlDocument _config = XmlProvider.Document("sqlConfig");
            String _sql = "";
            //读取xml文件
            try
            {
                string _path = "/app_data/car.xml";
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