using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookMVC
{
     public class RouteConfig
     {
          public static void RegisterRoutes(RouteCollection routes)
          {
               routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
               //routes.MapRoute(
               //     name: "Client logout",
               //     url: "Dang-xuat",
               //     defaults:new {controller = "Login", action = "Logout", id = UrlParameter.Optional},
               //     namespaces: new[] {"BookMVC.Controllers"}
               //     );
               //routes.MapRoute(
               //     name: "Client Register",
               //     url: "Dang-ky",
               //     defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
               //     namespaces: new[] { "BookMVC.Controllers" }
               //     );
               //routes.MapRoute(
               //     name: "Client Login",
               //     url: "Dang-nhap",
               //     defaults: new { controller = "User", action = "Login" },
               //     namespaces: new[] { "BookMVC.Controllers"}
               //     );
               //routes.MapRoute(
               //     name: "Client BookBy",
               //     url: "sach-theo-tac-gia-{id}",
               //     defaults: new { cotroller = "Book", action = "BookByAuthor", id = UrlParameter.Optional },
               //     namespaces: new[] {"BookMVC.Controllers"}
               //     );
               routes.MapRoute(
                   name: "Default",
                   url: "{controller}/{action}/{id}",
                   defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                   namespaces: new[] {"BookMVC.Controllers" }  // khai bao them namespace de khong bi xung dot giua Area va main (cung voi file AdminAreaRegistration.cs trong Area)
               );
          }
     }
}
