using System;
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
    [Authorize(Roles = Constants.UserRole)]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserTimeSlotService _userTimeSlotService;
        private readonly ITimeSlotService _timeSlotService;

        public UserController(UserManager<IdentityUser> userManager, IUserTimeSlotService userTimeSlotService, ITimeSlotService timeSlotService)
        {
            _userManager = userManager;
            _userTimeSlotService = userTimeSlotService;
            _timeSlotService = timeSlotService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var timeSlots = await _userTimeSlotService.GetUserTimeSlotsForUserAsync(currentUser);

            var viewModel = new UserTimeSlotViewModel
            {
                UserTimeSlotModels = timeSlots
            };
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddUserTimeSlot()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            
            var timeSlots = await _timeSlotService.GetAllTimeSlotsAsync();
            var currentUserTimeSlots = await _userTimeSlotService.GetUserTimeSlotsForUserAsync(currentUser);
            var currentTimeSlots = currentUserTimeSlots.Select(x => x.TimeSlotModel).ToList();
            
            var availableTimeSlots =
                timeSlots
                    .Where(x => !currentTimeSlots.Contains(x))
                    .Select(x => new SelectListItem
                    {
                        Text = $"{x.StartTime:G} - {x.EndTime:G} - {x.SessionModel.SpeakerModel.FirstName} {x.SessionModel.SpeakerModel.LastName} - {x.RoomModel.Name}",
                        Value = $"{x.StartTime:G} - {x.EndTime:G}"
                    })
                    .ToList();

            var userTimeSlotBindingModel = new UserTimeSlotBindingModel
            {
                TimeSlots = availableTimeSlots
            };

            return View(userTimeSlotBindingModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddUserTimeSlot(UserTimeSlotBindingModel userTimeSlotBindingModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var parsedDate = userTimeSlotBindingModel.TimeSlotDescription.Split('-');
            var trimedDates = parsedDate.Select(x => x.Trim()).ToList();
            var startTime = DateTime.Parse(trimedDates[0]);
            var endTime = DateTime.Parse(trimedDates[1]);

            var timeSlot = await _timeSlotService.GetTimeSlotByTimeAsync(startTime, endTime);

            var userTimeSlotModel = new UserTimeSlotModel
            {
                IdentityUser = currentUser,
                TimeSlotModel = timeSlot
            };

            var successful = await _userTimeSlotService.AddUserTimeSlotAsync(userTimeSlotModel);
            if (!successful)
            {
                return BadRequest("Could not add the user time slot");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveUserTimeSlot(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            
            var successful = await _userTimeSlotService.RemoveUserTimeSlotAsync(id);
            if (!successful)
            {
                return BadRequest("Could not remove the time slot");
            }

            return RedirectToAction("Index");
        }
    }
}