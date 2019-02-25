using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Model;
using System.Data;
using MySql.Data.MySqlClient;
using Common;

namespace TableBuilder
{
    class sys_dicService
    {
        public  List<sys_dic> getsysdic(string tablename)
        {
            string sql = "select * from sys_dic where table_name = '"+tablename+"'";
            MySqlParameter[] parms = null;
            Helpers.MySqlHelper a = new Helpers.MySqlHelper();
            DataTable dt = a.ExecuteDataTable(sql, parms);
            List<sys_dic> list = new List<sys_dic>();
            list = DataTableExtend.ToDataList<sys_dic>(dt);
            return list;
        }
    }
}
