using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class CarType
    {
        [Key]
        [Display(Name = "Araba Tip ID")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        public int? CarTypeId { get; set; }


        [Display(Name = "Araba Tip Adı")]
        [Required(ErrorMessage = "BOŞ BIRAKILAMAZ")]
        public string? CarTypeName { get; set; }

        public string? CarTypeFeatures { get; set; }

        public virtual List<Car>? Cars { get; set; }
    }
}
