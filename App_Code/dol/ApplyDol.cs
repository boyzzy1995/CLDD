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

    public class ApplyDol : HttpResponseProvider
    {
        public XmlDocument result;
        public string conn = "WEB";
        public HttpContext context;
        public ApplyDol(HttpContext _context) : base(_context) { this.context = _context; }
        //根据ID查找某一预约信息
        public XmlDocument getApply(string _guid)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("getApply");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _applyID = new SqlParameter("CarAppliedID", SqlDbType.NVarChar, 50);
                _applyID.Value = _guid;
                _cmd.Parameters.Add(_applyID);

                result = SQLProvider.GetData(_cmd);
            }

            return this.result;
        }
        //查找某车某日记录
        public XmlDocument getDayCar(string _reqLicenseID, string _stime)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("getDayCar");
            DateTime _stimeDate = Convert.ToDateTime(_stime);
            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _licenseid = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                _licenseid.Value = _reqLicenseID;
                _cmd.Parameters.Add(_licenseid);

                SqlParameter _starttime = new SqlParameter("starttime", SqlDbType.NVarChar, 50);
                _starttime.Value = _stimeDate.Year + "-" + _stimeDate.Month + "-" + _stimeDate.Day + " 23:00:00";
                _cmd.Parameters.Add(_starttime);
                result = SQLProvider.GetData(_cmd);
            }

            return this.result;
        }
        //根据车牌查找所有对应的待出车记录
        public XmlDocument getListByLicenseID(string _reqLicenseID)
        {
            this.result = XmlProvider.Document("sqldata");
            try
            {
                string _sql = readXml("getListByLicenseID");
                //读取数据
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = _sql;

                    SqlParameter _appLicenseID = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                    _appLicenseID.Value = _reqLicenseID;
                    _cmd.Parameters.Add(_appLicenseID);

                    result = SQLProvider.GetData(_cmd);
                }
            }
            catch (Exception _exc)
            {
                Provider.LogErr(this.result, _exc);
            }
            return this.result;
        }
       
        //申请人获取列表
        public XmlDocument getListA(string _account)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("getListA");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _appAccounnt = new SqlParameter("CarAppliedAccount", SqlDbType.NVarChar, 50);
                _appAccounnt.Value = _account;
                _cmd.Parameters.Add(_appAccounnt);

                result = SQLProvider.GetData(_cmd);
            }

            return this.result;
        }
        //司机获取列表
        public XmlDocument getListD(string _account)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("getListD");
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _coAccount = new SqlParameter("COAccount", SqlDbType.NVarChar, 50);
                _coAccount.Value = _account;
                _cmd.Parameters.Add(_coAccount);

                result = SQLProvider.GetData(_cmd);
            }
            return this.result;
        }
        //申请人获取列表
        public XmlDocument getApplyReviewList()
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("getApplyReviewList");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                result = SQLProvider.GetData(_cmd);
            }

            return this.result;
        }
        //申请人获取列表
        public XmlDocument getApplyWaitList()
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("getApplyWaitList");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                result = SQLProvider.GetData(_cmd);
            }

            return this.result;
        }
        //插入预约信息
        public XmlDocument insertApply(string _name, string _account)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("addApply");
            string _guid = Provider.GUID;
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _applyID = new SqlParameter("CarAppliedID", SqlDbType.NVarChar, 50);
                _applyID.Value = _guid;
                _cmd.Parameters.Add(_applyID);

                SqlParameter _appname = new SqlParameter("CarAppliedName", SqlDbType.NVarChar, 50);
                _appname.Value = _name;
                _cmd.Parameters.Add(_appname);

                SqlParameter _appaccount = new SqlParameter("CarAppliedAccount", SqlDbType.NVarChar, 50);
                _appaccount.Value = _account;
                _cmd.Parameters.Add(_appaccount);

                SqlParameter _appTel = new SqlParameter("AppliedTel", SqlDbType.NVarChar, 20);
                _appTel.Value = this.context.Request.Form["AppliedTel"];
                _cmd.Parameters.Add(_appTel);

                SqlParameter _appdate = new SqlParameter("ApplyDate", SqlDbType.NVarChar, 50);
                _appdate.Value = DateTime.Now;
                _cmd.Parameters.Add(_appdate);

                SqlParameter _startdate = new SqlParameter("Starttime", SqlDbType.NVarChar, 50);
                _startdate.Value = this.context.Request.Form["Starttime"];
                _cmd.Parameters.Add(_startdate);

                SqlParameter _days = new SqlParameter("Days", SqlDbType.Int);
                _days.Value = this.context.Request.Form["Days"];
                _cmd.Parameters.Add(_days);
                DateTime _sttime = Convert.ToDateTime(this.context.Request.Form["Starttime"]);
                DateTime _begintime = _sttime.AddDays(Convert.ToInt32(this.context.Request.Form["Days"]) - 1);
                string _endstr = _begintime.Year + "-" + _begintime.Month + "-" + _begintime.Day + " 23:59:59";
                SqlParameter _endtime = new SqlParameter("EndTime", SqlDbType.NVarChar, 50);

                _endtime.Value = _endstr;
                _cmd.Parameters.Add(_endtime);

                SqlParameter _place = new SqlParameter("Destination", SqlDbType.NVarChar, 100);
                _place.Value = this.context.Request.Form["Destination"];
                _cmd.Parameters.Add(_place);

                SqlParameter _tripNum = new SqlParameter("TripNum", SqlDbType.Int);
                _tripNum.Value = this.context.Request.Form["TripNum"];
                _cmd.Parameters.Add(_tripNum);

                SqlParameter _tripMember = new SqlParameter("TripMembers", SqlDbType.NVarChar, 100);
                _tripMember.Value = this.context.Request.Form["TripMembers"];
                _cmd.Parameters.Add(_tripMember);


                SqlParameter _reson = new SqlParameter("ApplieReson", SqlDbType.NVarChar, 200);
                _reson.Value = this.context.Request.Form["ApplieReson"];
                _cmd.Parameters.Add(_reson);

                SqlParameter _statue = new SqlParameter("AppliedStatue", SqlDbType.NVarChar, 50);
                _statue.Value = "待处理";
                _cmd.Parameters.Add(_statue);

                result = SQLProvider.Transcation(_cmd);
                result.DocumentElement.SetAttribute("guid", _guid);
                // return this.result;
            }
            return this.result;
        }
        //派遣司机
        public XmlDocument sendDriver(string _guid, string _account, string _name, string _linceseid, string _tel)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("sendDriver");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _licenid = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                _licenid.Value = _linceseid;
                _cmd.Parameters.Add(_licenid);

                SqlParameter _owner = new SqlParameter("CarOwner", SqlDbType.NVarChar, 50);
                _owner.Value = _name;
                _cmd.Parameters.Add(_owner);

                SqlParameter _coAccount = new SqlParameter("COAccount", SqlDbType.NVarChar, 50);
                _coAccount.Value = _account;
                _cmd.Parameters.Add(_coAccount);

                SqlParameter _carTel = new SqlParameter("CarTelephone", SqlDbType.NVarChar, 50);
                _carTel.Value = _tel;
                _cmd.Parameters.Add(_carTel);

                SqlParameter _guids = new SqlParameter("CarAppliedID", SqlDbType.NVarChar, 50);
                _guids.Value = _guid;
                _cmd.Parameters.Add(_guids);

                result = SQLProvider.Transcation(_cmd);
            }

            return this.result;

        }
        //更改预约状态
        public XmlDocument upApplyStatue(string _guid, string _statue)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("upApplyStatue");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _applyID = new SqlParameter("CarAppliedID", SqlDbType.NVarChar, 50);
                _applyID.Value = _guid;
                _cmd.Parameters.Add(_applyID);

                SqlParameter _statues = new SqlParameter("AppliedStatue", SqlDbType.NVarChar, 50);
                _statues.Value = _statue;
                _cmd.Parameters.Add(_statues);

                result = SQLProvider.Transcation(_cmd);
            }

            return this.result;
        }
	 //更改预约状态
        public XmlDocument upEndApply(string _guid, string EndTime,int days)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("upApplyStatue");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _applyID = new SqlParameter("CarAppliedID", SqlDbType.NVarChar, 50);
                _applyID.Value = _guid;
                _cmd.Parameters.Add(_applyID);

                SqlParameter _endTime= new SqlParameter("EndTime", SqlDbType.NVarChar, 50);
                _endTime.Value = EndTime;
                _cmd.Parameters.Add(_endTime);

		 SqlParameter _days= new SqlParameter("Days", SqlDbType.Int);
                _days.Value = days;
                _cmd.Parameters.Add(_days);

                result = SQLProvider.Transcation(_cmd);
            }

            return this.result;
        }
        //拒绝预约
        public XmlDocument upStauteRefuse(string _guid, string _statue, string _reson)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("upStauteRefuse");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _applyID = new SqlParameter("CarAppliedID", SqlDbType.NVarChar, 50);
                _applyID.Value = _guid;
                _cmd.Parameters.Add(_applyID);

                SqlParameter _refuseReson = new SqlParameter("RefuseReson", SqlDbType.NVarChar, 200);
                _refuseReson.Value = _reson;
                _cmd.Parameters.Add(_refuseReson);

                SqlParameter _statues = new SqlParameter("AppliedStatue", SqlDbType.NVarChar, 50);
                _statues.Value = _statue;
                _cmd.Parameters.Add(_statues);

                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        //延期返回
        public XmlDocument delayApply(string _guid, int _delayDays, DateTime _delyEndTime)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("delayApply");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _applyID = new SqlParameter("CarAppliedID", SqlDbType.NVarChar, 50);
                _applyID.Value = _guid;
                _cmd.Parameters.Add(_applyID);

                SqlParameter _endTime = new SqlParameter("EndTime", SqlDbType.NVarChar, 50);
                _endTime.Value = _delyEndTime;
                _cmd.Parameters.Add(_endTime);

                SqlParameter _days = new SqlParameter("Days", SqlDbType.Int);
                _days.Value = _delayDays;
                _cmd.Parameters.Add(_days);

                result = SQLProvider.Transcation(_cmd);
            }
            return result;
        }
        //查找某车某天冲突记录(id)
        public XmlDocument clashApply(string _delayLicenseid, DateTime _dalyStarTtime)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = readXml("delayClashApply");

            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;

                SqlParameter _licenseid = new SqlParameter("LicenseID", SqlDbType.NVarChar, 50);
                _licenseid.Value = _delayLicenseid;
                _cmd.Parameters.Add(_licenseid);

                SqlParameter _starttime = new SqlParameter("StartTime", SqlDbType.NVarChar, 50);
                _starttime.Value = _dalyStarTtime;
                _cmd.Parameters.Add(_starttime);

                result = SQLProvider.GetData(_cmd);
            }
            return result;
        }
        //获取有出车天数的车牌
        public XmlDocument getCarListOnDesc(string _licenseidList)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = " SELECT SUM(CAST(DAYS as int)) AS DAYS ,LicenseID FROM CAR_APPLIED WHERE  AppliedStatue!='拒绝' and AppliedStatue!='取消'  AND LicenseID!='NULL' AND " + _licenseidList + "  group by licenseid order by DAYS ASC";
            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                this.result = SQLProvider.GetData(_cmd);
            }
            return result;
        }
          //获取有出车天数的车牌
        public string getApplyCount(string _SqlLicenseID,DateTime _SqlStart,DateTime _SqlEnd)
        {
            result = XmlProvider.Document("sqldata");
            string _sql = "Select Count(*) AS NUM from Car_Applied where licenseid='"+_SqlLicenseID+"' and StartTime>='"+_SqlStart.ToString()+"' and StartTime<='"+_SqlEnd.ToString()+"'";
            //读取数据
            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = _sql;
                this.result = SQLProvider.GetData(_cmd);
            }
            return result.DocumentElement.SelectSingleNode("SchemaTable/Num").InnerText.ToString().Trim();
        }
        //读取XML
        public string readXml(string _type)
        {
            XmlDocument _config = XmlProvider.Document("sqlConfig");
            String _sql = "";
            //读取xml文件
            try
            {
                string _path = "/app_data/apply.xml";
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