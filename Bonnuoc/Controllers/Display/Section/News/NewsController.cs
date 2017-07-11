using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonnuoc.Models;
using PagedList;
using PagedList.Mvc;
using System.Text.RegularExpressions;
namespace Bonnuoc.Controllers.Display.Section.NewsHomes
{
    
    public class NewsController : Controller
    {
        BonnuocContext db = new BonnuocContext();
        // GET: News
        public ActionResult Index()
        {

            return View();
        } string nUrl = "";
        public string UrlNews(int idCate)
        {
            var ListMenu = db.tblGroupNews.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = " <a href=\"/0/" + ListMenu[i].Tag + "\" title=\"" + ListMenu[i].Name + "\"> " + " " + ListMenu[i].Name + "</a> <i></i>" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlNews(id);
                }

            }
            return nUrl;
        }
        public PartialViewResult partialNewsHomes()
        {
            var ListNew1 = db.tblNews.Where(p => p.Active == true  && p.idCate==1).OrderByDescending(p => p.DateCreate).Take(10).ToList();
            string chuoi1 = "";
            string chuoi2 = "";
            for (int i = 0; i < ListNew1.Count;i++ )
            {
                if(i==0)
                {
                    chuoi1 += "<a href=\"/Tin-tuc/" + ListNew1[i].Tag + "\" title=\"" + ListNew1[i].Name + "\"><img src=\"" + ListNew1[i].Images + "\" alt=\"" + ListNew1[i].Name + "\" /></a>";
                    chuoi1 += "<h3><a href=\"/Tin-tuc/" + ListNew1[i].Tag + "\" title=\"" + ListNew1[i].Name + "\" class=\"Name\">" + ListNew1[i].Name + "</a></h3>";
                    chuoi1 += "<span>" + ListNew1[i].Description + "</span>";
                }
                else
                {

                    chuoi2 += "<h3><a href=\"/Tin-tuc/" + ListNew1[i].Tag + "\" title=\"" + ListNew1[i].Name + "\" class=\"Name\">" + ListNew1[i].Name + "</a></h3>";
                }
            }
            ViewBag.chuoi1 = chuoi1;
            ViewBag.chuoi2 = chuoi2;
            var ListNew2 = db.tblNews.Where(p => p.Active == true && p.idCate == 6).OrderByDescending(p => p.DateCreate).Take(10).ToList();
            string chuoi3 = "";
            string chuoi4 = "";
            for (int i = 0; i < ListNew2.Count; i++)
            {
                if (i == 0)
                {
                    chuoi3 += "<a href=\"/Tin-tuc/" + ListNew2[i].Tag + "\" title=\"" + ListNew2[i].Name + "\"><img src=\"" + ListNew2[i].Images + "\" alt=\"" + ListNew2[i].Name + "\" /></a>";
                    chuoi3 += "<h3><a href=\"/Tin-tuc/" + ListNew2[i].Tag + "\" title=\"" + ListNew2[i].Name + "\" class=\"Name\">" + ListNew2[i].Name + "</a></h3>";
                    chuoi3 += "<span>" + ListNew2[i].Description + "</span>";
                }
                else
                {

                    chuoi4 += "<h3><a href=\"/Tin-tuc/" + ListNew2[i].Tag + "\" title=\"" + ListNew2[i].Name + "\" class=\"Name\">" + ListNew2[i].Name + "</a></h3>";
                }
            }
            ViewBag.chuoi3 = chuoi3;
            ViewBag.chuoi4 = chuoi4;
                return PartialView();
        }
        private string StripHtml(string source)
        {
            string output;

            //get rid of HTML tags
            output = Regex.Replace(source, "<[^>]*>", string.Empty);

            //get rid of multiple blank lines
            output = Regex.Replace(output, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

            return output;
        }
        public ActionResult NewsDetail(string tag)
        {
            string url = Request.Url.ToString();
            string[] mang = url.Split('.');
            if (mang[mang.Length - 1] == "html")
            {
                string[] mang1 = tag.Split('_');
                string v1 = mang1[mang1.Length - 1];
                string tags = tag.Substring(0, tag.Length - (v1.Length+1));
                return Redirect("/tin-tuc/" + tags + "");
            }
         
            var tblnews = db.tblNews.First(p => p.Tag == tag);
            int idUser = int.Parse(tblnews.idUser.ToString());
            ViewBag.Username = db.tblUsers.Find(idUser).UserName;
            int idCate = int.Parse(tblnews.idCate.ToString());
            var groupnews = db.tblGroupNews.First(p => p.id == idCate);
            ViewBag.NameMenu = groupnews.Name;
            ViewBag.Title = "<title>" + tblnews.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + StripHtml(tblnews.Description) + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblnews.Keyword + "\" /> ";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblnews.Title + "\" />";
            ViewBag.dcDescription = "<meta name=\"DC.description\" content=\"" + StripHtml(tblnews.Description) + "\" />";
            string meta = "";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://bonnuoc.vn/tin-tuc/" + StringClass.NameToTag(tag) + "\" />";

            meta += "<meta itemprop=\"name\" content=\"" + tblnews.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + StripHtml(tblnews.Description) + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://bonnuoc.vn" + tblnews.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblnews.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://bonnuoc.vn" + tblnews.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://Bonnuoc.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + StripHtml(tblnews.Description) + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Descriptionss = StripHtml(tblnews.Description);
            ViewBag.Meta = meta;
            int id = int.Parse(tblnews.id.ToString());
            if (tblnews.Keyword != null)
            {
                string Chuoi = tblnews.Keyword;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    string tabs = Mang[i].ToString();
                    var listnew = db.tblNews.Where(p => p.Keyword.Contains(tabs) && p.id != id && p.Active == true).ToList();
                    for (int j = 0; j < listnew.Count; j++)
                    {
                        araylist.Add(listnew[j].id);
                    }

                }


                var Lienquan = db.tblNews.Where(p => araylist.Contains(p.id) && p.Active == true && p.id != id).OrderByDescending(p => p.Ord).Take(3).ToList();
                string chuoinew = "";
                if (Lienquan.Count > 0)
                {

                    chuoinew += " <div class=\"Lienquan\">";
                    for (int i = 0; i > Lienquan.Count; i++)
                    {
                        chuoinew += "<a href=\"/Tin-tuc/" + Lienquan[i].Tag + "\" title=\"" + Lienquan[i].Name + "\"> " + Lienquan[i].Name + "</a>";
                    }
                    chuoinew += "</div>";
                }
                ViewBag.chuoinew = chuoinew;


                //Load tin mới

            }

            string chuoinewnew = "";
            var NewsNew = db.tblNews.Where(p => p.Active == true && p.id != id).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < NewsNew.Count; i++)
            {
                chuoinewnew += "<li><a href=\"/Tin-tuc/" + NewsNew[i].Tag + "\" title=\"" + NewsNew[i].Name + "\" rel=\"nofollow\"> " + NewsNew[i].Name + " <span>" + NewsNew[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoinewnews = chuoinewnew;

            //load tag
            string chuoitag = "";
            if (tblnews.Keyword != null)
            {
                string Chuoi = tblnews.Keyword;
                string[] Mang = Chuoi.Split(',');

                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {

                    chuoitag += "<h2><a href=\"/TagNews/" + StringClass.NameToTag(Mang[i]) + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;

            //Load root

            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlNews(idCate);
            tblConfig tblconfig = db.tblConfigs.First();
            if(tblconfig.Coppy==true)
            {
                ViewBag.Coppy="<link href=\"/Content/Display/Style/Coppy.css\" rel=\"stylesheet\" /><script src=\"/Scripts/disable-copyright.js\"></script>";
            }
            return View(tblnews);
        }
        public PartialViewResult paritalRighNewsDetail()
        {
            var listnews = db.tblNews.Where(p => p.Active == true && p.idCate == 3).OrderBy(p => p.Ord).ToList();
            string chuoi = "";
            for (int i = 0; i < listnews.Count; i++)
            {
                chuoi += " <li><a href=\"/Tin-tuc/" + listnews[i].Tag + "\" title=\"" + listnews[i].Name + "\">- " + listnews[i].Name + "</a></li>";
            }
            ViewBag.chuoi = chuoi;
            var listtuyendung = db.tblNews.Where(p => p.Active == true && p.idCate == 2).OrderBy(p => p.DateCreate).Take(5).ToList();
            string chuotuyendungi = "";
            for (int i = 0; i < listtuyendung.Count; i++)
            {
                chuotuyendungi += " <li><a href=\"/Tin-tuc/" + listtuyendung[i].Tag + "\" title=\"" + listtuyendung[i].Name + "\">- " + listtuyendung[i].Name + "</a></li>";
            }
            ViewBag.chuotuyendungi = chuotuyendungi;
            var Listn = db.tblNews.Where(p => p.Active == true && p.idCate == 2).OrderBy(p => p.DateCreate).Take(5).ToList();
            string chuoin = "";
            for (int i = 0; i < Listn.Count; i++)
            {
                chuoin += " <li><a href=\"/Tin-tuc/" + Listn[i].Tag + "\" title=\"" + Listn[i].Name + "\">- " + Listn[i].Name + "</a></li>";
            }
            ViewBag.chuoin = chuoin;
            return PartialView();
        }
        public ActionResult ListNews(string tag, int? page)
        {
            var groupnew = db.tblGroupNews.First(p => p.Tag == tag);
            int idcate=groupnew.id;
            var listnews = db.tblNews.Where(p => p.idCate == idcate && p.Active == true).OrderByDescending(p => p.Ord).ToList();
            string chuoinewnew = "";
            var NewsNew = db.tblNews.Where(p => p.Active == true && p.idCate != idcate).OrderByDescending(p => p.DateCreate).Take(5).ToList();
            for (int i = 0; i < NewsNew.Count; i++)
            {
                chuoinewnew += "<li><a href=\"/Tin-tuc/" + NewsNew[i].Tag + "\" title=\"" + NewsNew[i].Name + "\" rel=\"nofollow\"> " + NewsNew[i].Name + " <span>" + NewsNew[i].DateCreate + "</span></a></li>";
            }
            ViewBag.chuoinewnews = chuoinewnew;
            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            // Thiết lập phân trang
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;

            ViewBag.Name = groupnew.Name;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlNews(groupnew.id);
            ViewBag.Title = "<title>" + groupnew.Title + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + groupnew.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + groupnew.Keyword + "\" /> ";
            return View(listnews.ToPagedList(pageNumber, pageSize));
             
        }
        public ActionResult TagNews(string tag, int? page)
        {
            string[] Mang1 = StringClass.COnvertToUnSign1(tag).Split('-');
            string chuoitag = "";
            for (int i = 0; i < Mang1.Length; i++)
            {
                if (i == 0)
                    chuoitag += Mang1[i];
                else
                    chuoitag += " " + Mang1[i];
            }
            int dem = 1;
            string name = "";
            List<tblNew> ListNew = (from c in db.tblNews where c.Active == true select c).ToList();
            List<tblNew> listnews = ListNew.FindAll(delegate(tblNew math)
            {
                if (StringClass.COnvertToUnSign1(math.Keyword.ToUpper()).Contains(chuoitag.ToUpper()))
                {

                    string[] Manghienthi = math.Keyword.Split(',');
                    foreach (var item in Manghienthi)
                    {
                        if (dem == 1)
                        {
                            var kiemtra = StringClass.COnvertToUnSign1(item.ToUpper()).Contains(chuoitag.ToUpper());
                            if (kiemtra == true)
                            {
                                name = item;
                                dem = 0;
                            }
                        }
                    }

                    return true;
                }

                else
                    return false;
            }
            );
            const int pageSize = 10;
            var pageNumber = (page ?? 1);
            var ship = new PagedListRenderOptions
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.Always,
                DisplayLinkToLastPage = PagedListDisplayMode.Always,
                DisplayLinkToPreviousPage = PagedListDisplayMode.Always,
                DisplayLinkToNextPage = PagedListDisplayMode.Always,
                DisplayLinkToIndividualPages = true,
                DisplayPageCountAndCurrentLocation = false,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true,
                EllipsesFormat = "&#8230;",
                LinkToFirstPageFormat = "Trang đầu",
                LinkToPreviousPageFormat = "«",
                LinkToIndividualPageFormat = "{0}",
                LinkToNextPageFormat = "»",
                LinkToLastPageFormat = "Trang cuối",
                PageCountAndCurrentLocationFormat = "Page {0} of {1}.",
                ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.",
                FunctionToDisplayEachPageNumber = null,
                ClassToApplyToFirstListItemInPager = null,
                ClassToApplyToLastListItemInPager = null,
                ContainerDivClasses = new[] { "pagination-container" },
                UlElementClasses = new[] { "pagination" },
                LiElementClasses = Enumerable.Empty<string>()
            };
            ViewBag.ship = ship;

            ViewBag.Name = name;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i> " + name + "";
            ViewBag.Title = "<title>" + name + "</title>";
            ViewBag.Description = "<meta name=\"description\" content=\"" + name + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + name + "\" /> ";
            return View(listnews.ToPagedList(pageNumber, pageSize));
        }

    }
}