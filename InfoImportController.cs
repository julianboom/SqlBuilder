using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using IBLL;
using BLL;
using System.Diagnostics;
using System.Runtime.Serialization.Json;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;
using Aspose.Words;
using Aspose.Cells;



namespace Archive.Controllers
{
    

    public class InfoImportController : BaseController
    {


        #region 日期转换工具
        /// <summary>
        /// 自定义的日期转换工具
        /// </summary>
        /// <param name="date"></param>
        /// <returns>result</returns>
        public string MyDateConvert(string date)
        {
            if (date.IndexOf("/") > 1)
            {
                string[] dateArr = date.Split('/');
                string result = "";
                for (int i = 0; i < 3; i++)
                {
                    if (int.Parse(dateArr[i]) < 10)
                    {
                        dateArr[i] = "0" + dateArr[i];
                    }
                    result += dateArr[i] + "/";
                }
                result = result.Substring(0, result.Length - 1);
                return result;
            }
            else
            {
                return date;
            }

        }
        #endregion


        public ActionResult MyUpload(HttpPostedFileBase fileData, string guid, string folder)
        {
            Isys_dicService sys_dicservice = new sys_dicService();
            string tableName = "";
            if (fileData != null)
            {
                //try
                //{
                ControllerContext.HttpContext.Request.ContentEncoding = Encoding.GetEncoding("UTF-8");
                ControllerContext.HttpContext.Response.ContentEncoding = Encoding.GetEncoding("UTF-8");
                ControllerContext.HttpContext.Response.Charset = "UTF-8";

                if (fileData.FileName == "表格一.xls")
                {
                    tableName = "Temp_ClueInfo_History";
                }
                else if (fileData.FileName == "表格二.xls")
                {
                    tableName = "Temp_CaseInfo_History";
                }
                else
                {
                    tableName = folder;
                }

                // 文件上传后的保存路径
                string filePath = Server.MapPath("~/UploadFiles/");
                //DirectoryUtil.AssertDirExist(filePath);

                string fileName = Path.GetFileName(fileData.FileName);      //原始文件名称
                string fileExtension = Path.GetExtension(fileName);         //文件扩展名
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                string saveName = Guid.NewGuid().ToString() + fileExtension; //保存文件名称
                string logname = saveName + "/" + fileNameWithoutExtension;
                if (!System.IO.Directory.Exists(filePath))
                {
                    System.IO.Directory.CreateDirectory(filePath);
                }
                fileData.SaveAs(Path.Combine(filePath, saveName));

                System.IO.Stream stream = fileData.InputStream;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                stream.Close();

                Workbook workbook = new Workbook();
                workbook.Open(Path.Combine(filePath, saveName));
                Cells cells = workbook.Worksheets[0].Cells;

                String[,] array = new String[cells.MaxDataRow + 1, cells.MaxDataColumn + 1];
                for (int i = 0; i < cells.MaxDataRow + 1; i++)
                {
                    for (int j = 0; j < cells.MaxDataColumn + 1; j++)
                    {

                        string s = cells[i, j].StringValue.Trim();
                        array[i, j] = s;
                    }
                }



                #region 新方法
                Guid handleDept = (Guid)Session["HandleDept"];//拿到会话值
                Guid handleStaff = (Guid)Session["UserID"];
                Guid personID = new Guid("00000000-0000-0000-0000-000000000000");
                IEnumerable<sys_dic> diclist = null;//定义枚举类型，用于接收字典表的里的记录
                string cxsql = "Select * From sys_dic Where table_name = '"+ tableName+"'";
                string crsql = null;
                crsql = "REPLACE into " + tableName + " (id,handleDept,HandleStaff,PersonID,";//定义一个用于插入数据的全局sql字符串，用于拼接
                diclist = sys_dicservice.SqlQuery(cxsql);//将查询到的记录存到枚举里
                List<int> arraylist = new List<int>();//新建一个集合用于记录表格新增的字段，增强系统容错

                for (int i = 0; i < array.GetLength(1); i++)//遍历列
                {
                    Boolean flag = true;//定义一个标志
                    foreach (sys_dic dic in diclist)//遍历字段表里的记录
                    {
                        if (array[2, i] == dic.name_cn)//如果表格标题等于字典表里的name_cn对应
                        {
                            crsql += dic.name_en + ",";//编辑插入的sql语句，将标题对应的字段名加入到即将插入业务的sql中。
                            flag = false;
                            break;
                        }

                    }
                    if (flag)
                    {
                        arraylist.Add(i);//如果字段表里没有该字段，那么将该列记录暂存。
                    }
                }

                crsql = crsql.Substring(0, crsql.Length - 1) + ")";//删除sql尾部逗号
                string sql2 = "";
                for (int i = 3; i < array.GetLength(0); i++)//从表格第四行“值”开始遍历
                {
                    string valuessql = "";//定义sql语句的values部分
                    valuessql += "(uuid(),\"" + handleDept + "\",\"" + handleStaff + "\",\"" + personID + "\",";
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        Boolean flag = false;
                        foreach (int num in arraylist)//判断字典表是否有该字段，没有就跳过录入
                        {
                            if (j == num)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag) continue;

                        ////添加日期格式转换
                        //string a;
                        //if (array[2, j].IndexOf("时间") > 0)
                        //{
                        //    a = MyDateConvert(array[i, j]);
                        //    valuessql += "\'" + a + "\',";
                        //}
                        //else
                        //{
                        //    valuessql += "\'" + array[i, j] + "\',";
                        //}

                        valuessql += "\'" + array[i, j] + "\',";//不为空继续
                    }
                    valuessql = valuessql.Substring(0, valuessql.Length - 1);
                    valuessql += "),";
                    sql2 += valuessql;
                }

                sql2 = sql2.Substring(0, sql2.Length - 1);
                string sql3 = "; update "+tableName+"set handletype = REPLACE(CONCAT(Handle1Time,\",\",HandleType1First,\",\",HandleType1Second,\";\",Handle2Time,\",\",HandleType2First,\",\",HandleType2Second,\";\",Handle3Time,\",\",HandleType3First,\",\",HandleType3Second,\";\",Handle4Time,\",\",HandleType4First,\",\",HandleType4Second,\";\",Handle5Time,\",\",HandleType5First,\",\",HandleType5Second,\";\",Handle6Time,\",\",HandleType6First,\",\",HandleType6Second,\";\",Handle7Time,\",\",HandleType7First,\",\",HandleType7Second,\";\"),\",,;\",\"\")";
                string ss = crsql + " values" + sql2 ;//拼接要插入的字段和其对应的values
                sys_dicservice.SqlCommand(ss);//开始导入数据库
                #endregion

                return Content("true");
            }

            else
            {
                return Content("false");
            }
        }
    }
}




