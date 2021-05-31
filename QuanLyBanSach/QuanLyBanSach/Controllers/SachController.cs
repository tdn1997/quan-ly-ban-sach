using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSach.Models;
using PagedList;
using PagedList.Mvc;

namespace QuanLyBanSach.Controllers
{
    public class SachController : Controller
    {
        private QuanLyBanSachDataContext data = new QuanLyBanSachDataContext();

        public ActionResult Index(int? trang)
        {
            // số sách mỗi trang
            int soLuongSach = 8;
            // số trang
            int soTrang = (trang ?? 1);

            var listSach = LaySachMoi(50);

            return View(listSach.ToPagedList(soTrang, soLuongSach));
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

        public ActionResult ChuDeTheoId(int id)
        {
            var chuDe = from cd in data.ChuDes where cd.MaCD == id select cd;
            return PartialView(chuDe.Single());
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
        public ActionResult Tim()
        {
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Tim(FormCollection collection)
        {
            var query = collection["query"];
            if (query == "") { 
                return View(new List<Sach>());
            }
            ViewData["Query"] = query;
            var sach = data.Saches.Where(s => s.TenSach.Contains(query)).ToList();
            return View(sach);
        }

        public ActionResult SachTheoNXB(int id)
        {
            var sach = from s in data.Saches where s.MaNXB == id select s;
            return View(sach);
        }
        public ActionResult SachTheoNXBPartial(int id)
        {
            var sach = from s in data.Saches where s.MaNXB == id select s;

            return PartialView(sach.Take(4));
        }

        public ActionResult SachTheoChuDe(int id)
        {
            var sach = from s in data.Saches where s.MaCD == id select s;
            return View(sach);
        }
        public ActionResult SachTheoChuDePartial(int id)
        {
            var sach = from s in data.Saches where s.MaCD == id select s;

            return PartialView(sach.Take(4));
        }

        public ActionResult NXBTheoId(int id)
        {
            var nxb = from n in data.NhaXuatBans where n.MaNXB == id select n;
            return PartialView(nxb.Single());
        }
    }
}