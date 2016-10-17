using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Route
    {
        public int RouteId { get; set; }
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        

    }
}
