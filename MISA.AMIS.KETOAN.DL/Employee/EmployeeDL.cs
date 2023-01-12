using Dapper;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.KETOAN.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        public EmployeeDL(IConnectionLayer connectionLayer) : base(connectionLayer)
        {
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Created by: PVLONG (26/12/2022)
        public string GetNewEmployeeCode()
        {
            string className = "employee";

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.GetBiggestCode, className);

            // Khởi tạo kết nối đến database
            string connectionString = DatabaseContext.ConnectionString;
            using (var connection = _connectionLayer.InitConnection(connectionString))
            {
                // Truy vấn database
                var biggestCode = _connectionLayer.QueryFirstOrDefault<string>(connection, storedProcedure, commandType: System.Data.CommandType.StoredProcedure);

                return biggestCode;
            }
        }

        /// <summary>
        /// Xóa nhiều nhân viên dựa theo danh sách ID
        /// </summary>
        /// <param name="listEmployeeIDs">Danh sách ID của các nhân viên cần xóa/param>
        /// <returns>Danh sách GUID của các nhân viên vừa được xóa</returns>
        /// Created by: PVLONG (26/12/2022)
        public int DeleteBatchEmployees(ListEmployeeIDs listEmployeeIDs)
        {
            return 0;
        }
    }
}
