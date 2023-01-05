using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KETOAN.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Bản ghi cần lấy</returns>
        /// Created by: PVLONG (26/12/2022)
        public T GetRecordByID(Guid recordID)
        {
            string className = typeof(T).Name;

            // Chuẩn bị câu lệnh sql
            string storedProcedure = $"Proc_{className}_GetByID";

            // Chuẩn bị dữ liệu đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{className}ID", recordID);

            // Khởi tạo kết nối đến database
            string connectionString = "Server=localhost;Port=3305;Database=misa.web11.ctm.pvlong;Uid=root;Pwd=Gnolneih;";
            var mySqlConnection = new MySqlConnection(connectionString);

            // Truy vấn database
            var record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

            return record;
        }
    }
}
