using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineStore.Models;

namespace WineStore.Controllers
{
    public class UiController : Controller
    {
        // GET: Ui
        WineStoreContext db =  new WineStoreContext();  
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Select(s=>s);
            return View(sanPhams.ToList());
        }
    }
}