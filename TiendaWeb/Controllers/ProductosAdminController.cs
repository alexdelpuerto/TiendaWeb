using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaWeb.Models;
using TiendaWeb.Models.Sesion;

namespace TiendaWeb.Controllers
{
    public class ProductosAdminController : Controller
    {
        private ModeloTiendaWebContainer db = new ModeloTiendaWebContainer();

        // GET: ProductosAdmin
        [Authorize(Users = "admin@admin.com")]
        public ActionResult Index()
        {
            List<Producto> listProductos = new List<Producto>();
            foreach (Producto p in db.Productos)
            {
                if (db.Productos.Find(p.Id).Cantidad > 0)
                {
                    listProductos.Add(db.Productos.Find(p.Id));
                }
            }
            return View(listProductos);
        }

        // GET: ProductosAdmin/Details/5
        [Authorize(Users = "admin@admin.com")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: ProductosAdmin/Create
        [Authorize(Users = "admin@admin.com")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductosAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin@admin.com")]
        public ActionResult Create([Bind(Include = "Id,Nombre,Precio,Cantidad,Descripcion,FotoID")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: ProductosAdmin/Edit/5
        [Authorize(Users = "admin@admin.com")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: ProductosAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin@admin.com")]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Precio,Cantidad,Descripcion,FotoID")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: ProductosAdmin/Delete/5
        [Authorize(Users = "admin@admin.com")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productos.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: ProductosAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin@admin.com")]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productos.Find(id);
            db.Productos.Remove(producto);
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