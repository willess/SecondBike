using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.Data.Entities
{
    public class Advertisement
    {
        public int AdvertisementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // subcategory
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // User
        public User User { get; set; }
    }
}
