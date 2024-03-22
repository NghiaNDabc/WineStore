namespace WineStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietGioHang")]
    public partial class ChiTietGioHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maSp { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string maGioHang { get; set; }

        public int soLuong { get; set; }

        public virtual SanPham SanPham { get; set; }

        public virtual GioHang GioHang { get; set; }
    }
}
