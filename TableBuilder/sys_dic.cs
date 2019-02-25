//------------------------------------------------------------------------------
//字典表模型
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;

    public  class sys_dic
    {
        public int id { get; set; }
        public string db_name { get; set; }
        public string table_name { get; set; }
        public string name_en { get; set; }
        public string name_cn { get; set; }
    }
}
