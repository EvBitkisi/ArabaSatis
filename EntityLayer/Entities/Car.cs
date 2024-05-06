using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Display(Name ="Araba Adı")]
        [Required(ErrorMessage ="BOŞ BIRAKILAMAZ")]
        public string? CarName { get; set;}

        [Display(Name = "Araba Fiyatı")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        public int? CarPrice { get; set; }

        [Display(Name = "Araba Gücü")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        public int? CarHp { get; set; }

        [Display(Name = "Araba Km")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        public int? CarKm { get; set; }

        public string? CarImage { get; set; }

        public string? CarDescription { get; set; }


        [Display(Name = "Koltuk Sayısı")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        public int? Seats { get; set; }


        [Display(Name = "Araba Yılı")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        public int? Years { get; set; }


        [Display(Name = "Araba Tip ID")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        [ForeignKey("CarType")]
        public int? CarTypeId { get; set; }

        public virtual CarType? CarType { get; set; }


        [Display(Name = "Araba Marka ID")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        [ForeignKey("CarBrand")]
        public int? CarBrandId { get; set; }

        public virtual CarBrand? CarBrand { get; set; }

    
        [NotMapped]
        public IFormFile? ImageUpload { get; set; }
    }
}
