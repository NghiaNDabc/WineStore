namespace WineStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietGioHangs = new HashSet<ChiTietGioHang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Mã sản phẩm")]
        public int maSp { get; set; }

        [Required]
        [StringLength(10)]
        public string maDanhMuc { get; set; }

       
        [StringLength(50)]
        [DisplayName("Tên sản phẩm")]
        public string tenSp { get; set; }

        [Column(TypeName = "text")]
        [DisplayName("Mô tả")]
        public string moTa { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Giá nhập")]
        public decimal giaNhap { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Giá bán")]
        public decimal giaBan { get; set; }
        [DisplayName("Số lượng")]

        public int soLuong { get; set; }

        [Column(TypeName = "text")]

        public string image { get; set; }

        [StringLength(100)]
        public string khuVuc { get; set; }

        [StringLength(20)]
        public string Vintage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
