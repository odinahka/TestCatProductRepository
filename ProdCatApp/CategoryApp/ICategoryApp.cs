using PFS.ProdCat.Model.Common;
using PFS.ProdCat.Model.Dtos;

namespace PFS.ProdCat.App.CategoryApp
{
    public interface ICategoryApp
    {
        Task<ResponseVM<bool>> CreateCategoryAsync(CreateCategoryDto dto);
        Task<ResponseVM<List<GetCategories>>> GetCategoriesAsync();
    }
}