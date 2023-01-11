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
    public class MySqlConnectionLayer : IConnectionLayer
    {
        private IDbConnection _connection = null;

        #region Method

        /// <summary>
        /// Hàm khởi tạo kết nối database
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>Kết nối database</returns>
        /// Created by: PVLONG (10/01/2023)
        public IDbConnection InitConnection(string connectionString) 
        {
            _connection = new MySqlConnection(connectionString);
            return _connection;
        }

        /// <summary>
        /// Hàm giải phóng
        /// Created by: PVLONG (10/01/2023)
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
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
            return dbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
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
            return dbConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
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
            // throw new Exception();
            return dbConnection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
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
            return dbConnection.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }

        #endregion

        #region Destructor

        ~MySqlConnectionLayer()
        {

        }

        #endregion
    }
}
