using GreenSale.ViewModels.Models.Categories;
using GreenSale.ViewModels.Models.SellerPosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Integrated.Interfaces.Categories
{
    public interface ICategoryGetAll
    {
        public Task<List<CategoryViewModel>> GetAllAsync();
    }
}
