﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Repositories.Interfaces
{
    public interface IDataBaseManager
    {
        public ICategoriesRepository CategoriesRepository { get; }
        public ISubcategoryRepository SubcategoryRepository { get; }
        public IProductsRepository ProductsRepository { get; }


    }
}
