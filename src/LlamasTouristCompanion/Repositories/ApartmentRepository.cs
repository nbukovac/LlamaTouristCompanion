using LlamasTouristCompanion.Models;

namespace LlamasTouristCompanion.Repositories {

    public class ApartmentRepository : IRepository<Apartment, Guid> {

        private readonly TouristDbContext _dbContext;

        public ApartmentRepository(TouristDbContext dbContext) {
            _dbContext = dbContext;
        }

        public Task<List<Apartment>> GetAll() {
            return _dbContext.Apartments.ToListAsync();
        }

        public Apartment GetById(Guid id) {
            return _dbContext.Apartments.First(m => m.ApartmentId == id);
        }

        public Task<List<Apartment>> GetAllWhere(Expression<Func<Apartment, bool>> predicate) {
            return _dbContext.Apartments.Where(predicate).ToListAsync();
        }

        public void Insert(Apartment entity) {
            _dbContext.Apartments.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Apartment entity) {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Guid id){
            _dbContext.Apartments.Remove(GetById(id));
            _dbContext.SaveChanges();
        }
    }
}