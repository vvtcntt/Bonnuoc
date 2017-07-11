using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonnuoc.Models;
namespace Bonnuoc.Controllers.Display.Header
{
    public class HeaderController : Controller
    {
        // GET: Header
        BonnuocContext db = new BonnuocContext();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult partialControlHeader()
        {
            var giohang = (clsGiohang)Session["giohang"];
            string chuoi = "";
            if(giohang!=null)
            {
                foreach (var item in giohang.CartItem)
                {
                    chuoi += "<div class=\"Tear_OrderToolbar\">";
                    chuoi += "<a href=\"/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.Images + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += "<a href=\"" + item.Tag + "\" class=\"Name\" title=\"" + item.Name + "\">" + item.Name + "</a>";
                    chuoi += "<span>" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "<a href=\"\" class=\"Del\"></a>";
                    chuoi += "</div>";
                }
                ViewBag.chuoi = chuoi;
            }
            var listSupport = db.tblSupports.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            string chuoisp = "";
            foreach(var item in listSupport)
            {
                chuoisp += "<a href=\"Skype:"+item.Skyper+"?chat\" title=\""+item.Skyper+"\" class=\"skype\">";
                chuoisp += "<img class=\"imgSkyper\" src=\"~/Content/Display/iCon/skype-icon.png\" title=\"" + item.Name + "\" alt=\"" + item.Name + "\">";
                chuoisp += " </a>";
            }
            ViewBag.chuoisp = chuoisp;

            var ListMenu = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == null).OrderBy(p => p.Ord).ToList();
            string menu = "";
            foreach (var item in ListMenu)
            {
                menu += "<li class=\"li_1\">";
                menu += "<a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">" + item.Name + " <span></span></a>";

                int idPa1 = item.id;
                var ListMenu1 = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idPa1).OrderBy(p => p.Ord).ToList();
                if (ListMenu1.Count > 0)
                {
                    menu += "<div class=\"sub_item\" style=\"background:url(" + item.Background + ") no-repeat right bottom scroll #fff\">";
                    menu += "<ul class=\"ul_2\">";
                    foreach (var item1 in ListMenu1)
                    {
                        menu += "<li class=\"li_2\">";
                        menu += "<a href=\"/" + item1.Tag + ".html\" title=\"" + item1.Name + "\">" + item1.Name + "  <span></span></a>";
                        int idPa2 = item1.id;
                        var ListMenu2 = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idPa2).OrderBy(p => p.Ord).ToList();

                        if (ListMenu2.Count > 0)
                        {
                            menu += "<div class=\"sub_item1\">";
                            menu += "<ul class=\"ul_3\">";
                            foreach (var item2 in ListMenu2)
                            {
                                menu += "<li class=\"li_3\">";
                                menu += "<a href=\"/" + item2.Tag + ".html\" title=\"" + item2.Name + "\">" + item2.Name + " <span></span></a>";

                                int idPa3 = item2.id;
                                var ListMenu3 = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID == idPa3).OrderBy(p => p.Ord).ToList();
                                if (ListMenu3.Count > 0)
                                {
                                    menu += "<div class=\"sub_item2\">";
                                    menu += "<ul class=\"ul4\">";
                                    foreach (var item3 in ListMenu3)
                                    {
                                        menu += "<li class=\"li4\">";
                                        menu += "<a href=\"/" + item3.Tag + ".html\" title=\"" + item3.Name + "\">› " + item3.Name + "</a>";
                                        menu += "</li> ";
                                    }
                                    menu += "</ul>";
                                    menu += "</div> ";
                                }
                               
                                menu += "</li> ";
                            }
                            menu += " </ul>";
                            menu += "</div> ";
                        }
                        menu += "</li> ";
                    }
                    menu += "</ul>";
                    menu += "</div> ";
                }
                
                menu += "</li>";
            }
            ViewBag.menu = menu;
            return PartialView(db.tblConfigs.First());

        }
        public PartialViewResult partialcheckorder()
        {
            return PartialView();
        }
        public ActionResult CommandSearch(FormCollection collection)
        {
            Session["Search"] = collection["txtSearch"];

            return Redirect("/Product/Search");
        }
        public PartialViewResult partialSidebars()
        {
            var listCapacity = db.tblCapacities.Where(p => p.Active == true ).OrderBy(p => p.Ord).ToList();
            string chuoi = "";
            for (int i = 0; i < listCapacity.Count; i++)
            {
                if(listCapacity[i].Priority==true)
                {
                    if (i == 0)
                        chuoi += "<li class=\"current\"><a href=\"#neo-" + i + "\" class=\"neo\" title=\"" + listCapacity[i].Name + "\">" + listCapacity[i].Name + "</a></li>";
                    else
                        chuoi += "<li  ><a href=\"#neo-" + i + "\" class=\"neo\" title=\"" + listCapacity[i].Name + "\">" + listCapacity[i].Name + "</a></li>";
                }
               
            }
            ViewBag.chuoi = chuoi;
            return PartialView();
        }
    }
}