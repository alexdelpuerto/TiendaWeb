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
    public class FacturasController : Controller
    {
        private ModeloTiendaWebContainer db = new ModeloTiendaWebContainer();

        // GET: Facturas
        public ActionResult Index()
        {
            List<Factura> listFacturas = new List<Factura>();
            foreach (Factura factura in db.Facturas)
            {
                if(factura.ClienteID == User.Identity.Name)
                {
                    listFacturas.Add(factura);
                }
            }
            return View(listFacturas);
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
