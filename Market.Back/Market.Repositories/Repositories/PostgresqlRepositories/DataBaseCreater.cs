using Dapper;
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
        public DataBaseCreater()
        {

            _commandsCreate = new List<string>();
             

            Connection = new NpgsqlConnection($"Host=192.168.133.128;Port=5432;Database = Market; Username=postgres;Password=123qwe45asd");

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
    SubcategoryId INTEGER REFERENCES subcategory (Id) NOT NULL,
    СharacteristicId SERIAL UNIQUE
);");
            /// таблица с типами характеристик конкретного товара для телефона например
            /// "Сотовая связь","Камера"
            _commandsCreate.Add(@"CREATE  TABLE IF NOT EXISTS  TypeСharacteristics
(
    Id SERIAL PRIMARY KEY,
    Name text,
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
        
        }

        /// <summary>
        /// создание таблиц
        /// </summary>
        public void Create()
        {
            //if (_dataBaseExist) return;
            foreach (var t in _commandsCreate)
            {
                Console.WriteLine(t);
                Connection.Execute(t);
            }
             
        }
    }
}
