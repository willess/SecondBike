using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondBike.Data;
using SecondBike.Data.Entities;
using SecondBike.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecondBike.Controllers
{
    public class AppController : Controller
    {
        private readonly ISecondBikeRepository _repository;
        private readonly IMapper _mapper;

        public AppController(ISecondBikeRepository repository,
                IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var results = _repository.GetAllAdvertisements();
            return View(_mapper.Map<IEnumerable<Advertisement>, IEnumerable<AdvertisementViewModel>>(results));
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
