using DataAccsessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ArabaSatis.Controllers
{
    public class CarBrandController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        public CarBrandController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var CarBrandList = _context.CarBrands.ToList();
            return View(CarBrandList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CarBrand Gelen)
        {
            _context.CarBrands.Add(Gelen);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            CarBrand duzelt = _context.CarBrands.Where(u => u.CarBrandId == id).FirstOrDefault();
            if (duzelt == null)
            {
                return NotFound();
            }
            return View(duzelt);
        }
        [HttpPost]
        public IActionResult Edit(CarBrand Gelen)
        {
            if (ModelState.IsValid && Gelen.CarBrandId > 0)
            {
                _context.CarBrands.Update(Gelen);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(Gelen);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            CarBrand sil = _context.CarBrands.Where(u => u.CarBrandId == id).FirstOrDefault();
            if (sil == null)
            {
                return NotFound();
            }
            return View(sil);
        }
        [HttpPost]
        public IActionResult Delete(CarBrand Gelen)
        {
            CarBrand sil = _context.CarBrands.Where(u => u.CarBrandId == Gelen.CarBrandId).FirstOrDefault();
            if (sil != null)
            {
                _context.CarBrands.Remove(sil);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(Gelen);
        }

    }
}
