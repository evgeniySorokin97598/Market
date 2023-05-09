using Dapper;
using Market.Entities.Dto;
using Market.Entities.Requests;
using Market.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Repositories.Repositories.PostgresqlRepositories
{
    public class CommentsRepository : ICommentsRepository
    {
        public static string TableName = "Comments";

        public static string Id = "Id";

        /// <summary>
        /// колонка с достоинствами товара
        /// </summary>
        public static string DignityColumnName = "Dignity";

        /// <summary>
        /// колонка с недостатками товара
        /// </summary>
        public static string Flaws = "Flaws";

        /// <summary>
        /// колонка с текстом комментария
        /// </summary>
        public static string Comment = "Comment";

        public static string ProductId = "ProductId";

        private NpgsqlConnection _connection;

        public CommentsRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task AddAsync(AddCommentRequest request, UserInfo info)
        {
            string sql = $"INSERT INTO {TableName} ({DignityColumnName},{Flaws},{Comment},{ProductId}) VALUES (@Dignity,@Flaws,@Comment,@ProductId)";
            await _connection.QueryAsync(sql, request);
        }
    }
}
