using LlamasTouristCompanion.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Repositories
{
    public class ReservationRepository
    {
        protected readonly TouristDbContext Context;
        protected DbSet<Reservation> DbSet;

        public ReservationRepository(TouristDbContext context)
        {
            Context = context;
            DbSet = context.Set<Reservation>();
        }
        public Task<List<Reservation>> GetAll()
        {
            return Context.Reservations.ToListAsync();
        }

        public Reservation GetById(Guid id)
        {
            return Context.Reservations.First(d => d.ReservationId == id);
        }

        public Task<List<Reservation>> GetAllWhere(Expression<Func<Reservation, bool>> predicate)
        {
            return Context.Reservations.Where(predicate).ToListAsync();
        }

        public void Insert(Reservation entity)
        {
            Context.Reservations.Add(entity);
            Context.SaveChanges();
        }

        public void Update(Reservation entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            Context.Reservations.Remove(GetById(id));
            Context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
