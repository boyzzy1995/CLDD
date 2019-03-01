namespace CLDD.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    ///User 的摘要说明
    /// </summary>
    public class UserBean
    {
        public UserBean()
        {
        }
        public string uid { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string tel { get; set; }
        public string mail { get; set; }
        public string sexy { get; set; }
       // public Position[] position { get; set; }
        public IList<Position> position { get;set;}
    }
    public class Position
    {
        public string OUCode { get; set; }
        public string FulName { get; set; }
    }
}