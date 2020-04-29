using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.Data.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string name { get; set; }

        // a Category can has more Advertisements
        public ICollection<Advertisement> Advertisements { get; set; }

        // a category has one Maincategory (but a maincategory can has more categories)
        public int MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }
    }
}
