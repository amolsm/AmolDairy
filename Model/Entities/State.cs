using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class State
    {
        
        public int StateId { get; set; }
        [Required]
        public string StateName { get; set; }
        public bool IsActive { get; set; }
        public int CountryId { get; set; }
    }
}
