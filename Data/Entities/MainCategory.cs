using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.Data.Entities
{
    public class MainCategory
    {
        public int MainCategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
