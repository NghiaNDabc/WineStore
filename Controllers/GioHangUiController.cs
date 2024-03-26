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
            if (kh != null)
            {
                var maKH = kh.maKhacHang;
                var maGH = db.GioHangs.Where(s => s.maKhacHang == maKH).SingleOrDefault().maGioHang;
                //List<SanPham> li = new List<SanPham>();

                //List<ChiTietGioHang> chitietgiohang = db.ChiTietGioHangs.Where(s => s.maGioHang == maGH).ToList();
                //foreach (ChiTietGioHang chiTiet in chitietgiohang)
                //{
                //    SanPham sp = db.SanPhams.Where(s => s.maSp == chiTiet.maSp).SingleOrDefault();
                //    li.Add(sp);
                //}
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

                List<ProductViewModel> results = (List<ProductViewModel>)query.ToList();
                Session["GioHang"] = results;
               // if(results!=null)
                return View(results);
                
            }
            List<ChiTietGioHang> ctgioHang=(List < ChiTietGioHang > )Session["GioHang"];
            if (Session["GioHang"] == null) ctgioHang = new List<ChiTietGioHang>();
            var query2 = from chiTiet in ctgioHang
                         join sanPham in db.SanPhams on chiTiet.maSp equals sanPham.maSp
                        
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

            List<ProductViewModel> results2 = (List<ProductViewModel>)query2.ToList();
            if (results2 != null)
                return View(results2);
            ViewBag.Notification = "Your cart is empty.";
            return View(new List<ProductViewModel>());
            
        }
       
        public ActionResult Delete(List<int> selectedProducts)
        {
            if (selectedProducts == null || !selectedProducts.Any())
            {
                // If no products selected, you can redirect or return a message
                return RedirectToAction("Index");

            }
            var list = db.ChiTietGioHangs.Where(s=>selectedProducts.Contains(s.maSp)).ToList();
            db.ChiTietGioHangs.RemoveRange(list);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PlaceOrder(List<int> selectedProducts)
        {
            if (selectedProducts == null || !selectedProducts.Any())
            {
                // If no products selected, you can redirect or return a message
                return RedirectToAction("Index");

            }
            var list = db.ChiTietGioHangs.Where(s => selectedProducts.Contains(s.maSp)).ToList();
            db.ChiTietGioHangs.RemoveRange(list);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            List<ChiTietGioHang> ctgioHang =(List<ChiTietGioHang>)Session["GioHang"];
            String maGH2 = "1";
            ChiTietGioHang check2 =  null;
            if (Session["GioHang"]!= null)
            {
                check2 = ctgioHang.Where(s => s.maSp == id).SingleOrDefault();
            }
            else
            {
                ctgioHang = new List<ChiTietGioHang>();
                
            }
            if(check2 != null)
            {
                check2.soLuong += quantity;
            }
            else
            {
                ctgioHang.Add(new ChiTietGioHang
                {
                    maGioHang = maGH2,
                    maSp = id,
                    soLuong = quantity
                });
            }
            Session["GioHang"] = ctgioHang;
            //String maGH = gioHang.Where(s => s.maKhacHang == kh.maKhacHang).SingleOrDefault().maGioHang;

            return RedirectToAction("Index", "Ui");
        }
    }
}