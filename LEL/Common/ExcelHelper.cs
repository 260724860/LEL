using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Common
{
    public class ExcelHelper
    {

        public static DataTable DataReaderExcelFile(string filePath, string SheetName)
        {
            #region//初始化信息  
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook hssfworkbook = new XSSFWorkbook(file);
                    ISheet sheet;
                    if (string.IsNullOrEmpty(SheetName))
                    {
                         sheet = hssfworkbook.GetSheet(SheetName);
                    }
                   else
                    {
                         sheet = hssfworkbook.GetSheetAt(0);
                    }

                    //sheet.ShiftRows(1, sheet.LastRowNum, -1);
                    //sheet.RemoveRow(sheet.GetRow(sheet.LastRowNum));

                    DataTable dt = new DataTable();
                    for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
                    {
                        dt.Columns.Add(sheet.GetRow(0).GetCell(j).ToString());
                    }
                    // dt.Columns.AddRange(columns);
                    for (int k = 0; k <= sheet.LastRowNum; k++)
                    {
                        IRow row = sheet.GetRow(k);
                        if (row.RowNum > 0)
                        {
                            sheet.AutoSizeColumn(k, true);
                            DataRow dr = dt.NewRow();
                            bool IsNotEmptyFlag = false;
                            for (int i = 0; i < row.LastCellNum; i++)
                            {
                                IsNotEmptyFlag = false;
                                ICell cell = row.GetCell(i);
                                if (cell == null)
                                {
                                    IsNotEmptyFlag = IsNotEmptyFlag || false;
                                    dr[i] = null;
                                }
                                else
                                {
                                    if (cell.CellType == CellType.Numeric)
                                    {
                                        if (DateUtil.IsCellDateFormatted(cell) && !string.IsNullOrEmpty(cell.ToString()))
                                            dr[i] = cell.DateCellValue.ToString("yyyy-MM-dd");
                                        else
                                            dr[i] = cell.ToString();
                                        IsNotEmptyFlag = IsNotEmptyFlag || true;
                                    }
                                    else
                                    {
                                        if (cell.ToString() != "")
                                        {
                                            dr[i] = cell.ToString();
                                            IsNotEmptyFlag = IsNotEmptyFlag || true;
                                        }
                                    }

                                }
                            }
                            if (IsNotEmptyFlag)
                            {
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                    hssfworkbook.Close();
                    file.Close();
                    return dt;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion


        }

        public static XSSFWorkbook BuildWorkbook(DataTable dt, string SheetName)
        {
            var book = new XSSFWorkbook();
            ISheet sheet = book.CreateSheet(SheetName);
            //Data Columns
            IRow crow = sheet.CreateRow(0);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = crow.CreateCell(j, CellType.String);
                cell.SetCellValue(dt.Columns[j].ColumnName);
            }

            //Data Rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow drow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = drow.CreateCell(j, CellType.String);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            //自动列宽
            for (int i = 0; i <= dt.Columns.Count; i++)
                sheet.AutoSizeColumn(i, true);

            return book;
        }


        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public static int DataTableToExcel(string fileName, List<DtoImportExcel> List, bool isColumnWritten, string savePath)
        {
            XSSFWorkbook workbook = null;
            //HSSFWorkbook workbook=null;
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            //HSSFWorkbook workbook;
            if (!string.IsNullOrEmpty(fileName))
            {
              using (FileStream stream = File.OpenRead(fileName))
                workbook = new XSSFWorkbook(stream);
            }
            try
            {
                foreach (var model in List)
                {
                    if (workbook != null)
                    {
                        sheet = workbook.CreateSheet(model.SheetNmae);

                    }
                    else
                    {
                        workbook = BuildWorkbook(model.dt,model.SheetNmae);
                        sheet = workbook.GetSheet(model.SheetNmae);
                        //return -1;
                    }

                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        IRow row = sheet.CreateRow(0);
                        for (j = 0; j < model.dt.Columns.Count; ++j)
                        {

                            row.CreateCell(j).SetCellValue(model.dt.Columns[j].ColumnName);
                            sheet.SetColumnWidth(j, 7000);
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    for (i = 0; i < model.dt.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < model.dt.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(model.dt.Rows[i][j].ToString());
                        }
                        ++count;
                    }
                }
                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    workbook.Write(fs);
                //fs.Flush();
                //workbook.Write(fs); //写入到excel

                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

    }

    public class DtoImportExcel
    {
        /// <summary>
        /// 数据表
        /// </summary>
        public DataTable dt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SheetNmae { get; set; }
    }
}
