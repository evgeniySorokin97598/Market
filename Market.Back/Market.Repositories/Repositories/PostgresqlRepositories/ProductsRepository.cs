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
        public static string TableName = "Products";
        public static string SubCategoryIdColumn = "SubcategoryId";

        public ProductsRepository(NpgsqlConnection connection)
        {
            _connection = connection;

        }

        public async Task<ProductDto> GetProductById(long Id)
        {

            string sql = @"select " +
                $"{TableName}.{nameof(ProductDto.Name)}," +
                $"{TableName}.{nameof(ProductDto.Id)}," +
                $"{TableName}.{nameof(ProductDto.Description)}," +
                $"{TableName}.{nameof(ProductDto.Quantity)}," +
                $"{TableName}.{nameof(ProductDto.Brend)}," +
                $"{TableName}.{nameof(ProductDto.Price)}, " +
                $"{TableName}.{nameof(ProductDto.Image)}, " +
                $"typeСharacteristics.name as typeСharacteristicName, " +
                $"Сharacteristics.Сharacteristicname, " +
                $"Сharacteristics.Сharacteristic  " +
                $" From {TableName} " +
                @"join typeСharacteristics on products.Сharacteristicid = typeСharacteristics.productid
                  join Сharacteristics on typeСharacteristics.id = Сharacteristics.typeСharacteristicsid
                  where products.id = @id";

            var list = (await _connection.QueryAsync(sql, new
            {
                Id
            }));
            
            var first = list.FirstOrDefault();
            ProductDto product = new ProductDto()
            {
                Id = first.id,
                Brend = first.brend,
                Name = first.name,
                Description = first.description,
                Image = first.image,
                Price = first.price,
                Quantity = first.quantity,
            };


            foreach (var t in list)
            {
                var type = product.TypesCharacteristics.FirstOrDefault(p => p.Name == t.typeСharacteristicname);
                if (type != null)
                {
                    type.Charastitics.Add(new Charastitic()
                    {
                        Name = t.Сharacteristicname,
                        Text = t.Сharacteristic
                    });
                }
                else
                {
                    var newType = new ProductCharacteristicType()
                    {
                        Name = t.typeСharacteristicname,
                    };
                    newType.Charastitics.Add(new Charastitic()
                    {
                        Name = t.Сharacteristicname,
                        Text = t.Сharacteristic
                    });

                    product.TypesCharacteristics.Add(newType);
                }
            }

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

             
            string insetTypeChararistic = $"INSERT INTO typeСharacteristics (Name,ProductId) VALUES(@Name,@ProductId) returning id";
            string insetChararistic = $" INSERT INTO Сharacteristics (Сharacteristicname,TypeСharacteristicsId,Сharacteristic) VALUES(@Сharacteristic,@TypeId,@Text)";

            //// получения айдишника нового типа хараектристики
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
