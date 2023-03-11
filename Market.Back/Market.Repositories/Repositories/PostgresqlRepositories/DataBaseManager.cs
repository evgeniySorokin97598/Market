using Market.Repositories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Repositories.Repositories.PostgresqlRepositories
{
    public class DataBaseManager : IDataBaseManager
    {
        public ICategoriesRepository CategoriesRepository { get; private set; }

        public ISubcategoryRepository SubcategoryRepository { get; private set; }

        public IProductsRepository ProductsRepository { get; }

        public DataBaseManager()
        {
            var connection = new NpgsqlConnection($"Host=192.168.133.128;Port=5432;Database = Market; Username=postgres;Password=123qwe45asd");
            CategoriesRepository = new CategoriesRepository(connection);
            SubcategoryRepository = new SubcategoryRepository(connection);
            ProductsRepository = new ProductsRepository(connection);
        }


    }
}
