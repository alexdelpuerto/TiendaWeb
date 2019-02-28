using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaWeb.Models.Sesion;

namespace TiendaWeb.Models.Binders
{
    public class CarritoCompraModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CarritoCompra cc =
            (CarritoCompra)controllerContext.HttpContext.Session["carrito"];

            if (cc == null)
            {
                cc = new CarritoCompra();
                controllerContext.HttpContext.Session["carrito"] = cc;
            }

            return cc;
        }
    }
}