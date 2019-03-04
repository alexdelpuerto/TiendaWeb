using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TiendaWeb.Models;

namespace TiendaWeb.Controllers
{
    public class StocksController : Controller
    {
        private ModeloTiendaWebContainer db = new ModeloTiendaWebContainer();

        // GET: Stocks
        [Authorize(Users = "admin@admin.com")]
        public ActionResult Index()
        {
            List<Producto> listProductos = new List<Producto>();
            foreach (Stock stock in db.Stock)
            {
                listProductos.Add(db.Productos.Find(stock.Producto.Id));
            }

            return View(listProductos);
        }

        // GET: Stocks/Delete/5
        [Authorize(Users = "admin@admin.com")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock stock = db.Stock.Find(id);
            Producto producto = stock.Producto;
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Stocks/Delete/5
        [Authorize(Users = "admin@admin.com")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stock stock = db.Stock.Find(id);
            db.Stock.Remove(stock);
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
