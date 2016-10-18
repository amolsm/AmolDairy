using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanjilERP.Areas.Transport.Models
{
    public class ModelListVM
    {

        public int VehicleModelId { get; set; }

        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public bool IsActive { get; set; }
    }
}