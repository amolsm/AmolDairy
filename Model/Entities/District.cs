using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class District
    {
        public int DistrictId { get; set; }
        [Required]
        public string DistrictName { get; set; }
        [Required]
        public int StateId { get; set; }
        public bool IsActive { get; set; }
    }
}
