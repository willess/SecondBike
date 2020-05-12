using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecondBike.Data;
using SecondBike.Data.Entities;
using SecondBike.ViewModels;

namespace SecondBike.Controllers
{
    [Authorize]
    public class MyAccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<MyAccountController> _logger;
        private readonly IMapper _mapper;
        private readonly ISecondBikeRepository _repository;
        private readonly SecondBikeContext _context;

        public MyAccountController(UserManager<User> userManager,
                ILogger<MyAccountController> logger,
                IMapper mapper,
                ISecondBikeRepository repository,
                SecondBikeContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            try
            {
                //get user profile data
                var user = await _userManager.GetUserAsync(User);

                if(user != null)
                {
                    return View(_mapper.Map<User, UserProfileViewModel>(user));
                }
                else
                {
                    return BadRequest("User data is empty");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Showing user profile page with logged in user data failed, {ex}");

            }
            return BadRequest("Failed to show user profile page");

        }

        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("User not logged in");
            }

            try
            {

                if (ModelState.IsValid)
                {
                    var userToUpdate = await _userManager.GetUserAsync(User);

                    userToUpdate.FirstName = model.FirstName;
                    userToUpdate.LastName = model.LastName;

                    _repository.UpdateEntity(userToUpdate);

                    if(_repository.SaveAll())
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        _logger.LogError("Could not Save for some reason");

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not update user profile", ex);
            }

            return View(model);
        }

        public async Task<IActionResult> MyAdvertisements()
        {
            var user = await _userManager.GetUserAsync(User);

            var ads = _repository.GetAdvertisementsByUser(user);

            return View(ads);
        }

        public IActionResult MyFavorites()
        {
            return View();
        }

        public IActionResult MyBids()
        {
            return View();
        }
    }
}