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

    public class ApplyService : HttpResponseProvider
    {
        public XmlDocument result;
        public string conn = "WEB";
        public HttpContext context;
        public ApplyService(HttpContext _context) : base(_context) { this.context = _context; }

        /// <summary>
        /// 根据AppliedID获取 开始时间、天数、人数
        /// </summary>
        /// <returns></returns>
        public XmlDocument getApplyInfoByAppliedID(string _appliedID)
        {
            this.result = XmlProvider.Document("data");
            //根据AppliedID获取相应记录
            XmlDocument _appliedList = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _appliedID });
            //将查询到的参数存入result
            foreach (XmlNode _section in _appliedList.DocumentElement.SelectNodes("SchemaTable"))
            {
                this.result.DocumentElement.SetAttribute("_starttime", _section.SelectSingleNode("StartTime").InnerText.ToString().Trim());
                this.result.DocumentElement.SetAttribute("_tripNum", _section.SelectSingleNode("TripNum").InnerText.ToString().Trim());
                this.result.DocumentElement.SetAttribute("_day", _section.SelectSingleNode("Days").InnerText.ToString().Trim());
            }
            return this.result;
        }
        public XmlDocument getFreeCarListOnDesc(DateTime _startReq, int _dayReq, int _tripNum)
        {
        _startReq = Convert.ToDateTime(_startReq.ToShortDateString()+" 00:00:00");
            int _minWeekDays = int.MaxValue;
            XmlDocument _desc = XmlProvider.Document("data");
            XmlDocument _list = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getFreeCarList", new object[] { this.context }, new object[] { _startReq, _dayReq, _tripNum });
            string _licenseidList = "("; //符合条件的licenseid

            //判断星期几
            int[] _dayOfWeek = { 7, 1, 2, 3, 4, 5, 6 };
            //查询车辆月出行天数

            int _pre = 13 + _dayOfWeek[Convert.ToInt32(_startReq.DayOfWeek)];
            foreach (XmlNode _section in _list.DocumentElement.SelectNodes("SchemaTable"))
            {
                if (_section.SelectSingleNode("LicenseID").InnerText.ToString().Trim() != _list.DocumentElement.LastChild.SelectSingleNode("LicenseID").InnerText.ToString().Trim())
                    _licenseidList += " LicenseID='" + _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim() + "' or ";
                else
                    _licenseidList += " LicenseID='" + _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim() + "') ";
            }
            string _sql = " Select SUM(CAST(DAYS as int)) As MonthDays,LicenseID  from Car_Applied where AppliedStatue!='拒绝' and AppliedStatue!='取消'  AND LicenseID!='NULL' AND " + _licenseidList + " and StartTime>='" + _startReq.AddDays(-_pre).ToString() + "' and StartTime<='" + _startReq.AddDays(14).ToString() + "'  group by licenseid order by MonthDays ASC";
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                _desc = SQLProvider.GetData(_cmd);
            }

            if (!_desc.DocumentElement.HasChildNodes)
            {
                 foreach (XmlNode _section in _list.DocumentElement.SelectNodes("SchemaTable"))
                {
                    int _preWeek = _dayOfWeek[Convert.ToInt32(_startReq.DayOfWeek)] - 1;
                    int _next = 7 - _dayOfWeek[Convert.ToInt32(_startReq.DayOfWeek)];
                    string count = "";
                    using (SqlCommand _cmd = new SqlCommand())
                    {
                        _cmd.Connection = SQLConfig.Connection(this.conn);
                        _cmd.CommandText = "Select SUM(CAST(DAYS as int)) As WeekDays from Car_Applied where licenseid='" + _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim() + "' and AppliedStatue!='拒绝' and AppliedStatue!='取消' and StartTime>='" + _startReq.AddDays(-_preWeek).ToString() + "' and StartTime<='" + _startReq.AddDays(_next).ToString() + "'";
                        count = SQLProvider.GetData(_cmd).DocumentElement.SelectSingleNode("SchemaTable/WeekDays").InnerText.ToString().Trim();
                    }
                    if (count == "" || count == null) count = "0";
                    XmlElement _countEle = _list.CreateElement("WeekDays");
                    _countEle.InnerXml = "<![CDATA[" + count + "]]>";
                    XmlElement _month = _list.CreateElement("MonthDays");
                    _month.InnerXml = "<![CDATA[0]]>";
                    _section.AppendChild(_month);
                    _section.AppendChild(_countEle);
                    if (_minWeekDays > Convert.ToInt32(count))
                        _minWeekDays = Convert.ToInt32(count);
                    _minWeekDays = Convert.ToInt32(count);
                }
                _list.DocumentElement.SetAttribute("MinWeekDays", _minWeekDays.ToString());
                return _list;
            }
            //查询车辆周出行天数
            foreach (XmlNode _section in _desc.DocumentElement.SelectNodes("SchemaTable"))
            {
                int _preWeek = _dayOfWeek[Convert.ToInt32(_startReq.DayOfWeek)] - 1;
                int _next = 7 - _dayOfWeek[Convert.ToInt32(_startReq.DayOfWeek)];
                string count = "";
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = "Select SUM(CAST(DAYS as int)) As WeekDays from Car_Applied where licenseid='" + _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim() + "' and AppliedStatue!='拒绝' and AppliedStatue!='取消' and StartTime>='" + _startReq.AddDays(-_preWeek).ToString() + "' and StartTime<='" + _startReq.AddDays(_next).ToString() + "'";
                    count = SQLProvider.GetData(_cmd).DocumentElement.SelectSingleNode("SchemaTable/WeekDays").InnerText.ToString().Trim();
                }
                if (count=="" || count == null) count = "0";
                XmlElement _countEle = _desc.CreateElement("WeekDays");
                _countEle.InnerXml = "<![CDATA[" + count + "]]>";
                _section.AppendChild(_countEle);
                if (_minWeekDays > Convert.ToInt32(count))
                    _minWeekDays = Convert.ToInt32(count);
            }
            _desc.DocumentElement.SetAttribute("MinWeekDays", _minWeekDays.ToString());
            return _desc;
        }
        /// <summary>
        /// 某段时间内，符合出行条件的所有车辆信息
        /// </summary>
        /// <returns></returns>
        public XmlDocument getFreeCarList(DateTime _startReq, int _dayReq, int _tripNum)
        {
        _tripNum++;
            this.result = XmlProvider.Document("data");
            DateTime _applyDate =  Convert.ToDateTime(_startReq.ToShortDateString()+" 00:00:00");
            //DateTime _applyDate = _startReq;
            //获取所有可用车辆信息
            XmlDocument _carList = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getUseCars", new object[] { this.context }, null);
            //查询所有车是否有满足条件的车辆
            foreach (XmlNode _section in _carList.DocumentElement.SelectNodes("SchemaTable"))
            {
                if (_tripNum <= Convert.ToInt32(_section.SelectSingleNode("Sites").InnerText.ToString().Trim()))//座位数符合条件
                {
                    string _licenseID = _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim();//车牌
                    Boolean _enoughTime = true;
                    string _state = Provider.Invoke("CLDD.Providers.CarDol.getStatue", new object[] { this.context }, new object[] { _licenseID }).ToString().Trim();//获取车辆状态
                    if (_state == "空闲" || _state == "出车") //车辆为空闲或者出车状态方可预约
                    {
                        for (int i = 1; i <= _dayReq; i++)
                        {
                            //获取该车此日是否有申请记录
                            XmlDocument _carApplyData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getDayCar", new object[] { this.context }, new object[] { _licenseID, _applyDate.ToString() });
                            //获取该车此日是否有维修记录
                            XmlDocument _carFixData = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getDayFixedByLicense", new object[] { this.context }, new object[] { _licenseID, _applyDate.ToString() });
                            if (_carApplyData.DocumentElement.SelectSingleNode("SchemaTable") != null) //存在记录，不符合条件
                            {
                                _enoughTime = false;
                                break;
                            }
                            if (_carFixData.DocumentElement.SelectSingleNode("SchemaTable") != null)//存在维修记录
                            {
                                _enoughTime = false;
                                break;
                            }
                            _applyDate = _applyDate.AddDays(1);//时间改为接下来一天
                        }
                    }
                    else
                    {
                        _enoughTime = false;
                    }


                    if (_enoughTime)//可以预约
                    {
                        XmlNode _clone = this.result.ImportNode(_section, true);
                        this.result.DocumentElement.AppendChild(_clone);
                    }
                }
                _applyDate = _startReq; //查询下一辆之前将开始时间恢复为开始时间
            }
            return this.result;
        }
        /// <summary>
        /// 返回某天有几辆空闲车辆
        /// </summary>
        /// <param name="_datetime"></param>
        /// <returns></returns>
        public int getDayCarNum(string _datetime)
        {
            //获取所有可用车辆信息
            XmlDocument _carList = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getUseCars", new object[] { this.context }, null);
            int _num = 0;
            foreach (XmlNode _section in _carList.DocumentElement.SelectNodes("SchemaTable"))
            {
                string _licenseid = _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim();//车牌
                string _state = _section.SelectSingleNode("CarStatue").InnerText.ToString().Trim();//获取车辆状态
                XmlDocument _fixDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getDayFixedByLicense", new object[] { this.context }, new object[] { _licenseid, _datetime });
                if (_fixDom.DocumentElement.SelectSingleNode("SchemaTable") == null)//无维修记录
                {
                    if (_state == "空闲" || _state == "出车") //车辆为空闲或者出车状态方可预约
                    {
                        //获取该车此日是否有申请记录
                        XmlDocument _carApplyData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getDayCar", new object[] { this.context }, new object[] { _licenseid, _datetime });
                        if (_carApplyData.DocumentElement.SelectSingleNode("SchemaTable") == null)
                            _num++;
                    }
                }

            }
            return _num;
        }

        /// <summary>
        /// 选择派遣车辆
        /// </summary>
        /// <param name="_applyDom">申请记录</param>
        /// <returns></returns>
        public XmlDocument sendDriver(XmlDocument _applyDom)
        {
            this.result = XmlProvider.Document("data");
            //申请记录数据
            DateTime _aStartTime = Convert.ToDateTime(_applyDom.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString());
            int _aDay = Convert.ToInt32(_applyDom.DocumentElement.SelectSingleNode("SchemaTable/Days").InnerText.ToString());
            int _aTripNum = Convert.ToInt32(_applyDom.DocumentElement.SelectSingleNode("SchemaTable/TripNum").InnerText.ToString());//加上司机
            //申请记录数据
            //获取空闲车辆
            XmlDocument _freeCarDom = this.getFreeCarListOnDesc(_aStartTime, _aDay, _aTripNum);
            int _minWeekDays = Convert.ToInt32(_freeCarDom.DocumentElement.GetAttribute("MinWeekDays").ToString().Trim());

            foreach (XmlNode _section in _freeCarDom.DocumentElement.SelectNodes("SchemaTable"))
            {
                int _MonthDays = Convert.ToInt32(_section.SelectSingleNode("MonthDays").InnerText.ToString().Trim());
                int _weekDays = Convert.ToInt32(_section.SelectSingleNode("WeekDays").InnerText.ToString().Trim());
                if (_weekDays == _minWeekDays)
                {
                    this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getSingleCarByLicenseID", new object[] { this.context }, new object[] { _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim() });
                    break;
                }
            }
            return this.result;
        }
        //获取当前空闲车辆(用户申请车辆时系统判断车辆的方法)
        public XmlDocument getFreeCars(string _datetime)
        {
            result = XmlProvider.Document("sqldata");
            //获取所有可用车辆信息
            XmlDocument _carList = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getUseCars", new object[] { this.context }, null);
            int _maxSites = 0;
            foreach (XmlNode _section in _carList.DocumentElement.SelectNodes("SchemaTable"))
            {
                string _licenseID = _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim();//车牌
                int _sites = Convert.ToInt32(_section.SelectSingleNode("Sites").InnerText.ToString().Trim()) - 1;//座位数
                string _state = Provider.Invoke("CLDD.Providers.CarDol.getStatue", new object[] { this.context }, new object[] { _licenseID }).ToString().Trim();//获取车辆状态
                //车辆维修记录
                XmlDocument _fixDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getDayFixedByLicense", new object[] { this.context }, new object[] { _licenseID, _datetime });
                if (_fixDom.DocumentElement.SelectSingleNode("SchemaTable") == null)//无维修记录
                {
                    if (_state == "空闲" || _state == "出车") //车辆为空闲或者出车状态方可预约
                    {
                        //获取该车此日是否有申请记录
                        XmlDocument _carApplyData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getDayCar", new object[] { this.context }, new object[] { _licenseID, _datetime });
                        if (_carApplyData.DocumentElement.SelectSingleNode("SchemaTable") == null)
                        {
                            XmlElement _elSchemaTable = this.result.CreateElement("SchemaTable");
                            XmlElement _elLicenseID = this.result.CreateElement("LicenseID");
                            _elLicenseID.InnerXml = "<![CDATA[" + _licenseID + "]]>";
                            _elSchemaTable.AppendChild(_elLicenseID);
                            this.result.DocumentElement.AppendChild(_elSchemaTable);

                            XmlElement _elSites = this.result.CreateElement("Sites");
                            _elSites.InnerXml = "<![CDATA[" + _sites.ToString() + "]]>";
                            _elSchemaTable.AppendChild(_elSites);

                            if (_sites > _maxSites)
                                _maxSites = _sites;
                        }
                    }
                }

            }
            this.result.DocumentElement.SetAttribute("maxSites", _maxSites.ToString());
            return this.result;
        }
        //查找某车冲突记录
        public XmlDocument getClashApplyList(string _licenseid, int _dalyDay, DateTime _sqlEndTime)
        {
            this.result = XmlProvider.Document("sqldata");
            DateTime _starttime = _sqlEndTime.AddDays(1);//延迟后的第一天
            DateTime _finalStartTime = _sqlEndTime.AddDays(_dalyDay);   //最迟的申请记录的开始时间
            while (DateTime.Compare(_starttime, _finalStartTime) <= 0)  //最迟的开始时间比上一条记录的结束时间大
            {
                //获取从延迟第一日开始的所有预约记录
                XmlDocument _clashDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.clashApply", new object[] { this.context }, new object[] { _licenseid, _starttime });//冲突记录
                if (_clashDom.DocumentElement.SelectSingleNode("SchemaTable") != null)  //有冲突记录
                {
                    //将冲突记录放入result
                    XmlNode _section = this.result.ImportNode(_clashDom.DocumentElement.SelectSingleNode("SchemaTable"), true);
                    this.result.DocumentElement.AppendChild(_section);
                    //冲突记录的结束时间为下一个记录的开始时间
                    _starttime = Convert.ToDateTime(_clashDom.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString());

                }
                _starttime = _starttime.AddDays(1);
            }
            return this.result;
        }

    }
}