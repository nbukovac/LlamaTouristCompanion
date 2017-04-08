using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LlamasTouristCompanion.Repositories {

    public class LocationRepository : IRepository<Location, Guid> {

        private readonly TouristDbContext _dbContext;

        public LocationRepository(TouristDbContext dbContext) {
            _dbContext = dbContext;
        }

        public Task<List<Location>> GetAll() {
            return _dbContext.Locations.ToListAsync();
        }

        public Location GetById(Guid id) {
            return _dbContext.Locations.First(m => m.LocationId == id);
        }

        public Task<List<Location>> GetAllWhere(Expression<Func<Location, bool>> predicate) {
            return _dbContext.Locations.Where(predicate).ToListAsync();
        }

        public void Insert(Location entity) {
            _dbContext.Locations.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Location entity) {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id){
            _dbContext.Locations.Remove(GetById(id));
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}