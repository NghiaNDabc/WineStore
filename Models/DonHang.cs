namespace WineStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        [StringLength(10)]
        public string maDonHang { get; set; }

        public DateTime? ngayDatHang { get; set; }

        [StringLength(50)]
        public string diaChiGiao { get; set; }

        public DateTime? ngayGiaoHang { get; set; }

        [Column(TypeName = "text")]
        public string ghiChu { get; set; }

        [StringLength(40)]
        public string tinhTrangThanhToan { get; set; }

        [StringLength(40)]
        public string tinhTrangGiaoHang { get; set; }

        [Required]
        [StringLength(10)]
        public string maKhacHang { get; set; }

        [Required]
        [StringLength(10)]
        public string maHinhThucThanhToan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual HinhThucThanhToan HinhThucThanhToan { get; set; }
    }
}
