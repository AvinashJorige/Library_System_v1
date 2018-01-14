using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_System_v1.Areas.Student.Controllers
{
    public class StdHomeController : Controller
    {
        // GET: Student/StdHome
        public ActionResult Index()
        {
            return View();
        }
    }
}