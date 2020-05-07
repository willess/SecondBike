using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondBike.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondBike.Controllers
{
    public class AppController : Controller
    {
        private readonly ISecondBikeRepository _repository;

        public AppController(ISecondBikeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var results = _repository.GetAllAdvertisements();
            return View(results);
        }

        [Authorize]
        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
