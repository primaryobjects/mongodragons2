using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDragons2.Repository.Concrete;
using MongoDragons2.Types;

namespace MongoDragons2.Web.Controllers
{
    public class ServiceController : Controller
    {
        [HttpGet]
        public JsonResult Dragons()
        {
            IEnumerable<Dragon> dragons = DragonRepository.ToList();

            return Json(dragons, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Spawn()
        {
            Dragon dragon = DragonRepository.Spawn();

            return Json(dragon);
        }

        [HttpPost]
        public JsonResult Remove(Dragon dragon)
        {
            bool result = DragonRepository.Remove(dragon);

            return Json(result);
        }
    }
}