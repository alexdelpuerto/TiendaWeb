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
    public class ProductosController : Controller
    {
        private ModeloTiendaWebContainer db = new ModeloTiendaWebContainer();

        // GET: Productos
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

        // GET: Productos/AddCart/5
        public ActionResult AddCart(int? id, CarritoCompra cc)
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
            Producto prodCart = cc.Find(x => x.Id == id);
            producto.maxAlmacen = producto.Cantidad;

            if (prodCart == null)
            {    
                producto.Cantidad = 1;
                cc.Add(producto);
            }
            else
            {
                if (producto.maxAlmacen - prodCart.Cantidad - 1 >= 0)
                {
                    cc.Remove(prodCart);
                    producto.Cantidad = prodCart.Cantidad + 1;
                    cc.Add(producto);
                }
            }
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