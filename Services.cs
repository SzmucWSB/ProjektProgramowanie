namespace ProjektZamawianiePosiłków
{
    public interface IMealService
    {
        Task<IEnumerable<Meal>> GetAllAsync();
        Task<Meal> GetByIdAsync(int id);
        Task AddAsync(Meal meal);
        Task UpdateAsync(Meal meal);
        Task DeleteAsync(int id);
    }

    public class MealService : IMealService
    {
        private readonly ApplicationDbContext _context;

        public MealService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meal>> GetAllAsync()
        {
            return await _context.Meals.Include(m => m.Category).ToListAsync();
        }

        public async Task<Meal> GetByIdAsync(int id)
        {
            return await _context.Meals.Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal != null)
            {
                _context.Meals.Remove(meal);
                await _context.SaveChangesAsync();
            }
        }
    }
}
