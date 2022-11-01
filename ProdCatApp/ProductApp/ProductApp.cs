using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PFS.ProdCat.DataAccess.DataAccess;
using PFS.ProdCat.Model.Common;
using PFS.ProdCat.Model.Dtos;
using PFS.ProdCat.Repository.EntityDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProdCatLib.Models;

namespace PFS.ProdCat.App.ProductApp
{
    public class ProductApp : IProductApp
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ProductApp> _logger;
        private readonly IMapper _mapper;

        public ProductApp(IGenericRepository<Product> productRepo, ApplicationDbContext dbContext, ILogger<ProductApp> logger, IMapper mapper)
        {
            _productRepo = productRepo;
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseVM<bool>> CreateProductAsync(CreateProductDto dto)
        {
            try
            {
                var product = _mapper.Map<Product>(dto);
                if (await _productRepo.AddAsync(product)) return ResponseVM<bool>.Success(true, "Product Created Successfully");
                return ResponseVM<bool>.Failure("Failed to create Product, try again later");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ResponseVM<bool>.Failure("An error occurred, contact support");
            }
        }
        public async Task<ResponseVM<GetProductsDto>> GetProductByCodeAsync(string productCode)
        {
            try
            {
                var product = await _dbContext.Products.Where(q => q.Code == productCode).FirstOrDefaultAsync();
                if (product != null)
                {
                    var productDto = _mapper.Map<GetProductsDto>(product);
                    return ResponseVM<GetProductsDto>.Success(productDto, "Product retrieved Successfully");
                }
                return ResponseVM<GetProductsDto>.Failure("No product found with this code");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ResponseVM<GetProductsDto>.Failure("An error occurred, contact support");
            }
        }

        public async Task<ResponseVM<List<GetProductsDto>>> GetProductsAsync(GetProductsQueryParam query)
        {
            try
            {
                var products =  await _dbContext.Products.Where(q => q.ExpiryDate >= query.ExpiryDateFrom && q.ExpiryDate <= query.ExpiryDateTo).ToListAsync();
                if (products != null)
                {
                    var productDtos = _mapper.Map<List<GetProductsDto>>(products);
                    return ResponseVM<List<GetProductsDto>>.Success(productDtos, "Products retrieved Successfully");
                }
                return ResponseVM<List<GetProductsDto>>.Failure("No product found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ResponseVM<List<GetProductsDto>>.Failure("An error occurred, contact support");
            }
        }
    }
}
