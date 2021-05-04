using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSach.Models;


namespace QuanLyBanSach.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanSachDataContext data = new QuanLyBanSachDataContext();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }

        public ActionResult ThemGioHang(int MaSach, string strUrl)
        {
            List<GioHang> listGioHang = LayGioHang();

            GioHang sanPham = listGioHang.Find(n => n.MaSach == MaSach);

            if (sanPham == null)
            {
                sanPham = new GioHang(MaSach);
                listGioHang.Add(sanPham);
                //return Redirect(strUrl);
            }
            else
            {
                sanPham.SoLuong++;
            }

            return Redirect("/");
        }

        private int TongSoLuong()
        {
            int tongSoLuong = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;

            if (listGioHang != null)
            {
                tongSoLuong = listGioHang.Sum(n => n.SoLuong);
            }

            return tongSoLuong;
        }

        private double TongTien()
        {
            double tongTien = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;

            if (listGioHang != null)
            {
                tongTien = listGioHang.Sum(n => n.ThanhTien);
            }

            return tongTien;
        }

        public ActionResult Index()
        {
            List<GioHang> listGioHang = LayGioHang();

            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Sach");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(listGioHang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return PartialView();
        }

        public ActionResult MuaSau(int id)
        {
            List<GioHang> listGioHang = LayGioHang();

            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Sach");
            }

            GioHang sp = listGioHang.SingleOrDefault(n => n.MaSach == id);

            if (sp != null)
            {
                listGioHang.RemoveAll(n => n.MaSach == id);
            }

            return RedirectToAction("Index", "GioHang");
        }

        public ActionResult CapNhat(int id, FormCollection form)
        {
            List<GioHang> listGioHang = LayGioHang();

            GioHang sp = listGioHang.SingleOrDefault(n => n.MaSach == id);

            if (sp != null)
            {
                sp.SoLuong = int.Parse(form["SoLuong"].ToString());
            }

            return RedirectToAction("Index", "GioHang");
        }

        public ActionResult XoaGioHang()
        {
            List<GioHang> listGioHang = LayGioHang();

            listGioHang.Clear();

            return RedirectToAction("Index", "Sach");
        }

    }
}