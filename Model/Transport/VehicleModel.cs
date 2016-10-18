using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Transport
{
   public class VehicleModel
    {
        public int VehicleModelId { get; set; }
        [Required]
        public int VehicleBrandId { get; set; }
        [Required]
        public string ModelName { get; set; }
        public bool IsActive { get; set; }
    }
}
