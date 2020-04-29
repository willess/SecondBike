using System.Collections.Generic;
using SecondBike.Data.Entities;

namespace SecondBike.Data
{
    public interface ISecondBikeRepository
    {
        IEnumerable<Advertisement> GetAllAdvertisements();
        Advertisement GetAdvertisementById(int id);

        IEnumerable<MainCategory> GetMainCategories();
        IEnumerable<Advertisement> GetAdvertisementsByCategory();
        bool SaveAll();
    }
}