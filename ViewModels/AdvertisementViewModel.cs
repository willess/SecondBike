using SecondBike.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.ViewModels
{
    public class AdvertisementViewModel
    {
        [Required]
        public int AdvertisementId { get; set; }
        [Required]
        [MinLength(10)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }

    }
}
