using ArabaSatis.Models;
using DataAccsessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using BusinessLayer.ViewModels;

namespace ArabaPazarlama.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(string SearchString, List<int> CarBrandIds, List<int> SelectedYears ,List<int> SelectedSeats,string SelectedTypes)
        {
            var carList = _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarType)
                .ToList();

            ViewBag.SelectedCarBrandIds = CarBrandIds;
            ViewBag.SearchString = SearchString;
            ViewBag.SelectedYears = SelectedYears;
            ViewBag.SelectedSeats = SelectedSeats;
            ViewBag.SelectedType= SelectedTypes;

            

            if (!string.IsNullOrEmpty(SearchString))
            {
                carList = carList.Where(car => car.CarName.Contains(SearchString)).ToList();
            }

            if (SelectedYears != null && SelectedYears.Any())
            {
                carList = carList.Where(car => SelectedYears.Contains(car.Years ?? 0)).ToList();
            }

            if (SelectedSeats != null && SelectedSeats.Any())
            {
                carList = carList.Where(car => SelectedSeats.Contains(car.Seats ?? 0)).ToList();
            }

            if (CarBrandIds != null && CarBrandIds.Any())
            {
                carList = carList.Where(car => CarBrandIds.Contains(car.CarBrandId ?? 0)).ToList();
            }
            if (!string.IsNullOrEmpty(SelectedTypes))
            {
                carList = carList.Where(car => car.CarType.CarTypeName == SelectedTypes).ToList();
            }

            return View(carList);
        }



        public IActionResult Details(int id)
        {
            var carDetails = _context.Cars
                .Include(c => c.CarBrand)
                .Include(c => c.CarType)
                .SingleOrDefault(c => c.CarId == id);

            var favoriteCars = _context.FavoriteCars
                .Where(f => f.CarId == id)
                .ToList();

            var viewModel = new FavoriteCarDetailsViewModel
            {
                Car = carDetails,
                FavoriteCarList = favoriteCars
            };

            return View(viewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}