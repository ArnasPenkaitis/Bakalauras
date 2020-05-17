using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Shared.DataManagement.BaseRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bakalauras.Shared.DataManagement.BaseRepository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync<T>(int id) where T : class, IBaseEntity
        {
            var delete = await Task.Run(() => _context.Set<T>().SingleOrDefault(x => x.Id == id));
            if (delete != null)
            {
                await Task.Run(() => _context.Set<T>().Remove(delete));
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteBulk<T>(IQueryable<int> ids) where T : class, IBaseEntity
        {
            List<T> listToDelete = new List<T>();
            foreach (int id in ids)
            {
                bool contains = await Task.Run(() => _context.Set<T>().Any<T>(o => o.Id == id));
                if (contains == false)
                {
                    return;
                }
                var item = await Task.Run(() => _context.Set<T>().SingleOrDefault(x => x.Id == id));
                listToDelete.Add(item);
            }
            if (listToDelete.Count == 0)
                return;
            await Task.Run(() => _context.Set<T>().RemoveRange(listToDelete));
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> CreateAsync<T>(T data) where T : class, IBaseEntity
        {
            var result = _context.Set<T>().Add(data);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IQueryable<T>> QueryAsync<T>() where T : class, IBaseEntity
        {
            return await Task.Run(() => _context.Set<T>());
        }

        public async Task<T> QueryByIdAsync<T>(int id) where T : class, IBaseEntity
        {
            return await Task.Run(() => _context.Set<T>().SingleOrDefault(x => x.Id == id));
        }

        public async Task UpdateAsync<T>(int id, T data) where T : class, IBaseEntity
        {

            if (_context.Set<T>().Any(o => o.Id == id))
            {

                await Task.Run(() => _context.Set<T>().Update(data));
                await _context.SaveChangesAsync();
                return;
            }
            throw new Exception($"Entity with id: {id} not found.");
        }
    }
}
