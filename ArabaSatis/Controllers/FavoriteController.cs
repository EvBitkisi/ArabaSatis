using BusinessLayer.ViewModels;
using DataAccsessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ArabaSatis.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriteController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var userId = HttpContext.Session.GetInt32("Id");
            var favoriteCars = _context.FavoriteCars
                .Where(f => f.UserId == userId)
                .Include(fc => fc.Car)
                .ToList();

            // FavoriteCarDetailsViewModel listesi oluşturuyoruz
            var viewModelList = new List<FavoriteCarDetailsViewModel>();

            // Her bir FavoriteCar nesnesini FavoriteCarDetailsViewModel'e dönüştürüyoruz
            foreach (var favoriteCar in favoriteCars)
            {
                var viewModel = new FavoriteCarDetailsViewModel
                {
                    Car = favoriteCar.Car,
                    FavoriteCar = favoriteCar
                };
                viewModelList.Add(viewModel);
            }

            // Favori arabaları view'e gönderiyoruz
            return View(viewModelList);
        }

        // Kullanıcı kimliğini almak için yardımcı metot
        private int? GetUserId()
        {
            return HttpContext.Session.GetInt32("Id");
        }

        [HttpPost]
        public IActionResult AddToFavorites(int carId)
        {
            // Kullanıcı kimliğini al
            int? userId = GetUserId();

            // Kullanıcının favorilerine eklemek istediği aracı bul
            var car = _context.Cars.FirstOrDefault(c => c.CarId == carId);

            // Favorilere eklemek istenen araç var mı kontrol et
            if (car != null && userId.HasValue)
            {
                // Kullanıcının favorilerine eklemek istediği araç zaten favorilerde mi kontrol et
                var existingFavorite = _context.FavoriteCars.FirstOrDefault(f => f.UserId == userId.Value && f.CarId == carId);

                if (existingFavorite == null)
                {
                    // Favorilere eklemek istenen aracı kullanıcının favorilerine ekle
                    var favoriteCar = new FavoriteCar { UserId = userId.Value, CarId = carId };
                    _context.FavoriteCars.Add(favoriteCar);
                    _context.SaveChanges();
                }
            }

            // Favorilere ekleme işlemi tamamlandıktan sonra, aracın ana sayfasına yönlendir
            return RedirectToAction("Index", "Home", new { id = carId });
        }



        // favori kaldırma
        [HttpPost]
        public IActionResult RemoveFromFavorites(int carId)
        {
            
            int? userId = GetUserId();

            
            var favoriteCar = _context.FavoriteCars.FirstOrDefault(f => f.UserId == userId && f.CarId == carId);

            if (favoriteCar != null)
            {
                // aracı kaldır
                _context.FavoriteCars.Remove(favoriteCar);
                _context.SaveChanges();
            }
            else if (favoriteCar == null)
            {

                Console.WriteLine("Favori kaydı bulunamadı.");
            }

            return RedirectToAction("Index", "Home");
        }


       

    }
}
