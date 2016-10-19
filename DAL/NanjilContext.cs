using Model;
using Model.Sales;
using Model.Transport;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NanjilContext : DbContext 
    {
        public NanjilContext() : base("DefaultConnection")
        { }

        #region Admin
        public DbSet<Brand> brands { get; set; }
        public DbSet<Types> types { get; set; }
        public DbSet<Commodity> commodities { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Slab> slabs { get; set; }
        public DbSet<Unit> units { get; set; }

        public DbSet<Country> countries { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<District> districts { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Route> routes { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Agency> agencies { get; set; }
        #endregion

        #region Sales
        public DbSet<BindSlab> bindSlabs { get; set; }
        #endregion

        #region Transport
        public DbSet<VehicleBrand> vehicleBrands { get; set; }
        public DbSet<VehicleModel> vehicleModels { get; set; }
        #endregion

    }
}
