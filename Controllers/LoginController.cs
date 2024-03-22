using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineStore.Models;

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

        public ActionResult CheckLogin(string taikhoan, string matkhau)
        {
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