using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LlamasTouristCompanion.Repositories {

    public class GuestRepository : IRepository<Guest, Guid> {

        private readonly TouristDbContext _dbContext;

        public GuestRepository(TouristDbContext dbContext) {
            _dbContext = dbContext;
        }

        public Task<List<Guest>> GetAll() {
            return _dbContext.Guests.ToListAsync();
        }

        public Guest GetById(Guid id) {
            return _dbContext.Guests.First(m => m.GuestId == id);
        }

        public Task<List<Guest>> GetAllWhere(Expression<Func<Guest, bool>> predicate) {
            return _dbContext.Guests.Where(predicate).ToListAsync();
        }

        public void Insert(Guest entity) {
            _dbContext.Guests.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Guest entity) {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id){
            _dbContext.Guests.Remove(GetById(id));
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}