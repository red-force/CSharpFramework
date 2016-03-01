using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF
{
    partial class GlobalClass
    {
        public partial class DB
        {
            /// <summary>
            /// Create Select SQL
            /// </summary>
            /// <param name="tableName">the table Name</param>
            /// <param name="columnNameValues">column name and value pair</param>
            /// <param name="columnNameTypes">column name and type pair</param>
            /// <param name="conditionTextValue">condition text value pair</param>
            /// <param name="pageNum">the number of page, start from 1</param>
            /// <param name="pageSize">the record size of per page</param>
            /// <returns></returns>
            /// <example >
            /// <code language="SQL" title="Supposed to be">
            /// SELECT * FROM [log] ORDER BY [id] OFFSET 10 ROWS FETCH NEXT 5 ROWS ONLY;
            /// </code>
            /// </example>
            public static string createSelectSQL(string tableName, Dictionary<string, string> columnNameValues, Dictionary<string, string> columnNameTypes = null, Dictionary<string, string> conditionTextValue = null, Dictionary<string, string> orderBy = null, Dictionary<string, string> paging = null)
            {
                string result = String.Empty;
                result = @"SELECT ";
                string selectStr = String.Empty;
                if (null != columnNameValues && columnNameValues.Count != 0)
                {
                    columnNameValues.Select(delegate(KeyValuePair<string, string> kvpss)
                    {
                        selectStr += @"[" + kvpss.Key + @"],";
                        return kvpss;
                    }).ToArray();
                }
                else
                {
                    selectStr = "*";
                }
                result += (@"" + selectStr.TrimEnd(','));
                result += @" FROM [" + tableName + "] ";

                #region condition
                if (null != conditionTextValue && 0 != conditionTextValue.Count)
                {
                    try
                    {
                        string conditionStr = String.Empty;
                        conditionTextValue.Select(delegate(KeyValuePair<string, string> kvpss)
                        {
                            string quoteSymbol = @"'";
                            #region set quoteSymbol
                            quoteSymbol = getQuoteSymbol(columnNameTypes: columnNameTypes, kvpss: kvpss);
                            #endregion
                            conditionStr += @"[" + kvpss.Key + @"] = " + quoteSymbol + kvpss.Value + quoteSymbol + @",";
                            return kvpss;
                        }).ToArray();
                        result += (@" WHERE " + conditionStr.TrimEnd(','));
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else { }
                #endregion

                #region paging
                int pageIndex = 0;
                int pageNum = 0;
                int pageSize = 0;
                if (null != orderBy && 0 < orderBy.Count)
                {
                    string orderByStr = "";
                    orderBy.Keys.Select(delegate(string name){
                        orderByStr += (@" [" + name + @"] " + orderBy[name] + @",");
                        return name;
                    }).ToArray();
                    result += (" ORDER BY " + orderByStr.TrimEnd(','));

                    #region paging
                    if (null != paging && 1 < paging.Count && (paging.Keys.Contains("PageIndex") || paging.Keys.Contains("PageNum")) && paging.Keys.Contains("PageSize"))
                    {
                        if ((int.TryParse(paging["PageNum"], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out pageNum) || int.TryParse(paging["PageIndex"], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out pageIndex)) && int.TryParse(paging["PageSize"], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out pageSize))
                        {
                            try
                            {
                                if (0 != pageNum)
                                {
                                    pageIndex = pageNum - 1;
                                }
                                else { }
                                result += @" OFFSET " + pageIndex * pageSize + @" ROWS FETCH NEXT " + pageSize + " ROWS ONLY ";
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        else { }
                    }
                    else { }
                    #endregion

                }else{}
                #endregion
                return result;
            }

            /// <summary>
            /// Create Insert SQL
            /// </summary>
            /// <param name="tableName">Table Name</param>
            /// <param name="columnNameValues">column name and value pair</param>
            /// <param name="columnNameTypes">column name and type pair</param>
            /// <returns>Insert SQL</returns>
            public static string createInsertSQL(string tableName, Dictionary<string, string> columnNameValues, Dictionary<string, string> columnNameTypes = null)
            {
                string result = String.Empty;
                if (columnNameValues.Count != 0)
                {
                    result = @"INSERT INTO [" + tableName + @"] ";
                    string columnNames = @"(";
                    string columnValues = @"(";
                    columnNameValues.Select(delegate(KeyValuePair<string, string> kvpss)
                    {
                        string quoteSymbol = @"'";
                        #region set quoteSymbol
                        quoteSymbol = getQuoteSymbol(columnNameTypes: columnNameTypes, kvpss: kvpss);
                        #endregion
                        columnNames += @"[" + kvpss.Key + @"],";
                        columnValues += @"" + quoteSymbol + kvpss.Value + quoteSymbol + @",";
                        return kvpss;
                    }).ToArray();

                    result += columnNames.TrimEnd(',') + @") VALUES " + columnValues.TrimEnd(',') + @")";
                }
                else { }
                return result;
            }

            /// <summary>
            /// Create Update SQL
            /// </summary>
            /// <param name="tableName">the table Name</param>
            /// <param name="columnNameValues">column name and value pair</param>
            /// <param name="columnNameTypes">column name and type pair</param>
            /// <param name="conditionTextValue">condition text value pair</param>
            /// <returns></returns>
            public static string createUpdateSQL(string tableName, Dictionary<string, string> columnNameValues, Dictionary<string, string> columnNameTypes = null, Dictionary<string, string> conditionTextValue = null)
            {
                string result = String.Empty;
                if (null != columnNameValues && columnNameValues.Count != 0)
                {
                    result = @"UPDATE [" + tableName + @"] ";
                    string setStr = String.Empty;
                    columnNameValues.Select(delegate(KeyValuePair<string, string> kvpss)
                    {
                        string quoteSymbol = @"'";
                        #region set quoteSymbol
                        quoteSymbol = getQuoteSymbol(columnNameTypes: columnNameTypes, kvpss: kvpss);
                        #endregion
                        setStr += @"[" + kvpss.Key + @"]" + @" = " + quoteSymbol + kvpss.Value + quoteSymbol + @",";
                        return kvpss;
                    }).ToArray();
                    result += (@" SET " + setStr.TrimEnd(','));
                    #region condition
                    if (null != conditionTextValue && 0 != conditionTextValue.Count)
                    {
                        string conditionStr = String.Empty;
                        conditionTextValue.Select(delegate(KeyValuePair<string, string> kvpss)
                        {
                            string quoteSymbol = @"'";
                            #region set quoteSymbol
                            quoteSymbol = getQuoteSymbol(columnNameTypes: columnNameTypes, kvpss: kvpss);
                            #endregion
                            conditionStr += @"[" + kvpss.Key + @"] = " + quoteSymbol + kvpss.Value + quoteSymbol + @",";
                            return kvpss;
                        }).ToArray();
                        result += (@" WHERE " + conditionStr.TrimEnd(','));
                    }
                    else { }
                    #endregion
                }
                else { }
                return result;
            }

            /// <summary>
            /// Set the QuoteSymbol value according to the column type
            /// </summary>
            /// <param name="type">the type to identify</param>
            /// <param name="columnNameTypes">to get the type if type not filled</param>
            /// <param name="kvpss">to get the type if type not filled</param>
            /// <returns></returns>
            /// <example>
            ///  quoteSymbol = getQuoteSymbol(columnNameTypes: columnNameTypes, kvpss:kvpss);
            /// </example>
            private static string getQuoteSymbol(string type = null, Dictionary<string, string> columnNameTypes = null, KeyValuePair<string, string> kvpss = new KeyValuePair<string,string>())
            {
                string result = String.Empty;
                if (null != type)
                {
                }
                else if (null != columnNameTypes && columnNameTypes.ContainsKey(kvpss.Key))
                {
                    type = columnNameTypes[kvpss.Key].ToLower();
                }
                else { type = "default"; }
                switch (type)
                {
                    case "int":
                    case "bool":
                        result = @"";
                        break;
                    default:
                        result = @"'";
                        break;
                }
                return result;
            }
        }
    }
}
