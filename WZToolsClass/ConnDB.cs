using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
//namespace Com.LXJ.Database

namespace RF
{
    partial class GlobalClass
    {
        /// <summary>
        /// 数据操作相关类
        /// </summary>
        public partial class DB
        {
            /// <summary>
            /// 用于连接本机服务器localhost
            /// </summary>
            public class ConnDB
            {
                protected SqlConnection Connection;
                private string connectionString;
                /// <summary>
                /// 默认构造函数
                /// </summary>
                public ConnDB()
                {
                    string connStr;
                    connStr = System.Configuration.ConfigurationSettings.AppSettings["connStr"].ToString();
                    connectionString = connStr;
                    Connection = new SqlConnection(connectionString);
                }

                /// <summary>
                /// 带参数的构造函数
                /// </summary>
                /// <param name="newConnectionString"> 数据库联接字符串 </param>
                public ConnDB(string newConnectionString)
                {
                    connectionString = newConnectionString;
                    Connection = new SqlConnection(connectionString);
                }

                /// <summary>
                /// 完成 SqlCommand 对象的实例化
                /// </summary>
                /// <param name="storedProcName"></param>
                /// <param name="parameters"></param>
                /// <returns></returns>
                private SqlCommand BuildCommand(string storedProcName, IDataParameter[] parameters)
                {
                    SqlCommand command = BuildQueryCommand(storedProcName, parameters);
                    command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
                    return command;
                }

                /// <summary>
                /// 创建新的 SQL 命令对象 ( 存储过程)
                /// </summary>
                /// <param name="storedProcName"></param>
                /// <param name="parameters"></param>
                /// <returns></returns>
                private SqlCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters)
                {
                    SqlCommand command = new SqlCommand(storedProcName, Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    return command;
                }

                /// <summary>
                /// 执行存储过程 , 无返回值
                /// </summary>
                /// <param name="storedProcName">the name of the stored procdure;存儲過程的名稱；</param>
                /// <param name="parameters">the instance of IDataParameter array;</param>
                /// <example>
                /// <code language="C#" title="Call ExceuteProcedure">
                /// RF.GlobalClass.DB.ConnDB cdb = new RF.GlobalClass.DB.ConnDB(global::WindowsFormsApplication4sdtapi.Properties.Settings.Default.ClientScenicTicketSaleSystemConnectionString + @"Password=""7654321"";");
                /// #region procedure is not available in local database file
                /// cdb.ExecuteProcedure("Create_ScenicTicketOrder", new System.Data.IDataParameter[] {
                ///                     new System.Data.SqlClient.SqlParameter("@ORDER_D_ORDER_ID",order.id)
                ///                     ,new System.Data.SqlClient.SqlParameter("@ORDER_PRO_ID",order.product.id)
                ///                     ,new System.Data.SqlClient.SqlParameter("@ORDER_SALE_PRICE",order.amount)
                ///                     ,new System.Data.SqlClient.SqlParameter("@ORDER_SALE_UNIT_PRICE",order.product.price)
                ///                     ,new System.Data.SqlClient.SqlParameter("@ORDER_STATUS",Dict.translate("ORDER_STATUS",order.status))
                ///                     ,new System.Data.SqlClient.SqlParameter("@ORDER_TICKET_COUNT",dbType:System.Data.SqlDbType.Int,size:order.ticketCount.Length,direction:System.Data.ParameterDirection.Input,isNullable:false,precision:0,scale:0,sourceColumn:"TICKET_COUNT",sourceVersion:System.Data.DataRowVersion.Current,value:order.ticketCount)
                ///                     ,new System.Data.SqlClient.SqlParameter("@ORDER_USE_TIME",order.visitDate)
                ///                     ,new System.Data.SqlClient.SqlParameter("@PERSONS",personsInfoStr)
				///    });
                /// #endregion
				/// </code>
                /// </example>
                public void ExecuteProcedure(string storedProcName, IDataParameter[] parameters)
                {
                    Connection.Open();
                    SqlCommand command;
                    command = BuildQueryCommand(storedProcName, parameters);
                    command.ExecuteNonQuery();
                    Connection.Close();
                }

                /// <summary>
                /// 执行存储过程，返回执行操作影响的行数目
                /// </summary>
                /// <param name="storedProcName"></param>
                /// <param name="parameters"></param>
                /// <param name="rowsAffected"></param>
                /// <returns></returns>
                public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
                {
                    int result;
                    Connection.Open();
                    SqlCommand command = BuildCommand(storedProcName, parameters);
                    rowsAffected = command.ExecuteNonQuery();
                    result = (int)command.Parameters["ReturnValue"].Value;
                    Connection.Close();
                    return result;
                }

                /// <summary>
                /// 重载 RunProcedure 把执行存储过程的结果放在 SqlDataReader 中
                /// </summary>
                /// <param name="storedProcName"></param>
                /// <param name="parameters"></param>
                /// <returns></returns>
                public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
                {
                    SqlDataReader returnReader;
                    SqlCommand command = BuildQueryCommand(storedProcName, parameters);
                    command.Connection = Connection; // added int 20150105
                    command.Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    return returnReader;
                }

                /// <summary>
                /// 重载 RunProcedure 把执行存储过程的结果存储在 DataSet 中和表 tableName 为可选参数
                /// </summary>
                /// <param name="storedProcName"></param>
                /// <param name="parameters"></param>
                /// <param name="tableName"></param>
                /// <returns></returns>
                public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, params string[] tableName)
                {
                    DataSet dataSet = new DataSet();
                    Connection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = BuildQueryCommand(storedProcName, parameters);
                    string flag;
                    flag = "";
                    for (int i = 0; i < tableName.Length; i++)
                    {
                        flag = tableName[i];
                    }
                    if (flag != "")
                    {
                        sqlDA.Fill(dataSet, tableName[0]);
                        foreach (DataTable t in dataSet.Tables)
                        {
                            int tableIndex = dataSet.Tables.IndexOf(t);
                            if (tableIndex < tableName.Length)
                            {
                                t.TableName = tableName[tableIndex] ?? t.TableName;
                            }
                            else { }
                        }
                    }
                    else
                    {
                        sqlDA.Fill(dataSet);
                    }
                    Connection.Close();
                    return dataSet;
                }

                /// <summary>
                /// 执行 SQL 语句，返回数据到 DataSet 中
                /// </summary>
                /// <param name="sql"></param>
                /// <param name="tableName"></param>
                /// <returns></returns>
                public DataSet ReturnDataSet(string sql, string tableName = "table")
                {
                    DataSet dataSet = new DataSet();
                    Connection.Open();
                    SqlDataAdapter sqlDA = new SqlDataAdapter(sql, Connection);
                    sqlDA.Fill(dataSet, tableName);
                    Connection.Close();
                    return dataSet;
                }

                /// <summary>
                /// 执行 SQL 语句，返回 DataReader
                /// </summary>
                /// <param name="sql"></param>
                /// <returns></returns>
                public SqlDataReader ReturnDataReader(String sql)
                {
                    Connection.Open();
                    SqlCommand command = new SqlCommand(sql, Connection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    return dataReader;
                }

                /// <summary>
                /// 执行 SQL 语句，返回记录数
                /// </summary>
                /// <param name="sql"></param>
                /// <returns></returns>
                public int ReturnRecordCount(string sql)
                {
                    int recordCount = 0;
                    Connection.Open();
                    SqlCommand command = new SqlCommand(sql, Connection);
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        recordCount++;
                    }
                    dataReader.Close();
                    Connection.Close();
                    return recordCount;
                }

                /// <summary>
                /// 执行 SQL 语句
                /// </summary>
                /// <param name="sql"></param>
                /// <returns></returns>
                public bool EditDatabase(string sql)
                {
                    bool successState = false;
                    Connection.Open();
                    SqlTransaction myTrans = Connection.BeginTransaction();
                    SqlCommand command = new SqlCommand(sql, Connection, myTrans);
                    try
                    {
                        command.ExecuteNonQuery();
                        myTrans.Commit();
                        successState = true;
                    }
                    catch
                    {
                        myTrans.Rollback();
                    }
                    finally
                    {
                        Connection.Close();
                    }
                    return successState;
                }

                /// <summary>
                /// 关闭数据库联接
                /// </summary>
                public void Close()
                {
                    Connection.Close();
                }
            }//end class

        }
    }
}
/* vim: set si ts=4 sts=4 sw=4 fdm=indent : */
