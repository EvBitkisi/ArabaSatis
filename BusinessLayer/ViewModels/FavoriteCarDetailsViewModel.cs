using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModels
{
    public class FavoriteCarDetailsViewModel
    {
        public Car Car { get; set; }

        public FavoriteCar FavoriteCar { get; set; }

        public List<FavoriteCar> FavoriteCarList { get; set; }

    }
}
