using Dapper;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.DL
{
    public class DepartmentDL : BaseDL<Department>, IDepartmentDL
    {
        public DepartmentDL(IConnectionLayer connectionLayer) : base(connectionLayer)
        {
        }
    }
}
