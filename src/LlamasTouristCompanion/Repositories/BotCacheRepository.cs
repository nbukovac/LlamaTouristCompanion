using LlamasTouristCompanion.Models;

namespace LlamasTouristCompanion.Repositories {

    public class BotCacheRepository : IRepository<BotCache, Guid> {

        private readonly TouristDbContext _dbContext;

        public BotCacheRepository(TouristDbContext dbContext) {
            _dbContext = dbContext;
        }

        public Task<List<BotCache>> GetAll() {
            return _dbContext.Infos.ToListAsync();
        }

        public BotCache GetById(Guid id) {
            return _dbContext.BotCaches.First(m => m.BotCacheId == id);
        }

        public Task<List<BotCache>> GetAllWhere(Expression<Func<BotCache, bool>> predicate) {
            return _dbContext.BotCaches.Where(predicate).ToListAsync();
        }

        public void Insert(BotCache entity) {
            _dbContext.BotCaches.Add(entity);
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
    }
}