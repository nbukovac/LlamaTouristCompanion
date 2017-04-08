using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LlamasTouristCompanion.Repositories
{
    public class EventRepository : IRepository<Event, Guid>
    {
        protected readonly TouristDbContext Context;
        protected DbSet<Event> DbSet;

        public EventRepository(TouristDbContext context)
        {
            Context = context;
            DbSet = context.Set<Event>();
        }
        public Task<List<Event>> GetAll()
        {
            return Context.Events.ToListAsync();
        }

        public Event GetById(Guid id)
        {
            return Context.Events.First(d => d.EventId == id);
        }

        public Task<List<Event>> GetAllWhere(Expression<Func<Event, bool>> predicate)
        {
            return Context.Events.Where(predicate).ToListAsync();
        }

        public void Insert(Event entity)
        {
            Context.Events.Add(entity);
            Context.SaveChanges();
        }

        public void Update(Event entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            Context.Events.Remove(GetById(id));
            Context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
