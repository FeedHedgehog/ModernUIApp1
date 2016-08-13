using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ModernUI.Common.Utility
{
    public static class CSVHelper
    {
        #region Method

        /// <summary>
        /// 将数据导出为CSV文件
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <param name="strFilePath">CSV文件路径</param>
        /// <returns>是否导出成功</returns>
        public static bool DataTableToCSV(DataTable dt, string strFilePath)
        {
            try
            {
                FileStream fs = new FileStream(strFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                string data = string.Empty;
                //写出列名称
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    data += dt.Columns[i].Caption.ToString();
                    if (i < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);

                //写出各行数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data = string.Empty;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        data += dt.Rows[i][j].ToString();
                        if (j < dt.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="filePath">CSV文件的路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        public static DataTable CSVToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            string strLine = string.Empty;
            //记录每行记录中的各字段内容
            string[] aryLine;
            //表示列数
            int columnCount = 0;
            //标识 是否是读取的第一行
            bool isFirst = true;
            //逐行读取CSV文件中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (isFirst)
                {
                    isFirst = false;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(aryLine[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            sr.Close();
            fs.Close();
            return dt;
        }

        #endregion
    }
}
