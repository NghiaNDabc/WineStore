using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineStore.Models;

namespace WineStore.Controllers
{
    public class GioHangUiController : Controller
    {
        // GET: GioHangUi

        WineStoreContext db = new WineStoreContext();

        public class ProductViewModel
        {
            public int maSp { get; set; }
            public string tenDanhMuc { get; set; }
            public string tenSp { get; set; }
            public string moTa { get; set; }
            public decimal giaNhap { get; set; }
            public decimal giaBan { get; set; }
            public int soLuong { get; set; }
            public string image { get; set; }
            public string khuVuc { get; set; }
           
        }

        public ActionResult Index()
        {
            KhachHang kh = (KhachHang)Session["user"];
            var maKH = kh.maKhacHang;
            var maGH = db.GioHangs.Where(s => s.maKhacHang == maKH).SingleOrDefault().maGioHang;
            List<SanPham> li = new List<SanPham>();

            List<ChiTietGioHang> chitietgiohang = db.ChiTietGioHangs.Where(s => s.maGioHang == maGH).ToList();
            foreach(ChiTietGioHang chiTiet in chitietgiohang)
            {
                SanPham sp = db.SanPhams.Where(s => s.maSp == chiTiet.maSp).SingleOrDefault();
                li.Add(sp);
            }
            var query = from chiTiet in db.ChiTietGioHangs
                        join sanPham in db.SanPhams on chiTiet.maSp equals sanPham.maSp
                        where chiTiet.maGioHang == maGH
                        select new ProductViewModel
                        {
                            maSp = sanPham.maSp,
                             tenDanhMuc = sanPham.DanhMuc.tenDanhMuc, // Sử dụng mối quan hệ để truy cập thuộc tính của DanhMuc
                            tenSp = sanPham.tenSp,
                            moTa = sanPham.moTa,
                            giaNhap = sanPham.giaNhap,
                            giaBan = sanPham.giaBan,
                            image = sanPham.image,
                            khuVuc = sanPham.khuVuc,
                            soLuong = chiTiet.soLuong
                        };

            List<ProductViewModel> results =(List < ProductViewModel >) query.ToList();
            return View(results);
        }

        public ActionResult AddToCart(int id,int quantity)
        {
            if (Session["user"] != null)
            {
                KhachHang kh = (KhachHang)Session["user"];
                ChiTietGioHang gh = new ChiTietGioHang();
                String maGH = db.GioHangs.Where(s => s.maKhacHang == kh.maKhacHang).SingleOrDefault().maGioHang;
                ChiTietGioHang check  = db.ChiTietGioHangs.Where(s=>s.maGioHang == maGH && s.maSp == id).SingleOrDefault();
                if(check != null)
                {
                    check.soLuong += quantity;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Ui");
                }
                gh.maGioHang = maGH;
                gh.maSp = id;
                gh.soLuong = 1;
                db.ChiTietGioHangs.Add(gh);
                db.SaveChanges();
                return RedirectToAction("Index","Ui");
            }
            return RedirectToAction("Index", "Ui");
        }
    }
}