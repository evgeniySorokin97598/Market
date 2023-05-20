using Dapper;
using Market.Entities.Dto;
using Market.Entities.Requests;
using Market.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Market.Repositories.Repositories.PostgresqlRepositories
{
    public class ProductsRepository : IProductsRepository
    {


        private NpgsqlConnection _connection;
        public static string TableName = "products";
        public static string SubCategoryIdColumn = "SubcategoryId";
        public static string Id = "id";
        public static string CommentsForKey = "commmetsId";

        public ProductsRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<ProductDto> GetProductById(long id)
        {


            string sql = $"select * FROM {TableName} " +
    $" join typeСharacteristics on products.Сharacteristicid = typeСharacteristics.productid " +
     " join Сharacteristics on typeСharacteristics.id = Сharacteristics.typeСharacteristicsid " +
    $" left join {CommentsRepository.TableName} on {TableName}.{nameof(ProductDto.Id)} = {CommentsRepository.TableName}.{CommentsRepository.ProductId} " +
    $" left join {UsersRepository.Table} on {CommentsRepository.TableName}.{CommentsRepository.UserIdCol} = {UsersRepository.Table}.{UsersRepository.IdCol}" + /// join пользователей которые оставляли комментарии
      $" where {TableName}.{Id} = @id ";

            var list = (await _connection.QueryAsync(sql, new
            {
                id
            }));

            var first = list.FirstOrDefault();
            if (first == null) return null;
            ProductDto product = new ProductDto()
            {
                Id = first.id,
                Brend = first.brend,
                Name = first.name,
                Description = first.description,
                Image = first.image,
                Price = first.price,
                Quantity = first.quantity,
                TypesCharacteristics = list
                .GroupBy(l => l.typeСharacteristicsname)
                .Select(p => new ProductCharacteristicType()
                {
                    Name = p.Key,
                    Charastitics = list.Where(t => t.typeСharacteristicsname == p.Key)
                    .DistinctBy(p => p.Сharacteristicname)
                    .Select(k => new Charastitic()
                    {
                        Name = k.Сharacteristicname,
                        Text = k.Сharacteristic
                    }).ToList()
                }).ToList(),
                Comments = list.Where(p => !string.IsNullOrEmpty(p.comment) || !string.IsNullOrEmpty(p.dignity) || !string.IsNullOrEmpty(p.flaws)).Select(p => new CommentDto()
                {
                    CommentId = p.commentid,
                    Comment = p.comment,
                    Dignity = p.dignity,
                    Flaws = p.flaws,
                    UserName = string.IsNullOrEmpty(p.nickname) ? "Пользователь" : p.nickname,
                    Stars= p.stars,
                }).ToList()
            };
            product.Comments = product.Comments.DistinctBy(p => p.CommentId).ToList();
            return product;
        }

        public async Task<int> AddAsync(ProductDto product)
        {
            string insert = $"INSERT INTO {TableName} (" +
                $"{nameof(product.Name)}," +
                $"{nameof(product.Description)}," +
                $"{nameof(product.Price)}," +
                $"{nameof(product.Image)}," +
                $"{nameof(product.Quantity)}," +
                $"{nameof(product.Brend)}," +
                $"{nameof(product.SubCategoryid)})" +
                $" VALUES (" +
                $" @{nameof(product.Name)}," +
                $" @{nameof(product.Description)}," +
                $" @{nameof(product.Price)}," +
                $" @{nameof(product.Image)}," +
                $" @{nameof(product.Quantity)}," +
                $" @{nameof(product.Brend)}," +
                $" @{nameof(product.SubCategoryid)}" +
                $") returning id";
            int id = (await _connection.QueryAsync<int>(insert, product)).FirstOrDefault();
            return id;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategory(string categyName)
        {
            string sql = $"SELECT  " +
                $"{TableName}.{nameof(ProductDto.Name)}," +
                $"{TableName}.{nameof(ProductDto.Description)}," +
                $"{TableName}.{nameof(ProductDto.Quantity)}," +
                $"{TableName}.{nameof(ProductDto.Brend)}," +
                $"{TableName}.{nameof(ProductDto.Price)}, " +
                $"{TableName}.{nameof(ProductDto.Id)} " +
                $" From {TableName} " +
                $" Join {SubcategoryRepository.TableName} ON {SubcategoryRepository.TableName}.{SubcategoryRepository.IdColumnName} =  {TableName}.{SubCategoryIdColumn}" +
                $" WHERE {SubcategoryRepository.TableName}.{SubcategoryRepository.NameColumnName} = @Category";

            var result = await _connection.QueryAsync<ProductDto>(sql, new
            {
                Category = categyName
            });
            return result;
        }

        public async Task AddCharectiristic(ProductCharacteristicType characteristic)
        {


            string insetTypeChararistic = $"INSERT INTO typeСharacteristics (typeСharacteristicsName,ProductId) VALUES(@Name,@ProductId) returning id";
            string insetChararistic = $" INSERT INTO Сharacteristics (Сharacteristicname,TypeСharacteristicsId,Сharacteristic) VALUES(@Сharacteristic,@TypeId,@Text)";

            //// получение айдишника нового типа хараектристики
            long id = (await _connection.QueryAsync<long>(insetTypeChararistic, new
            {
                Name = characteristic.Name,
                ProductId = characteristic.ProductId,
            })).FirstOrDefault();

            foreach (var t in characteristic.Charastitics)
            {
                await _connection.QueryAsync(insetChararistic, new
                {
                    Сharacteristic = t.Name,
                    TypeId = id,
                    Text = t.Text
                });
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(GetProductsRequest request)
        {
            List<ProductDto> products = new List<ProductDto>();
            foreach (var t in request.Id)
            {
                if (t != 0) products.Add(await GetProductById(t));
            }
            return products;
        }
    }
}
