using Microsoft.EntityFrameworkCore;
using PFS.ProdCat.DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFS.ProdCat.Repository.EntityDb
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await _context.AddAsync(entity);
                return await SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            try
            {
                await _context.AddRangeAsync(entities);
                return await SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<bool> Exists<U>(U parameter)
        {
            try
            {
                var entity = await GetAsync<U>(parameter);
                return entity != null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _context.Set<T>().AsNoTrackingWithIdentityResolution().ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<T> GetAsync<U>(U parameter)
        {
            try
            {
                return await _context.Set<T>().FindAsync(parameter);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                return await SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<bool> UpdateRangeAsync(List<T> entities)
        {
            try
            {
                _context.Set<T>().UpdateRange(entities);
                return await SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() >= 0 ? true : false;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}

