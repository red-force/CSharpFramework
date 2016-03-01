using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAExample
{
    [Serializable]
    public class UserSession
    {
        /// <summary>
        /// 
        /// </summary>
        public string GetCode { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 部门描述
        /// </summary>
        public string dptdes { get; set; }
        private string _dptid { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string dptid
        {
            get { return (this.log == "wangzhi") ? this.depid : this._dptid; }
            set { this._dptid = value; }
        }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string empid { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 【弃用】部门编号
        /// </summary>
        public string depid { get; set; }
        /// <summary>
        /// 人员描述
        /// </summary>
        public string empdes { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string login_name { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public string lasttime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cont { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isstp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idno { get; set; }
        /// <summary>
        /// 登录类型
        /// </summary>
        public string log { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public string cansee { get; set; }
        /// <summary>
        /// 是否可见钞票
        /// </summary>
        public string seemoney { get; set; }
    }
}