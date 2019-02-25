using Aspose.Cells;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using TableBuilder;

namespace Common
{


    public  class TableFactory 
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


        public string sqlBuilder(string path, string tableName)
        {
                sys_dicService sys_dicservice = new sys_dicService();
                string temp = path.Substring(0, path.LastIndexOf('\\') );
                string filePath = temp;// 生成mysql脚本要保存在根目录下
                string fileName = path;//要读取的文件完整路径

                string fileExtension = ".sql";//文件扩展名
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);//文件名前缀

                string saveName = fileNameWithoutExtension + fileExtension; //保存文件名称
                                                                            //这里要写一个创建文件的判断，1/22
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                if (!File.Exists(filePath+"\\"+saveName))
                {
                    File.Create(Path.Combine(filePath, saveName)).Close();
                    //File.Create(filePath + "\\" + saveName);
                }
                else
                {
                     File.Delete(filePath + "\\" + saveName);
                     File.Create(filePath + "\\" + saveName).Close();
                }


                FileStream stream = File.Open(fileName, FileMode.Open);//获取文件对象
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
                stream.Close();

                Workbook workbook = new Workbook();
                workbook.Open(fileName);
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



                #region 制作数据表
                string init = "DROP TABLE IF EXISTS `"+ tableName + "`;CREATE TABLE `" + tableName + "` ( `ID` char(36) NOT NULL, `HandleDept` char(36) DEFAULT NULL,`HandleStaff` char(36) DEFAULT NULL,`PersonID` char(36) DEFAULT NULL,";//脚本文件开头初始化
                IEnumerable<sys_dic> diclist = null;//定义枚举类型，用于接收字典表的里的记录
                diclist = sys_dicservice.getsysdic(tableName);//获取字典表list
                List<int> arraylist = new List<int>();//新建一个集合用于记录表格新增的字段，增强系统容错

                for (int i = 0; i < array.GetLength(1); i++)//遍历列
                {
                    Boolean flag = true;//定义一个标志
                    foreach (sys_dic dic in diclist)//遍历字段表里的记录
                    {
                        if (array[2, i] == dic.name_cn)//如果表格标题等于字典表里的name_cn对应
                        {
                            string temp1 = "`" + dic.name_en + "`varchar(100) DEFAULT '' COMMENT '" + dic.name_cn + "',\r\n";
                            init += temp1;//拼接sql脚本
                            flag = false;
                            break;
                        }

                    }
                    if (flag)
                    {
                        arraylist.Add(i);//如果字典表里没有该字段，那么将该列记录暂存。
                        Console.WriteLine("字典缺少关于中文" + array[2, i] + "。");
                    }
                }

                string end = "PRIMARY KEY (`ID`),KEY `PersonName` (`PersonName`) USING BTREE,KEY `PersonID` (`PersonID`) USING BTREE) ENGINE = InnoDB DEFAULT CHARSET = utf8; ";//SQL 结尾，设置主键、编码、引擎
                string resultsql = init + end;

                StreamWriter sw = File.AppendText(filePath + "\\" + saveName);//开始写出sql脚本
                sw.Write(resultsql);
                sw.Flush();
                sw.Close();//关闭输出流
            #endregion

                string msg = "脚本制作成功，已为您保存至" + filePath + "\r\n\r\n------------------------------------\r\n\r\n\r\n";
                string lackof = "当前案管内容已更新,以下是目前缺少的字段：\r\n\r\n";
                if(arraylist !=null)
                 {
                    foreach (int i in arraylist)
                    {
                        lackof += array[2, i]+"\r\n";
                    }
                 string showmsg = msg + lackof;
                 return showmsg;
                 }

                else
                {
                    return msg;
                }
        }
    }
}





