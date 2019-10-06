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
    public class SpeakerController : Controller
    {
        private readonly ISpeakerService _speakerService;
        private readonly ISubjectService _subjectService;
        private readonly UserManager<IdentityUser> _userManager;

        public SpeakerController(ISpeakerService speakerService, ISubjectService subjectService, UserManager<IdentityUser> userManager)
        {
            _speakerService = speakerService;
            _subjectService = subjectService;
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

            var speakers = await _speakerService.GetAllSpeakersAsync();

            var viewModel = new SpeakerViewModel
            {
                SpeakerModels = speakers
            };
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddSpeaker()
        {
            var subjects = await _subjectService.GetAllSubjectsAsync();
            var subjectTitles = subjects
                .Select(x => new SelectListItem
                {
                    Text = x.Title,
                    Value = x.Id.ToString()
                }).ToList();

            var speakerBindingModel = new SpeakerBindingModel
            {
                SubjectTitles = subjectTitles
            };

            return View(speakerBindingModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddSpeaker(SpeakerBindingModel speakerBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var subjectModel = await _subjectService.GetSubjectByIdAsync(speakerBindingModel.SubjectId);
            if (subjectModel == null)
            {
                return BadRequest("Could not find the subject.");
            }

            var speakerModel = new SpeakerModel
            {
                SubjectModel = subjectModel,
                FirstName = speakerBindingModel.FirstName,
                LastName = speakerBindingModel.LastName,
                Email = speakerBindingModel.Email,
                Title = speakerBindingModel.Title
            };

            var successful = await _speakerService.AddSpeakerAsync(speakerModel);
            if (!successful)
            {
                return BadRequest("Could not add the room");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveSpeaker(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var successful = await _speakerService.RemoveSpeakerAsync(id);
            if (!successful)
            {
                return BadRequest("Could not remove speaker.");
            }

            return RedirectToAction("Index");
        }
    }
}