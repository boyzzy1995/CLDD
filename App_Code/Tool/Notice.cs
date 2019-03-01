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
    public class Notice : HttpResponseProvider
    {
        public XmlDocument result;
        public string conn = "WEB";
        public HttpContext context;
        public Notice(HttpContext _context) : base(_context) { this.context = _context; }


        //获取调度员账号
        public string getSysAccount()
        {
            string _sysAccount = "";
            XmlDocument _lsDom = (XmlDocument)Provider.Invoke("CLDD.Providers.DispatcherDol.getLsDispatcher", new object[] { this.context }, null);
            if (_lsDom.DocumentElement.SelectSingleNode("SchemaTable") != null)//存在临时调度员
            {
                _sysAccount = _lsDom.DocumentElement.SelectSingleNode("SchemaTable/PermitAccount").InnerText.ToString();
            }
            else
            {
                _sysAccount = (string)Provider.Invoke("CLDD.Providers.ToolDol.getSysAccount", new object[] { this.context }, null);
            }

            return _sysAccount;

        }
        //获取领导账号
        public string getDepart(string _apply)
        {
            GetUserInfo user = new GetUserInfo(_apply);
            return (string)Provider.Invoke("CLDD.Providers.ToolDol.getDepartAccount", new object[] { this.context }, new object[] { user.getOUCode("0002") });

        }
        //通知调度员审核
        public void sendSureApply(XmlDocument _data)
        {
            string _sysAccount = getSysAccount();
            string _applyAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedAccount").InnerText.ToString().Trim();//申请人账号
            string _applyName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedName").InnerText.ToString().Trim();//申请人名字
            string _place = _data.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
            string _startTime = _data.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
            string _endTime = _data.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim();//开始时间
            string _reson = _data.DocumentElement.SelectSingleNode("SchemaTable/ApplieReson").InnerText.ToString().Trim();//申请理由
            string _txt = "车辆申请通知：\r" + _applyName + "申请目的地为：" + _place + "；\r开始时间为：" + _startTime + "；\r结束时间为：" + _endTime + "的车辆，\r申请理由为理由为：" + _reson + "\r通知时间：" + DateTime.Now;
            string _applyTxt = "车辆申请通知：\r 您申请的开始时间为：" + _startTime + "\r 目的地为：" + _place + "\r 的车辆等待管理员的审核\r通知时间：" + DateTime.Now;

            //钉钉通知
            string _userid = _sysAccount;
            sendDingdingMsg(_userid, _txt);

            _userid = _applyAccount;
            sendDingdingMsg(_userid, _applyTxt);

            //短信提示
            XmlDocument _sms = XmlProvider.Document("api");
            XmlDocument _smsApply = XmlProvider.Document("api");
            _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "&Message=" + _txt);
            _smsApply.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _applyAccount + "&Message=" + _txt);

        }
        //发送钉钉通知方法封装
        public string sendDingdingMsg(string _userid, string _msg)
        {
            DDMessage.dingtalkSoapClient _dd = new DDMessage.dingtalkSoapClient();
            return _dd.SendTextMessage(_userid, _msg);
        }
        //获取全部用户列表方法封装
        public string getAllUsers(string[] notifiedUsers)//排除已经通知的相关人员
        {
            string users = "";
            int flag = 0;
            int isNotified = 0;
            XmlDocument _allUsers = XmlProvider.Document("api");
            _allUsers.Load("http://bpm.qgj.cn//api/bpmapi.asmx/GetUsers?oucode=0002&deep=true");
            foreach (XmlNode _section in _allUsers.DocumentElement.SelectNodes("User"))
            {
                string currtUser = _section.SelectSingleNode("samaccountname").InnerText;
                //检查当前用户是否在已发送列表notifiedUsers中
                foreach (string user in notifiedUsers)
                {
                    if (user == currtUser)
                    {
                        isNotified = 1;
                        break;
                    }
                }
                //如果未发送过则加入将发送列表users
                if (isNotified == 0)
                {
                    if (flag == 1)
                    {
                        users += ",";
                        users += currtUser;
                    }
                    else
                    {
                        users += currtUser;
                        flag = 1;
                    }
                }
                //恢复默认值
                isNotified = 0;
            }
            return users;
        }

        //预约成功通知（司机、申请人、管理员、部门领导）
        public void applySuccResult(XmlDocument _data)
        {
            //通知信息参数
            string _applyAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedAccount").InnerText.ToString().Trim();//申请人账号
            string _applyName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedName").InnerText.ToString().Trim();//申请人姓名
            string _place = _data.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
            string _driAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/COAccount").InnerText.ToString().Trim();//司机账号
            string _driName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarOwner").InnerText.ToString().Trim();//司机姓名
            string _startTime = _data.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
            string _endTime = _data.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim();//结束时间
            string _driTel = _data.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();//司机联系方式
            string _applyTel = _data.DocumentElement.SelectSingleNode("SchemaTable/AppliedTel").InnerText.ToString().Trim();//申请人联系方式
            string _txt = "车辆预约成功通知：\r申请人：" + _applyName + "\r目的地：" + _place + "\r开始时间：" + _startTime + "\r结束时间：" + _endTime + "\r预约状态：待出车\r联系方式："
                + _applyTel + "\r车辆信息：\r司机:" + _driName + "\r联系方式：" + _driTel + "\r请申请人与司机尽快联系确认\r通知时间：" + DateTime.Now;
            string _sysAccount = getSysAccount();
            string _leaderAccount = getDepart(_applyAccount);
            XmlDocument _notify = XmlProvider.Document("api");



            //sendDingdingMsg(_applyAccount, "测试中："+getAllUsers());
            if (_leaderAccount != "")
            {
                //钉钉通知
                string _userid = _applyAccount + "," + _leaderAccount + "," + _sysAccount + "," + _driAccount;
                sendDingdingMsg(_userid, _txt);
                //短信提示 
                XmlDocument _sms = XmlProvider.Document("api");
                _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "," + _leaderAccount + "," + _driAccount + "," + _applyAccount + "&Message=" + _txt);
                //通知全体
                // string[] notifiedUsers = new string[] { _applyAccount, _leaderAccount, _sysAccount, _driAccount };
                //sendAllOut(_data, notifiedUsers);
            }
            else
            {
                //钉钉通知
                string _userid = _applyAccount + "," + _sysAccount + "," + _driAccount;
                sendDingdingMsg(_userid, _txt);
                //短信提示 
                XmlDocument _sms = XmlProvider.Document("api");
                _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "," + _driAccount + "," + _applyAccount + "&Message=" + _txt);
                //通知全体
                //string[] notifiedUsers = new string[] { _applyAccount, _sysAccount, _driAccount };
                // sendAllOut(_data, notifiedUsers);
            }


        }

        //预约失败通知（申请人）
        public void applyFailResult(XmlDocument _data)
        {
            string _applyAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedAccount").InnerText.ToString().Trim();
            string _place = _data.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
            string _startTime = _data.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
            string _reson = _data.DocumentElement.SelectSingleNode("SchemaTable/RefuseReson").InnerText.ToString().Trim();//拒绝理由
            string _txt = "车辆预约失败通知：\r目的地：" + _place + "，开始时间：" + _startTime + "\r申请状态：拒绝\r理由：" + _reson + "\r通知时间：" + DateTime.Now;

            //钉钉通知
            string _userid = _applyAccount;
            sendDingdingMsg(_userid, _txt);

            //短信提示 
            XmlDocument _sms = XmlProvider.Document("api");
            _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _applyAccount + "&Message=" + _txt);
        }

        //延迟归来通知（管理员、部门领导）
        public void delyApply(XmlDocument _data)
        {
            string _sysAccount = getSysAccount();
            string _place = _data.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
            string _startTime = _data.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
            string _txt = "车辆延迟归来通知：\r您预约的目的地为：" + _place + "，开始时间：" + _startTime + "的车辆预约因为该车当前使用者申请延期返回而被取消，系统将为你重新分配车辆。" + "\r通知时间：" + DateTime.Now;
            string _applyAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedAccount").InnerText.ToString().Trim();
            string _leaderAccount = getDepart(_applyAccount);
            //XmlDocument _notify = XmlProvider.Document("api");
            if (_leaderAccount != "")
            {
                //钉钉通知
                string _userid = _leaderAccount + "," + _sysAccount;
                sendDingdingMsg(_userid, _txt);
                //短信提示 
                XmlDocument _sms = XmlProvider.Document("api");
                _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _leaderAccount + "," + _sysAccount + "&Message=" + _txt);
            }
            else
            {
                //钉钉通知
                string _userid = _sysAccount;
                sendDingdingMsg(_userid, _txt);
                //短信提示 
                XmlDocument _sms = XmlProvider.Document("api");
                _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "&Message=" + _txt);
            }

        }
        //重新预约成功通知（司机、申请人、管理员）
        public void applyAginSuccResult(XmlDocument _data)
        {
            string _sysAccount = getSysAccount();
            string _applyName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedName").InnerText.ToString().Trim();//申请人姓名
            string _place = _data.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
            string _driAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/COAccount").InnerText.ToString().Trim();//司机账号
            string _driName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarOwner").InnerText.ToString().Trim();//司机姓名
            string _startTime = _data.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
            string _endTime = _data.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim();//结束时间
            string _driTel = _data.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();//司机联系方式
            string _applyTel = _data.DocumentElement.SelectSingleNode("SchemaTable/AppliedTel").InnerText.ToString().Trim();//申请人联系方式
            string _txt = "车辆重新预约成功通知：\r申请人：" + _applyName + "\r目的地：" + _place + "\r开始时间：" + _startTime + "\r结束时间：" + _endTime + "\r预约状态：待出车\r联系方式：" + _applyTel + "\r车辆信息：\r司机:" + _driName + "\r联系方式：" + _driTel + "\r请申请人与司机尽快联系确认" + "\r通知时间：" + DateTime.Now;
            string _applyAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedAccount").InnerText.ToString().Trim();
            string _leaderAccount = getDepart(_applyAccount);
            //XmlDocument _notify = XmlProvider.Document("api");
            if (_leaderAccount != "")
            {
                string _userid = _applyAccount + "," + _leaderAccount + "," + _sysAccount + "," + _driAccount;
                sendDingdingMsg(_userid, _txt);
                //短信提示 
                XmlDocument _sms = XmlProvider.Document("api");
                _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "," + _leaderAccount + "," + _driAccount + "," + _applyAccount + "&Message=" + _txt);
                //通知全体
                //string[] notifiedUsers = new string[] { _applyAccount, _leaderAccount, _sysAccount, _driAccount };
                //sendAllOut(_data, notifiedUsers);
            }
            else
            {
                //钉钉通知
                string _userid = _applyAccount + "," + _sysAccount + "," + _driAccount;
                sendDingdingMsg(_userid, _txt);
                //短信提示 
                XmlDocument _sms = XmlProvider.Document("api");
                _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "," + _driAccount + "," + _applyAccount + "&Message=" + _txt);
                //通知全体
                //string[] notifiedUsers = new string[] { _applyAccount, _sysAccount, _driAccount };
                //sendAllOut(_data, notifiedUsers);
            }

        }
        //取消预约通知（管理员、申请人、部门领导、司机）
        public void cancelApplyResult(XmlDocument _data)
        {
            //通知信息参数
            string _applyName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedName").InnerText.ToString().Trim();//申请人姓名
            string _place = _data.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
            string _driAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/COAccount").InnerText.ToString().Trim();//司机账号
            string _driName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarOwner").InnerText.ToString().Trim();//司机姓名
            string _startTime = _data.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
            string _driTel = _data.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();//司机联系方式
            string _endTime = _data.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim();//结束时间
            string _txt = "车辆预约取消通知：\r申请人：" + _applyName + "\r目的地：" + _place + "\r开始时间：" + _startTime + "\r结束时间：" + _endTime + "\r车辆信息：\r司机:" + _driName + "\r联系方式：" + _driTel + "\r车辆申请已被申请人取消。" + "\r通知时间：" + DateTime.Now;
            string _applyAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedAccount").InnerText.ToString().Trim();
            string _sysAccount = getSysAccount();
            string _leaderAccount = getDepart(_applyAccount);
            //XmlDocument _notify = XmlProvider.Document("api");
            if (_leaderAccount != "")
            {
                //钉钉通知
                string _userid = _applyAccount + "," + _leaderAccount + "," + _sysAccount + "," + _driAccount;
                sendDingdingMsg(_userid, _txt);
                //短信提示 
                XmlDocument _sms = XmlProvider.Document("api");
                _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "," + _leaderAccount + "," + _driAccount + "," + _applyAccount + "&Message=" + _txt);
            }
            else
            {
                //钉钉通知
                string _userid = _applyAccount + "," + _sysAccount + "," + _driAccount;
                sendDingdingMsg(_userid, _txt);
                //短信提示 
                XmlDocument _sms = XmlProvider.Document("api");
                _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "," + _driAccount + "," + _applyAccount + "&Message=" + _txt);
            }

        }

        //更换车辆通知（申请人）
        public void applychangeCar(XmlDocument _data)
        {
            //通知信息参数
            string _applyAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedAccount").InnerText.ToString().Trim();//申请人账号
            string _applyName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedName").InnerText.ToString().Trim();//申请人姓名
            string _place = _data.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
            string _driAccount = _data.DocumentElement.SelectSingleNode("SchemaTable/COAccount").InnerText.ToString().Trim();//司机账号
            string _driName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarOwner").InnerText.ToString().Trim();//司机姓名
            string _startTime = _data.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
            string _endTime = _data.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim();//结束时间
            string _driTel = _data.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();//司机联系方式
            string _applyTel = _data.DocumentElement.SelectSingleNode("SchemaTable/AppliedTel").InnerText.ToString().Trim();//申请人联系方式
            string _txt = "车辆更换通知：\r申请人：" + _applyName + "\r目的地：" + _place + "\r您的车辆因为调度员调换的原因被取消，系统将重新为您分配车辆" + "\r通知时间：" + DateTime.Now;
            //原 腾讯通 通知
            //XmlDocument _notify = XmlProvider.Document("api");
            //_notify.Load("http://api.qgj.cn/webapi/rtx.asmx/SendNotify?Uids=" + _applyAccount + ";&Title=车辆预约结果&Txt=[" + _txt + "|http://webservices.qgj.cn/cldd/index.html]&Time=20000");
            //钉钉通知
            string _userid = _applyAccount;
            sendDingdingMsg(_userid, _txt);
            //短信提示 
            XmlDocument _sms = XmlProvider.Document("api");
            _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _applyAccount + "&Message=" + _txt);
        }
        //通知全体员工车辆 出行
        //public void sendAllOut(XmlDocument _data, string[] notifiedUsers)
        //{

        //    //通知信息参数
        //    string _place = _data.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
        //    string _driName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarOwner").InnerText.ToString().Trim();//司机姓名
        //    string _startTime = _data.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
        //    string _endTime = _data.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim();//结束时间
        //    string _applyName = _data.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedName").InnerText.ToString().Trim();//申请人姓名
        //    string _driTel = _data.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();//司机联系方式
        //    string _applyTel = _data.DocumentElement.SelectSingleNode("SchemaTable/AppliedTel").InnerText.ToString().Trim();//申请人联系方式

        //    string _txt = "车辆调度信息通知-通知全体员工：\r申请人：" + _applyName + "\r目的地：" + _place + "\r开始时间：" + _startTime + "\r结束时间：" + _endTime + "\r预约状态：待出车\r联系方式："
        //        + _applyTel + "\r车辆信息：\r司机:" + _driName + "\r联系方式：" + _driTel + "\r通知时间：" + DateTime.Now;

        //    钉钉通知
        //    BUG修复：之前已经通知的相关人员在全体通知中不应该再次被通知
        // sendDingdingMsg(getAllUsers(notifiedUsers), _txt);

        //}

        //通知调度员维修延期
        public void delyFixedApply(string _reqLicenseID, int _reqDalyDays)
        {
            string _sysAccount = getSysAccount();
            string _txt = "车辆维修延期通知：车牌：" + _reqLicenseID + ",因为申请人申请延期，维修时间延期：" + _reqDalyDays + "天" + "\r通知时间：" + DateTime.Now;
            //钉钉通知
            string _userid = _sysAccount;
            sendDingdingMsg(_userid, _txt);
            //短信提示 
            XmlDocument _sms = XmlProvider.Document("api");
            _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "&Message=" + _txt);

        }
        //通知原司机、新司机、调度员、申请人，车辆变更成功
        public void changeSuccSendAllOut(XmlDocument _oldData, XmlDocument _newData)
        {
            //通知信息参数（原数据）
            string _place = _oldData.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
            string _driAccount = _oldData.DocumentElement.SelectSingleNode("SchemaTable/COAccount").InnerText.ToString().Trim();//司机账号
            string _licenseID = _oldData.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString().Trim();//车牌
            string _driName = _oldData.DocumentElement.SelectSingleNode("SchemaTable/CarOwner").InnerText.ToString().Trim();//司机姓名
            string _startTime = _oldData.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
            string _endTime = _oldData.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim();//结束时间

            /*通知原司机，提示该车被取消*/
            string _txt = "车辆预约变更通知：有预约车辆被取消，原预约的信息为：\r司机：" + _driName + "\r目的地：" + _place + "\r开始时间：" + _startTime + "\r结束时间：" + _endTime + "\r通知时间：" + DateTime.Now;
            //钉钉通知
            string _userid = _driAccount;
            sendDingdingMsg(_userid, _txt);
            //短信提醒
            XmlDocument _sms = XmlProvider.Document("api");
            _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _driAccount + "&Message=" + _txt);

            //通知信息参数（新数据）
            string new_applyAccount = _newData.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedAccount").InnerText.ToString().Trim();//申请人账号
            string new_applyName = _newData.DocumentElement.SelectSingleNode("SchemaTable/CarAppliedName").InnerText.ToString().Trim();//申请人姓名
            string new_place = _newData.DocumentElement.SelectSingleNode("SchemaTable/Destination").InnerText.ToString().Trim();//目的地
            string new_driAccount = _newData.DocumentElement.SelectSingleNode("SchemaTable/COAccount").InnerText.ToString().Trim();//司机账号
            string new_licenseID = _newData.DocumentElement.SelectSingleNode("SchemaTable/LicenseID").InnerText.ToString().Trim();//车牌
            string new_driName = _newData.DocumentElement.SelectSingleNode("SchemaTable/CarOwner").InnerText.ToString().Trim();//司机姓名
            string new_startTime = _newData.DocumentElement.SelectSingleNode("SchemaTable/StartTime").InnerText.ToString().Trim();//开始时间
            string new_endTime = _newData.DocumentElement.SelectSingleNode("SchemaTable/EndTime").InnerText.ToString().Trim();//结束时间
            string new_driTel = _newData.DocumentElement.SelectSingleNode("SchemaTable/CarTelephone").InnerText.ToString().Trim();//司机联系方式
            string new_applyTel = _newData.DocumentElement.SelectSingleNode("SchemaTable/AppliedTel").InnerText.ToString().Trim();//申请人联系方式

            /*通知新司机，提示有新的出车记录，将申请信息发送给司机*/
            _txt = "车辆预约通知：有新的出车记录，预约申请信息为：\r司机：" + new_driName + "\r车牌：" + new_licenseID + "\r目的地：" + new_place +
                "\r开始时间：" + new_startTime + "\r结束时间：" + new_endTime + "\r申请人：" + new_applyName + "联系方式：" + new_applyTel + "\r通知时间：" + DateTime.Now;
            //原 腾讯通 通知
            //_notify.Load("http://api.qgj.cn/webapi/rtx.asmx/SendNotify?Uids=" + new_driAccount + ";&Title=车辆更改通知&Txt=[" + _txt + "|http://webservices.qgj.cn/cldd/index.html]&Time=20000");
            //钉钉通知
            _userid = new_driAccount;
            sendDingdingMsg(_userid, _txt);
            //短信提醒
            _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + new_driAccount + "&Message=" + _txt);

            /*通知调度员，提示该记录的车辆已经由xx车牌车改成xx车牌车*/
            string _sysAccount = getSysAccount();
            _txt = "车辆变更通知：有预约车辆被更改，车辆由" + _licenseID + "变更为" + new_licenseID + "\r通知时间：" + DateTime.Now;
            //原 腾讯通 通知
            //_notify.Load("http://api.qgj.cn/webapi/rtx.asmx/SendNotify?Uids=" + _sysAccount + ";&Title=车辆更改通知&Txt=[" + _txt + "|http://webservices.qgj.cn/cldd/index.html]&Time=20000");
            //钉钉通知
            _userid = _sysAccount;
            sendDingdingMsg(_userid, _txt);
            //短信提醒
            _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + _sysAccount + "&Message=" + _txt);

            /*通知申请人，将申请信息带上，提示车辆由xxx更改为xxx*/
            _txt = "车辆变更通知：有预约车辆被更改，车辆由" + _licenseID + "变更为" + new_licenseID +
                "\r变更后的预约信息为：\r申请人：" + new_applyName + "\r目的地：" + new_place + "\r开始时间：" + new_startTime + "\r结束时间：" + new_endTime +
                "\r预约状态：待出车\r联系方式：" + new_applyTel + "\r车辆信息：\r司机: " + new_driName + "\r联系方式：" + new_driTel + "\r请申请人与司机尽快联系确认" + "\r通知时间：" + DateTime.Now;
            //原 腾讯通 通知
            //_notify.Load("http://api.qgj.cn/webapi/rtx.asmx/SendNotify?Uids=" + new_applyAccount + ";&Title=车辆更改通知&Txt=[" + _txt + "|http://webservices.qgj.cn/cldd/index.html]&Time=20000");
            //钉钉通知
            _userid = new_applyAccount;
            sendDingdingMsg(_userid, _txt);
            //短信提醒
            _sms.Load("http://api.qgj.cn/webapi/sms.asmx/Send?Uids=" + new_applyAccount + "&Message=" + _txt);

        }


    }
}