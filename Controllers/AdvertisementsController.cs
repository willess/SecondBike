using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SecondBike.Data;
using SecondBike.Data.Entities;
using SecondBike.ViewModels;

namespace SecondBike.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly SecondBikeContext _context;
        private readonly ISecondBikeRepository _repository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AdvertisementsController> _logger;

        public AdvertisementsController(SecondBikeContext context,
                ISecondBikeRepository repository, 
                UserManager<User> userManager,
                ILogger<AdvertisementsController> logger)
        {
            _context = context;
            _repository = repository;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Advertisements
        public IActionResult Index()
        {
            return RedirectToAction("Index", "App");
        }

        // GET: Advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AdvertisementId == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Advertisements/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(AdvertisementCreationModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int.TryParse(model.CategoryId, out int categoryId);

                    //check category
                    var category = _repository.GetCategory(categoryId);

                    if(User.Identity.IsAuthenticated)
                    {
                        // get logged in user
                        var user = await _userManager.GetUserAsync(User);

                        // create new advertisement
                        var advertisement = new Advertisement()
                        {
                            Title = model.Title,
                            Description = model.Description,
                            Category = category,
                            User = user
                        };

                        // Add new advertisement
                        _repository.AddEntity(advertisement);

                        //save and redirect to created advertisement detail page
                        if(_repository.SaveAll())
                        {
                            return RedirectToAction("Details", new { id = advertisement.AdvertisementId });
                        }
                    }
                    else
                    {
                        // user is not logged in so return badrequest
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create a new advertisement: ", ex);
            }

            return View(model);

            //if (ModelState.IsValid)
            //{
            //    _context.Add(advertisement);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", advertisement.CategoryId);
            //return View(advertisement);
        }

        // GET: Advertisements/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var advertisement = _repository.GetAdvertisementById(id);

                if (advertisement == null)
                {
                    return NotFound();
                }

                //check if logged in user owns this specific advertisement
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (advertisement.User == user)
                    {
                        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", advertisement.CategoryId);
                        return View(advertisement);
                    }
                }

                return RedirectToAction("Details", new { id = advertisement.AdvertisementId });
            }
            catch (Exception ex)
            {
                _logger.LogError("failed to load advertisement edit page", ex);
            }

            return BadRequest();
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertisementId,Title,Description,CategoryId")] Advertisement advertisement)
        {
            if (id != advertisement.AdvertisementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.AdvertisementId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", advertisement.CategoryId);
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AdvertisementId == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.AdvertisementId == id);
        }
    }
}
