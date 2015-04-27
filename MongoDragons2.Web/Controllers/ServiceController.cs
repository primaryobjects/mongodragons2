using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDragons2.Repository;
using MongoDragons2.Types;

namespace MongoDragons2.Web.Controllers
{
    public class ServiceController : Controller
    {
        [HttpGet]
        public JsonResult Dragons(string q)
        {
            IEnumerable<Dragon> dragons = string.IsNullOrEmpty(q) ? DragonRepository.ToList() : DragonRepository.Search(q);

            return Json(dragons, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Spawn()
        {
            // Commented out for demo.
            // Dragon dragon = DragonRepository.Spawn();
            Dragon dragon = new Dragon() { Name = "Test", Age = 10, Gold = 100 };

            return Json(dragon);
        }

        [HttpPost]
        public JsonResult Remove(Dragon dragon)
        {
            // Commented out for demo.
            // bool result = DragonRepository.Remove(dragon);
            bool result = true;

            return Json(result);
        }
    }
}