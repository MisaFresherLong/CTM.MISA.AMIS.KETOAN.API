using Dapper;
using MISA.AMIS.KETOAN.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.KETOAN.DL
{
    public class FakeConnectionLayer : IConnectionLayer
    {

        #region Method

        /// <summary>
        /// Hàm khởi tạo kết nối database
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>Kết nối database</returns>
        /// Created by: PVLONG (10/01/2023)
        public IDbConnection InitConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Hàm giải phóng
        /// Created by: PVLONG (10/01/2023)
        /// </summary>
        public void Dispose()
        {
            
        }

        /// <summary>
        /// Hàm thực thi câu lệnh sql
        /// </summary>
        /// <param name="sql">Câu lệnh sql</param>
        /// <param name="param">Tham số của câu lệnh</param>
        /// <param name="transaction">Transaction của câu lệnh</param>
        /// <param name="commandTimeout">Thời gian tối đa có thể thực thi</param>
        /// <param name="commandType">Kiểu câu lệnh</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: PVLONG (10/01/2023)
        public int Execute(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return 1;
        }

        /// <summary>
        /// Hàm truy vấn câu lệnh sql
        /// </summary>
        /// <param name="sql">Câu lệnh sql</param>
        /// <param name="param">Tham số của câu lệnh</param>
        /// <param name="transaction">Transaction của câu lệnh</param>
        /// <param name="commandTimeout">Thời gian tối đa có thể thực thi</param>
        /// <param name="commandType">Kiểu câu lệnh</param>
        /// <returns>Đối tượng được truy vấn</returns>
        /// Created by: PVLONG (10/01/2023)
        public IEnumerable<T> Query<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return default;
        }

        /// <summary>
        /// Hàm truy vấn câu lệnh sql hoặc trả về mặc định
        /// </summary>
        /// <param name="sql">Câu lệnh sql</param>
        /// <param name="param">Tham số của câu lệnh</param>
        /// <param name="transaction">Transaction của câu lệnh</param>
        /// <param name="commandTimeout">Thời gian tối đa có thể thực thi</param>
        /// <param name="commandType">Kiểu câu lệnh</param>
        /// <returns>Đối tượng được truy vấn</returns>
        /// Created by: PVLONG (10/01/2023)
        public T QueryFirstOrDefault<T>(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return default;
        }

        /// <summary>
        /// Hàm truy vấn câu lệnh sql trả về nhiều kết quả
        /// </summary>
        /// <param name="sql">Câu lệnh sql</param>
        /// <param name="param">Tham số của câu lệnh</param>
        /// <param name="transaction">Transaction của câu lệnh</param>
        /// <param name="commandTimeout">Thời gian tối đa có thể thực thi</param>
        /// <param name="commandType">Kiểu câu lệnh</param>
        /// <returns>GridReader của kết quả truy vấn</returns>
        /// Created by: PVLONG (10/01/2023)
        public GridReader QueryMultiple(IDbConnection dbConnection, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return default;
        }

        #endregion
    }
}
