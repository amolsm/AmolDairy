using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanjilERP.Areas.Admin.Models
{
    public class CommodityListVM
    {
        public int CommodityId { get; set; }
        public string CommodityName { get; set; }
        public string TypesName { get; set; }
        public bool IsActive { get; set; }
    }
}