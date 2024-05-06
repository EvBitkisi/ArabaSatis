using DataAccsessLayer.Data;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArabaSatis.Controllers
{
    public class CarTypeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;
        public CarTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var carTypeList = _context.CarTypes.ToList();
            return View(carTypeList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CarType Gelen)
        {
            _context.CarTypes.Add(Gelen);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CarType duzelt = _context.CarTypes.Where(u => u.CarTypeId == id).FirstOrDefault();
            if (duzelt == null)
            {
                return NotFound();
            }
            return View(duzelt);
        }

        [HttpPost]
        public IActionResult Edit(CarType Gelen)
        {
            if (ModelState.IsValid && Gelen.CarTypeId > 0)
            {
                _context.CarTypes.Update(Gelen);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(Gelen);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            CarType sil = _context.CarTypes.Where(u => u.CarTypeId == id).FirstOrDefault();
            if (sil == null)
            {
                return NotFound();
            }
            return View(sil);
        }
        [HttpPost]
        public IActionResult Delete(CarType Gelen)
        {
            CarType sil = _context.CarTypes.Where(u => u.CarTypeId == Gelen.CarTypeId).FirstOrDefault();
            if (sil != null)
            {
                _context.CarTypes.Remove(sil);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(Gelen);
        }
    }
}
