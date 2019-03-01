namespace CLDD.Providers
{
    using System;
    using System.Web;
    using System.Web.Security;
    using System.Xml;
    using System.Data;
    using System.Data.SqlClient;
    using CS.Providers;

    public class ProfileProvider : CS.Providers.ProfileProvider
    {
        private readonly string conn = "SYSLOG";
        private readonly string key = "QGJJX.TOKEN";
        private readonly string rnd = "QGJJX.RND"; 

        private string token = "";
        private string ip;
        private string client;

        public ProfileProvider(HttpContext _context)
            : base(_context)
        {
            this.ip = WebProvider.GetIP(_context);
            this.client = WebProvider.GetClient(_context);

            HttpCookie _cookie = this.context.Request.Cookies[this.key];
            if (_cookie == null) { this.TokenCreate(); }
            this.TokenActive();
            this.current = TokenData();
	   
        }

        public void Dispose()
        {
            HttpCookie _cookie = this.context.Request.Cookies[this.key];
            if (_cookie != null)
            {
                this.context.Request.Cookies.Remove(this.key);
                _cookie.Expires = DateTime.Now.AddHours(-1);
                this.context.Response.Cookies.Add(_cookie);
            }
        }

        private XmlDocument TokenData()
        {
            XmlDocument _result = XmlProvider.Document("sqldata");
            try
            {
                //获取用户
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = "select top 1 * from V_Token where TokenID=@id";

                    SqlParameter _id = new SqlParameter("id", SqlDbType.NVarChar, 50);
                    _id.Value = this.token;
                    _cmd.Parameters.Add(_id);

                    _result = SQLProvider.GetData(_cmd);
                    //XmlDocument _api = XmlProvider.Document("api");
                    //_api.Load("http://api.qgj.cn/webapi/profile.asmx/GetUser?User=" + _result.DocumentElement.SelectSingleNode("SchemaTable/TokenAccount").InnerText.Trim());
		    //设置用户名
                    XmlElement _name = _result.CreateElement("TokenName");
		    GetUserInfo user = new GetUserInfo(_result.DocumentElement.SelectSingleNode("SchemaTable/TokenAccount").InnerText.Trim());
                    _name.InnerXml = "<![CDATA[" + user.getUserName() + "]]>";
                    _result.DocumentElement.SelectSingleNode("SchemaTable").AppendChild(_name);
		            
                   //设置权限节点
                   XmlElement _realPermit = _result.CreateElement("TokenPermit");
                   _realPermit.InnerXml = "<![CDATA[ ]]>";
                   _result.DocumentElement.SelectSingleNode("SchemaTable").AppendChild(_realPermit);
                }
            } 
            catch (Exception _exc)
            {
                Provider.LogErr(_result, _exc);
            }

            return _result;
        }

        private XmlDocument TokenExecute(string _txt)
        {
            return this.TokenExecute(_txt, this.token);
        }

        private XmlDocument TokenExecute(string _txt, string _token)
        {
            XmlDocument _result = XmlProvider.Document("sqldata");

            try
            {
                using (SqlCommand _cmd = new SqlCommand())
                {
                    _cmd.Connection = SQLConfig.Connection(this.conn);
                    _cmd.CommandText = _txt;

                    SqlParameter _id = new SqlParameter("id", SqlDbType.NVarChar, 50);
                    _id.Value = _token;
                    _cmd.Parameters.Add(_id);
                    SqlParameter _ip = new SqlParameter("ip", SqlDbType.NVarChar, 50);
                    _ip.Value = this.ip;
                    _cmd.Parameters.Add(_ip);
                    SqlParameter _client = new SqlParameter("client", SqlDbType.NVarChar, 50);
                    _client.Value = this.client;
                    _cmd.Parameters.Add(_client);
                    SqlParameter _expired = new SqlParameter("expired", SqlDbType.DateTime);
                    _expired.Value = DateTime.Now.AddHours(1);
                    _cmd.Parameters.Add(_expired);

                    _result = SQLProvider.Transcation(_cmd);
                }
            }
            catch (Exception _exc) { Provider.LogErr(_result, _exc); }

            return _result;
        }

        private void TokenCreate()
        {
            string _token = DateTime.Now.ToString("yyyyMMddHHmmssfff") + (new Random()).Next().ToString();
            _token += this.ip + this.client;
            _token = FormsAuthentication.HashPasswordForStoringInConfigFile(_token, "md5");

            XmlDocument _result = this.TokenExecute("insert into SysToken (TokenID,TokenIP,TokenClient,TokenExpired) values (@id,@ip,@client,@expired)", _token);
            if (int.Parse(_result.DocumentElement.GetAttribute("affect")) > 0)
            {
                this.token = _token;
                HttpCookie _cookie = new HttpCookie(this.key);
                _cookie.Value = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, this.key, DateTime.Now, DateTime.Now.AddHours(1), false, this.token));
                this.context.Response.Cookies.Add(_cookie);
		
            }
        }

        private void TokenActive()
        {
            try
            {
                HttpCookie _cookie = this.context.Request.Cookies[this.key];
                this.token = FormsAuthentication.Decrypt(_cookie.Value).UserData;

                XmlDocument _result = this.TokenExecute("update SysToken set TokenExpired=@expired where TokenID=@id and TokenIP=@ip and TokenClient=@client");

                if (int.Parse(_result.DocumentElement.GetAttribute("affect")) > 0)
                {
                    _cookie.Expires.AddHours(1);
                    this.context.Response.Cookies.Add(_cookie);
                }
                else
                {
                    this.Dispose();
                    this.TokenCreate();
                }
            }
            catch
            {
                this.Dispose();
                this.TokenCreate();
            }
        }

        public string Token
        {
            get { return this.token; }
        }

        public override XmlDocument CURRENT
        {
            get { this.current = TokenData(); return this.current; }
        }
    }
}