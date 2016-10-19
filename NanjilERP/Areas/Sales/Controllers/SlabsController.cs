using DAL;
using NanjilERP.Areas.Sales.Models;
using NanjilERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NanjilERP.Areas.Sales.Controllers
{
    public class SlabsController : Controller
    {
        private NanjilContext db = new NanjilContext();
        ApplicationDbContext appdb = new ApplicationDbContext();

        //#region Slab
        //public ActionResult SlabList()
        //{

        //    return View(db.slabs.ToList());
        //}

        //public ActionResult SlabCreate()
        //{
        //    return PartialView("_SlabCreate");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SlabCreate([Bind(Include = "SlabId,SlabName,Description,IsActive,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] Slab slab)
        //{
        //    slab.CreatedBy = 1;
        //    slab.ModifiedBy = 1;
        //    slab.CreatedDate = DateTime.Now;
        //    slab.ModifiedDate = DateTime.Now;
        //    if (ModelState.IsValid)
        //    {
        //        db.slabs.Add(slab);
        //        await db.SaveChangesAsync();
        //        return Json(new { success = true });
        //    }

        //    return PartialView("_SlabCreate", slab);
        //}

        ////public async Task<ActionResult> BrandEdit(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }
        ////    Brand brand = await db.brands.FindAsync(id);
        ////    if (brand == null)
        ////    {
        ////        return HttpNotFound();
        ////    }
        ////    return PartialView("_BrandEdit", brand);
        ////}
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> BrandEdit([Bind(Include = "BrandId,BrandName,TinNo,Description,IsActive")] Brand brand)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        db.Entry(brand).State = EntityState.Modified;
        ////        try
        ////        {
        ////            await db.SaveChangesAsync();
        ////        }
        ////        catch (Exception e)
        ////        {
        ////            string s = e.ToString();
        ////        }
        ////        return Json(new { success = true });
        ////    }
        ////    return PartialView("_BrandEdit", brand);
        ////}
        ////public ActionResult BrandDeactive(bool confirm, int? id)
        ////{
        ////    //System.Windows.Forms.MessageBox.Show("Test");
        ////    if (id == null)
        ////    {
        ////        // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }
        ////    Brand brand = db.brands.Find(id);
        ////    if (brand == null)
        ////    {
        ////        //return HttpNotFound();
        ////    }
        ////    brand.IsActive = false;
        ////    db.SaveChanges();
        ////    return RedirectToAction("BrandList");
        ////}

        //#endregion

        #region BindSlab
        public async Task<ActionResult> BindSlabList()
        {
            var list = from ag in db.agencies
                       join rt in db.routes on ag.RouteId equals rt.RouteId
                       select new BindSlabListVM { AgencyId = ag.AgencyId, AgencyCode = ag.AgencyCode, AgencyName= ag.AgencyName, IsActive=ag.IsActive, RouteName = rt.RouteName };
            return View(await list.ToListAsync());
        }

        public async Task<ActionResult> MapSlab(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BindSlab brand = await db.brands.FindAsync(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return PartialView("_BrandEdit", brand);
        }
        #endregion
    }
}