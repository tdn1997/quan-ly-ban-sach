using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Models
{
    public class GioHang
    {
        QuanLyBanSachDataContext data = new QuanLyBanSachDataContext();

        public GioHang(int MaSach)
        {
            this.MaSach = MaSach;
            Sach sach = data.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            TenSach = sach.TenSach;
            AnhBia = sach.AnhBia;
            DonGia = Double.Parse(sach.GiaBan.ToString());
            SoLuong = 1;

        }

        public int MaSach
        {
            set;
            get;
        }
        public string TenSach
        {
            set;
            get;
        }
        public string AnhBia
        {
            set;
            get;
        }
        public Double DonGia
        {
            set;
            get;
        }
        public int SoLuong
        {
            set;
            get;
        }
        public Double ThanhTien
        {
            get
            {
                return SoLuong * DonGia;
            }
        }
    }
}