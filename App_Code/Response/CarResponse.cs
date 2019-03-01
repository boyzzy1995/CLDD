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

    public class CarResponse : HttpResponseProvider
    {
        public HttpContext context;
        public XmlDocument config;
        public XmlDocument result;
        public string conn = "WEB";
        public CarResponse(HttpContext _context) : base(_context) { this.context = _context; }
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
                case "getkm": getKm(); return this.result;    //获取总公里数（AJAX）
                case "motifykm": upKm(); return this.result;    //更新总公里数（ajax）
                case "getcarlist": getCarList(); break;//获取车辆列表
                case "getsinglecar": getSingleCar(); break;//获取当个车辆信息
                case "reqcarmotify": getSingleCar(); break;    //车辆修改请求
                case "cardetail": getSingleCar(); break; //查看车辆详细信息
                case "disable_car": disCar(); return this.result;    //禁用车辆，修改车辆状态
                case "add_car": addCar(); return this.result;   //添加车辆
                case "reqadd": reqAdd(); break;      //请求添加
                case "carmotify": motifyCar(); return this.result;    //修改车辆
                case "motify_controlnum": motifyControlNum(); return this.result;//修改控制车辆数
                case "getcarstatebyday": getCarStateByDay(); break;//获取车辆在开始时间后七天内的车辆状态
                case "getfixedlist": getfixedList(); break; //获取维修列表
                case "addfixlist": reqFixed(); break; //请求添加维修记录
                case "cancel_fix_statue": cancelFixedStatue(); return this.result;//取消维修
                case "end_fix_statue": endFixedStatue(); return this.result; //结束维修
                case "add_fix": addFixed(); return this.result;//添加维修记录
            }
            //附加显示样式
            XmlElement _template = this.result.CreateElement("template");
            _template.SetAttribute("output", "html");
            if (File.Exists(Provider.Path("/ui/car/" + this.PARAMS["guid"].ToString() + ".xsl")))
            {
                _template.InnerXml = "/ui/car/" + this.PARAMS["guid"].ToString() + ".xsl";
            }
            else
            {
                _template.InnerXml = "/ui/pages/err.xsl";
            }
            this.result.DocumentElement.AppendChild(_template);
            return this.result;
        }
        //获取车辆在开始时间后七天内的车辆状态
        public void getCarStateByDay()
        {
            this.result = XmlProvider.Document("sqldata");
            //创建SchemaTable标签

            //获取当前日期
            string _todayTime = DateTime.Now.ToShortDateString().ToString();//2025-11-5

            //获取开始时间
            string _year = this.context.Request.Form["year"].ToString().Trim();//2017
            string _month = this.context.Request.Form["month"].ToString().Trim();//7
            string _day = this.context.Request.Form["day"].ToString().Trim();//17
            string _startTime = _year + "-" + _month + "-" + _day;//2017-7-17



            //将字符串转换成时间类型
            DateTime dt1 = Convert.ToDateTime(_todayTime);
            DateTime dt2 = Convert.ToDateTime(_startTime);
            DateTime dt3 = Convert.ToDateTime(_startTime);


            //调用CarDol中的getEnableCarLicense方法
            XmlDocument _LicenseList = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getEnableCarLicense", new object[] { this.context }, null);

            //遍历车牌集合
            foreach (XmlNode _section in _LicenseList.DocumentElement.SelectNodes("SchemaTable"))
            {
                //创建Row标签
                XmlElement _elRow = this.result.CreateElement("Row");

                string _licenseID = _section.SelectSingleNode("LicenseID").InnerText.ToString().Trim();
                string _Day;
                string _Week;
                string _State;
                //创建LicenseID标签
                XmlElement _elLicenseID = this.result.CreateElement("LicenseID");
                _elLicenseID.InnerXml = _licenseID;
                _elRow.AppendChild(_elLicenseID);

                //创建Content标签
                XmlElement _elContent = this.result.CreateElement("Content");
                dt2 = dt3;
                //循环七天
                int i = 0;//查询成功的天数
                for (int j = 0; j < 7; j++, i++, dt2 = dt2.AddDays(1))
                {
                    //如果超过今天，就跳出循环
                    if (DateTime.Compare(dt2, dt1) > 0) break;
                    //调用CarDol中的getCarStateByLicenseIDAndDay方法

                    //实际要查询的日期
                    _Day = dt2.ToString();
                    //为方便数据库查询，将天数加一天再传入dol层的方法
                    string _queryDay = dt2.AddDays(1).ToString();
                    ;//通过方法从日期算出星期
                    _Week = Convert.ToString(dt2.DayOfWeek);
                    //获取某车牌在某天的状态

                    //获取车辆状态
                    if ((_State = Provider.Invoke("CLDD.Providers.CarDol.getCarStateByLicenseIDAndDay", new object[] { this.context }, new object[] { _licenseID, _queryDay }).ToString().Trim()) == "")
                    {
                        _State = "无记录";
                    }

                    //创建OneDay标签
                    XmlElement _elOneDay = this.result.CreateElement("OneDay");

                    //创建Day标签
                    XmlElement _elDay = this.result.CreateElement("Day");
                    _elDay.InnerXml = _Day;
                    _elOneDay.AppendChild(_elDay);

                    //创建Week标签
                    XmlElement _elWeek = this.result.CreateElement("Week");
                    _elWeek.InnerXml = _Week;
                    _elOneDay.AppendChild(_elWeek);

                    //创建State标签
                    XmlElement _elState = this.result.CreateElement("State");
                    _elState.InnerXml = _State;
                    _elOneDay.AppendChild(_elState);

                    _elContent.AppendChild(_elOneDay);

                    //dt2 = dt2.AddDays(1); //增加一天
                    //i++;//天数计数加一天
                }


                _elRow.AppendChild(_elContent);

                //创建DayNums标签,成功查询的天数
                XmlElement _elDayNums = this.result.CreateElement("DayNums");
                _elDayNums.InnerXml = i.ToString();
                _elRow.AppendChild(_elDayNums);

                //将一行Row添加进SchemaTable
                this.result.DocumentElement.AppendChild(_elRow);
            }



        }
        /// <summary>
        /// 请求添加维修记录
        /// </summary>
        public void reqFixed()
        {
            this.result = XmlProvider.Document("sqldata");
            //获取车牌
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getLicenseIDList", new object[] { this.context }, null);
        }
        /// <summary>
        /// 车辆维修列表
        /// </summary>
        public void getfixedList()
        {
            this.result = XmlProvider.Document("sqldata");
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getFixedList", new object[] { this.context }, null);
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
        /// 添加维修记录
        /// </summary>
        public void addFixed()
        {
            this.result = XmlProvider.Document("sqldata");
            string _reqLisenceID = this.context.Request.Form["LicenseID"];
            string _reqStartTime = this.context.Request.Form["StartTime"];
            string _reqEndTime = this.context.Request.Form["EndTime"];
            XmlDocument _fixedResult = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.addFixed", new object[] { this.context }, new object[] { _reqLisenceID, _reqStartTime, _reqEndTime });
            if (_fixedResult.DocumentElement.Attributes["affect"].Value.ToString() == "1")//维修记录添加成功
            {
                //查找影响的出车记录
                int _days = (Convert.ToDateTime(_reqEndTime) - Convert.ToDateTime(_reqStartTime)).Days;
                XmlDocument _clashList = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyService.getClashApplyList", new object[] { this.context }, new object[] { _reqLisenceID, _days, Convert.ToDateTime(_reqStartTime) });
                //匹配冲突记录
                foreach (XmlNode _section in _clashList.DocumentElement.SelectNodes("SchemaTable"))
                {

                    string _carAppliedID = _section.SelectSingleNode("CarAppliedID").InnerText;//冲突记录ID
                                                                                               //插入数据库的记录
                    XmlDocument _insertData = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _carAppliedID });

                    //维修预约通知
                    //Provider.Invoke("CLDD.Providers.Notice.delyFixChangeApply", new object[] { this.context }, new object[] { _insertData });
                    //派遣司机
                    Provider.Invoke("CLDD.Providers.ApplyResponse.sendDriver", new object[] { this.context }, new object[] { _carAppliedID });
                }
            }
            this.result = _fixedResult;
        }
        /// <summary>
        /// 取消维修状态
        /// </summary>
        public void cancelFixedStatue()
        {
            this.result = XmlProvider.Document("data");
            string _fixID = this.context.Request.QueryString["fixID"];
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.cancelFixedStatue", new object[] { this.context }, new object[] { _fixID });

        }
        /// <summary>
        /// 取消维修状态
        /// </summary>
        public void endFixedStatue()
        {
            this.result = XmlProvider.Document("data");
            string _fixID = this.context.Request.QueryString["fixID"];
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.endFixedStatue", new object[] { this.context }, new object[] { _fixID });

        }
        //修改控制车辆数
        public void motifyControlNum()
        {
            this.result = XmlProvider.Document("sqldata");
            int _num = Convert.ToInt32(this.context.Request.QueryString["num"]);
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.upControlNum", new object[] { this.context }, new object[] { _num });
        }
        //添加车辆请求页 
        public void reqAdd()
        {
            this.result = XmlProvider.Document("sqldata");
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.DriverDol.getFreeDriver", new object[] { this.context }, null);
        }
        //获取总公里数
        public void getKm()
        {
            this.result = XmlProvider.Document("sqldata");
            string _account = this.context.Request.QueryString["account"];  //司机账号
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getKm", new object[] { this.context }, new object[] { _account });
        }
        //修改共公里数
        public void upKm()
        {
            this.result = XmlProvider.Document("sqldata");
            string _km = this.context.Request.Form["km"];
            string _account = this.context.Request.QueryString["account"];
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.upKm", new object[] { this.context }, new object[] { _km, _account });
        }
        //更改车辆状态
        public void disCar()
        {
            this.result = XmlProvider.Document("sqldata");
            string _guid = this.context.Request.QueryString["guid"];
            string _carStatue = this.context.Request.QueryString["statue"];
            //调用CarDol的upCarStatue方法，修改车辆状态为禁用
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.upCarStatue", new object[] { this.context }, new object[] { _guid, _carStatue });
            if (this.result.DocumentElement.Attributes["affect"].Value == "1")
            {

                //获取车辆信息
                XmlDocument _carDom = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getSingleCar", new object[] { this.context }, new object[] { _guid });
                string _licenseid = _carDom.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString();
                if (_carStatue != "禁用")//禁用时，不添加记录
                {
                    //插入状态更改记录
                    this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.insertChangeState", new object[] { this.context }, new object[] { _licenseid, _carStatue });
                }
                //为所有给车辆待出车或者待处理记录重新分配车辆
                if (_carStatue == "禁用" || _carStatue == "维修")
                {
                    XmlDocument _applyListDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getListByLicenseID", new object[] { this.context }, new object[] { _licenseid });
                    try
                    {
                        foreach (XmlNode _section in _applyListDom.DocumentElement.SelectNodes("SchemaTable"))
                        {
                            string _carAppliedID = _section.SelectSingleNode("CarAppliedID").InnerText.ToString().Trim();
                            //派车
                            //获取申请详细记录
                            XmlDocument _applyDetail = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.getApply", new object[] { this.context }, new object[] { _carAppliedID });
                            string _starttime = _applyDetail.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString();
                            //判断是否派遣车辆
                            int _freeCarNum = Convert.ToInt32(Provider.Invoke("CLDD.Providers.ApplyService.getDayCarNum", new object[] { this.context }, new object[] { _starttime }));
                            if (_freeCarNum == 0)
                            {
                                //拒绝申请
                                this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upStauteRefuse", new object[] { this.context }, new object[] { _carAppliedID, "拒绝", "当前无可用车辆" });

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
                                XmlDocument _inresult = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.sendDriver", new object[] { this.context }, new object[] { _carAppliedID, _cdAccount, _cdName, _cdLicenseID, _cdTel });
                                if (_inresult.DocumentElement.Attributes["affect"].Value == "1")
                                {
                                    //判断是否需要调度员确认申请
                                    int _controlNum = Convert.ToInt32(Provider.Invoke("CLDD.Providers.CarDol.getControlNum", new object[] { this.context }, null));
                                    if (_controlNum >= _freeCarNum)//需要调度员确认
                                    {
                                        //更改申请记录状态
                                        this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upApplyStatue", new object[] { this.context }, new object[] { _carAppliedID, "待处理" });

                                        this.result.DocumentElement.SetAttribute("affect", "0");
                                        this.result.DocumentElement.SetAttribute("title", "已申请，等待调度员确认");
                                        //通知调度员
                                        Provider.Invoke("CLDD.Providers.Notice.sendSureApply", new object[] { this.context }, new object[] { _applyDetail });
                                    }
                                    else//不需要调度员
                                    {

                                        //更改申请记录状态
                                        this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.upApplyStatue", new object[] { this.context }, new object[] { _carAppliedID, "待出车" });
                                        //通知申请人、司机、调度员、部门领导
                                        Provider.Invoke("CLDD.Providers.Notice.applySuccResult", new object[] { this.context }, new object[] { _applyDetail });
                                        this.result.DocumentElement.SetAttribute("affect", "1");
                                        this.result.DocumentElement.SetAttribute("title", "申请成功");
                                    }
                                }
                                else
                                {
                                    //制空车辆信息
                                    XmlDocument _toNullDom = (XmlDocument)Provider.Invoke("CLDD.Providers.ApplyDol.sendDriver", new object[] { this.context }, new object[] { _carAppliedID, "", "", "", "" });
                                    this.result.DocumentElement.SetAttribute("affect", "-1");
                                    this.result.DocumentElement.SetAttribute("title", "插入车辆信息出错！");
                                }
                            }
                        }
                    }
                    catch (Exception _exc)
                    {
                        Provider.LogErr(this.result, _exc);
                    }
                }
                this.result.DocumentElement.SetAttribute("affect", "1");
            }
            else
            {
                this.result.DocumentElement.SetAttribute("affect", "-1");
            }
        }
        //添加车辆
        public void addCar()
        {
            this.result = XmlProvider.Document("sqldata");
            string _guid = Provider.GUID;
            string _licenseID = this.context.Request.Form["licenseID"].ToString().Trim();
            string _sites = this.context.Request.Form["sites"].ToString().Trim();
            string _mot = this.context.Request.Form["mot"].ToString().Trim();
            string _insurance = this.context.Request.Form["insurance"].ToString().Trim();
            string _kilometers = this.context.Request.Form["kilometers"].ToString().Trim();
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.addCar", new object[] { this.context }, new object[] { _guid, _licenseID, _sites, _mot, _insurance, _kilometers });
        }
        //修改车辆
        public void motifyCar()
        {
            this.result = XmlProvider.Document("sqldata");
            string _guid = this.context.Request.QueryString["guid"].ToString().Trim();
            string _licenseID = this.context.Request.Form["licenseID"].ToString().Trim();
            string _driverAccount = this.context.Request.Form["driverAccount"].ToString().Trim();
            string _sites = this.context.Request.Form["sites"].ToString().Trim();
            string _mot = this.context.Request.Form["mot"].ToString().Trim();
            string _insurance = this.context.Request.Form["insurance"].ToString().Trim();
            string _kilometers = this.context.Request.Form["kilometers"].ToString().Trim();
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.motifyCar", new object[] { this.context }, new object[] { _guid, _licenseID, _driverAccount, _sites, _mot, _insurance, _kilometers });
        }
        //获取车辆列表
        public void getCarList()
        {
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getCarList", new object[] { this.context }, null);

            int _freeCarNum = Convert.ToInt32(Provider.Invoke("CLDD.Providers.CarDol.getControlNum", new object[] { this.context }, null));
            this.result.DocumentElement.SetAttribute("ControlNum", _freeCarNum.ToString());
        }
        //获取单个车辆
        public void getSingleCar()
        {
            string _guid = this.context.Request.QueryString["guid"].ToString();
            this.result = (XmlDocument)Provider.Invoke("CLDD.Providers.CarDol.getSingleCar", new object[] { this.context }, new object[] { _guid });
        }
    }
}