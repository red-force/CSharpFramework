extern alias rf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RF
{
    public partial class GlobalClass
    {
        public partial class Excel
        {
            public Excel()
            {
                this.package = new rf.OfficeOpenXml.ExcelPackage();
                hssfworkbook = new rf.NPOI.HSSF.UserModel.HSSFWorkbook();
            }

            public Excel(rf.OfficeOpenXml.ExcelPackage excelPackage)
            {
                this.suffix = Suffix.xlsx;
                this.package = excelPackage;
            }

            /// <summary>
            /// init Excel by passing fileInfo
            /// </summary>
            /// <param name="fileInfo"></param>
            public Excel(System.IO.FileInfo fileInfo)
            {
                this.fileInfo = fileInfo;
                this.package = new rf.OfficeOpenXml.ExcelPackage(fileInfo);
            }

            public Excel(rf.NPOI.HSSF.UserModel.HSSFWorkbook workbook)
            {
                hssfworkbook = workbook;

                ////create a entry of DocumentSummaryInformation
                rf.NPOI.HPSF.DocumentSummaryInformation dsi = rf.NPOI.HPSF.PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "rf.NPOI Team";
                hssfworkbook.DocumentSummaryInformation = dsi;

                ////create a entry of SummaryInformation
                rf.NPOI.HPSF.SummaryInformation si = rf.NPOI.HPSF.PropertySetFactory.CreateSummaryInformation();
                si.Subject = "rf.NPOI SDK Example";
                hssfworkbook.SummaryInformation = si;

            }

            public rf.OfficeOpenXml.ExcelPackage package { get; set; }

            public System.IO.FileInfo fileInfo { get; set; }

            #region createWorksheet

            /// <summary>
            /// Creat Worksheet of Excel for Table Data
            /// </summary>
            /// <param name="name">the name of the worksheet</param>
            /// <param name="worksheetTemplate">the .xlsx support template</param>
            /// <param name="headers">the header of the table to be shown in excel</param>
            /// <param name="rowsData">the data of the table</param>
            /// <param name="suffix">the suffix of the excel, which is used to define the type of the excel.</param>
            /// <returns>rf.OfficeOpenXml.ExcelWorksheet/rf.NPOI.SS.UserModel.ISheet/null</returns>
            /// <example>
            /// <code language="C#" title="create .xlsx excel">
            /// RF.GlobalClass.Excel excel = new RF.GlobalClass.Excel();
            /// excel.createWorksheet(name: "LuckDrawData", headers: new string[] { "NO", "scoreCardCode", "scoreCardOwnerName", "scoreTimes", "Item" }, rowsData: result.data["TableItemList"], suffix:RF.GlobalClass.Excel.suffix.xlsx);
            /// RF.GlobalClass.WebForm.Excel.saveAs(excel: excel, Response: Response, name: "LuckDrawData" + RF.GlobalClass.Utils.DateTime.GetDateTimeString(DateTime.Now), suffix: RF.GlobalClass.Excel.suffix.xlsx);
            /// </code>
            /// </example>
            /// <example>
            /// <code language="C#" title="create .xls excel"> 
            /// RF.GlobalClass.Excel excel = new RF.GlobalClass.Excel();
            /// excel.createWorksheet(name: "LuckDrawData", headers: new string[] { "NO", "scoreCardCode", "scoreCardOwnerName", "scoreTimes", "Item" }, rowsData: result.data["TableItemList"], suffix:RF.GlobalClass.Excel.suffix.xls);
            /// RF.GlobalClass.WebForm.Excel.saveAs(excel: excel, Response: Response, name: "LuckDrawData" + RF.GlobalClass.Utils.DateTime.GetDateTimeString(DateTime.Now), suffix: RF.GlobalClass.Excel.suffix.xls);
            /// </code>
            /// </example>
            public object createWorksheet(string name = defaultWorksheetName,  string[] headers = null, List<Dictionary<string, string>> rowsData = null, Excel.Suffix suffix = Excel.Suffix.xls)
            {
                rf.OfficeOpenXml.ExcelWorksheet worksheetTemplate = null;
                try
                {
                    suffix = Suffix.unknow == suffix ? this.suffix : suffix;
                    switch (suffix)
                    {
                        case Excel.Suffix.xls:
                            #region fillWorksheet
                            try
                            {
                                name = name + ".xls";
                                rf.NPOI.SS.UserModel.ISheet sheet = hssfworkbook.CreateSheet(name);
                                return fillWorksheet(sheet: sheet, headers: headers, rowsData: rowsData);
                            }
                            catch (Exception ex)
                            {
                            }
                            #endregion
                            break;
                        case Excel.Suffix.xlsx:
                            #region fillWorksheet
                            try
                            {
                                rf.OfficeOpenXml.ExcelWorksheet worksheet;
                                name = name + ".xlsx";
                                if (null != worksheetTemplate)
                                {
                                    worksheet = package.Workbook.Worksheets.Add(name, worksheetTemplate);
                                }
                                else
                                {
                                    worksheet = package.Workbook.Worksheets.Add(name);
                                }
                                return fillWorksheet(worksheet, headers: headers, rowsData: rowsData);
                            }
                            catch (Exception ex)
                            {
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }
                    this.suffix = suffix;
                }
                catch (Exception ex) { }
                return null;
            }

            /// <summary>
            /// Creat Worksheet of Excel for Table Data
            /// </summary>
            /// <param name="name">the name of the worksheet</param>
            /// <param name="worksheetTemplate">the .xlsx support template</param>
            /// <param name="headers">the header of the table to be shown in excel</param>
            /// <param name="rowsData">the data of the table</param>
            /// <returns>rf.OfficeOpenXml.ExcelWorksheet</returns>
            public rf.OfficeOpenXml.ExcelWorksheet createWorksheet(string name = defaultWorksheetName, rf.OfficeOpenXml.ExcelWorksheet worksheetTemplate = null, string[] headers = null, List<Dictionary<string, object>> rowsData = null)
            {
                rf.OfficeOpenXml.ExcelWorksheet worksheet;
                name = name + ".xlsx";
                if (null != worksheetTemplate)
                {
                    worksheet = package.Workbook.Worksheets.Add(name, worksheetTemplate);
                }
                else
                {
                    worksheet = package.Workbook.Worksheets.Add(name);
                }
                this.suffix = Suffix.xlsx;
                return fillWorksheet(worksheet, headers: headers, rowsData: rowsData);
            }

            private rf.OfficeOpenXml.ExcelWorksheet createWorksheet(string name = defaultWorksheetName, rf.OfficeOpenXml.ExcelWorksheet worksheetTemplate = null, string[] headers = null, List<Dictionary<string, string>> rowsData = null)
            {
                rf.OfficeOpenXml.ExcelWorksheet worksheet;
                if (null != worksheetTemplate)
                {
                    worksheet = package.Workbook.Worksheets.Add(name, worksheetTemplate);
                }
                else
                {
                    worksheet = package.Workbook.Worksheets.Add(name);
                }
                return fillWorksheet(worksheet, headers: headers, rowsData: rowsData);
            }

            private rf.NPOI.SS.UserModel.ISheet createWorksheet(string name = defaultWorksheetName, string[] headers = null, List<Dictionary<string, string>> rowsData = null)
            {
                rf.NPOI.SS.UserModel.ISheet sheet = hssfworkbook.CreateSheet(name);

                #region comment
                // sheet.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
                //int x = 1;
                //for (int i = 1; i <= 15; i++)
                //{
                //    rf.NPOI.SS.UserModel.IRow row = sheet.CreateRow(i);
                //    for (int j = 0; j < 15; j++)
                //    {
                //        row.CreateCell(j).SetCellValue(x++);
                //    }
                //}
                #endregion

                return fillWorksheet(sheet: sheet, headers: headers, rowsData: rowsData);
            }
            #endregion

            #region fillWorksheet

            /// <summary>
            /// Fill Worksheet of .xlsx excel with data
            /// </summary>
            /// <param name="worksheet">the worksheet to be filled</param>
            /// <param name="headers">the headers to fill</param>
            /// <param name="rowsData">the row data to fill</param>
            /// <returns>rf.OfficeOpenXml.ExcelWorksheet</returns>
            public rf.OfficeOpenXml.ExcelWorksheet fillWorksheet(rf.OfficeOpenXml.ExcelWorksheet worksheet, string[] headers = null, List<Dictionary<string, object>> rowsData = null)
            {
                // add header
                headers.Select(delegate(string _value, int _idx)
                {
                    return worksheet.Cells[1, (_idx + 1)].Value = _value;
                }).ToArray();

                // add rows
                rowsData.Select(delegate(Dictionary<string, object> _rowData, int _rowIndex)
                {
                    return _rowData.Select(delegate(KeyValuePair<string, object> _kvp, int _colIndex)
                    {
                        worksheet.Cells[_kvp.Key].Value = _kvp.Value;
                        return worksheet.Cells[_kvp.Key];
                    }).ToArray();
                }).ToArray();

                //There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                //For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                //you want to use the result of a formula in your program.
                // worksheet.Calculate();

                worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                return worksheet;
            }

            /// <summary>
            /// Fill Worksheet of .xlsx excel with data
            /// </summary>
            /// <param name="worksheet">the worksheet to be filled</param>
            /// <param name="headers">the headers to fill</param>
            /// <param name="rowsData">the row data to fill</param>
            /// <returns>rf.OfficeOpenXml.ExcelWorksheet</returns>
            public rf.OfficeOpenXml.ExcelWorksheet fillWorksheet(rf.OfficeOpenXml.ExcelWorksheet worksheet, string[] headers = null, List<Dictionary<string, string>> rowsData = null)
            {
                // add header
                headers.Select(delegate(string _value, int _idx)
                {
                    return worksheet.Cells[1, (_idx + 1)].Value = _value;
                }).ToArray();

                // add rows
                rowsData.Select(delegate(Dictionary<string, string> _rowData, int _rowIndex)
                {
                    return _rowData.Select(delegate(KeyValuePair<string, string> _kvp, int _colIndex)
                    {
                        worksheet.Cells[_kvp.Key].Value = _kvp.Value;
                        return worksheet.Cells[_kvp.Key];
                    }).ToArray();
                }).ToArray();

                //There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                //For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                //you want to use the result of a formula in your program.
                //worksheet.Calculate();
                

                worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                return worksheet;
            }

            /// <summary>
            /// Fill sheet of .xls excel with data
            /// </summary>
            /// <param name="sheet">the work sheet to be filled</param>
            /// <param name="headers">the headers to fill</param>
            /// <param name="rowsData">the row data to fill</param>
            /// <returns>rf.NPOI.SS.UserModel.ISheet</returns>
            public rf.NPOI.SS.UserModel.ISheet fillWorksheet(rf.NPOI.SS.UserModel.ISheet sheet, string[] headers = null, List<Dictionary<string, string>> rowsData = null)
            {
                // add header
                rf.NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
                headers.Select<string,rf.NPOI.SS.UserModel.IRow>(delegate(string _value, int _idx)
                {
                    row.CreateCell(_idx).SetCellValue(_value);
                    return row;
                }).ToArray();

                // add rows
                rowsData.Select(delegate(Dictionary<string, string> _rowData, int _rowIndex)
                {
                    row = sheet.CreateRow(_rowIndex + 1);
                    return _rowData.Select(delegate(KeyValuePair<string, string> _kvp, int _colIndex)
                    {
                        row.CreateCell(_colIndex).SetCellValue(_kvp.Value);
                        return row;
                    }).ToArray();
                }).ToArray();
                return sheet;
            }

            #endregion

            #region styleWorksheet
            public rf.OfficeOpenXml.ExcelWorksheet styleWorksheet(rf.OfficeOpenXml.ExcelWorksheet worksheet)
            {
                // lets set the header text 
                worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" " + rf.OfficeOpenXml.ExcelHeaderFooter.SheetName;
                // add the page number to the footer plus the total number of pages
                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                    string.Format("Page {0} of {1}", rf.OfficeOpenXml.ExcelHeaderFooter.PageNumber, rf.OfficeOpenXml.ExcelHeaderFooter.NumberOfPages);
                // add the sheet name to the footer
                worksheet.HeaderFooter.OddFooter.CenteredText = rf.OfficeOpenXml.ExcelHeaderFooter.SheetName;
                // add the file path to the footer
                worksheet.HeaderFooter.OddFooter.LeftAlignedText = rf.OfficeOpenXml.ExcelHeaderFooter.FilePath + rf.OfficeOpenXml.ExcelHeaderFooter.FileName;

                worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:1"];
                //worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                // Change the sheet view to show it in page layout mode
                worksheet.View.PageLayoutView = true;
                return worksheet;
            }
            #endregion

            #region getWorksheet
            public rf.OfficeOpenXml.ExcelWorksheet getWorksheet(int index)
            {
                return package.Workbook.Worksheets[index] ?? package.Workbook.Worksheets.Add(defaultWorksheetName);
            }
            public rf.OfficeOpenXml.ExcelWorksheet getWorksheet(string name = defaultWorksheetName)
            {
                return package.Workbook.Worksheets[name] ?? package.Workbook.Worksheets.Add(name);
            }
            #endregion

            #region deleteWorksheet
            public void deleteWorksheet(string name = defaultWorksheetName)
            {
                package.Workbook.Worksheets.Delete(name);
            }

            public void deleteWorksheet(int index)
            {
                package.Workbook.Worksheets.Delete(index);
            }

            public void deleteWorksheet(rf.OfficeOpenXml.ExcelWorksheet worksheet)
            {
                package.Workbook.Worksheets.Delete(worksheet);
            }
            #endregion

            #region setWorkbookProperties
            /// <summary>
            /// Set the properties of the workbook
            /// </summary>
            /// <param name="title">the title of the workbook</param>
            /// <param name="author">the author of the workbook</param>
            /// <param name="comments">the comments of the workbook</param>
            /// <param name="company">the company of the workbook</param>
            /// <param name="customProperties">the custom properties</param>
            /// <param name="suffix">program will auto get the suffix of the excel. Usually you do NOT have to specify it. </param>
            /// <returns>rf.NPOI.HSSF.UserModel.HSSFWorkbook/rf.OfficeOpenXml.ExcelWorkbook</returns>
            public object setWorkbookProperties(string title = defaultWorkbookTitle, string author = defaultWorkbookAuthor, string comments = defaultWorkbookComments, string company = defaultWorkbookCompany, Dictionary<string, object> customProperties = null, Suffix suffix = Suffix.unknow)
            {
                suffix = Suffix.unknow == suffix ? this.suffix : suffix;
                switch (suffix)
                {
                    case Suffix.xls:
                        #region set properties
                        try
                        {
                            // set some document properties

                            hssfworkbook.SummaryInformation.Title = title;
                            hssfworkbook.SummaryInformation.Author = author;
                            hssfworkbook.SummaryInformation.Comments = comments;

                            // set some extended property values
                            hssfworkbook.DocumentSummaryInformation.Company = company;
                            customProperties.Select(delegate(KeyValuePair<string, object> _kvp, int _index)
                            {
                                hssfworkbook.DocumentSummaryInformation.CustomProperties.Add(_kvp.Key, _kvp.Value);
                                return _kvp;
                            }).ToArray();

                            return hssfworkbook;
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion
                        break;
                    case Suffix.xlsx:
                        #region set properties
                        try
                        {
                            // set some document properties
                            package.Workbook.Properties.Title = title;
                            package.Workbook.Properties.Author = author;
                            package.Workbook.Properties.Comments = comments;

                            // set some extended property values
                            package.Workbook.Properties.Company = company;

                            // set some custom property values
                            customProperties.Select(delegate(KeyValuePair<string, object> _kvp, int _index)
                            {
                                package.Workbook.Properties.SetCustomPropertyValue(_kvp.Key, _kvp.Value);
                                return _kvp;
                            }).ToArray();

                            return package.Workbook;
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion
                        break;
                    default: break;
                }
                return null;
            }
            #endregion

            #region save Package
            public void svae(string password = null)
            {
                this.package.Save(password: password);
            }
            #endregion

            #region save package as
            public void saveAs(System.IO.FileInfo fileInfo, string password = null)
            {
                this.package.SaveAs(fileInfo, password: password);
            }
            #endregion


            #region more


            #region Add Comments to Excel Cell
            /// <summary>
            /// <seealso cref="http://zeeshanumardotnet.blogspot.com/2011/06/creating-reports-in-excel-2007-using.html"/>
            /// </summary>
            /// <param name="ws"></param>
            /// <param name="colIndex"></param>
            /// <param name="rowIndex"></param>
            /// <param name="comment"></param>
            /// <param name="author"></param>
            private static void AddComment(rf.OfficeOpenXml.ExcelWorksheet ws, int colIndex, int rowIndex, string comment, string author)
            {
                //Adding a comment to a Cell
                var commentCell = ws.Cells[rowIndex, colIndex];
                commentCell.AddComment(comment, author);
            }
            #endregion

            #region Add Image in Excel Sheet
            private static void AddImage(rf.OfficeOpenXml.ExcelWorksheet ws, int columnIndex, int rowIndex, string filePath)
            {
                //How to Add a Image using EP Plus
                System.Drawing.Bitmap image = new System.Drawing.Bitmap(filePath);
                rf.OfficeOpenXml.Drawing.ExcelPicture picture = null;
                if (image != null)
                {
                    picture = ws.Drawings.AddPicture("pic" + rowIndex.ToString() + columnIndex.ToString(), image);
                    picture.From.Column = columnIndex;
                    picture.From.Row = rowIndex;
                    picture.From.ColumnOff = Pixel2MTU(2); //Two pixel space for better alignment
                    picture.From.RowOff = Pixel2MTU(2);//Two pixel space for better alignment
                    picture.SetSize(100, 100);
                }
            }
            #endregion

            #region Add Custom objects to Excel Sheet
            private static void AddCustomShape(rf.OfficeOpenXml.ExcelWorksheet ws, int colIndex, int rowIndex, eShapeStyle shapeStyle, string text)
            {
                rf.OfficeOpenXml.Drawing.ExcelShape shape = ws.Drawings.AddShape("cs" + rowIndex.ToString() + colIndex.ToString(), shapeStyle);
                shape.From.Column = colIndex;
                shape.From.Row = rowIndex;
                shape.From.ColumnOff = Pixel2MTU(5);
                shape.SetSize(100, 100);
                shape.RichText.Add(text);
            }
            #endregion

            private static int Pixel2MTU(int p)
            {
                return (p / 9525);
            }

            #endregion

            public const string defaultWorksheetName = "the default sheet";
            public const string defaultWorkbookTitle = "the default workbook";
            public const string defaultWorkbookAuthor = "WANG ZHI";
            public const string defaultWorkbookComments = "This document is default created by the author.";
            public const string defaultWorkbookCompany = "Hong Qi Chain Inc.";

            public NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook { get; set; }

            /// <summary>
            /// the result state of the method ValidateXMLWidthXSDResult
            /// </summary>
            /// <value>Failed</value>
            public enum Suffix
            {
                unknow = 0,
                /// <summary>
                /// Failed to pass the validation.
                /// </summary>
                xls = 1,
                /// <summary>
                /// Passed the validation.
                /// </summary>
                xlsx = 2
            };

            public Suffix suffix = Suffix.xls;
        }
    }
}
