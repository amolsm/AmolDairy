using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Brand
    {
        public int BrandId { get; set; }

        [Required]
        [StringLength(50)]
        
        [DisplayName("Brand Name")]
        
        public string BrandName { get; set; }

        [Required]
        public string TinNo { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
