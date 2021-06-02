using JobSearch.Domain.Models;
using JobSearch.Web.Models;
using JobSearch.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearch.Web.Controllers
{
    public class UsersController : Controller
    {
        private UserService _service;

        public async Task<IActionResult> Index()
        {
            _service = new UserService();

            List<User> usersList = null;

            ResponseService<List<User>> responseService = await _service.GetUsers();

            if (responseService.IsSucess)
            {
                usersList = responseService.Data.ToList();
            }
            return View(usersList);
        }
    }
}
