using Dapper;
using Market.Entities.Dto;
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
    public class UsersRepository : IUsersRepository
    {
        private NpgsqlConnection _connection;
        public static string Table = "Users";
        public static string IdCol = "id";
        public static string Nickname = "Nickname";
        public static string UserId = "UserId";


        public UsersRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<long> GetUser(UserInfo info)
        {
            string sql = $"SELECT {IdCol} FROM {Table} where {UserId} = @user";
            var result = (await _connection.QueryAsync<long>(sql, new
            {
                user = info.Id
            })).FirstOrDefault();
            if (result == 0)
            {
                return await AddAsync(info);
            }
            return result;
        }

        public async Task<long> AddAsync(UserInfo info)
        {
            string sql = $"INSERT INTO {Table} ({IdCol},{Nickname}) VALUES (@UserId,@nick) returning id";
            var result = (await _connection.QueryAsync<long>(sql, new
            {
                UserId = info.Id,
                nick = info.Username
            })).FirstOrDefault();
            return result;
        }
    }
}
