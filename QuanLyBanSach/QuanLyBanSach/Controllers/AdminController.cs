using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSach.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace QuanLyBanSach.Controllers
{
    public class AdminController : Controller
    {
        private QuanLyBanSachDataContext data = new QuanLyBanSachDataContext();

        public ActionResult Index()
        {
            var kh = Session["TaiKhoanAdmin"];

            if (kh == null)
            {
                return RedirectToAction("Index", "Sach");
            }

            return View(kh);
        }

        public ActionResult DangNhap()
        {
            var kh = Session["TaiKhoanAdmin"];

            if (kh != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var taiKhoan = collection["TaiKhoan"];
            var matKhau = collection["MatKhau"];

            if (taiKhoan == null)
            {
                ViewData["LoiTaiKhoan"] = "Chưa nhập tài khoản";
            }
            else if (matKhau == null)
            {
                ViewData["LoiMatKhau"] = "Chưa nhập mật khẩu";
            }
            else
            {
                Admin admin = data.Admins.SingleOrDefault(n => n.TaiKhoan == taiKhoan && n.MatKhau == matKhau);
                if (admin != null)
                {
                    Session["TaiKhoanAdmin"] = admin;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Sach(FormCollection collection, Sach sach, HttpPostedFileBase fileAnhBia, int? trang)
        {
            var kh = Session["TaiKhoanAdmin"];

            if (kh == null)
            {
                return RedirectToAction("Index", "Sach");
            }

            if (fileAnhBia != null)
            {
                var tenFile = Path.GetFileName(fileAnhBia.FileName);
                var path = Path.Combine(Server.MapPath("~/assets"), tenFile);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileAnhBia.SaveAs(path);
                }

            }


            var tenSach = collection["tenSach"];
            var giaBan = collection["giaBan"];
            var moTa = collection["moTa"];
            var anhBia = fileAnhBia.FileName;
            var soLuongTon = collection["soLuongTon"];
            var maCD = collection["MaCD"];
            var maNXB = collection["MaNXB"];


            if (
                String.IsNullOrEmpty(tenSach)
                || String.IsNullOrEmpty(giaBan)
                || String.IsNullOrEmpty(moTa)
                || String.IsNullOrEmpty(anhBia)
                || String.IsNullOrEmpty(soLuongTon)
                || String.IsNullOrEmpty(maCD)
                || String.IsNullOrEmpty(maNXB)
            )
            {
                ViewData["Loi"] = "Xin nhập đầy đủ ";
            }
            else
            {
                sach.TenSach = tenSach;
                sach.GiaBan = int.Parse(giaBan);
                sach.MoTa = moTa;
                sach.AnhBia = anhBia;
                sach.SoLuongTon = int.Parse(soLuongTon);
                sach.MaNXB = int.Parse(maNXB);
                sach.MaCD = int.Parse(maCD);
                sach.NgayCapNhat = DateTime.Now;

                data.Saches.InsertOnSubmit(sach);
                data.SubmitChanges();
                ViewBag.ThongBao = "Tạo Sách thành công!";
            }

            // số sách mỗi trang
            int soLuongSach = 5;
            // số trang
            int soTrang = (trang ?? 1);

            ViewBag.MaCD = new SelectList(data.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(data.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            var listSach = this.data.Saches.OrderByDescending(a => a.NgayCapNhat).ToList();
            return View(listSach.ToPagedList(soTrang, soLuongSach));
        }
        public ActionResult Sach(int? trang)
        {
            var kh = Session["TaiKhoanAdmin"];

            if (kh == null)
            {
                return RedirectToAction("Index", "Sach");
            }

            // số sách mỗi trang
            int soLuongSach = 5;
            // số trang
            int soTrang = (trang ?? 1);

            ViewBag.MaCD = new SelectList(data.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(data.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            var listSach = this.data.Saches.OrderByDescending(a => a.NgayCapNhat).ToList();
            return View(listSach.ToPagedList(soTrang, soLuongSach));
        }

        [HttpPost]
        public ActionResult ChuDe(FormCollection collection, ChuDe cd)
        {
            var kh = Session["TaiKhoanAdmin"];

            if (kh == null)
            {
                return RedirectToAction("Index", "Sach");
            }

            var tenChuDe = collection["TenChuDe"];
            if (String.IsNullOrEmpty(tenChuDe))
            {
                ViewData["LoiTenChuDe"] = "Không được để trống :<";
            }
            else
            {
                cd.TenChuDe = tenChuDe;
                data.ChuDes.InsertOnSubmit(cd);
                data.SubmitChanges();
                ViewBag.ThongBao = "Tạo chủ đề thành công!";
            }
            var listChuDe = this.data.ChuDes.ToList();
            return View(listChuDe);
        }

        public ActionResult ChuDe()
        {
            var kh = Session["TaiKhoanAdmin"];

            if (kh == null)
            {
                return RedirectToAction("Index", "Sach");
            }

            var listChuDe = this.data.ChuDes.ToList();
            return View(listChuDe);
        }

        [HttpPost]
        public ActionResult NhaXuatBan(FormCollection collection, NhaXuatBan nxb)
        {
            var kh = Session["TaiKhoanAdmin"];

            if (kh == null)
            {
                return RedirectToAction("Index", "Sach");
            }

            var tenNhaXuatBan = collection["tenNhaXuatBan"];
            var diaChiNhaXuatBan = collection["diaChiNhaXuatBan"];
            var dienThoaiNhaXuatBan = collection["dienThoaiNhaXuatBan"];

            if (String.IsNullOrEmpty(tenNhaXuatBan))
            {
                ViewData["LoiTenNhaXuatBan"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(diaChiNhaXuatBan))
            {
                ViewData["LoiDiaChiNhaXuatBan"] = "Không được để trống :<";
            }
            else if (String.IsNullOrEmpty(dienThoaiNhaXuatBan))
            {
                ViewData["LoiDienThoaiNhaXuatBan"] = "Không được để trống :<";
            }
            else
            {
                nxb.TenNXB = tenNhaXuatBan;
                nxb.DiaChi = diaChiNhaXuatBan;
                nxb.DienThoai = dienThoaiNhaXuatBan;
                data.NhaXuatBans.InsertOnSubmit(nxb);
                data.SubmitChanges();
                ViewBag.ThongBao = "Tạo Nhà xuất bản thành công!";
            }
            var listNXB = this.data.NhaXuatBans.ToList();
            return View(listNXB);
        }
        public ActionResult NhaXuatBan()
        {
            var kh = Session["TaiKhoanAdmin"];

            if (kh == null)
            {
                return RedirectToAction("Index", "Sach");
            }

            var listNXB = this.data.NhaXuatBans.ToList();
            return View(listNXB);
        }

        public ActionResult ChonNhaXuatBan()
        {
            var listNXB = this.data.NhaXuatBans.ToList();
            return PartialView(listNXB);
        }
        public ActionResult ChonChuDe()
        {
            var listChuDe = this.data.ChuDes.ToList();
            return PartialView(listChuDe);
        }
    }
}