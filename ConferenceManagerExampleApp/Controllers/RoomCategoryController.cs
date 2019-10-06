using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.BindingModels;
using ConferenceManagerExampleApp.Models.Domains;
using ConferenceManagerExampleApp.Models.ViewModels;
using ConferenceManagerExampleApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagerExampleApp.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class RoomCategoryController : Controller
    {
        private readonly IRoomCategoryService _roomCategoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public RoomCategoryController(IRoomCategoryService roomCategoryService, UserManager<IdentityUser> userManager)
        {
            _roomCategoryService = roomCategoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Check that the user is authenticated and is an administrator.
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            
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

        [HttpGet]
        public IActionResult AddRoomCategory()
        {
            var bindingModel = new RoomCategoryBindingModel();
            return View(bindingModel);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddRoomCategory(RoomCategoryBindingModel roomCategoryBindingModel)
        {
            // Check that the binding model is valid.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            // If that's the case, create a model from the binding model.
            var roomCategoryModel = new RoomCategoryModel
            {
                Description = roomCategoryBindingModel.Description,
                MaxCapacity = roomCategoryBindingModel.MaxCapacity
            };
            var successful = await _roomCategoryService.AddRoomCategoryAsync(roomCategoryModel);
            if (!successful)
            {
                return BadRequest("Could not add the room category.");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveRoomCategory(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var successful = await _roomCategoryService.RemoveRoomCategoryAsync(id);
            if (!successful)
            {
                return BadRequest("Could not remove room category.");
            }

            return RedirectToAction("Index");
        }
    }
}