using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanjilERP.Areas.Sales.Models
{
    public class BindSlabListVM
    {
        public int AgencyId { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public string RouteName { get; set; }
        public bool IsActive { get; set; }
    }
}