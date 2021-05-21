using JobSearch.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobSearch.Web.Controllers
{
    public class UsersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            IEnumerable<User> usersList;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44384/api/Users/"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    usersList = JsonConvert.DeserializeObject<IEnumerable<User>>(apiResponse);
                }
            }
            return View(usersList);
        }
    }
}
