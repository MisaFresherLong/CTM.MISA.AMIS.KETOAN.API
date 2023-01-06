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
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: PVLONG (26/12/2022)
        public string GetNewCode()
        {
            // Chuẩn bị câu lệnh sql
            string storedProcedure = "Proc_employee_GetBiggestCode";

            // Khởi tạo kết nối đến database
            string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
            using(var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Truy vấn database
                var biggestCode = mySqlConnection.QueryFirstOrDefault<string>(storedProcedure, commandType: System.Data.CommandType.StoredProcedure);

                return biggestCode;
            }
        }
    }
}
