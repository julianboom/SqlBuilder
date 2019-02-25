using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using Model;

namespace TableBuilderDateBase
{
    public static class DateBase
    {
        /// <summary>
        /// 用于获取数据库连接对象
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection getConnection()
        {
            string mysqlconfig = ConfigurationManager.ConnectionStrings["Mysql"].ToString();
            MySqlConnection conn = new MySqlConnection(mysqlconfig);
            return conn;
        }
        /// <summary>
        /// 用于测试数据库是否能正确连接
        /// </summary>
        /// <returns></returns>
        public static bool testConnection()
        {
            MySqlConnection conn = getConnection();
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException e)
            {
                return false;
            }

        }
    }
}
