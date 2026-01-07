using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macreel_Software.Models.Master
{
    public class MasterData
    {

    }

    public class role
    {
        public int id { get; set; }
        public string rolename { get; set; }
    }
    public class department
    {
        public int id { get; set; }
        public string departmentName { get; set; }
    }

    public class designation
    {
        public int id { get; set; }
        public string designationName { get; set; }
    }

    public class ReportingManger
    {
        public int id { get; set; }
        public string ReportingManagerName { get; set; }
    }
}
