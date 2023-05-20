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
        public static string UserIdCol = "UserId";
        private NpgsqlConnection _connection;
        private IUsersRepository _usersRepository;
        public CommentsRepository(NpgsqlConnection connection, IUsersRepository usersRepository)
        {
            _connection = connection;
            _usersRepository = usersRepository;
        }

        public async Task AddAsync(AddCommentRequest request, UserInfo info)
        {

            if (!string.IsNullOrEmpty(request?.Comment)|| !string.IsNullOrEmpty(request?.Dignity)||!string.IsNullOrEmpty(request?.Flaws)) 
            {
                long id = await _usersRepository.GetUser(info);
                string sql = $"INSERT INTO {TableName} ({DignityColumnName},{Flaws},{Comment},{ProductId},{UsersRepository.UserId}) VALUES (@Dignity,@Flaws,@Comment,@ProductId,@userId)";
                await _connection.QueryAsync(sql, new
                {
                    Dignity = request.Dignity,
                    Flaws = request.Flaws,
                    Comment = request.Comment,
                    ProductId = request.ProductId,
                    userId = id
                });
            }
            
            
        }
    }
}
