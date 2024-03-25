using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineStore.Models;

namespace WineStore.Controllers
{
    public class UiController : Controller
    {
        // GET: Ui
        WineStoreContext db =  new WineStoreContext();  
        public ActionResult Index()
        {
            var kh = db.KhachHangs.Where(s => s.tenDangNhap == "nghia" && s.matKhau == "123").SingleOrDefault();
            
                Session["user"] = kh;
                var sanPhams = db.SanPhams.Select(s=>s);
            return View(sanPhams.ToList());
        }
    }
}