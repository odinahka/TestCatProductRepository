using PFS.ProdCat.Model.Common;
using PFS.ProdCat.Model.Dtos;

namespace PFS.ProdCat.App.ProductApp
{
    public interface IProductApp
    {
        Task<ResponseVM<bool>> CreateProductAsync(CreateProductDto dto);
        Task<ResponseVM<GetProductsDto>> GetProductByCodeAsync(string productCode);
        Task<ResponseVM<List<GetProductsDto>>> GetProductsAsync(GetProductsQueryParam query);
    }
}