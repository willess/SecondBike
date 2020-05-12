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
        public int AdvertisementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
