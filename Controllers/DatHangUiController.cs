using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineStore.Models;
using static WineStore.Controllers.GioHangUiController;
namespace WineStore.Controllers
{
    public class DatHangUiController : Controller
    {
        // GET: DatHangUi

        WineStoreContext db = new WineStoreContext();
        public ActionResult Index(List<int> selectedProducts)
        {
            if (selectedProducts == null || !selectedProducts.Any())
            {
                // If no products selected, you can redirect or return a message
                return RedirectToAction("Index","DonHangUi");

            }
            var listSpMua = db.ChiTietGioHangs.Where(s => selectedProducts.Contains(s.maSp)).ToList();
          
            List<ProductViewModel> products = new List<ProductViewModel>();
            var query = from chiTiet in listSpMua
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
            products = query.ToList();
            ViewBag.products = products;
            TempData["products"] = products;
            var listThanhToan = db.HinhThucThanhToans.Select(s=>s).ToList();
            ViewBag.listThanhToan = listThanhToan;
            return View();
        }
        public ActionResult DatHang(string diaChiGiao,string hoTenNguoiNhan,string sdtNguoiNhan, string emailNguoiNhan, string maHinhThucThanhToan)
        {
            DonHang dh = new DonHang();
            string maxId = db.DonHangs.Max(d => d.maDonHang);
            // Tạo mã đơn hàng mới bằng cách thêm 1 vào số đơn hàng lớn nhất và kết hợp với chuỗi "DH"
            int stt = (int.Parse(maxId.Substring(2)) + 1);
            string newId = "DH" +stt.ToString() ;
            
            dh.maDonHang = newId;
            dh.ngayDatHang = DateTime.Now.Date;
            dh.hoTenNguoiNhan = hoTenNguoiNhan;
            dh.ngayGiaoHang = DateTime.Now.Date.AddDays(3);
            dh.diaChiGiao = diaChiGiao;
            dh.sdtNguoiNhan = sdtNguoiNhan;
            dh.emailNguoiNhan = emailNguoiNhan;
            dh.maHinhThucThanhToan = maHinhThucThanhToan;
            KhachHang kh = (KhachHang)Session["user"];
            dh.maKhacHang = kh.maKhacHang;
            db.DonHangs.Add(dh);
            
            List<ProductViewModel> list =  (List<ProductViewModel>) TempData["products"];
          


            foreach (var i in list)
            {
                ChiTietDonHang ctDH = new ChiTietDonHang();
                ctDH.maDonHang = newId;
                ctDH.maSp = i.maSp;
                ctDH.soLuongMua = i.soLuong;
                db.ChiTietDonHangs.Add(ctDH);
            }
            //xoa gio hang
            var maGH = db.GioHangs.Where(s => s.maKhacHang == kh.maKhacHang).SingleOrDefault().maGioHang;
            var DeleteList = 
                             from sp2 in list
                             join sp in db.ChiTietGioHangs on sp2.maSp equals sp.maSp
                             where sp.maGioHang == maGH
                             select sp;
            db.ChiTietGioHangs.RemoveRange(DeleteList.ToList());
            db.SaveChanges();
            return View();
        }


    }
}