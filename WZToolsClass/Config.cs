using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF
{

    public partial class GlobalClass
    {
        /// <summary>
        /// 用於記錄配置；for recording the configure.
        /// </summary>
        public class Config
        {
            #region System.diagnostics.Debug
            private static String _baseDir = "D:/rf/";
            /// <summary>
            /// 程序的基礎目錄文件夾地址；the base directory of the programme.
            /// </summary>
            protected internal static String BaseDir
            {
                get { return Config._baseDir; }
                set
                {
                    String sPattern = "^[A-Z]:";
                    String sRootDir = System.Text.RegularExpressions.Regex.Match(value, sPattern).Value;
                    String[] sRootDirList = { "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "A", "B" };
                    if ("" != sRootDir && false == System.IO.Directory.Exists(sRootDir))
                    {
                        // 當滿足匹配條件時，檢查分區是否存在，若不存在，則執行下面的邏輯，智能更改分區。
                        foreach (var i in sRootDirList)
                        {
                            sRootDir = i + ":";
                            if (true == System.IO.Directory.Exists(sRootDir))
                            {
                                break;
                            }
                        }
                        System.Text.RegularExpressions.Regex.Replace(value, sPattern, sRootDir);
                    }
                    Config._baseDir = Utils.Format.FormatPath(value, "folder");
                }
            }

            private static String _logDir = "Log/";
            /// <summary>
            /// 程序的日誌文件夾地址；the log directory of the programme.
            /// </summary>
            protected internal static String LogDir
            {
                get { return Config._logDir; }
                set { Config._logDir = Utils.Format.FormatPath(value, "folder"); }
            }

            private static String _monitoringLogFileName = "monitoring.log";
            /// <summary>
            /// 用於記錄監視日誌的日誌文件的名稱；the name of the file that store monitoring log.
            /// </summary>
            protected internal static String MonitoringLogFileName
            {
                get { return Config._monitoringLogFileName; }
                set { Config._monitoringLogFileName = Utils.Format.FormatPath(value, "file"); }
            }
            private static String _underconstructionLogFileName = "underconstruction.log";
            /// <summary>
            /// 用於記錄程序開發狀態下的日誌文件的名稱；the name of the file hold log when programme is underconstruction.
            /// </summary>
            protected internal static String UnderconstructionLogFileName
            {
                get { return Config._underconstructionLogFileName; }
                set { Config._underconstructionLogFileName = Utils.Format.FormatPath(value, "file"); }
            }
            /// <summary>
            /// 監視日誌路徑；the path of monitoring log.
            /// </summary>
            public static String MonitoringLogPath
            {
                get
                {
                    return Config.BaseDir + Config.LogDir + Config.MonitoringLogFileName;
                }
            }
            /// <summary>
            /// 開發日誌路徑；the path of underconstruction log.
            /// </summary>
            public static String UnderconstructionLogPath
            {
                get { return Config.BaseDir + Config.LogDir + Config.UnderconstructionLogFileName; }
            }

            private static bool _underconstruction = true;
            /// <summary>
            /// 記錄當前是否是在開發狀態；Record the status of the programme, if it is underconstruction or not.
            /// </summary>
            public static bool IsUnderconstruction
            {
                get { return Config._underconstruction; }
            }
            /// <summary>
            /// 設置當前是否是在開發狀態；Setting the status of the programme, if it is underconstruction or not.
            /// </summary>
            /// <permission cref="GlobalClass.Config.IsUnderconstruction">this is protected and internal.</permission>
            protected internal static bool LetUnderconstructionBe
            {
                set { Config._underconstruction = value; }
            }
            /// <summary>
            /// 記錄當前是否啟用程序監控；Record whether to monitor the programme.
            /// </summary>
            private static bool _monitoring = true;
            /// <summary>
            /// 記錄當前是否啟用程序監控；Record whether to monitor the programme.
            /// </summary>
            public static bool IsMonitoring
            {
                get { return Config._monitoring; }
            }
            /// <summary>
            /// 設置當前是否啟用程序監控；Setting whether needs to monitor the programme. 
            /// </summary>
            /// <permission cref="GlobalClass.Config.IsMonitoring">this is protected and internal.</permission>
            protected internal static bool LetMonitoringBe
            {
                set { Config._monitoring = value; }
            }
            #endregion
        }
    }
}
