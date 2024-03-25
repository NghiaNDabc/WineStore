namespace WineStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
            GioHangs = new HashSet<GioHang>();
        }

        [Key]
        [StringLength(10)]
        
        public string maKhacHang { get; set; }

        [Required(ErrorMessage = "Fullname is required")]
        [StringLength(50)]
        public string hoTen { get; set; }

        
        [StringLength(15)]
        public string tenDangNhap { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20)]
        public string matKhau { get; set; }

        [StringLength(15)]
        public string sdt { get; set; }

        [StringLength(100)]
        public string diaChi { get; set; }

        [StringLength(25)]
        public string email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang> DonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
}
