using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.ViewModels;
using ConferenceManagerExampleApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagerExampleApp.Controllers
{
    public class RoomCategoryController : Controller
    {
        private readonly IRoomCategoryService _roomCategoryService;

        public RoomCategoryController(IRoomCategoryService roomCategoryService)
        {
            _roomCategoryService = roomCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            // Get all RoomCategory items from DB
            var roomCategories = await _roomCategoryService.GetAllRoomCategoriesAsync();
            
            // Put them in a viewModel
            var viewModel = new RoomCategoryViewModel
            {
                RoomCategories = roomCategories
            };
            
            // Inject this viewModel in the view and return it.
            return View(viewModel);
        }
    }
}