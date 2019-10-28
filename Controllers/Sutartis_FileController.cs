/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;
namespace AlausDaryklosGamybosValdymoSistema.Controllers
{
    public class Sutartis_FileController : Controller
    {
        public ActionResult UploadFile()
        {

            var path = HttpContext.Current.Server.MapPath("~/Files");
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            return View();
        }
    }
}*/