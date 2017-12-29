using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMPE312_PROJECT.Models.Repository;


namespace CMPE312_PROJECT.Controllers
{
    /*
     * This class is created for providing connection between View and Model.
     */
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}