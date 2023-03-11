using Market.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Market.Repositories.Interfaces
{
    public interface ICategoriesRepository
    {
        public Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        public Task<long> AddCategoryAsync(CategoryDto category);
         

    }
}
