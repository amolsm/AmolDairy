using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Sales
{
    public class BindSlab
    {
        public int BindSlabId { get; set; }
        [Required]
        public int AgencyId { get; set; }
        [Required]
        public int TypeId { get; set; }
        [Required]
        public int SlabId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
