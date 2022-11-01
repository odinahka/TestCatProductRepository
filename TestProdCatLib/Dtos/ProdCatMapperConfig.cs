using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProdCatLib.Models;

namespace PFS.ProdCat.Model.Dtos
{
    public class ProdCatMapperConfig : Profile
    {
        public ProdCatMapperConfig()
        {
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, GetProductsDto>().ReverseMap();
            CreateMap<Category, GetCategories>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
        }
    }
}
