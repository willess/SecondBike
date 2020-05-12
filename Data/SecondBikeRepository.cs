using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecondBike.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.Data
{
    public class SecondBikeRepository : ISecondBikeRepository
    {
        private readonly SecondBikeContext _ctx;
        private readonly ILogger<SecondBikeRepository> _logger;

        public SecondBikeRepository(SecondBikeContext ctx, ILogger<SecondBikeRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Advertisement> GetAllAdvertisements()
        {
            var ads = _ctx.Advertisements
                    .Include(c => c.Category)
                        .ThenInclude(m => m.MainCategory)
                        .ThenInclude(c => c.Categories)
                    .ToList();
            ads.Reverse();
            return ads;
        }

        public Advertisement GetAdvertisementById(int id)
        {
            return _ctx.Advertisements
                    .Include(c => c.Category)
                        .ThenInclude(m => m.MainCategory)
                            .ThenInclude(c => c.Categories)
                    .Include(u => u.User)
                    .Where(a => a.AdvertisementId == id)
                    .FirstOrDefault();
        }

        public IEnumerable<Advertisement> GetAdvertisementsByCategory()
        {

            var results = _ctx.Advertisements
                            .Include(c => c.Category)
                            .ToList();
                            
            return results;
        }

        public IEnumerable<Advertisement> GetAdvertisementsByUser(User user)
        {

            var results = _ctx.Advertisements
                            .Include(c => c.Category)
                                .ThenInclude(m => m.MainCategory)
                            .Where(u => u.User == user)
                            .ToList();

            return results;
        }

        public IEnumerable<MainCategory> GetMainCategories()
        {
            var results = _ctx.MainCategories
                 .Include(c => c.Categories)
                 .ToList();
            return results;
        }

        public Category GetCategory(int id)
        {
            var category = _ctx.Categories.Find(id);
            return category;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public void UpdateEntity(object model)
        {
            _ctx.Update(model);
        }
    }
}
