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
    public class SubjectController : Controller
    {
        private readonly ISubjectCategoryService _subjectCategoryService;
        private readonly ISubjectService _subjectService;
        private readonly UserManager<IdentityUser> _userManager;

        public SubjectController(ISubjectCategoryService subjectCategoryService, ISubjectService subjectService, UserManager<IdentityUser> userManager)
        {
            _subjectCategoryService = subjectCategoryService;
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

            var subjects = await _subjectService.GetAllSubjectsAsync();

            var viewModel = new SubjectViewModel
            {
                SubjectModels = subjects
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddSubject()
        {
            var subjectCategories = await _subjectCategoryService.GetAllSubjectCategoryAsync();
            var categoryDescriptions = subjectCategories
                .Select(x => new SelectListItem
                {
                    Text = x.Description,
                    Value = x.Id.ToString()
                }).ToList();

            var subjectBindingModel = new SubjectBindingModel
            {
                SubjectCategoryDescription = categoryDescriptions
            };

            return View(subjectBindingModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectBindingModel subjectBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var subjectCategoryModel =
                await _subjectCategoryService.GetSubjectCategoryById(subjectBindingModel.SubjectCategoryId);
            if (subjectCategoryModel == null)
            {
                return BadRequest("Could not find the subject category.");
            }

            var subjectModel = new SubjectModel
            {
                Title = subjectBindingModel.Title,
                SubjectCategoryModel = subjectCategoryModel
            };

            var successful = await _subjectService.AddSubjectAsync(subjectModel);
            if (!successful)
            {
                return BadRequest("Could not add the subject");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveSubject(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var successful = await _subjectService.RemoveSubjectAsync(id);
            if (!successful)
            {
                return BadRequest("Could not remove the subject");
            }

            return RedirectToAction("Index");
        }
    }
}