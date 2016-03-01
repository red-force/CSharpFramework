using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAExample
{
    public class Location
    {
        /// <summary>
        /// 協議
        /// </summary>
        /// <example>
        /// "http:"
        /// </example>
        public string protocol { get; set; }

        /// <summary>
        /// 主機名
        /// </summary>
        /// <example>
        /// "localhost"
        /// </example>
        public string hostname { get; set; }

        /// <summary>
        /// 端口號
        /// </summary>
        /// <example>
        /// "1352"
        /// </example>
        public string port { get; set; }

        /// <summary>
        /// 路徑名稱
        /// </summary>
        /// <example>
        /// "/WFScoreCardQuery.aspx"
        /// </example>
        public string pathname { get; set; }

        /// <summary>
        /// 查詢參數
        /// </summary>
        /// <example>
        /// "?view=0"
        /// </example>
        public string search { get; set; }

        /// <summary>
        /// 錨點
        /// </summary>
        /// <example>
        /// "#go"
        /// </example>
        public string hash { get; set; }

        /// <summary>
        /// 主機地址
        /// </summary>
        /// <example>
        /// "localhost:1352"
        /// </example>
        public string host
        {
            get
            {
                string result = String.Empty;
                if (null != this.hostname && String.Empty != this.hostname)
                {
                    result = this.hostname
                        + ((String.Empty != this.port) ? (":" + this.port) : String.Empty);
                }
                else { }
                return result;
            }
            // set;
        }

        /// <summary>
        /// 主機連接地址
        /// </summary>
        /// <example>
        /// "http://localhost:1352"
        /// </example>
        public string origin
        {
            get
            {
                string result = String.Empty;
                if (null != this.protocol && String.Empty != this.protocol
                    && null != this.hostname && String.Empty != this.hostname)
                {
                    result = this.protocol
                        + "//" + this.hostname
                        + ((String.Empty != this.port) ? (":" + this.port) : String.Empty);
                }
                else { }
                return result;
            }
            // set;
        }

        /// <summary>
        /// 完整的連接地址
        /// </summary>
        /// <example>
        /// "http://localhost:1352/WFScoreCardQuery.aspx?view=0"
        /// </example>
        public string href
        {
            get
            {
                string result = String.Empty;
                if (null != this.protocol && String.Empty != this.protocol
                    && null != this.hostname && String.Empty != this.hostname)
                {
                    result = this.protocol
                        + "//" + this.hostname
                        + ((String.Empty != this.port) ? (":" + this.port) : String.Empty)
                        + ((String.Empty != this.pathname) ? ("/" + this.pathname) : String.Empty)
                        + this.search
                        + this.hash;
                }
                else { }
                return result;
            }
            // set;
        }
    }
}