using System.Linq;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.BindingModels;
using ConferenceManagerExampleApp.Models.Domains;
using ConferenceManagerExampleApp.Models.ViewModels;
using ConferenceManagerExampleApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConferenceManagerExampleApp.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IRoomCategoryService _roomCategoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public RoomController(UserManager<IdentityUser> userManager, IRoomService roomService, IRoomCategoryService roomCategoryService)
        {
            _userManager = userManager;
            _roomService = roomService;
            _roomCategoryService = roomCategoryService;
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
            var rooms = await _roomService.GetAllRoomsAsync();
            
            // Put them in a viewModel
            var viewModel = new RoomViewModel
            {
                RoomModels = rooms
            };
            
            // Inject this viewModel in the view and return it.
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddRoom()
        {
            var roomCategories = await _roomCategoryService.GetAllRoomCategoriesAsync();
            var categoryDescriptions = roomCategories
                .Select(cate => new SelectListItem
                {
                    Text = cate.Description,
                    Value = cate.Id.ToString()
                }).ToList();

            var roomBindingModel = new RoomBindingModel
            {
                RoomCategoryDescription = categoryDescriptions
            };
            
            return View(roomBindingModel);
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomBindingModel roomBindingModel)
        {
            // Check that the binding model is valid.
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            // If that's the case, create a model from the binding model.
            var roomCategoryModel = await _roomCategoryService.GetRoomCategoryById(roomBindingModel.RoomCategoryId);
            if (roomCategoryModel == null)
            {
                return BadRequest("Could not find the room category");
            }
            
            var roomModel = new RoomModel
            {
                Name = roomBindingModel.Name,
                RoomCategoryModel = roomCategoryModel
            };
            var successful = await _roomService.AddRoomAsync(roomModel);
            if (!successful)
            {
                return BadRequest("Could not add the room.");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveRoom(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var successful = await _roomService.RemoveRoomAsync(id);
            if (!successful)
            {
                return BadRequest("Could not remove room.");
            }

            return RedirectToAction("Index");
        }
    }
}