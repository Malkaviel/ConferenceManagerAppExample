using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagerExampleApp.Controllers
{
    [Authorize(Roles = Constants.UserRole)]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}