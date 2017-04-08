using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LlamasTouristCompanion.Repositories {

    public class InfoRepository : IRepository<Info, Guid> {

        private readonly TouristDbContext _dbContext;

        public InfoRepository(TouristDbContext dbContext) {
            _dbContext = dbContext;
        }

        public Task<List<Info>> GetAll() {
            return _dbContext.Infos.ToListAsync();
        }

        public Info GetById(Guid id) {
            return _dbContext.Infos.First(m => m.InfoId == id);
        }

        public Task<List<Info>> GetAllWhere(Expression<Func<Info, bool>> predicate) {
            return _dbContext.Infos.Where(predicate).ToListAsync();
        }

        public void Insert(Info entity) {
            _dbContext.Infos.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Info entity) {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id){
            _dbContext.Infos.Remove(GetById(id));
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}