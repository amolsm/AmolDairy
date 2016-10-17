using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Unit
    {
        public int UnitId { get; set; }

        [Required]
        [DisplayName("Unit Name")]
        public string UnitName { get; set; }

        [Required]
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
    }
}
