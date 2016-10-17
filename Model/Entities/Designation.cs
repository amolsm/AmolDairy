using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Designation
    {
        public int DesignationId { get; set; }
        [Required]
        public string DesignationName { get; set; }
        
        public string Description { get; set; }
        
        public string Responsibility { get; set; }
    }
}
