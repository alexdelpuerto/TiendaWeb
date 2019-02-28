using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaWeb.Models;
using TiendaWeb.Models.Sesion;

namespace TiendaWeb.Controllers
{
    public class CarritoController : Controller
    {
        private ModeloTiendaWebContainer db = new ModeloTiendaWebContainer();

        // GET: Carrito
        public ActionResult Index(CarritoCompra cc)
        {
            return View(cc);
        }

        // POST: Carrito/Delete/5
        public ActionResult Delete(int id, FormCollection collection, CarritoCompra cc)
        {
            try
            {
                Producto producto = cc.Find(x => x.Id == id);
                cc.Remove(producto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize]
        // POST: Carrito/Buy
        public ActionResult Buy(CarritoCompra cc)
        {
            decimal precio = 0;
            int cantidad;

            if (cc.Count > 0)
            {
                Factura factura = new Factura();
                foreach (Producto p in cc)
                {
                    Pedido pedido = new Pedido();
                    cantidad = db.Productos.Find(p.Id).Cantidad;
                    if (cantidad > 0)
                    {
                        db.Productos.Find(p.Id).Cantidad -= p.Cantidad;
                        precio = precio + p.Cantidad * p.Precio;
                    }
                    pedido.Cantidad = p.Cantidad;
                    pedido.Factura = factura;
                    pedido.Producto = db.Productos.Find(p.Id);
                    db.Pedidos.Add(pedido);
                }
                factura.ClienteID = User.Identity.Name;
                //factura.ClienteID = "pepe";
                factura.Importe = precio;
                db.Facturas.Add(factura);
                db.SaveChanges();
            }
            return RedirectToAction("EmptyCart");
        }

        public ActionResult EmptyCart(CarritoCompra cc)
        {
            cc.Clear();
            return RedirectToAction("Index");
        }
    }
}