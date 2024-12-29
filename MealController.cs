using Microsoft.AspNetCore.Mvc;

namespace ProjektZamawianiePosiłków
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        public async Task<IActionResult> Index()
        {
            var meals = await _mealService.GetAllAsync();
            return View(meals);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meal meal)
        {
            if (ModelState.IsValid)
            {
                await _mealService.AddAsync(meal);
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }

        // Add Edit, Delete, and Details actions
    }

}
