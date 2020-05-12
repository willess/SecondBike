using AutoMapper;
using SecondBike.Data.Entities;
using SecondBike.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecondBike.Data
{
    public class SecondBikeMappingProfile : Profile
    {
        public SecondBikeMappingProfile()
        {
            CreateMap<Advertisement, AdvertisementViewModel>()
                .ReverseMap();

            CreateMap<Category, CategoryViewModel>()
                .ReverseMap();

            CreateMap<MainCategory, MainCategoryViewModel>()
                .ReverseMap();

            CreateMap<User, UserProfileViewModel>()
                .ReverseMap();
        }
    }
}
