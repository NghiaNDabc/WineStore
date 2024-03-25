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
    }
}