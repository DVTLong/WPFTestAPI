﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFTestAPI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLKIOSKEntities : DbContext
    {
        public QLKIOSKEntities()
            : base("name=QLKIOSKEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LoaiMatHang> LoaiMatHangs { get; set; }
        public virtual DbSet<v_api_BuoiAn> v_api_BuoiAn { get; set; }
        public virtual DbSet<v_api_ChiTietGiamGiaTheoSL> v_api_ChiTietGiamGiaTheoSL { get; set; }
        public virtual DbSet<v_api_DotKhuyenMai> v_api_DotKhuyenMai { get; set; }
        public virtual DbSet<v_api_HinhThucThanhToan> v_api_HinhThucThanhToan { get; set; }
        public virtual DbSet<v_api_LoaiMatHang> v_api_LoaiMatHang { get; set; }
        public virtual DbSet<v_api_MatHang> v_api_MatHang { get; set; }
        public virtual DbSet<v_api_QuangCao> v_api_QuangCao { get; set; }
    }
}
