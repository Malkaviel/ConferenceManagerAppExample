using System;
using System.Globalization;
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
using SQLitePCL;

namespace ConferenceManagerExampleApp.Controllers
{
    [Authorize(Roles = Constants.AdministratorRole)]
    public class TimeSlotController : Controller
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly ISessionService _sessionService;
        private readonly IRoomService _roomService;
        private readonly UserManager<IdentityUser> _userManager;

        public TimeSlotController(ITimeSlotService timeSlotService, ISessionService sessionService, IRoomService roomService, UserManager<IdentityUser> userManager)
        {
            _timeSlotService = timeSlotService;
            _sessionService = sessionService;
            _roomService = roomService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var timeSlots = await _timeSlotService.GetAllTimeSlotsAsync();
            
            var viewModel = new TimeSlotViewModel
            {
                TimeSlotModels = timeSlots
            };
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddTimeSlot()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            var roomNames = rooms
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            var sessions = await _sessionService.GetAllSessionsAsync();
            var sessionTitles = sessions
                .Select(x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                }).ToList();

            var possibleTimeSlots = await _timeSlotService.GetAllLegalTimeSlotsAsync();
            var strTimeSlots = possibleTimeSlots
                .Select(x => new SelectListItem
                {
                    Text = x.ToString("G"),
                    Value = x.ToString("G")
                }).ToList();

            var timeSlotBindingModel = new TimeSlotBindingModel
            {
                SessionTitles = sessionTitles,
                RoomNames = roomNames,
                TimeSlots = strTimeSlots
            };

            return View(timeSlotBindingModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddTimeSlot(TimeSlotBindingModel timeSlotBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var startTime = DateTime.Parse(timeSlotBindingModel.StartTime);
            var endTime = DateTime.Parse(timeSlotBindingModel.EndTime);
            var isValid = await _timeSlotService.AreStartTimeAndEndTimeLegal(startTime, endTime);

            if (!isValid)
            {
                return BadRequest("The time slot is invalid");
            }

            var timeSlot = await _timeSlotService.GetTimeSlotByTimeAsync(startTime, endTime);
            if (timeSlot != null)
            {
                return BadRequest("The time slot is already taken");
            }

            var roomModel = await _roomService.GetRoomByIdAsync(timeSlotBindingModel.RoomId);
            if (roomModel == null)
            {
                return BadRequest("Could not find the room.");
            }

            var sessionModel = await _sessionService.GetSessionByIdAsync(timeSlotBindingModel.SessionId);
            if (sessionModel == null)
            {
                return BadRequest("Could not find the session");
            }

            var timeSlotModel = new TimeSlotModel
            {
                StartTime = startTime,
                EndTime = endTime,
                RoomModel = roomModel,
                SessionModel = sessionModel
            };
            var successful = await _timeSlotService.AddTimeSlotAsync(timeSlotModel);
            if (!successful)
            {
                return BadRequest("Could not add the time slot");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveTimeSlot(DateTime startTime, DateTime endTime)
        {
            var isValid = await _timeSlotService.AreStartTimeAndEndTimeLegal(startTime, endTime);
            if (!isValid)
            {
                return BadRequest("The time slot is not valid");
            }

            var successful = await _timeSlotService.RemoveTimeSlotAsync(startTime, endTime);
            if (!successful)
            {
                return BadRequest("Could not remove the time slot");
            }

            return RedirectToAction("Index");
        }
    }
}