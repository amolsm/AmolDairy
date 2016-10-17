using DAL;
using Model.Transport;
using NanjilERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NanjilERP.Areas.Transport.Controllers
{
    public class TransportMasterController : Controller
    {
        private NanjilContext db = new NanjilContext();
        ApplicationDbContext appdb = new ApplicationDbContext();


        #region VehicleBrand
        public async Task<ActionResult> BrandList()
        {
            return View(await db.vehicleBrands.ToListAsync());
        }

        public ActionResult BrandCreate()
        {
            return PartialView("_BrandCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BrandCreate([Bind(Include = "BrandId,BrandName,IsActive")] VehicleBrand brand)
        {
            if (ModelState.IsValid)
            {
                db.vehicleBrands.Add(brand);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_BrandCreate", brand);
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
    }
}