using Dapper;
using Market.Entities.Configs;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Repositories.Repositories.PostgresqlRepositories
{
    public class DataBaseCreater
    {
        /// <summary>
        /// запросы на создания таблиц, Базы данных и тд.
        /// </summary>
        private List<string> _commandsCreate;
        bool _dataBaseExist = false;
        public NpgsqlConnection Connection { get; private set; }
        private string _databaseName = "Market";
        private LoggerLib.Interfaces.ILogger _logger;
        public DataBaseCreater(Configs config, LoggerLib.Interfaces.ILogger logger)
        {
            _logger = logger;
            _commandsCreate = new List<string>();
            Console.WriteLine($"Host={config.DataBaseConfig.Host}:{config.DataBaseConfig.Port}; Username={config.DataBaseConfig.Username};Password={config.DataBaseConfig.Password}");
            Connection = new NpgsqlConnection($"Host={config.DataBaseConfig.Host}:{config.DataBaseConfig.Port}; Username={config.DataBaseConfig.Username};Password={config.DataBaseConfig.Password}");

            Connection.Open();
            CreateDataBase().GetAwaiter().GetResult();
            Connection.Close();
            Connection.Dispose();

            //Connection = new NpgsqlConnection($"Host=192.168.133.128;Port=5432;Database = Market; Username=postgres;Password=123qwe45asd");
            Connection = new NpgsqlConnection($"Host={config.DataBaseConfig.Host}:{config.DataBaseConfig.Port};Database = Market; Username={config.DataBaseConfig.Username};Password={config.DataBaseConfig.Password}");

            

            //категории товаров
            _commandsCreate.Add(@"CREATE TABLE IF NOT EXISTS Categories
(
    Id    BIGSERIAL PRIMARY KEY,
    UrlIcon Text,
    Name  Text NOT NULL 
);");
            //подкатегории 
            _commandsCreate.Add(@"CREATE TABLE IF NOT EXISTS subcategory
(
    Id    BIGSERIAL PRIMARY KEY,
    UrlIcon Text,
    Name  Text NOT NULL,
    CategoryId INTEGER REFERENCES Categories (Id) NOT NULL
);");

            ///таблица с товарами
            _commandsCreate.Add(@"CREATE  TABLE IF NOT EXISTS  Products
(
    Id SERIAL PRIMARY KEY,
    Name text,
    Description Text default 'Нет описания',
    Price integer NOT NULL,
    Image Text ,
    Quantity integer,
    Brend text NOT NULL,
    ImageUrl text,
    SubcategoryId INTEGER REFERENCES subcategory (Id) NOT NULL, " + 
    " СharacteristicId SERIAL UNIQUE "+
");");
            /// таблица с типами характеристик конкретного товара для телефона например
            /// "Сотовая связь","Камера"
            _commandsCreate.Add(@"CREATE  TABLE IF NOT EXISTS  TypeСharacteristics
(
    Id SERIAL PRIMARY KEY,
    TypeСharacteristicsName text,
    ProductId bigint REFERENCES Products (СharacteristicId) NOT NULL  
);");

            //// характеристики  свойственные для конкретного типа товара
            ///например  для телефона есть тип характеристики "Сотовая связь" и в этой таблице будет содержаться 
            ///то что к ней относится: "Количество физических SIM-карт", "Стандарт связи" и тд
            _commandsCreate.Add(@"CREATE  TABLE IF NOT EXISTS  Сharacteristics
(
    Id SERIAL PRIMARY KEY,
    СharacteristicName text ,
    Сharacteristic text,
    TypeСharacteristicsId bigint REFERENCES TypeСharacteristics (Id) NOT NULL
);");

            /// таблица с пользователями
            _commandsCreate.Add($"CREATE  TABLE IF NOT EXISTS {UsersRepository.Table} (" +
                $"{UsersRepository.IdCol} SERIAL PRIMARY KEY," +
                $"{UsersRepository.Nickname} text," +
                $"{UsersRepository.UserId} text" +
                $")");

            /// комментарии
            _commandsCreate.Add($"CREATE  TABLE IF NOT EXISTS  {CommentsRepository.TableName}" +
"(" +
    $" {CommentsRepository.Id} SERIAL PRIMARY KEY, " +
    $" {CommentsRepository.DignityColumnName} text, " +
    $" {CommentsRepository.Flaws} text, " +
    $" {CommentsRepository.Comment} text, " +
    $" {CommentsRepository.ProductId} bigint REFERENCES {ProductsRepository.TableName} ({ProductsRepository.Id}) NOT NULL,  " +
    $" {CommentsRepository.UserIdCol} bigint REFERENCES {UsersRepository.Table} ({UsersRepository.IdCol})  " +

");") ;

            


    //        /// добавление внешнего ключа комментариев в таблицу с товарами
    //        _commandsCreate.Add($"ALTER TABLE {ProductsRepository.TableName} "+
    //$"ADD CONSTRAINT fk_products_comments FOREIGN KEY ({ProductsRepository.Id}) REFERENCES {CommentsRepository.TableName} ({CommentsRepository.Id});");
        }


        /// <summary>
        /// создание таблиц
        /// </summary>
        public void Create()
        {
            //if (_dataBaseExist) return;
            foreach (var t in _commandsCreate)
            {
                try
                {
                    Connection.Execute(t);
                }
                catch (Exception ex) { }
            }

        }

        public async Task CreateDataBase()
        {
            try
            {
                Console.WriteLine($"Попытка создания БД");
                _logger.Info("Создание базы данных");
                string sql = @"CREATE DATABASE " + $"\"{_databaseName}\"" +
                    @"WITH
                OWNER = postgres
                ENCODING = 'UTF8'
                LC_COLLATE = 'en_US.utf8'
                LC_CTYPE = 'en_US.utf8'
                TABLESPACE = pg_default
                CONNECTION LIMIT = -1
                IS_TEMPLATE = False;";


                Connection.Execute(sql);
                _logger.Info("Добавление тестовых данных");

                

            }
            catch (Exception ex)
            {
                _logger.Error($"Ошибка при создании БД  {ex.Message}");
            }

        }


    }

}

