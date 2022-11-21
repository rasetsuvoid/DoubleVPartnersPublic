using Application.Common.Interfaces;
using Application.DTOS;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context= context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task DeletePerson(Expression<Func<T, bool>> predicate)
        {

            T entity = await _dbSet.Where(predicate).FirstOrDefaultAsync();
            if (!object.Equals(entity, null))
            {
                entity.Active = false;
                entity.IsDeleted = true;
                entity.UpdateDate = DateTime.Now;

                _context.Update(entity);

            }
            else
            {
                throw new Exception("No se encontro el registro");
            }

        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
           List<T> response = new List<T>();
            try
            {
                response = await _dbSet.Where(predicate).ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            
            try
            {
                return await _dbSet.Where(predicate).FirstOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity); 
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
