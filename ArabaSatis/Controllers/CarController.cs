using DataAccsessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ArabaSatis.Controllers
{
    public class CarController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext _context;

        public CarController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var carList = _context.Cars
            .Include(c => c.CarBrand)
            .Include(c => c.CarType)
            .ToList();
            return View(carList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Car Gelen,IFormFile ImageUpload)
        {
            if (ModelState.IsValid)
            {
                if (ImageUpload != null && ImageUpload.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageUpload.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageUpload.CopyTo(stream);
                    }

                    Gelen.CarImage = uniqueFileName;
                }

                _context.Cars.Add(Gelen);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //if (ImageUpload!=null)
            //{
            //    var Uzantı=Path.GetExtension(ImageUpload.FileName);
            //    string isim=Guid.NewGuid().ToString();
            //    string yol = Path.Combine(Directory.GetCurrentDirectory()+"/wwwroot/Pictures/" + isim);
            //    using(var stream = new FileStream(yol,FileMode.Create))
            //    {
            //        ImageUpload.CopyTo(stream);
            //    }
            //    Gelen.CarImage = isim;

            //}
            if (ModelState.IsValid)
            {
                _context.Cars.Add(Gelen);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Car duzelt = _context.Cars.Where(u => u.CarId == id).FirstOrDefault();
            if (duzelt == null)
            {
                return NotFound();
            }
            return View(duzelt);
        }
        [HttpPost]
        public IActionResult Edit(Car Gelen,IFormFile ImageUpload) 
        {
            if (ModelState.IsValid && Gelen.CarId > 0)
            {
                if (ImageUpload != null && ImageUpload.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageUpload.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageUpload.CopyTo(stream);
                    }

                    Gelen.CarImage = uniqueFileName;
                }

                _context.Cars.Update(Gelen);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid && Gelen.CarId>0)
            {
                _context.Cars.Update(Gelen);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(Gelen);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Car sil = _context.Cars.Where(u => u.CarId == id).FirstOrDefault();
            if (sil == null)
            {
                return NotFound();
            }
            return View(sil);
        }
        [HttpPost]
        public IActionResult Delete(Car Gelen)
        {
            Car sil = _context.Cars.Where(u => u.CarId == Gelen.CarId).FirstOrDefault();
            if ( sil!=null)
            {
                _context.Cars.Remove(sil);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(Gelen);
        }
        

    }
}
