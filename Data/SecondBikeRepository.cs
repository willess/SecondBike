﻿using Microsoft.EntityFrameworkCore;
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
            return _ctx.Advertisements
                    .Include(c => c.Category)
                        .ThenInclude(m => m.MainCategory)
                        .ThenInclude(c => c.Categories)
                    .ToList();
        }

        public Advertisement GetAdvertisementById(int id)
        {
            return _ctx.Advertisements
                    .Include(c => c.Category)
                        .ThenInclude(m => m.MainCategory)
                        .ThenInclude(c => c.Categories)
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

        public IEnumerable<MainCategory> GetMainCategories()
        {
            var results = _ctx.MainCategories
                 .Include(c => c.Categories)
                 .ToList();
            return results;
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
    }
}
