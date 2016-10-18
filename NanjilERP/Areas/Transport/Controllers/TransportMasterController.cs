using DAL;
using Microsoft.AspNet.Identity;
using Model.Transport;
using NanjilERP.Areas.Transport.Models;
using NanjilERP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public async Task<ActionResult> BrandCreate([Bind(Include = "VehicleBrandId,BrandName,IsActive")] VehicleBrand brand)
        {
           
            if (ModelState.IsValid)
            {
                db.vehicleBrands.Add(brand);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }

            return PartialView("_BrandCreate", brand);
        }

        public async Task<ActionResult> BrandEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleBrand brand = await db.vehicleBrands.FindAsync(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return PartialView("_BrandEdit", brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BrandEdit([Bind(Include = "VehicleBrandId,BrandName,IsActive")] VehicleBrand brand)
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
            VehicleBrand brand = db.vehicleBrands.Find(id);
            if (brand == null)
            {
                //return HttpNotFound();
            }
            brand.IsActive = false;
            db.SaveChanges();
            return RedirectToAction("BrandList");
        }

        #endregion

        #region VehicleModel
        public async Task<ActionResult> ModelList()
        {
            var modellist = from mdl in db.vehicleModels
                           join vbrd in db.vehicleBrands on mdl.VehicleBrandId equals vbrd.VehicleBrandId
                           select new ModelListVM { VehicleModelId = mdl.VehicleModelId,BrandName=vbrd.BrandName, ModelName = mdl.ModelName, IsActive = mdl.IsActive };
            return View(await modellist.ToListAsync());
        }
        public ActionResult ModelCreate()
        {

            ViewBag.Brands = new SelectList(db.vehicleBrands, "VehicleBrandId", "BrandName");
            return PartialView("_ModelCreate");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModelCreate([Bind(Include = "VehicleModelId,ModelName,VehicleBrandId,IsActive")] VehicleModel vmodel)
        {
           
            if (ModelState.IsValid)
            {
                db.vehicleModels.Add(vmodel);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            ViewBag.Brands = new SelectList(db.vehicleBrands, "VehicleBrandId", "BrandName");
            return PartialView("_ModelCreate", vmodel);
        }
        #endregion
    }
}