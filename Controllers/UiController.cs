using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineStore.Models;
using PagedList;
using System.Net;
using System.Web.Routing;

namespace WineStore.Controllers
{
    public class UiController : Controller
    {
        // GET: Ui
        WineStoreContext db = new WineStoreContext();
        public ActionResult Index(int? page, String searchString)
        {
            var kh = db.KhachHangs.Where(s => s.tenDangNhap == "nghia" && s.matKhau == "123").SingleOrDefault();

            Session["user"] = kh;
            var sanPhams = db.SanPhams.Select(s => s);
            var copy = sanPhams;
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(p => p.tenSp.Contains(searchString.Trim()));
                if (sanPhams.ToList().Count == 0)
                {
                    sanPhams = copy.Where(p => p.DanhMuc.tenDanhMuc.Contains(searchString.Trim()));
                }
            }
            sanPhams = sanPhams.OrderBy(s => s.maDanhMuc);
            ViewBag.searchString = searchString;
            int pagesize = 9;
            int pageNumber = (page ?? 1);
            return View(sanPhams.ToPagedList(pageNumber, pagesize));
        }

        public ActionResult Details(int? maSp)
        {
            if (maSp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(maSp);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        [ChildActionOnly]
        public ActionResult Menu2()
        {
            var danhmuc = db.DanhMucs.Select(s => s);
            return PartialView(danhmuc.ToList());
        }
        
        [Route("danhmuc/{maDanhMuc?}")]
        public ActionResult ProductByCatagory(string maDanhMuc, int? page)
        {
            // Xử lý logic ở đây và trả về view
            var sanPhams = db.SanPhams.Where(p => p.maDanhMuc == maDanhMuc);
            sanPhams = sanPhams.OrderBy(s => s.maDanhMuc);
          
            int pagesize = 9;
            int pageNumber = (page ?? 1);
            return View(sanPhams.ToPagedList(pageNumber, pagesize));
        }



    }
}