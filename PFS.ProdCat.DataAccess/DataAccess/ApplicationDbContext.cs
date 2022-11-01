﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProdCatLib.Models;

namespace PFS.ProdCat.DataAccess.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
            public ApplicationDbContext(DbContextOptions options) : base(options)
            {
            }

            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }


        }

}
