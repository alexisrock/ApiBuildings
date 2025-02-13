using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly PruebaServiceDBContext _manejoRHContext;
        private readonly DbSet<T> table;
        public Repository(PruebaServiceDBContext manejoRHContext)
        {

            _manejoRHContext = manejoRHContext;
            table = _manejoRHContext.Set<T>();
        }


        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll()
        {
            return await table.ToListAsync();
        }

        public async Task<T?> GetById(object id)
        {
            T? t = await table.FindAsync(id);
            return t;
        }

        public async Task<T?> GetByParam(Expression<Func<T, bool>> obj)
        {
            T? t = await table.AsNoTracking().FirstOrDefaultAsync(obj);
            return t;
        }

        public async Task<List<T>> GetListByParam(Expression<Func<T, bool>> obj, int pagina, int tamanioPagina)
        {
            return await table.Where(obj)
                .Skip((pagina - 1) * tamanioPagina)
            .Take(tamanioPagina).ToListAsync();
        }


        public async Task<List<T>> GetAllByParamIncluding(Expression<Func<T, bool>> obj, params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = table.AsQueryable();

            if (obj is not null)
            {
                query = query.Where(obj);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            List<T> result = await query.ToListAsync();
            return result;
        }


        public async Task Insert(T obj)
        {
            table.Add(obj);
            await Save();
        }

        public async Task Save()
        {
            await _manejoRHContext.SaveChangesAsync();
        }

        public async Task Update(T obj)
        {
            table.Update(obj);
            await Save();
        }

    }
}
