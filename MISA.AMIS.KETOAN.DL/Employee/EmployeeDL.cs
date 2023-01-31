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
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: PVLONG (26/12/2022)
        public int DeleteBatchEmployees(ListEmployeeIDs listEmployeeIDs)
        {
            string className = "employee";

            // Chuẩn bị câu lệnh sql
            string storedProcedure = String.Format(StoredProcedure.DeleteBatch, className);

            // Chuẩn bị dữ liệu đầu vào
            var parameters = new DynamicParameters();
            string formatedEmployeeIDs = listEmployeeIDs.GetFormatedEmployeeIDs();
            parameters.Add("@EmployeeIDs", formatedEmployeeIDs);

            // Khởi tạo kết nối đến database
            string connectionString = DatabaseContext.ConnectionString;
            using (var connection = _connectionLayer.InitConnection(connectionString))
            {
                connection.Open();
                var trans = connection.BeginTransaction();
                try
                {
                    // Truy vấn database
                    var numberOfEffectedRow = _connectionLayer.Execute(connection, storedProcedure, parameters, trans, commandType: System.Data.CommandType.StoredProcedure);

                    if(numberOfEffectedRow != listEmployeeIDs.EmployeeIDs.Count)
                    {
                        // Nếu số dòng thực thi khác số dòng cần thực thi, rollback transaction
                        trans.Rollback();
                        return 0;
                    }
                    // Nếu thành công, commit transaction
                    trans.Commit();
                    return numberOfEffectedRow;
                }
                catch (Exception)
                {
                    // Nếu có lỗi xảy ra, rollback transaction
                    trans.Rollback();
                    return 0;
                }

                
            }
        }
    }
}
