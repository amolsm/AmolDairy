using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Transport
{
    public class VehicleBrand
    {
        public int VehicleBrandId { get; set; }
        [Required]
        public string BrandName { get; set; }
        public bool IsActive { get; set; }
    }
}
