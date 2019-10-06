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
    public class SubjectCategoryController : Controller
    {
        private readonly ISubjectCategoryService _subjectCategoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public SubjectCategoryController(ISubjectCategoryService subjectCategoryService, UserManager<IdentityUser> userManager)
        {
            _subjectCategoryService = subjectCategoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var subjectCategories = await _subjectCategoryService.GetAllSubjectCategoryAsync();
            var viewModel = new SubjectCategoryViewModel
            {
                SubjectCategoryModels = subjectCategories
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddSubjectCategory()
        {
            var bindingModel = new SubjectCategoryBindingModel();
            return View(bindingModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddSubjectCategory(SubjectCategoryBindingModel subjectCategoryBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var subjectCategoryModel = new SubjectCategoryModel
            {
                Description = subjectCategoryBindingModel.Description
            };
            var successful = await _subjectCategoryService.AddSubjectCategoryAsync(subjectCategoryModel);
            if (!successful)
            {
                return BadRequest("Could not add the subject category");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> RemoveSubjectCategory(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var successful = await _subjectCategoryService.RemoveSubjectCategoryAsync(id);
            if (!successful)
            {
                return BadRequest("Could not remove the subject category");
            }

            return RedirectToAction("Index");
        }
    }
}