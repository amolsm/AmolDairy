using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NanjilERP.Areas.Admin.Models
{
    public class TypeListVM
    {
        public int TypesId { get; set; }
        public string TypesName { get; set; }
        public string BrandName { get; set; }
        public bool IsActive { get; set; }
    }
}