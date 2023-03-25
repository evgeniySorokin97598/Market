using Dapper;
using Market.Entities.Dto;
using Market.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Market.Repositories.Repositories.PostgresqlRepositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private string _tableName = "Categories";
        string ColumnName = "Name";
        string UrlIconColumnName = "UrlIcon";
        string IdColumn = "Id";
        private NpgsqlConnection _connection;
        public CategoriesRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }


        public async Task<long> AddCategoryAsync(CategoryDto category)
        {
            string sql = $"INSERT INTO {_tableName} ({ColumnName},{UrlIconColumnName}) VALUES (@Name,@UrlIcon) returning {IdColumn}";
            var result = (await _connection.QueryAsync<long>(sql, new
            {
                Name = category.CategoryName,
                UrlIcon = category.CategoryIconUrl,
            })).FirstOrDefault();
            return result;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategories(string category)
        {
            var result = (await GetCategoriesAsync(category))?.FirstOrDefault()?.SubCategories;
            if (result == null) { 
            return new List<SubCategory>();
            }
            return result;

        }


        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(string category = "")
        {
            string sql = $"select  " +
                $" subcategory.name as {nameof(SubCategory.SubCategoryName)}, " +
                $" subcategory.urlicon as {nameof(SubCategory.SubCategoryUrlIcon)}, " +
                $" categories.urlicon as {nameof(CategoryDto.CategoryIconUrl)}, " +
                $" categories.name as {nameof(CategoryDto.CategoryName)} " +
                $"from {SubcategoryRepository.TableName}  " +
                $"join {_tableName} on {SubcategoryRepository.TableName}.{SubcategoryRepository.CategoryIdColumnName} = {_tableName}.{IdColumn} ";
            if (!string.IsNullOrEmpty(category))
            {
                sql += $" WHERE {_tableName}.name = @category ";
            }
            IEnumerable<dynamic> responce = null;
            if (string.IsNullOrEmpty(category))
            {
                responce = (await _connection.QueryAsync(sql));
            }
            else
            {
                responce = (await _connection.QueryAsync(sql, new { category }));
            }
            var result = new List<CategoryDto>();
            foreach (var t in responce)
            {
                var find = result.FirstOrDefault(p => p.CategoryName == t.categoryname);
                if (find == null)
                {
                    result.Add(new CategoryDto()
                    {
                        CategoryName = t.categoryname,
                        CategoryIconUrl = t.subCategoryurlicon,
                        SubCategories = new List<SubCategory>() {
                             new SubCategory(){
                                SubCategoryName = t.subcategoryname,
                                 SubCategoryUrlIcon= t.subcategoryurlicon,
                             }
                         }
                    });
                }
                else
                {
                    find.SubCategories.Add(new SubCategory()
                    {
                        SubCategoryName = t.subcategoryname,
                        SubCategoryUrlIcon = t.subcategoryurlicon,
                    });
                }
            }


            return result;
        }
    }
}
