﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Adapters;
using DoAnPC.Models;

namespace DoAnPC.Controllers
{
    public class OrderProesController : Controller
    {
        private LoginEntities2 db = new LoginEntities2();

     


        // GET: OrderProes
        public ActionResult Index()
        {
            var orderPro = db.OrderPro.Include(o => o.User);

            return View(orderPro.ToList());


        }

        // GET: OrderProes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderPro.Find(id);
            if (orderPro == null )
            {
                return HttpNotFound();
            }
            return View(orderPro);
        }

        // GET: OrderProes/Create
        public ActionResult Create()
        {
            
            ViewBag.IDCus = new SelectList(db.User, "ID", "TaiKhoan");
            
            return View();
        }

        // POST: OrderProes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateOrder,IDCus,AddressDeliverry")] OrderPro orderPro)
        {
            if (ModelState.IsValid)
            {
                db.OrderPro.Add(orderPro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCus = new SelectList(db.User, "ID", "TaiKhoan", orderPro.IDCus);
            return View(orderPro);
        }

        // GET: OrderProes/Edit/5
        public ActionResult Edit(int? id)
        {

           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderPro.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCus = new SelectList(db.User, "ID", "TaiKhoan", orderPro.IDCus);
            return View(orderPro);
        }

        // POST: OrderProes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateOrder,IDCus,AddressDeliverry")] OrderPro orderPro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderPro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCus = new SelectList(db.User, "ID", "TaiKhoan", orderPro.IDCus);
            return View(orderPro);
        }

        // GET: OrderProes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderPro.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
            return View(orderPro);
        }

        // POST: OrderProes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderPro orderPro = db.OrderPro.Find(id);
            db.OrderPro.Remove(orderPro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
