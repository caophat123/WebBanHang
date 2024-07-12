﻿using DoAnPC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        LoginEntities2 db = new LoginEntities2();
        public ActionResult Index()
        {
            return View();
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }




        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



       

        //public ActionResult SanPham()
        //{
        //    return View();
        //}



        public ActionResult Introduce()
        {
            return View();
        }




        //HTTP get/Home/DangKy
        public ActionResult DangKy()
        {
            return View();
        }




        //HTTP Post/Home/DangKy
        [HttpPost]
        public ActionResult DangKy(User user)
        {
            var khachhang = db.User.FirstOrDefault(k => k.TaiKhoan == user.TaiKhoan);
            if (khachhang != null)
            {
                ViewBag.ThongBao = "Đã có người đăng kí tên này";
                return View();
            }
            else
            db.User.Add(user);
            db.SaveChanges();
            return RedirectToAction("DangNhap");
        }




        //HTTP get/Home/DangNhap
        public ActionResult DangNhap()
        {
            return View();
        }




        //HTTP Post/Home/DangKy
        [HttpPost]
        public ActionResult DangNhap(User user)
        {
            var TaiKhoanForm = user.TaiKhoan;
            var MatKhauForm = user.MatKhau;
            var userCheck = db.User.SingleOrDefault(x => x.TaiKhoan.Equals(TaiKhoanForm) && x.MatKhau.Equals(MatKhauForm));
            if (TaiKhoanForm.ToLower() == "admin" && MatKhauForm.ToLower() == "123456")
            {
                Session["User"] = "admin";
                return RedirectToAction("Index", "Admin");
            }
            if (userCheck != null)
            {
                Session["User"] = userCheck;
                return RedirectToAction("ProductList", "Products");
            }

            else
            {
                ViewBag.LoginFail = "Tài khoản hoặc mật khẩu không đúng, vui lòng kiểm tra lại";
                return View("DangNhap");
            }

        }


        //HTTP Post/Home/DangXuat
        public ActionResult DangXuat()
        {
            Session["User"] = null;
            return RedirectToAction("DangNhap", "Home");
        }
       public ActionResult SanPham()
        {
            var products = db.Pro.Include(p => p.Category1);
            return View(products.ToList());
        }
    }
}