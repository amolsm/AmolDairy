using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanjilERP.Areas.Admin.Models
{
    public class ProductListVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CommodityName { get; set; }
        public string TypeName { get; set; }
        public bool IsActive { get; set; }
    }
}