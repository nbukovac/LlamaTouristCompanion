using LlamasTouristCompanion.Models;

namespace LlamasTouristCompanion.Repositories {

    public class OwnerRepository : IRepository<Owner, Guid> {

        private readonly TouristDbContext _dbContext;

        public OwnerRepository(TouristDbContext dbContext) {
            _dbContext = dbContext;
        }

        public Task<List<Owner>> GetAll() {
            return _dbContext.Owners.ToListAsync();
        }

        public Owner GetById(Guid id) {
            return _dbContext.Owners.First(m => m.OwnerId == id);
        }

        public Task<List<Owner>> GetAllWhere(Expression<Func<Owner, bool>> predicate) {
            return _dbContext.Owners.Where(predicate).ToListAsync();
        }

        public void Insert(Owner entity) {
            _dbContext.Owners.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Owner entity) {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id){
            _dbContext.Owners.Remove(GetById(id));
            _dbContext.SaveChanges();
        }
    }
}