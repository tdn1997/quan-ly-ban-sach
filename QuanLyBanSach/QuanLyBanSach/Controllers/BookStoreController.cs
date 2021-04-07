using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Controllers
{

    public class BookStoreController : Controller
    {
        private QLBSDataContext data = new QLBSDataContext();

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
    }
}