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
    using System.Text;

    using Newtonsoft.Json;
    public class ApplyResponse : HttpResponseProvider
    {
        public HttpContext context;
        public XmlDocument config;
        public XmlDocument result;
        public string conn = "WEB";
        public ApplyResponse(HttpContext _context) : base(_context) { this.context = _context; }
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
		case "debug":break;
                case "index": break;    //主页
                case "getapplylist": getApplyList(); break; //当前任务
                case "reqapply": reqApply(); break;       //请求预约
                case "reqapplyform": break;//预约表单页  
                case "req_isapply": isApply(); return this.result;//判断是否有满足条件的车辆
                case "index_waitlist": getWaitApplyList(); break;//待出车列表
                //后台
                case "reqsites": isSites(); return this.result;//判断座位数是否有符合的车辆   
                case "applyreview": applyReview(); return this.result;//预约车辆审核
                case "apply_pass": upPassStatue(); return this.result;//通过预约申请
                case "apply_refuse": upRefuStatue(); return this.result;//拒绝申请
                case "cancal_apply": cancelApply(); return this.result;//取消申请
                case "getapplyreviewlist": getApplyReviewList(); break;//获取待审核列表
                case "getapply_waitlist": getWaitApplyList(); break;//待出车列表
                case "getcar_freelist": getFreeCarList(); return this.result;//返回空闲车辆车牌
                case "edit_car": editCar(); return this.result; //调度员更换用车
                case "manapply": break;
                case "req_manapply": reqManApply(); return this.result; //人工派车提交表单

                case "start_apply": startApply(); return this.result;    //开始行程
                case "end_apply": endApply(); return this.result;    //结束行程    
                case "dely_apply": delyApply(); return this.result; //延迟行程 
                case "backstage_homepage": break;   //后台主页

                case "getchangecar": getFreeCarListCanChange(); return this.result;//获取所有可以变更的（空闲）车辆信息
                case "changefreecar": changeFreeCar(); return this.result;//变更车辆信息
            }

            //附加显示样式
            XmlElement _template = this.result.CreateElement("template");
            _template.SetAttribute("output", "html");
            if (File.Exists(Provider.Path("/ui/apply/" + this.PARAMS["guid"].ToString() + ".xsl")))
            {
                _template.InnerXml = "/ui/apply/" + this.PARAMS["guid"].ToString() + ".xsl";
            }
            else
            {
                _template.InnerXml = "/ui/pages/err.xsl";
            }
            this.result.DocumentElement.AppendChild(_template);
            return result;
        }
        public XmlDocument getFreeCarListOnDesc(DateTime _startReq, int _dayReq, int _tripNum)
        {
            _startReq = Convert.ToDateTime(_startReq.ToShortDateString() + " 00:00:00");
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
                    _section.AppendChild(_countEle);
                    if (_minWeekDays > Convert.ToInt32(count))
                        _minWeekDays = Convert.ToInt32(count);
                    _minWeekDays = Convert.ToInt32(count);
                }
                _desc.DocumentElement.SetAttribute("MinWeekDays", _minWeekDays.ToString());
                return _list;
            }
            // return _desc;
            ////查询车辆周出行天数
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
                if (count == "" || count == null) count = "0";
                XmlElement _countEle = _desc.CreateElement("WeekDays");
                _countEle.InnerXml = "<![CDATA[" + count + "]]>";
                _section.AppendChild(_countEle);
                if (_minWeekDays > Convert.ToInt32(count))
                    _minWeekDays = Convert.ToInt32(count);
                _minWeekDays = Convert.ToInt32(count);
            }
            _desc.DocumentElement.SetAttribute("MinWeekDays", _minWeekDays.ToString());
            _desc.DocumentElement.SetAttribute("count", _desc.HasChildNodes.ToString());
            return _desc;
        }

       
        /// <summary>
        /// 变更预约记录的车辆信息（预约记录ID，变更的车牌）
        /// </summary>
        public void changeFreeCar()
        {
            this.result = XmlProvider.Document("data");

            //获取前端向服务器传递该记录的AppliedID
            string _appliedID = Convert.ToString(this.context.Request.QueryString["appliedID"]);
            //获取前端向服务器传递的LicenseID
            string _licenseID = Convert.ToString(this.context.Request.QueryString["licenseID"]);

            //根据AppliedID获取相应记录
            XmlDocument _appliedList = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _appliedID });


            //根据车牌查找车辆信息，变更预约记录中的司机车辆信息
            XmlDocument _carDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getSingleCarByLicenseID", new object[] { this.context }, new object[] { _licenseID });

            if (_carDom.DocumentElement.SelectSingleNode("SchemaTable") != null)//存在数据
            {
                string _driverName = _carDom.DocumentElement.SelectSingleNode("SchemaTable/DriverName").InnerText.ToString().Trim();
                string _driverAccount = _carDom.DocumentElement.SelectSingleNode("SchemaTable/DriverAccount").InnerText.ToString().Trim();
                string _LicenseID = _carDom.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString().Trim();
                string _tel = _carDom.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();
                //插入车牌信息
                XmlDocument _sendDriverDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.sendDriver", new object[] { this.context }, new object[] { _appliedID, _driverAccount, _driverName, _LicenseID, _tel });
                if (_sendDriverDom.DocumentElement.Attributes["affect"].Value == "1")//插入车辆信息成功
                {
                    this.result.DocumentElement.SetAttribute("affect", "1");
                    //获取更新后的预约信息
                    XmlDocument _applyDetail = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _appliedID });
                    //通知4类人员，原司机、调度员、新司机、申请人。
                    Provider.Invoke("CLDD.Providers.Notice.changeSuccSendAllOut", new object[] { this.context }, new object[] { _appliedList, _applyDetail });

                }
            }
        }

        /// <summary>
        /// 获取可以更换的空闲车辆车牌列表,返回所有可以的车辆信息
        /// </summary>
        public void getFreeCarListCanChange()
        {
            this.result = XmlProvider.Document("data");

            //获取前端向服务器传递该记录的AppliedID值
            string _appliedID = Convert.ToString(this.context.Request.QueryString["guid"]);
            //通过AppliedID获取该记录的（开始时间、天数、人数）
            XmlDocument _detailApplied = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _appliedID });
            //取出参数
            DateTime _starttime = Convert.ToDateTime(_detailApplied.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim());
            int _tripNum = Convert.ToInt32(_detailApplied.DocumentElement.SelectSingleNode("SchemaTable/TripNum").InnerText.ToString().Trim());
            int _day = Convert.ToInt32(_detailApplied.DocumentElement.SelectSingleNode("SchemaTable/Days").InnerText.ToString().Trim());

            //查找该时间段的空闲车辆,ApplyService.cs里面的getFreeCarList(开始时间、天数、人数)
            XmlDocument _carList = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getFreeCarList", new object[] { this.context }, new object[] { _starttime, _day, _tripNum });
            XmlElement _elSchemaTable = this.result.CreateElement("SchemaTable");
            foreach (XmlNode _section in _carList.DocumentElement.SelectNodes("SchemaTable"))
            {
                string _licenseID = _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim();//车牌
                XmlElement _elLicenseID = this.result.CreateElement("LicenseID");
                _elLicenseID.InnerXml = _licenseID;
                _elSchemaTable.AppendChild(_elLicenseID);
            }
            this.result.DocumentElement.AppendChild(_elSchemaTable);

        }
        /// <summary>
        /// 判断是否有符合条件的车辆
        /// </summary>
        public void isApply()
        {
            DateTime _reqStarttime = Convert.ToDateTime(this.context.Request.QueryString["starttime"]);
            int _reqDays = Convert.ToInt32(this.context.Request.QueryString["days"]);
            int _reqTripNum = Convert.ToInt32(this.context.Request.QueryString["tripnum"]);//+1是加上司机

            //获取空闲车辆
            XmlDocument _freeCarDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getFreeCarList", new object[] { this.context }, new object[] { _reqStarttime, _reqDays, _reqTripNum });
            if (_freeCarDom.DocumentElement.SelectSingleNode("SchemaTable") != null)//有车
            {
                this.result.DocumentElement.SetAttribute("affect", "1");
                this.result.DocumentElement.SetAttribute("title", "可申请");
            }
            else
            {
                XmlDocument _carDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getFreeCars", new object[] { this.context }, new object[] { _reqStarttime.ToString() });
                if (_carDom.DocumentElement.SelectNodes("SchemaTable").Count == 0)
                {
                    this.result.DocumentElement.SetAttribute("affect", "-1");
                    this.result.DocumentElement.SetAttribute("title", "当前无可用车辆");
                }
                else
                {
                    int _tempDay = 0;
                    foreach (XmlNode _section in _carDom.DocumentElement.SelectNodes("SchemaTable"))
                    {
                        string _licenseID = _section.SelectSingleNode("LicenseID").InnerText.ToString();
                        if (Convert.ToInt32(_section.SelectSingleNode("Sites").InnerText.ToString()) >= _reqTripNum)//车辆符合条件
                        {
                            DateTime _datetime = _reqStarttime;
                            int _outNum = 0;
                            for (int i = 1; i <= _reqDays; i++)
                            {
                                XmlDocument _fixDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getDayFixedByLicense", new object[] { this.context }, new object[] { _licenseID, _datetime.ToString() });
                                if (_fixDom.DocumentElement.SelectSingleNode("SchemaTable") == null)//无维修记录
                                {
                                    //查找某日是否有记录
                                    XmlDocument _dateDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getDayCar", new object[] { this.context }, new object[] { _licenseID, _datetime.ToString() });
                                    if (_dateDom.DocumentElement.SelectSingleNode("SchemaTable") == null)//无记录
                                    {
                                        _outNum++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                _datetime = _datetime.AddDays(1);
                            }
                            if (_tempDay < _outNum)
                            {
                                _tempDay = _outNum;
                            }
                        }

                    }
                    if (_tempDay == 0)
                    {
                        this.result.DocumentElement.SetAttribute("affect", "-1");
                        this.result.DocumentElement.SetAttribute("title", "当前无可用车辆");
                    }
                    else
                    {
                        this.result.DocumentElement.SetAttribute("affect", "0");
                        this.result.DocumentElement.SetAttribute("title", "当前车辆最多只能申请：" + _tempDay + "天");
                    }
                }
            }
        }
        /// <summary>
        /// 人工派车提交信息
        /// </summary>
        public void reqManApply()
        {
            this.result = XmlProvider.Document("data");
            //前台传递的参数
            string _reqAccount = this.context.Request.Form["ApplyAccount"];
           // XmlDocument _user = XmlProvider.Document("user");
            //_user.Load("http://api.qgj.cn/webapi/profile.asmx/GetUser?User=" + _reqAccount);
            //string _reqName = _user.DocumentElement.SelectSingleNode("ITEM/name").InnerText.ToString().Trim();
		GetUserInfo user = new GetUserInfo( _reqAccount);
		string _reqName = user.getUserName();
            string _reqGuid = this.context.Request.Form["LicenseID"];//车牌
                                                                     //向数据库插入申请记录
            XmlDocument _insert = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.insertApply", new object[] { this.context }, new object[] { _reqName, _reqAccount });
            if (_insert.DocumentElement.Attributes["affect"].Value == "1")//插入成功
            {
                string _guid = _insert.DocumentElement.Attributes["guid"].Value;//申请记录主键
                                                                                //根据车牌查找车辆信息
                XmlDocument _carDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getSingleCarAndCarTel", new object[] { this.context }, new object[] { _reqGuid });
                if (_carDom.DocumentElement.SelectSingleNode("SchemaTable") != null)//存在数据
                {
                    string _driverName = _carDom.DocumentElement.SelectSingleNode("SchemaTable/DriverName").InnerText.ToString().Trim();
                    string _driverAccount = _carDom.DocumentElement.SelectSingleNode("SchemaTable/DriverAccount").InnerText.ToString().Trim();
                    string _LicenseID = _carDom.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString().Trim();
                    string _tel = _carDom.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();
                    //插入车牌信息
                    XmlDocument _sendDriverDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.sendDriver", new object[] { this.context }, new object[] { _guid, _driverAccount, _driverName, _LicenseID, _tel });
                    if (_sendDriverDom.DocumentElement.Attributes["affect"].Value == "1")//插入车辆信息成功
                    {
                        //更改车辆状态为待出车
                        XmlDocument _changeCarStateDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upApplyStatue", new object[] { this.context }, new object[] { _guid, "待出车" });
                        if (_changeCarStateDom.DocumentElement.Attributes["affect"].Value == "1")
                        {
                            this.result.DocumentElement.SetAttribute("affect", "1");
                            XmlDocument _applyDetail = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _guid });
                            //通知司机
                            Provider.Invoke("CLDD.Providers.Notice.applySuccResult", new object[] { this.context }, new object[] { _applyDetail });
                        }

                    }
                }
            }
        }
        /// <summary>
        /// 审核预约记录列表
        /// </summary>
        public void getApplyReviewList()
        {
            this.result = XmlProvider.Document("data");
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApplyReviewList", new object[] { this.context }, null);
        }

        /// <summary>
        /// 查找某车牌某段时间内的维修记录
        /// </summary>
        /// <param name="_reqLicenseID">车牌</param>
        /// <param name="_reqStartTime">开始时间</param>
        /// <param name="_reqDays">持续天数</param>
        /// <returns></returns>
        public XmlDocument getFixedListByID(string _reqLicenseID, DateTime _reqStartTime, int _reqDays)
        {
            DateTime _reqEndTime = _reqStartTime.AddDays(_reqDays);
            while (DateTime.Compare(_reqEndTime, _reqStartTime) >= 0)
            {
                XmlDocument _dayFixedDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getDayFixedByLicense", new object[] { this.context }, new object[] { _reqLicenseID, _reqStartTime.ToString() });
                if (_dayFixedDom.DocumentElement.SelectSingleNode("SchemaTable") != null)//有记录
                {
                    //将记录添加到result中
                    XmlNode _clone = result.ImportNode(_dayFixedDom.DocumentElement.SelectSingleNode("SchemaTable"), true);
                    this.result.DocumentElement.AppendChild(_clone);
                    DateTime _endTime = Convert.ToDateTime(_dayFixedDom.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString());
                    _reqStartTime = _endTime.AddDays(1);

                }
                else
                {
                    _reqStartTime = _reqStartTime.AddDays(1);
                }
            }
            return result;


        }
        /// <summary>
        /// 获取空闲车辆车牌
        /// </summary>
        public void getFreeCarList()
        {
            this.result = XmlProvider.Document("data");
            DateTime _starttime = Convert.ToDateTime(this.context.Request.QueryString["starttime"]);
            int _tripNum = Convert.ToInt32(this.context.Request.QueryString["tripnum"]);
            int _day = Convert.ToInt32(this.context.Request.QueryString["days"]);
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getFreeCarList", new object[] { this.context }, new object[] { _starttime, _day, _tripNum });

        }
        /// <summary>
        /// 待出车记录列表
        /// </summary>
        public void getWaitApplyList()
        {
            this.result = XmlProvider.Document("data");
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApplyWaitList", new object[] { this.context }, null);
            foreach (XmlNode _section in this.result.DocumentElement.SelectNodes("SchemaTable"))
            {
                DateTime _startTime = Convert.ToDateTime(_section.SelectSingleNode("StartTime").InnerText.ToString());
                DateTime _endTime = Convert.ToDateTime(_section.SelectSingleNode("EndTime").InnerText.ToString());
                string _startWeek = _startTime.DayOfWeek.ToString();
                string _endWeek = _endTime.DayOfWeek.ToString();
                XmlElement _startNode = this.result.CreateElement("StartWeek");
                _startNode.InnerXml = _startWeek;
                _section.AppendChild(_startNode);
                XmlElement _endNode = this.result.CreateElement("EndWeek");
                _endNode.InnerXml = _endWeek;
                _section.AppendChild(_endNode);
            }
        }
        /// <summary>
        /// 调度员更换用车
        /// </summary>
        public void editCar()
        {
            this.result = XmlProvider.Document("data");
            //地址栏传递参数
            string _type = this.context.Request.QueryString["type"];//第一次调用为判断是否有冲突的记录，第二次为有冲突记录确认冲突记录要重新派车
            string _applyIDA = this.context.Request.QueryString["applyida"];//第一个记录
            string _applyIDB = this.context.Request.QueryString["applyidb"];//第二条记录
                                                                            //获取两条记录具体的内容
            XmlDocument _applyADom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _applyIDA });
            XmlDocument _applyBDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _applyIDB });
            //A车信息
            string _aLisenseID = _applyADom.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString().Trim();
            DateTime _aStartTime = Convert.ToDateTime(_applyADom.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim());
            string _aCarOwner = _applyADom.DocumentElement.SelectSingleNode("SchemaTable/CarOwner").InnerText.ToString().Trim();
            string _aCOAccount = _applyADom.DocumentElement.SelectSingleNode("SchemaTable/COAccount").InnerText.ToString().Trim();
            string _aCarTel = _applyADom.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();
            DateTime _aEndTime = Convert.ToDateTime(_applyADom.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim());

            //B车信息
            string _bLisenseID = _applyBDom.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString().Trim();
            string _bCarOwner = _applyBDom.DocumentElement.SelectSingleNode("SchemaTable/CarOwner").InnerText.ToString().Trim();
            string _bCOAccount = _applyBDom.DocumentElement.SelectSingleNode("SchemaTable/COAccount").InnerText.ToString().Trim();
            string _bCarTel = _applyBDom.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();
            DateTime _bStartTime = Convert.ToDateTime(_applyBDom.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim());
            DateTime _bEndTime = Convert.ToDateTime(_applyBDom.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim());
            //根据车牌Licenseid获取所有此车辆的待出车记录
            XmlDocument _AListDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getListByLicenseID", new object[] { this.context }, new object[] { _aLisenseID });
            XmlDocument _BListDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getListByLicenseID", new object[] { this.context }, new object[] { _bLisenseID });
            if (_type == "ischange")//第一次判断是否有冲突记录
            {
                int _tempNum = 0;//冲突记录数
                                 //判断A有没有冲突记录
                foreach (XmlNode _section in _AListDom.DocumentElement.SelectNodes("SchemaTable"))
                {
                    DateTime _sqlStartTime = Convert.ToDateTime(_section.SelectSingleNode("StartTime").InnerText.ToString().Trim());
                    DateTime _sqlEndTime = Convert.ToDateTime(_section.SelectSingleNode("EndTime").InnerText.ToString().Trim());
                    if (DateTime.Compare(_sqlStartTime, _bStartTime) > 0 && DateTime.Compare(_bEndTime, _sqlStartTime) > 0)//开始-->记录开始-->结束
                    {
                        if (_section.SelectSingleNode("CarAppliedID").InnerText.ToString().Trim() != _applyIDA)//该记录不为要更改的记录
                        {
                            _tempNum++;
                        }

                    }
                    if (DateTime.Compare(_bStartTime, _sqlStartTime) > 0 && DateTime.Compare(_sqlEndTime, _bStartTime) > 0)//记录开始-->开始-->记录结束
                    {
                        if (_section.SelectSingleNode("CarAppliedID").InnerText.ToString().Trim() != _applyIDA)//该记录不为要更改的记录
                        {
                            _tempNum++;
                        }
                    }
                }
                if (_tempNum > 0)
                {
                    this.result.DocumentElement.SetAttribute("affect", "0");
                    this.result.DocumentElement.SetAttribute("title", "当前存在冲突记录，是否确认继续。如果继续，系统将自动为冲突记录重新分配车辆。");
                }
                else
                {
                    _tempNum = 0;//冲突记录值复原
                                 //判断A有没有冲突记录
                    foreach (XmlNode _section in _BListDom.DocumentElement.SelectNodes("SchemaTable"))
                    {
                        DateTime _sqlStartTime = Convert.ToDateTime(_section.SelectSingleNode("StartTime").InnerText.ToString().Trim());
                        DateTime _sqlEndTime = Convert.ToDateTime(_section.SelectSingleNode("EndTime").InnerText.ToString().Trim());
                        if (DateTime.Compare(_sqlStartTime, _aStartTime) > 0 && DateTime.Compare(_aEndTime, _sqlStartTime) > 0)//开始-->记录开始-->结束
                        {
                            if (_section.SelectSingleNode("CarAppliedID").InnerText.ToString().Trim() != _applyIDB)//该记录不为要更改的记录
                            {
                                _tempNum++;
                            }
                        }
                        if (DateTime.Compare(_aStartTime, _sqlStartTime) > 0 && DateTime.Compare(_sqlEndTime, _aStartTime) > 0)//记录开始-->开始-->记录结束
                        {
                            if (_section.SelectSingleNode("CarAppliedID").InnerText.ToString().Trim() != _applyIDB)//该记录不为要更改的记录
                            {
                                _tempNum++;
                            }
                        }
                    }
                    if (_tempNum > 0)
                    {
                        this.result.DocumentElement.SetAttribute("affect", "0");
                        this.result.DocumentElement.SetAttribute("title", "当前存在冲突记录，是否确认继续。如果继续，系统将自动为冲突记录重新分配车辆。");
                    }
                    else//不存在冲突记录
                    {
                        //将A车和B车的车辆信息对换
                        XmlDocument _aChangeDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.sendDriver", new object[] { this.context }, new object[] { _applyIDA, _bCOAccount, _bCarOwner, _bLisenseID, _bCarTel });
                        if (_aChangeDom.DocumentElement.Attributes["affect"].Value == "1")
                        {
                            XmlDocument _bChangeDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.sendDriver", new object[] { this.context }, new object[] { _applyIDB, _aCOAccount, _aCarOwner, _aLisenseID, _aCarTel });
                            if (_bChangeDom.DocumentElement.Attributes["affect"].Value == "1")
                            {
                                this.result.DocumentElement.SetAttribute("affect", "1");
                                this.result.DocumentElement.SetAttribute("title", "交换车辆成功！");
                            }
                            else
                            {
                                this.result.DocumentElement.SetAttribute("affect", "-1");
                                this.result.DocumentElement.SetAttribute("title", "系统异常，请联系管理员！");
                            }
                        }
                        else
                        {
                            this.result.DocumentElement.SetAttribute("affect", "-1");
                            this.result.DocumentElement.SetAttribute("title", "系统异常，请联系管理员！");
                        }
                    }
                }
            }
            else if (_type == "change")//第二次确认要更改车辆
            {
                //更改车辆
                //将A车和B车的车辆信息对换
                XmlDocument _aChangeDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.sendDriver", new object[] { this.context }, new object[] { _applyIDA, _bCOAccount, _bCarOwner, _bLisenseID, _bCarTel });
                if (_aChangeDom.DocumentElement.Attributes["affect"].Value == "1")
                {
                    XmlDocument _bChangeDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.sendDriver", new object[] { this.context }, new object[] { _applyIDB, _aCOAccount, _aCarOwner, _aLisenseID, _aCarTel });
                    if (_bChangeDom.DocumentElement.Attributes["affect"].Value == "1")
                    {
                        try
                        {
                            //为冲突记录重新派车
                            foreach (XmlNode _section in _AListDom.DocumentElement.SelectNodes("SchemaTable"))
                            {
                                DateTime _sqlStartTime = Convert.ToDateTime(_section.SelectSingleNode("StartTime").InnerText.ToString().Trim());
                                DateTime _sqlEndTime = Convert.ToDateTime(_section.SelectSingleNode("EndTime").InnerText.ToString().Trim());
                                if (DateTime.Compare(_sqlStartTime, _bStartTime) > 0 && DateTime.Compare(_bEndTime, _sqlStartTime) > 0)//开始-->记录开始-->结束
                                {
                                    if (_section.SelectSingleNode("CarAppliedID").InnerText.ToString().Trim() != _applyIDA)//该记录不为要更改的记录
                                    {
                                        //通知司机车辆被换了通知申请人和司机
                                        XmlDocument _insertData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _section.SelectSingleNode("CarAppliedID").InnerText.ToString() });
                                        //换车通知
                                        Provider.Invoke("CLDD.Providers.Notice.applychangeCar", new object[] { this.context }, new object[] { _insertData });
                                        //重新分配车辆
                                        sendDriver(_section.SelectSingleNode("CarAppliedID").InnerText.ToString());
                                    }

                                }
                                if (DateTime.Compare(_bStartTime, _sqlStartTime) > 0 && DateTime.Compare(_sqlEndTime, _bStartTime) > 0)//记录开始-->开始-->记录结束
                                {
                                    if (_section.SelectSingleNode("CarAppliedID").InnerText.ToString().Trim() != _applyIDA)//该记录不为要更改的记录
                                    {
                                        //通知司机车辆被换了通知申请人和司机
                                        XmlDocument _insertData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _section.SelectSingleNode("CarAppliedID").InnerText.ToString() });
                                        //换车通知
                                        Provider.Invoke("CLDD.Providers.Notice.applychangeCar", new object[] { this.context }, new object[] { _insertData });
                                        //重新分配车辆
                                        sendDriver(_section.SelectSingleNode("CarAppliedID").InnerText.ToString());
                                    }
                                }
                            }
                            foreach (XmlNode _section in _BListDom.DocumentElement.SelectNodes("SchemaTable"))
                            {
                                DateTime _sqlStartTime = Convert.ToDateTime(_section.SelectSingleNode("StartTime").InnerText.ToString().Trim());
                                DateTime _sqlEndTime = Convert.ToDateTime(_section.SelectSingleNode("EndTime").InnerText.ToString().Trim());
                                if (DateTime.Compare(_sqlStartTime, _aStartTime) > 0 && DateTime.Compare(_aEndTime, _sqlStartTime) > 0)//开始-->记录开始-->结束
                                {
                                    if (_section.SelectSingleNode("CarAppliedID").InnerText.ToString().Trim() != _applyIDB)//该记录不为要更改的记录
                                    {
                                        //通知司机车辆被换了通知申请人和司机
                                        XmlDocument _insertData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _section.SelectSingleNode("CarAppliedID").InnerText.ToString() });
                                        //换车通知
                                        Provider.Invoke("CLDD.Providers.Notice.applychangeCar", new object[] { this.context }, new object[] { _insertData });
                                        //重新分配车辆
                                        sendDriver(_section.SelectSingleNode("CarAppliedID").InnerText.ToString());
                                    }
                                }
                                if (DateTime.Compare(_aStartTime, _sqlStartTime) > 0 && DateTime.Compare(_sqlEndTime, _aStartTime) > 0)//记录开始-->开始-->记录结束
                                {
                                    if (_section.SelectSingleNode("CarAppliedID").InnerText.ToString().Trim() != _applyIDB)//该记录不为要更改的记录
                                    {
                                        //通知司机车辆被换了通知申请人和司机
                                        XmlDocument _insertData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _section.SelectSingleNode("CarAppliedID").InnerText.ToString() });
                                        //换车通知
                                        Provider.Invoke("CLDD.Providers.Notice.applychangeCar", new object[] { this.context }, new object[] { _insertData });
                                        //重新分配车辆
                                        sendDriver(_section.SelectSingleNode("CarAppliedID").InnerText.ToString());
                                    }
                                }
                            }
                        }
                        catch (Exception _exc)
                        {
                            Provider.LogErr(this.result, _exc);
                        }

                        this.result.DocumentElement.SetAttribute("affect", "1");
                        this.result.DocumentElement.SetAttribute("title", "交换车辆成功！");
                    }
                    else
                    {
                        this.result.DocumentElement.SetAttribute("affect", "-1");
                        this.result.DocumentElement.SetAttribute("title", "系统异常，请联系管理员！");
                    }
                }
                else
                {
                    this.result.DocumentElement.SetAttribute("affect", "-1");
                    this.result.DocumentElement.SetAttribute("title", "系统异常，请联系管理员！");
                }

            }
            else
            {
                this.result.DocumentElement.SetAttribute("affect", "-1");
                this.result.DocumentElement.SetAttribute("title", "系统异常，请联系管理员！");
            }


        }
        /// <summary>
        /// 获取当前任务列表
        /// </summary>
        public void getApplyList()
        {
            this.result = XmlProvider.Document("sqldata");

            //通过参数获取当前权限与账号
            string _permit = this.context.Request.QueryString["permit"].ToString().Trim();
            string _account = this.context.Request.QueryString["account"].ToString().Trim();
            if (_permit == "ADMIN" || _permit == "APPLY" || _permit == "LSADMIN")
                this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getListA", new object[] { this.context }, new object[] { _account });
            else
            {
                this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getListD", new object[] { this.context }, new object[] { _account });

            }

        }
        /// <summary>
        /// 请求预约
        /// </summary>
        public void reqApply()
        {
            DateTime _date = DateTime.Now;  //当前时间
            _date = Convert.ToDateTime(_date.Year + "-" + _date.Month + "-" + _date.Day + " 23:00:00");
            string _datetime = _date.ToString();

            for (int i = 1; i <= 15; i++)
            {
                XmlElement _schemaTable = this.result.CreateElement("SchemaTable");
                this.result.DocumentElement.AppendChild(_schemaTable);
                XmlElement _Date = this.result.CreateElement("Date");
                _Date.InnerXml = "<![CDATA[" + _datetime + "]]>";
                _schemaTable.AppendChild(_Date);
                int _carNum = 0;//当前空闲车辆
                _carNum = (int)Provider.Invoke("CLDD.Providers.ApplyService.getDayCarNum", new object[] { this.context }, new object[] { _datetime });
                if (_carNum > 0)
                {
                    XmlElement _statue = this.result.CreateElement("statue");
                    _statue.InnerXml = "<![CDATA[able]]>";
                    _schemaTable.AppendChild(_statue);
                }
                else
                {
                    XmlElement _statue = this.result.CreateElement("statue");
                    _statue.InnerXml = "<![CDATA[disable]]>";
                    _schemaTable.AppendChild(_statue);
                }
                _schemaTable.SetAttribute("CarNum", _carNum.ToString());//空闲车辆数量
                _datetime = _date.AddDays(i).ToString();
            }

        }
        /// <summary>
        /// 判断符合座位数车辆
        /// </summary>
        public void isSites()
        {
            this.result = XmlProvider.Document("sqldata");
            string _date = this.context.Request.QueryString["starttime"].ToString();
            int _sites = Convert.ToInt32(this.context.Request.QueryString["sites"].ToString().Trim());//加上司机
            int _carNum = 0;//空闲车辆数
            _carNum = (int)Provider.Invoke("CLDD.Providers.ApplyService.getDayCarNum", new object[] { this.context }, new object[] { _date });
            this.result.DocumentElement.SetAttribute("CarNum", _carNum.ToString());
            this.result.DocumentElement.SetAttribute("sites", "false");//无满足要求车辆
            if (_carNum > 0)
            {
                XmlDocument _sitesapi = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getFreeCars", new object[] { this.context }, new object[] { _date });

                foreach (XmlNode _section in _sitesapi.DocumentElement.SelectNodes("SchemaTable"))
                {
                    int _realSites = Convert.ToInt32(_section.SelectSingleNode("Sites").InnerText.ToString().Trim());
                    this.result.DocumentElement.SetAttribute("realsites", _realSites.ToString());
                    this.result.DocumentElement.SetAttribute("rss", _sites.ToString());
                    if (_sites <= _realSites)
                    {
                        this.result.DocumentElement.SetAttribute("sites", "true");
                        break;
                    }
                }
                this.result.DocumentElement.SetAttribute("MaxSites", _sitesapi.DocumentElement.Attributes["maxSites"].Value);
                this.result.DocumentElement.SetAttribute("CarNum", _carNum.ToString());

            }
            else
            {
                this.result.DocumentElement.SetAttribute("MaxSites", "0");
            }

        }
        /// <summary>
        /// 车辆审核预约
        /// </summary>
        public void applyReview()
        {
            result = XmlProvider.Document("sqldata");
            //接收地址栏中的参数
            string _applyName = System.Web.HttpUtility.UrlDecode(this.context.Request.QueryString["name"], Encoding.UTF8);
            string _applyAccount = this.context.Request.QueryString["account"];

            //向数据库插入申请记录
            XmlDocument _insert = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.insertApply", new object[] { this.context }, new object[] { _applyName, _applyAccount });
            string _guid = "";
            if (_insert.DocumentElement.GetAttribute("affect") == "1")//插入成功
            {
                _guid = _insert.DocumentElement.Attributes["guid"].Value;//申请记录主键

                //派车
                sendDriver(_guid);

            }
            else
            {
                this.result.DocumentElement.SetAttribute("affect", "-1");
                this.result.DocumentElement.SetAttribute("title", "申请异常，请于管理员联系");
            }


        }
        /// <summary>
        /// 派遣车辆
        /// </summary>
        /// <param name="_guid">申请记录id</param>
        public void sendDriver(string _guid)
        {
            //获取申请详细记录
            XmlDocument _applyDetail = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _guid });
            string _starttime = _applyDetail.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString();
            int _days = Convert.ToInt32(_applyDetail.DocumentElement.SelectSingleNode("SchemaTable/Days").InnerText.ToString());
            int _tripNum = Convert.ToInt32(_applyDetail.DocumentElement.SelectSingleNode("SchemaTable/TripNum").InnerText.ToString());
            //判断是否派遣车辆
            XmlDocument _CarNumDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getFreeCarList", new object[] { this.context }, new object[] { Convert.ToDateTime(_starttime), _days, _tripNum });
            if (_CarNumDom.DocumentElement.SelectNodes("SchemaTable").Count <= 0)
            {
                //拒绝申请
                this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upStauteRefuse", new object[] { this.context }, new object[] { _guid, "拒绝", "当前无可用车辆" });

                this.result.DocumentElement.SetAttribute("affect", "-1");
                this.result.DocumentElement.SetAttribute("title", "申请失败，当前无可用车辆");
                //通知申请人
                Provider.Invoke("CLDD.Providers.Notice.applyFailResult", new object[] { this.context }, new object[] { _applyDetail });
            }
            else
            {

                //找到派遣的车辆
                //获得分配的车辆信息
                XmlDocument _carDetailDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.sendDriver", new object[] { this.context }, new object[] { _applyDetail });
                //XmlDocument _carDetailDom = sendDriver(_applyDetail);
                string _cdLicenseID = _carDetailDom.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString();
                string _cdAccount = _carDetailDom.DocumentElement.SelectSingleNode("SchemaTable/DriverAccount").InnerText.ToString();
                string _cdName = _carDetailDom.DocumentElement.SelectSingleNode("SchemaTable/DriverName").InnerText.ToString();
                string _cdTel = _carDetailDom.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString();
                //插入司机信息到数据库
                XmlDocument _inresult = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.sendDriver", new object[] { this.context }, new object[] { _guid, _cdAccount, _cdName, _cdLicenseID, _cdTel });
                this.result = _inresult;
                if (_inresult.DocumentElement.Attributes["affect"].Value == "1")
                {
                    _applyDetail = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _guid });
                    //更改申请记录状态
                    this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upApplyStatue", new object[] { this.context }, new object[] { _guid, "待出车" });
                    //通知申请人、司机、调度员、部门领导
                    Provider.Invoke("CLDD.Providers.Notice.applySuccResult", new object[] { this.context }, new object[] { _applyDetail });
                    this.result.DocumentElement.SetAttribute("affect", "1");
                    this.result.DocumentElement.SetAttribute("title", "申请成功");

                }

            }
        }
        /// <summary>
        /// 通过申请
        /// </summary>
        public void upPassStatue()
        {
            this.result = XmlProvider.Document("sqldata");
            //更改该记录状态
            string _guid = this.context.Request.QueryString["guid"].ToString().Trim();//申请编号
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upApplyStatue", new object[] { this.context }, new object[] { _guid, "待出车" });
            //通知
            string _account = this.context.Request.QueryString["account"];
            //插入数据库的记录
            XmlDocument _insertData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _guid });

            Provider.Invoke("CLDD.Providers.Notice.applySuccResult", new object[] { this.context }, new object[] { _insertData });

        }
        /// <summary>
        /// 拒绝申请
        /// </summary>
        public void upRefuStatue()
        {
            this.result = XmlProvider.Document("sqldata");
            //更改该记录状态
            string _guid = this.context.Request.QueryString["guid"].ToString().Trim();//申请编号
            string _reason = this.context.Request.Form["reason"].ToString();
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upStauteRefuse", new object[] { this.context }, new object[] { _guid, "拒绝", _reason });


            //插入数据库的记录
            XmlDocument _insertData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _guid });
            //通知
            string _account = _insertData.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedAccount").InnerText.ToString();
            Provider.Invoke("CLDD.Providers.Notice.applyFailResult", new object[] { this.context }, new object[] { _insertData });
        }
        /// <summary>
        /// 取消申请
        /// </summary>
        public void cancelApply()
        {
            this.result = XmlProvider.Document("sqldata");
            //更改该记录状态
            string _guid = this.context.Request.QueryString["guid"].ToString().Trim();//申请编号
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upApplyStatue", new object[] { this.context }, new object[] { _guid, "取消" });

            XmlDocument _sqldata = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _guid });
            //发送信息
            Provider.Invoke("CLDD.Providers.Notice.cancelApplyResult", new object[] { this.context }, new object[] { _sqldata });

        }
        /// <summary>
        /// 开始行程
        /// </summary>
        public void startApply()
        {
            this.result = XmlProvider.Document("sqldata");
            //更改该记录状态
            string _guid = this.context.Request.QueryString["guid"].ToString().Trim();//申请编号
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upApplyStatue", new object[] { this.context }, new object[] { _guid, "开始" });
            if (this.result.DocumentElement.Attributes["affect"].Value == "1")
            {
                //获取车辆信息
                XmlDocument _carDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _guid });
                string _licenseid = _carDom.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString();//车牌
                                                                                                                           //更改车辆状态
                this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.upCarStatueByLicenseID", new object[] { this.context }, new object[] { _licenseid, "出车" });
                if (this.result.DocumentElement.Attributes["affect"].Value == "1")
                {
                    //插入状态更改记录
                    this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.insertChangeState", new object[] { this.context }, new object[] { _licenseid, "出车" });
                }
            }


        }
        /// <summary>
        /// 结束行程
        /// </summary>
        public void endApply()
        {
            this.result = XmlProvider.Document("sqldata");
            //更改该记录状态
            string _guid = this.context.Request.QueryString["guid"].ToString().Trim();//申请编号
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upApplyStatue", new object[] { this.context }, new object[] { _guid, "已完成" });
            if (this.result.DocumentElement.Attributes["affect"].Value == "1")
            {
                //获取车辆信息
                XmlDocument _carDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _guid });
                string _licenseid = _carDom.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString();//车牌
		//结束返回更新信息


                //更改车辆状态
                this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.upCarStatueByLicenseID", new object[] { this.context }, new object[] { _licenseid, "空闲" });
                if (this.result.DocumentElement.Attributes["affect"].Value == "1")
                {
                    //插入状态更改记录
                    this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.insertChangeState", new object[] { this.context }, new object[] { _licenseid, "空闲" });
                }
            }
        }
        /// <summary>
        /// 延迟行程
        /// </summary>
        public void delyApply()
        {
            string _guid = this.context.Request.QueryString["applyid"];
            int _dalyDay = Convert.ToInt32(this.context.Request.QueryString["days"]);
            //获取申请列表详细信息
            XmlDocument _data = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _guid });
            DateTime _sqlEndTime = Convert.ToDateTime(_data.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText);//数据库中结束时间
            string _licenseid = _data.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText;//车牌
            int _days = Convert.ToInt32(_data.DocumentElement.SelectSingleNode("SchemaTable/Days").InnerText.ToString().Trim());
            //查找对应时间的维修记录
            XmlDocument _carFixedDom = getFixedListByID(_licenseid, _sqlEndTime, _dalyDay);
            this.result = _carFixedDom;
            foreach (XmlNode _section in _carFixedDom.DocumentElement.SelectNodes("SchemaTable"))
            {
                string _carSqlFixID = _section.SelectSingleNode("FixID").InnerText.ToString();
                string _carSqlLicenseID = _section.SelectSingleNode("LicenseID").InnerText.ToString();
                DateTime _carSqlStartTime = Convert.ToDateTime(_section.SelectSingleNode("StartTime").InnerText.ToString());
                DateTime _carSqlEndTime = Convert.ToDateTime(_section.SelectSingleNode("EndTime").InnerText.ToString());
                DateTime _carTempEndTime = _carSqlEndTime;
                _carSqlStartTime = _carSqlStartTime.AddDays(_dalyDay);
                _carSqlEndTime = _carSqlEndTime.AddDays(_dalyDay);
                //修改维修时间
                XmlDocument _editFixedTimeDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.editFixed", new object[] { this.context }, new object[] { _carSqlFixID, _carSqlStartTime, _carSqlEndTime });
                this.result = _editFixedTimeDom;
                if (_editFixedTimeDom.DocumentElement.Attributes["affect"].Value == "1")
                {
                    //通知调度员维修记录更改
                    Provider.Invoke("CLDD.Providers.Notice.delyFixedApply", new object[] { this.context }, new object[] { _carSqlLicenseID, _dalyDay });

                    //查找冲突记录
                    XmlDocument _clashFxedDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getClashApplyList", new object[] { this.context }, new object[] { _licenseid, _dalyDay, _carTempEndTime });
                    this.result = _clashFxedDom;
                    //匹配冲突记录
                    foreach (XmlNode _sectoinFixed in _clashFxedDom.DocumentElement.SelectNodes("SchemaTable"))
                    {

                        string _carAppliedID = _sectoinFixed.SelectSingleNode("CarAppliedID").InnerText;//冲突记录ID
                                                                                                        //插入数据库的记录
                        XmlDocument _insertData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _carAppliedID });

                        //延迟预约通知
                        Provider.Invoke("CLDD.Providers.Notice.delyApply", new object[] { this.context }, new object[] { _insertData });
                        //派遣司机
                        sendDriver(_carAppliedID);
                    }
                }
            }
            //查找冲突记录
            XmlDocument _clashDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getClashApplyList", new object[] { this.context }, new object[] { _licenseid, _dalyDay, _sqlEndTime });
            //修改本记录的时间
            XmlDocument _delaydom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.delayApply", new object[] { this.context }, new object[] { _guid, _dalyDay + _days, _sqlEndTime.AddDays(_dalyDay) });
            //匹配冲突记录
            foreach (XmlNode _section in _clashDom.DocumentElement.SelectNodes("SchemaTable"))
            {

                string _carAppliedID = _section.SelectSingleNode("CarAppliedID").InnerText;//冲突记录ID
                                                                                           //插入数据库的记录
                XmlDocument _insertData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _carAppliedID });

                //延迟预约通知
                Provider.Invoke("CLDD.Providers.Notice.delyApply", new object[] { this.context }, new object[] { _insertData });
                //派遣司机
                sendDriver(_carAppliedID);
            }
            this.result.DocumentElement.SetAttribute("affect", "1");

        }
    }
}