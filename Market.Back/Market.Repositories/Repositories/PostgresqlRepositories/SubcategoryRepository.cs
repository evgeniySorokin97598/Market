﻿using Dapper;
using Market.Entities.Dto;
using Market.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Market.Repositories.Repositories.PostgresqlRepositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {

        public static string TableName = "subcategory";
        public static string IdColumnName = "Id";
        public static string NameColumnName = "Name";
        public static string UrlIconcColumnName = "UrlIcon";
        public static string CategoryIdColumnName = "CategoryId";

        private NpgsqlConnection _connection;
        public SubcategoryRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<int> AddAsync(SubCategory category)
        {

            string checkSql = $"SELECT {IdColumnName} FROM {TableName} WHERE {NameColumnName} = @name";
            var check = await _connection.QueryAsync<int>(checkSql, new { name = category.SubCategoryName });
            if (!check.Any())
            {
                string sql = $"INSERT INTO {TableName} " +
                    $"({NameColumnName},{UrlIconcColumnName},{CategoryIdColumnName}) " +
                    $"VALUES (@Name,@UrlIcon,@CategoryId) " +
                    $"returning {IdColumnName}";

                var result = (await _connection.QueryAsync<int>(sql, new
                {
                    Name = category.SubCategoryName,
                    UrlIcon = category.SubCategoryUrlIcon,
                    CategoryId = category.CategoryId
                })).FirstOrDefault();
                return result;
            }
            else return check.FirstOrDefault();
            
        }
    }
}
