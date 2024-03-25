using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WineStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;
using Microsoft.Ajax.Utilities;

namespace WineStore.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        WineStoreContext db = new WineStoreContext();
      
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        public ActionResult PhanQuyen()
        {
            FormsAuthentication.SetAuthCookie("admin", false);
            return RedirectToAction("Index", "SanPhams", new { area = "Admin" });
        }
        public ActionResult CheckLogin(string taikhoan, string matkhau)
        {
            var admin = db.Admins.Where(s => s.taiKhoan == taikhoan && s.matKhau == matkhau).SingleOrDefault();
            if(admin != null)
            {
              
               
                return RedirectToAction("Index", "SanPhams", new { area = "Admin" });
                
            }
            var kh = db.KhachHangs.Where(s => s.tenDangNhap == taikhoan && s.matKhau == matkhau).SingleOrDefault();
            if (kh!=null){
                Session["user"] = kh;
                ViewBag.hoTen = kh.hoTen.ToString();
                return RedirectToAction("Index","Ui");
            }
            return RedirectToAction("Index", "Login");

        }
        public ActionResult DangKy()
        {
            
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Include = "maKhacHang,hoTen,tenDangNhap,matKhau,sdt,diaChi,email")] KhachHang kh)
        {
            if (db.KhachHangs.Any(s => s.maKhacHang == kh.maKhacHang))
            {
                ViewBag.tenDangNhap = "Username is already exists";
                return View(kh);
            }
                if (ModelState.IsValid)
            {
                kh.tenDangNhap = kh.maKhacHang;
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.err = "Khong them dc";
            return View(kh);
        }
    }
}