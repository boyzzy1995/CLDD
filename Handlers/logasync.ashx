<%@ WebHandler Language="C#" Class="LogAsync" %>

using System;
using System.Web;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using CS.Providers;
using CLDD.Providers;

public class LogAsync : IHttpHandler {

    private string conn = "SYSLOG";
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.Clear();
        context.Response.ContentType = "text/xml";
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        
        XmlDocument _result = XmlProvider.Document("sqldata");
        try
        {
            CLDD.Providers.ProfileProvider _profile = new CLDD.Providers.ProfileProvider(context);

            using (SqlCommand _cmd = new SqlCommand())
            {
                _cmd.Connection = SQLConfig.Connection(this.conn);
                _cmd.CommandText = "update systoken set TokenAccount=@account where tokenid=@token";
                
                SqlParameter _token = new SqlParameter("token", SqlDbType.NVarChar, 50);
                _token.Value = _profile.Token;
                _cmd.Parameters.Add(_token);

                SqlParameter _account = new SqlParameter("account", SqlDbType.NVarChar, 50);
                _account.Value = context.Request.QueryString["account"];
                _cmd.Parameters.Add(_account);

                if (context.Request.QueryString["pwd"] != null)
                {
                    _cmd.CommandText += "\r\ndelete sysdialog where tokenid=@token\r\ninsert sysdialog values (@token,@pwd)";
                    
                    SqlParameter _pwd = new SqlParameter("pwd", SqlDbType.NVarChar, 50);
                    _pwd.Value = context.Request.QueryString["pwd"];
                    _cmd.Parameters.Add(_pwd);
                }

                SQLProvider.Transcation(_cmd);
            }
            _result = _profile.CURRENT;
        }
        catch(Exception _exc)
        {
            Provider.LogErr(_result, _exc);
        }
        context.Response.Write(XmlProvider.ToString(_result));
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}