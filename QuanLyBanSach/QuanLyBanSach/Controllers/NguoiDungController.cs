using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Controllers
{
    public class NguoiDungController : Controller
    {
        private QuanLyBanSachDataContext data = new QuanLyBanSachDataContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoTen = collection["HoTen"];
            var taiKhoan = collection["TaiKhoan"];
            var matKhau = collection["MatKhau"];
            var matKhauNhapLai = collection["MatKhauNhapLai"];
            var email = collection["Email"];
            var diaChiKH = collection["DiaChiKH"];
            var dienThoaiKH = collection["DienThoaiKH"];
            var ngaySinh = collection["NgaySinh"];

            if (String.IsNullOrEmpty(hoTen))
            {
                ViewData["LoiHoTen"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(taiKhoan))
            {
                ViewData["LoiTaiKhoan"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["LoiMatKhau"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(matKhauNhapLai))
            {
                ViewData["LoiMatKhauNhapLai"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["LoiEmail"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(diaChiKH))
            {
                ViewData["LoiDiaChiKH"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(dienThoaiKH))
            {
                ViewData["LoiDienThoaiKH"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(ngaySinh))
            {
                ViewData["LoiNgaySinh"] = "Không được để trống :<";
            }
            else
            {
                kh.HoTen = hoTen;
                kh.TaiKhoan = taiKhoan;
                kh.MatKhau = matKhau;
                kh.Email = email;
                kh.DiaChiKH = diaChiKH;
                kh.DienThoaiKH = dienThoaiKH;
                kh.NgaySinh = DateTime.Parse(ngaySinh);

                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();

                return RedirectToAction("DangNhap", "NguoiDung");
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var taiKhoan = collection["TaiKhoan"];
            var matKhau = collection["MatKhau"];

            if (String.IsNullOrEmpty(taiKhoan))
            {
                ViewData["LoiTaiKhoan"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["LoiMatKhau"] = "Không được để trống :<";
            }
            else
            {
                KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.TaiKhoan == taiKhoan && n.MatKhau == matKhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    return Redirect("/");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        public ActionResult NguoiDung()
        {
            var kh = Session["TaiKhoan"];

            ViewBag.ThongBao = kh != null ? "Đăng nhập thành công" : "";

            return PartialView(kh);
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;

            return RedirectToAction("Index", "Sach");
        }
    }
}