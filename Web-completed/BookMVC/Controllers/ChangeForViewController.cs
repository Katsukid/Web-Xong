using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMVC.Controllers
{
    public class ChangeForViewController : Controller
    {
        // GET: ChangeForView
        public ActionResult Currency(decimal value)
        {
            return Json(value.ToString("N0"));
        }
    }
}