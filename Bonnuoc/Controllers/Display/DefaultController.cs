using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonnuoc.Models;
namespace Bonnuoc.Controllers.Display
{
    public class DefaultController : Controller
    {
        BonnuocContext db = new BonnuocContext();

        // GET: Default
        public ActionResult Index()
        {
     
            tblConfig config = db.tblConfigs.First();
            ViewBag.Title = "<title>" + config.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + config.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + config.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + config.Keywords + "\" /> ";
            ViewBag.h1 = "<h1 class=\"h1\">" + config.Title + "</h1>";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://bigsea.vn\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + config.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + config.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://bonnuoc.vn" + config.Logo + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + config.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://bonnuoc.vn" + config.Logo + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://bigsea.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + config.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            if (Session["Register"] != null && Session["Register"]!="")
            {
                ViewBag.register = Session["Register"].ToString();
                Session["Register"] = "";
            }
            return View();
        }
        public PartialViewResult partialdefault()
        {
            Session["Color"] = db.tblConfigs.First().Color;

            string chuoi = "";
            chuoi += "<style>";
            var listMenu = db.tblGroupProducts.Where(p => p.Active == true && p.Priority == true).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < listMenu.Count; i++)
            {
                chuoi += " .item_li" + listMenu[i].id + ":hover{background:#" + listMenu[i].Color + "}";
                chuoi += " .item_li" + listMenu[i].id + ":hover a{color:#FFF !important}";
            }

            chuoi += " </style>";
            ViewBag.chuoi = chuoi;
            return PartialView(db.tblConfigs.First());
        }
        public PartialViewResult partialSlide()
        {
            var listImage = db.tblImages.Where(p => p.Active == true && p.idCate == 1).OrderByDescending(p => p.Ord).ToList();
            return PartialView(listImage);
        }
    }
}