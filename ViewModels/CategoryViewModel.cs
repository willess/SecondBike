using SecondBike.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.Data.Entities
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // a category has one Maincategory (but a maincategory can has more categories)
        public int MainCategoryId { get; set; }
        public MainCategoryViewModel MainCategory { get; set; }
    }
}
