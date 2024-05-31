using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WineStore.Models;

namespace WineStore.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private WineStoreContext db = new WineStoreContext();

        // GET: Admin/SanPhams

       // [Authorize(Roles= "admin")]
        public ActionResult Index(int? page)
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc);
            sanPhams = sanPhams.OrderByDescending(s => s.maSp);
            int pageNumber = page ?? 1;
            int pagesize = 7;
            return View(sanPhams.ToPagedList(pageNumber,pagesize));
        }

        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.maDanhMuc = new SelectList(db.DanhMucs, "maDanhMuc", "tenDanhMuc");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Create([Bind(Include = "maSp,maDanhMuc,tenSp,moTa,giaNhap,giaBan,soLuong,image,khuVuc,Vintage")] SanPham sanPham)
        {
            //sanPham.maSp = db.SanPhams.OrderByDescending(s=>s.maSp).First().maSp+1;
            sanPham.maSp = db.SanPhams.OrderByDescending(s=>s.maSp).First().maSp+1;
            sanPham.image = "dd";
            if (ModelState.IsValid)
            {
                var f = Request.Files["imgfile"];
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Img/WineImages/" + fileName);
                    f.SaveAs(uploadPath);
                    sanPham.image = fileName;
                }
                db.SanPhams.Add(sanPham);
               
                db.SaveChanges();
                 return Json(new { success = true, message = "Thêm thành công." }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index");
            }

            ViewBag.maDanhMuc = new SelectList(db.DanhMucs, "maDanhMuc", "tenDanhMuc", sanPham.maDanhMuc);
            return Json(new { success = false, message ="Lỗi ko thêm đc" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.maDanhMuc = new SelectList(db.DanhMucs, "maDanhMuc", "tenDanhMuc", sanPham.maDanhMuc);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit([Bind(Include = "maSp,maDanhMuc,tenSp,moTa,giaNhap,giaBan,soLuong,image,khuVuc,Vintage")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["imgfile"];
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Img/WineImages/" + fileName);
                    f.SaveAs(uploadPath);
                    sanPham.image = fileName;
                }
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maDanhMuc = new SelectList(db.DanhMucs, "maDanhMuc", "tenDanhMuc", sanPham.maDanhMuc);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult DeleteList(List<int> selectedProducts)
        {
         ViewBag.sl = selectedProducts.Count;
            var sanPham = db.SanPhams.Where(s=> selectedProducts.Contains(s.maSp)).ToList();
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            db.SanPhams.RemoveRange(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
