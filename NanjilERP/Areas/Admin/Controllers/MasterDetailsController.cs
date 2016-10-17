using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Model;
using System.Net;
using System.Data.Entity;
using System.Threading.Tasks;
using NanjilERP.Models;
using Microsoft.AspNet.Identity;
using NanjilERP.Areas.Admin.Models;

namespace NanjilERP.Areas.Admin.Controllers
{
    public class MasterDetailsController : Controller
    {
        private NanjilContext db = new NanjilContext();
        ApplicationDbContext appdb = new ApplicationDbContext();
        // GET: Admin/MasterDetails
        
        public ActionResult Index()
        {
            
            return View();
        }

        #region Unit
        public async Task<ActionResult> UnitList()
        {
            return View(await db.units.ToListAsync());
        }

        public ActionResult UnitCreate()
        {
            return PartialView("_UnitCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UnitCreate([Bind(Include = "UnitId,UnitName,IsActive")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.units.Add(unit);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_UnitCreate", unit);
        }

        public ActionResult EditUnit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUnit([Bind(Include = "UnitId,UnitName,IsActive")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UnitsLists");
            }
            return View(unit);
        }

        public ActionResult DeactiveUnit(bool confirm, int? id)
        {
            //System.Windows.Forms.MessageBox.Show("Test");
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.units.Find(id);
            if (unit == null)
            {
                //return HttpNotFound();
            }
            unit.IsActive = false;
            db.SaveChanges();
            return RedirectToAction("UnitsLists");
        }


        #endregion

        #region Brand
        public async Task<ActionResult> BrandList()
        {
            return View(await db.brands.ToListAsync());
        }

        public ActionResult BrandCreate()
        {
            return PartialView("_BrandCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BrandCreate([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.brands.Add(brand);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_BrandCreate",brand);
        }
        
        public async Task<ActionResult> BrandEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = await db.brands.FindAsync(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return PartialView("_BrandEdit", brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    string s = e.ToString();
                }
                return Json(new { success = true });
            }
            return PartialView("_BrandEdit", brand);
        }
        public ActionResult BrandDeactive(bool confirm, int? id)
        {
            //System.Windows.Forms.MessageBox.Show("Test");
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.brands.Find(id);
            if (brand == null)
            {
                //return HttpNotFound();
            }
            brand.IsActive = false;
            db.SaveChanges();
            return RedirectToAction("BrandList");
        }

        #endregion

        #region Type
        public async Task<ActionResult> TypesList()
        {
            var typelist = from typ in db.types
                           join brnd in db.brands on typ.BrandId equals brnd.BrandId
                           select new TypeListVM { TypesId = typ.TypesId, BrandName = brnd.BrandName, TypesName = typ.TypesName, IsActive = typ.IsActive };
            return View(await typelist.ToListAsync());
        }

        public ActionResult TypesCreate()
        {

            ViewBag.Brands = new SelectList(db.brands, "BrandId", "BrandName");
            return PartialView("_TypesCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TypesCreate([Bind(Include = "TypesId,TypesName,BrandId,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,IsActive")] Types type)
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser currentUser = appdb.Users.FirstOrDefault(x => x.Id == userId);
           
            type.ModifiedDate = DateTime.Now;
            type.CreatedDate = DateTime.Now;
            type.CreatedBy = currentUser.EmployeeId;
            type.ModifiedBy = currentUser.EmployeeId;
            if (ModelState.IsValid)
            {
                db.types.Add(type);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            ViewBag.Brands = new SelectList(db.brands, "BrandId", "BrandName");
            return PartialView("_TypesCreate", type);
        }

        public async Task<ActionResult> TypesEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Types type = await db.types.FindAsync(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return PartialView("_TypesEdit", type);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region Commodity
        public async Task<ActionResult> CommodityList()
        {
            var list = from cmd in db.commodities
                        join tp in db.types on cmd.TypesId equals tp.TypesId
                        select new CommodityListVM { CommodityId = cmd.CommodityId, CommodityName = cmd.CommodityName, TypesName = tp.TypesName, IsActive = cmd.IsActive };
            return View(await list.ToListAsync());
        }

        public ActionResult CommodityCreate()
        {
            ViewBag.Types = new SelectList(db.types, "TypesId", "TypesName");
            return PartialView("_CommodityCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CommodityCreate([Bind(Include = "CommodityId,CommodityName,TypesId,IsActive,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Commodity commodity)
        {
            commodity.CreatedBy = 1;
            commodity.ModifiedBy = 1;
            commodity.CreatedDate = DateTime.Now;
            commodity.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.commodities.Add(commodity);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_CommodityCreate", commodity);
        }

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region Product
        public  ActionResult ProductList()
        {
            //ProductListVM listVM = new ProductListVM();
            var vm = from pd in db.products
                     join cmd in db.commodities on pd.CommodityId equals cmd.CommodityId
                     join tp in db.types on pd.TypesId equals tp.TypesId
                     orderby tp.TypesId
                     select new ProductListVM { ProductId = pd.ProductId, ProductName = pd.ProductName, CommodityName = cmd.CommodityName, IsActive = pd.IsActive, TypeName = tp.TypesName };
            return View(vm);
        }

        public ActionResult ProductCreate()
        {
            return PartialView("_ProductCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductCreate([Bind(Include = "ProductId,ProductName,TypesId,CommodityId,IsActive,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Product product)
        {
            product.CreatedBy = 1;
            product.ModifiedBy = 1;
            product.CreatedDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_ProductCreate", product);
        }

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region Slab
        public ActionResult SlabList()
        {
            
            return View(db.slabs.ToList());
        }

        public ActionResult SlabCreate()
        {
            return PartialView("_SlabCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SlabCreate([Bind(Include = "SlabId,SlabName,Description,IsActive,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Slab slab)
        {
            slab.CreatedBy = 1;
            slab.ModifiedBy = 1;
            slab.CreatedDate = DateTime.Now;
            slab.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.slabs.Add(slab);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_SlabCreate", slab);
        }

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region Employee
        public ActionResult EmployeeList()
        {

            return View(db.employees.ToList());
        }

        public ActionResult EmployeeCreate()
        {
            return PartialView("_EmployeeCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EmployeeCreate([Bind(Include = "EmployeeId,EmployeeName,EmployeeCode,IsActive")] Employee emp)
        {
            //product.CreatedBy = 1;
            //product.ModifiedBy = 1;
            //product.CreatedDate = DateTime.Now;
            //product.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.employees.Add(emp);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_EmployeeCreate", emp);
        }

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region Agency
        public ActionResult AgencyList()
        {

            return View(db.agencies.ToList());
        }

        public ActionResult AgencyCreate()
        {
            return PartialView("_AgencyCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AgencyCreate([Bind(Include = "AgencyId,AgencyName,AgencyCode,RouteId,IsActive")] Agency agency)
        {
            //product.CreatedBy = 1;
            //product.ModifiedBy = 1;
            //product.CreatedDate = DateTime.Now;
            //product.ModifiedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.agencies.Add(agency);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_AgencyCreate", agency);
        }

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region Route
        public async Task<ActionResult> RouteList()
        {
            return View(await db.routes.ToListAsync());
        }

        public ActionResult RouteCreate()
        {
            return PartialView("_RouteCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RouteCreate([Bind(Include = "RouteId,RouteName,RouteCode,Description,IsActive")] Route route)
        {
            if (ModelState.IsValid)
            {
                db.routes.Add(route);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_RouteCreate", route);
        }

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region Department
        public async Task<ActionResult> DepartmentList()
        {
            return View(await db.departments.ToListAsync());
        }

        //public ActionResult BrandCreate()
        //{
        //    return PartialView("_BrandCreate");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandCreate([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.brands.Add(brand);
        //        await db.SaveChangesAsync();
        //        return Json(new { success = true });
        //    }

        //    return PartialView("_BrandCreate", brand);
        //}

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region Designation
        //public async Task<ActionResult> CountryList()
        //{
        //    return View(await db.brands.ToListAsync());
        //}

        //public ActionResult BrandCreate()
        //{
        //    return PartialView("_BrandCreate");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandCreate([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.brands.Add(brand);
        //        await db.SaveChangesAsync();
        //        return Json(new { success = true });
        //    }

        //    return PartialView("_BrandCreate", brand);
        //}

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region Country
        public async Task<ActionResult> CountryList()
        {
            return View(await db.countries.ToListAsync());
        }

        public ActionResult CountryCreate()
        {
            return PartialView("_CountryCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CountryCreate([Bind(Include = "CountryId,CountryName,IsActive")] Country country)
        {
            if (ModelState.IsValid)
            {
                db.countries.Add(country);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_CountryCreate", country);
        }

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region District
        public async Task<ActionResult> DistrictList()
        {
            return View(await db.districts.ToListAsync());
        }

        //public ActionResult BrandCreate()
        //{
        //    return PartialView("_BrandCreate");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandCreate([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.brands.Add(brand);
        //        await db.SaveChangesAsync();
        //        return Json(new { success = true });
        //    }

        //    return PartialView("_BrandCreate", brand);
        //}

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion

        #region State
        public async Task<ActionResult> StateList()
        {
            return View(await db.states.ToListAsync());
        }

        public ActionResult StateCreate()
        {
            return PartialView("_StateCreate");
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandCreate([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.brands.Add(brand);
        //        await db.SaveChangesAsync();
        //        return Json(new { success = true });
        //    }

        //    return PartialView("_BrandCreate", brand);
        //}

        //public async Task<ActionResult> BrandEdit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = await db.brands.FindAsync(id);
        //    if (brand == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(brand).State = EntityState.Modified;
        //        try
        //        {
        //            await db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            string s = e.ToString();
        //        }
        //        return Json(new { success = true });
        //    }
        //    return PartialView("_BrandEdit", brand);
        //}
        //public ActionResult BrandDeactive(bool confirm, int? id)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Test");
        //    if (id == null)
        //    {
        //        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Brand brand = db.brands.Find(id);
        //    if (brand == null)
        //    {
        //        //return HttpNotFound();
        //    }
        //    brand.IsActive = false;
        //    db.SaveChanges();
        //    return RedirectToAction("BrandList");
        //}

        #endregion


    }
}