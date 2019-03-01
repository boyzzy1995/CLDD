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

    public class CarService : HttpResponseProvider
    {
        public XmlDocument result;
        public string conn = "WEB";
        public HttpContext context;
        public CarService(HttpContext _context) : base(_context) { this.context = _context; }

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


    }
}