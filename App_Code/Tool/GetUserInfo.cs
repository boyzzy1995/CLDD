namespace CLDD.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml;
    using Newtonsoft.Json;
    using System.Net;
    using System.Text;
    using System.IO;

    /// <summary>
    /// GetUserInfo json获取钉钉getUser方法对应的内容
    /// </summary>
    public class GetUserInfo
    {
        public string ACCOUNT, JSON;
        //实例化类输入账号
        public GetUserInfo(string _account)
        {
            this.ACCOUNT = _account;
            this.JSON = getUserMessage();
        }
        //调用接口获取用户信息；
        public string getUserMessage()
        {
            string _result, _json;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://115.236.68.206/dingtalk/profile.asmx/GetUser");
            request.Method = "POST";
            string json = "key=uid&value=" + this.ACCOUNT;
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            request.ContentType = "application/x-www-form-urlencoded";
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
                //stram response
                using (Stream response = request.GetResponse().GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(response, Encoding.UTF8))
                    {
                        _result = reader.ReadToEnd();
                        try
                        {
                            XmlDocument _xml = new XmlDocument();
                            _xml.LoadXml(_result);
                            _json = _xml.DocumentElement.InnerText.ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        reader.Close();
                    }

                    response.Close();
                }
            }
            return _json;
        }
        //判断用户是不是属于设计院人员
        public string getOUCode(string _oucode)
        {
            UserBean user = JsonConvert.DeserializeObject<UserBean>(this.JSON);
            foreach (var position in user.position)
            {
                if (position.OUCode.Contains(_oucode)) { return position.OUCode; }
            }
            return "";
        }
        //获取用户名信息
        public string getUserName()
        {
            //用户存在
            if(!this.JSON.Contains("errcode"))
            {
                UserBean user;
                try { user = JsonConvert.DeserializeObject<UserBean>(this.JSON); }
                catch (Exception e) { throw e; }
                return user.name;
            }
            else
            {
                return this.JSON;
            }
            
        }
    }
}