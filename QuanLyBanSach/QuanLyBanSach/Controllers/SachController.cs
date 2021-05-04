using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        private QuanLyBanSachDataContext data = new QuanLyBanSachDataContext();

        // GET: BookStore
        public ActionResult Index()
        {
            var listSach = LaySachMoi(10);
            return View(listSach);
        }

        private List<Sach> LaySachMoi(int count)
        {
            return this.data.Saches.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        public ActionResult ChuDe()
        {
            var listChuDe = this.data.ChuDes.ToList();
            return PartialView(listChuDe);
        }

        public ActionResult NhaXuatBan()
        {
            var listNhaXuatBan = this.data.NhaXuatBans.ToList();
            return PartialView(listNhaXuatBan);
        }
        
        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in data.Saches where s.MaSach == id select s;
            return View(sach.Single());
        }

        public ActionResult SachTheoNXB(int id)
        {
            var sach = from s in data.Saches where s.MaNXB == id select s;
            return View(sach);
        }

        public ActionResult SachTheoChuDe(int id)
        {
            var sach = from s in data.Saches where s.MaCD == id select s;
            return View(sach);
        }
    }
}