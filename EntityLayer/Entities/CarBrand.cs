using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class CarBrand
    {
        [Key]
        [Display(Name = "Araba Marka ID")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        public int? CarBrandId { get; set; }


        [Display(Name = "Araba Marka Adı")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        public string? CarBrandName { get; set; }

        public string? CarBrandDescription { get; set;}

        public virtual List<Car>? Cars { get; set; }
    }
}
