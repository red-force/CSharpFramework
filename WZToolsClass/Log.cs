using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF
{
    public partial class GlobalClass
    {
        /// <summary>
        /// 用於打印日誌；for recording the log.
        /// </summary>
        public class Log
        {
            #region System.diagnostics.Debug


            /// <summary>
            /// 開發狀態下，用於在終端打印消息的方法；function for writing line in the console when the programme is underconstruction.
            /// </summary>
            /// <param name="sMessage">要輸出的消息；the message to print</param>
            /// <returns>程序是否在開發狀態下；whether the programme is underconstruction</returns>
            /// <example>
            /// Boolean isUnderConstruction = GlobalClass.log.Underconstruction("Write Log Message Here.");
            /// </example>
            public static Boolean Underconstruction(String sMessage = "")
            {
                System.Diagnostics.Debug.WriteLine("Underconstruction");
                System.Diagnostics.Debug.WriteLineIf(Config.IsUnderconstruction == true, sMessage);
                return Config.IsUnderconstruction;
            }
            /// <summary>
            /// 用於在終端打印消息以跟踪程序的方法；function for writing line in the console.
            /// </summary>
            /// <param name="sMessage">要輸出的消息；the message to print</param>
            /// <returns>程序監控是否開啟；whether the monitoring is on</returns>
            /// <example>
            /// Boolean isMonitoring = GlobalClass.Log.Monitor("Write Log Message Here.");
            /// </example>
            public static Boolean Monitor(String sMessage = "")
            {
                String sFullPath = System.IO.Path.GetFullPath("log.txt");

                Utils.IO.AppendToFile("D:/log.txt", sContent: "F: is exist ? " + System.IO.Directory.Exists("F:/"));
                Utils.IO.AppendToFile("D:/log.txt", sContent: "D: is exist ? " + System.IO.Directory.Exists("D"));
                Utils.IO.AppendToFile("D:/log.txt", sContent: sFullPath);
                Utils.IO.AppendToFile("D:/log.txt", sContent: "Config.MonitoringLogPath:"+Config.MonitoringLogPath);
                Utils.IO.AppendToFile(Config.MonitoringLogPath, (System.DateTime.Now.ToString() + sMessage));
                System.Diagnostics.Debug.WriteLine("Monitor");
                System.Diagnostics.Debug.WriteLineIf(Config.IsMonitoring, sMessage);
                return Config.IsMonitoring;
            }

            #endregion
        }
    }
}