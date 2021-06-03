using JobSearch.Domain.Models;
using JobSearch.Web.Models;
using JobSearch.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobSearch.Web.Controllers
{
    public class JobsController : Controller
    {
        private JobService _service;

        public async Task<IActionResult> Index()
        {
            _service = new JobService();

            List<Job> jobsList = null;

            ResponseService<List<Job>> responseService = await _service.GetJobs();

            if (responseService.IsSucess)
            {
                jobsList = responseService.Data.ToList();
                ViewData["TotalRecords"] = responseService.Pagination.TotalItems.ToString("N0");
            }

            return View(jobsList);
        }
        public async Task<IActionResult> SearchJobs(string? word, string? cityState)
        {
            _service = new JobService();
            List<Job> jobsList = null;

            if (word == null)
            {
                word = "";
            }

            if (cityState == null)
            {
                cityState = "";
            }

            ViewData["word"] = word.ToString();
            ViewData["cityState"] = cityState.ToString();
            ViewData["TotalRecords"] = "0";

            ResponseService<List<Job>> responseService = await _service.GetJobs(word, cityState);
            if (responseService.IsSucess)
            {
                jobsList = responseService.Data.ToList();
                ViewData["TotalRecords"] = responseService.Pagination.TotalItems.ToString("N0");
            }

            return View(jobsList);

        }
    }
}
