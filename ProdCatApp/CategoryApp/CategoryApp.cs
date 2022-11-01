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

namespace PFS.ProdCat.App.CategoryApp
{
    public class CategoryApp : ICategoryApp
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CategoryApp> _logger;
        private readonly IMapper _mapper;

        public CategoryApp(IGenericRepository<Category> categoryRepo, ApplicationDbContext dbContext, ILogger<CategoryApp> logger, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseVM<bool>> CreateCategoryAsync(CreateCategoryDto dto)
        {
            try
            {
                var category = _mapper.Map<Category>(dto);
                if (await _categoryRepo.AddAsync(category)) return ResponseVM<bool>.Success(true, "Category Created Successfully");
                return ResponseVM<bool>.Failure("Failed to create Category, try again later");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ResponseVM<bool>.Failure("An error occurred, contact support");
            }
        }
        public async Task<ResponseVM<List<GetCategories>>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _dbContext.Categories.ToListAsync();
                if (categories != null)
                {
                    var categoriesDto = _mapper.Map<List<GetCategories>>(categories);
                    return ResponseVM<List<GetCategories>>.Success(categoriesDto, "Categories retrieved Successfully");
                }
                return ResponseVM<List<GetCategories>>.Failure("No category found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return ResponseVM<List<GetCategories>>.Failure("An error occurred, contact support");
            }
        }
    }
}
