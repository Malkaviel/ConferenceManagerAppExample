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
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ISpeakerService _speakerService;
        private readonly UserManager<IdentityUser> _userManager;

        public SessionController(ISessionService sessionService, ISpeakerService speakerService, UserManager<IdentityUser> userManager)
        {
            _sessionService = sessionService;
            _speakerService = speakerService;
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

            var sessions = await _sessionService.GetAllSessionsAsync();

            var viewModel = new SessionViewModel
            {
                SessionModels = sessions
            };
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddSession()
        {
            var speakers = await _speakerService.GetAllSpeakersAsync();
            var speakerNames = speakers
                .Select(x => new SelectListItem
                {
                    Text = $"{x.FirstName} {x.LastName}",
                    Value = x.Id.ToString()
                }).ToList();

            var sessionBindingModel = new SessionBindingModel
            {
                SpeakerNames = speakerNames
            };

            return View(sessionBindingModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddSession(SessionBindingModel sessionBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var speaker = await _speakerService.GetSpeakerByIdAsync(sessionBindingModel.SpeakerId);
            if (speaker == null)
            {
                return BadRequest("Could not find the speaker");
            }

            var sessionModel = new SessionModel
            {
                Title = sessionBindingModel.Title,
                SpeakerModel = speaker
            };

            var successful = await _sessionService.AddSessionAsync(sessionModel);
            if (!successful)
            {
                return BadRequest("Could not add the session.");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveSession(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var successful = await _sessionService.RemoveSessionAsync(id);
            if (!successful)
            {
                return BadRequest("Could not remove session.");
            }

            return RedirectToAction("Index");
        }
    }
}