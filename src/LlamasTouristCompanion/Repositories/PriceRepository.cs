using LlamasTouristCompanion.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Repositories
{
    public class PriceRepository
    {
        protected readonly TouristDbContext Context;
        protected DbSet<Price> DbSet;

        public PriceRepository(TouristDbContext context)
        {
            Context = context;
            DbSet = context.Set<Price>();
        }
        public Task<List<Price>> GetAll()
        {
            return Context.Prices.ToListAsync();
        }

        public Price GetById(Guid id)
        {
            return Context.Prices.First(d => d.PriceId == id);
        }

        public Task<List<Price>> GetAllWhere(Expression<Func<Price, bool>> predicate)
        {
            return Context.Prices.Where(predicate).ToListAsync();
        }

        public void Insert(Price entity)
        {
            Context.Prices.Add(entity);
            Context.SaveChanges();
        }

        public void Update(Price entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            Context.Prices.Remove(GetById(id));
            Context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
