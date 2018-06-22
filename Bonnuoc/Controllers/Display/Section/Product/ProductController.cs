using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonnuoc.Models;
using System.Text;

namespace Bonnuoc.Controllers.Display.Section.Product
{
    public class ProductController : Controller
    {
        BonnuocContext db = new BonnuocContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult partialProductHomes()
        {
            string chuoi = "";
            int dem = 0;
            var ListCapacity = db.tblCapacities.Where(p => p.Active == true ).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < ListCapacity.Count; i++)
            {
                if(ListCapacity[i].Priority==true)
                {
                    dem += 1;
                    chuoi += "<div class=\"Clear\"></div>";
                    chuoi += "<div class=\"cls_Product\">";
                    chuoi += "<div id=\"neo-" + i + "\" class=\"cneo\"></div>";
                    chuoi += "<div class=\"nVar\">";
                    chuoi += " <div class=\"Left_Nvar\">";
                    chuoi += "<div class=\"Name\">";
                    chuoi += "<h2><a href=\"/" + ListCapacity[i].Tag + "-dt\" title=\"" + ListCapacity[i].Name + "\">" + ListCapacity[i].Name + "</a></h2>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"iCon\"> " + dem + "</div>";
                    chuoi += " </div>";
                    chuoi += "<div class=\"Right_Nvar\">";
                    int idCapa = ListCapacity[i].id;
                    //ffffffffffffffg
                    var Listidmanu = from a in db.tblProducts join b in db.tblConnectManuProducts on a.idCate equals b.idCate where a.Capacity == idCapa select b;
                    List<int> Mangidmanu = new List<int>();
                    foreach(var item in Listidmanu)
                    {
                        int idmanu=int.Parse(item.idManu.ToString());
                        Mangidmanu.Add(idmanu);
                    }
                    var ListMenu = db.tblManufactures.Where(p => p.Active == true && Mangidmanu.Contains(p.id)).OrderBy(p => p.Ord).ToList();
                    foreach (var item in ListMenu)
                    {
                        chuoi += "<a href=\"/"+ListCapacity[i].Tag+"-dt/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.Images + "\" alt=\"" + item.Name + "\" /></a>";
                    }
                   

                    //fgfgfgfg


                    chuoi += "</div>";
                    chuoi += "</div>";
                    chuoi += " <div class=\"Content_ClsProduct\">";
                    chuoi += "<div class=\"ClsProduct_Tear\">";
                    chuoi += "<div class=\"nVar_01\">";
                    chuoi += "<div class=\"Left_Nvar_01\">";
                    chuoi += "<h3>Bồn đứng</h3>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Center_Nvar_01\">";
                    chuoi += "<a href=\"javascript:void(0)\" class=\"tn1" + ListCapacity[i].id + " set\" onclick=\"javascript:return Tab('n1" + ListCapacity[i].id + "-n2" + ListCapacity[i].id + "');\">Inox</a><a class=\"number tn2" + ListCapacity[i].id + "\" onclick=\"javascript:return Tab('n2" + ListCapacity[i].id + "-n1" + ListCapacity[i].id + "');\">Nhựa</a>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_Nvar_01\">";
                    chuoi += "<div class=\"stairs\">";
                    chuoi += "<a href=\"#neo-" + (i + 1) + "\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                    chuoi += "<i class=\"Elevator\"></i>";
                    chuoi += "<a href=\"#neo-" + (i - 1) + "\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                    chuoi += "</div>";
                    chuoi += "</div>";
                    chuoi += "</div>";


                    chuoi += "<div class=\"List_ProductHomes\">";
                    chuoi += " <div id=\"vn1" + ListCapacity[i].id + "\">";

                    
                    var listProduct1 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCapa && p.ViewHomes == true && p.Design == 1 && p.Material == 0).OrderBy(p => p.Ord).ToList();
                    foreach (var item in listProduct1)
                    {
                        int idcate = int.Parse(item.idCate.ToString());
                        string imagemanu = "";
                        var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                        foreach (var item1 in listManu)
                        {
                            imagemanu = item1.Images;

                        }
                        chuoi += "<div class=\"Tear_1\">";
                        chuoi += "<div class=\"OrderNow\">";
                        chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                        chuoi += "</div>";
                        if (item.New == true)
                            chuoi += "<div class=\"Note\"></div>";
                        chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu + ") no-repeat\"></div>";
                        chuoi += "<div class=\"img\">";
                        chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                        chuoi += " </div>";
                        chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                        chuoi += "<div class=\"Box_Tear\">";
                        chuoi += "<div class=\"Left_BoxTear\">";
                        chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                        chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                        chuoi += "</div>";
                        chuoi += "<div class=\"Right_BoxTear\">";
                        chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                        if (item.Material == 0)
                            chuoi += " <span class=\"Material\">Inox SUS304</span>";
                        else
                            chuoi += " <span class=\"Material\">Nhựa</span>";
                        chuoi += "  </div>";
                        chuoi += " </div>";

                        chuoi += "  </div>";

                    }


                    chuoi += "</div>";
                    chuoi += "<div id=\"vn2" + ListCapacity[i].id + "\" style=\"display:none\">";


                    var listProduct2 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCapa && p.ViewHomes == true && p.Design == 1 && p.Material == 1).OrderBy(p => p.Ord).ToList();
                    foreach (var item in listProduct2)
                    {
                        int idcate = int.Parse(item.idCate.ToString());
                        string imagemanu2 = "";
                        var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                        foreach (var item1 in listManu)
                        {
                            imagemanu2 = item1.Images;

                        }
                        chuoi += "<div class=\"Tear_1\">";
                        chuoi += "<div class=\"OrderNow\">";
                        chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                        chuoi += "</div>";
                        if (item.New == true)
                            chuoi += "<div class=\"Note\"></div>";
                        chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu2 + ") no-repeat\"></div>";
                        chuoi += "<div class=\"img\">";
                        chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                        chuoi += " </div>";
                        chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                        chuoi += "<div class=\"Box_Tear\">";
                        chuoi += "<div class=\"Left_BoxTear\">";
                        chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                        chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                        chuoi += "</div>";
                        chuoi += "<div class=\"Right_BoxTear\">";
                        chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                        if (item.Material == 0)
                            chuoi += " <span class=\"Material\">Inox SUS304</span>";
                        else
                            chuoi += " <span class=\"Material\">Nhựa</span>";
                        chuoi += "  </div>";
                        chuoi += " </div>";

                        chuoi += "  </div>";

                    }

                    chuoi += "</div>";


                    chuoi += "</div>";
                    chuoi += "</div>";
                    chuoi += " <div class=\"ClsProduct_Tear\">";
                    chuoi += "<div class=\"nVar_01\">";
                    chuoi += "<div class=\"Left_Nvar_01\">";
                    chuoi += "<h3>Bồn nằm</h3>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Center_Nvar_01\">";
                    chuoi += " <a href=\"javascript:void(0)\" class=\"tn3" + ListCapacity[i].id + " set\" onclick=\"javascript:return Tab('n3" + ListCapacity[i].id + "-n4" + ListCapacity[i].id + "');\">Inox</a><a title=\"v4\" class=\"number tn4" + ListCapacity[i].id + "\" onclick=\"javascript:return Tab('n4" + ListCapacity[i].id + "-n3" + ListCapacity[i].id + "');\">Nhựa</a>";
                    chuoi += "</div>";
                    chuoi += " <div class=\"Right_Nvar_01\">";
                    chuoi += "<div class=\"stairs\">";
                    chuoi += "<a href=\"\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                    chuoi += "<i class=\"Elevator\"></i>";
                    chuoi += "<a href=\"\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                    chuoi += " </div>";
                    chuoi += "</div>";
                    chuoi += " </div>";
                    chuoi += "<div class=\"List_ProductHomes\">";
                    chuoi += "<div id=\"vn3" + ListCapacity[i].id + "\">";
                    var listProduct3 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCapa && p.ViewHomes == true && p.Design == 0 && p.Material == 0).OrderBy(p => p.Ord).ToList();
                    foreach (var item in listProduct3)
                    {
                        int idcate = int.Parse(item.idCate.ToString());
                        string imagemanu3 = "";
                        var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                        foreach (var item1 in listManu)
                        {
                            imagemanu3 = item1.Images;

                        }
                        chuoi += "<div class=\"Tear_1\">";
                        chuoi += "<div class=\"OrderNow\">";
                        chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                        chuoi += "</div>";
                        if (item.New == true)
                            chuoi += "<div class=\"Note\"></div>";
                        chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu3 + ") no-repeat\"></div>";
                        chuoi += "<div class=\"img\">";
                        chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                        chuoi += " </div>";
                        chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                        chuoi += "<div class=\"Box_Tear\">";
                        chuoi += "<div class=\"Left_BoxTear\">";
                        chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                        chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                        chuoi += "</div>";
                        chuoi += "<div class=\"Right_BoxTear\">";
                        chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                        if (item.Material == 0)
                            chuoi += " <span class=\"Material\">Inox SUS304</span>";
                        else
                            chuoi += " <span class=\"Material\">Nhựa</span>";
                        chuoi += "  </div>";
                        chuoi += " </div>";

                        chuoi += "  </div>";

                    }

                    chuoi += "</div>";
                    chuoi += "<div id=\"vn4" + ListCapacity[i].id + "\" style=\"display:none\">";


                    var listProduct4 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCapa && p.ViewHomes == true && p.Design == 0 && p.Material == 1).OrderBy(p => p.Ord).ToList();
                    foreach (var item in listProduct4)
                    {
                        int idcate = int.Parse(item.idCate.ToString());
                        string imagemanu4 = "";
                        var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                        foreach (var item1 in listManu)
                        {
                            imagemanu4 = item1.Images;

                        }
                        chuoi += "<div class=\"Tear_1\">";
                        chuoi += "<div class=\"OrderNow\">";
                        chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                        chuoi += "</div>";
                        if (item.New == true)
                            chuoi += "<div class=\"Note\"></div>";
                        chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu4 + ") no-repeat\"></div>";
                        chuoi += "<div class=\"img\">";
                        chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                        chuoi += " </div>";
                        chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                        chuoi += "<div class=\"Box_Tear\">";
                        chuoi += "<div class=\"Left_BoxTear\">";
                        chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                        chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                        chuoi += "</div>";
                        chuoi += "<div class=\"Right_BoxTear\">";
                        chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                        if (item.Material == 0)
                            chuoi += " <span class=\"Material\">Inox SUS304</span>";
                        else
                            chuoi += " <span class=\"Material\">Nhựa</span>";
                        chuoi += "  </div>";
                        chuoi += " </div>";

                        chuoi += "  </div>";

                    }


                    chuoi += "</div>";


                    chuoi += "</div>";
                    chuoi += " </div>";
                    chuoi += " </div>";
                    chuoi += " </div>";
                    chuoi += "<div class=\"Clear\"></div>";
                }
                else
                {
                    chuoi+="<div class=\"cls_Product\">";
                    chuoi += "<h2 class=\"Xemthem\"><a href=\"/" + ListCapacity[i].Tag + "-dt\" title=\"" + ListCapacity[i].Name + "\">" + ListCapacity[i].Name + " <span> &rarr; Xem</span></a></h2>";
                    chuoi+="</div>";
                }
           }
            ViewBag.chuoi = chuoi;
                return PartialView();
        }
        string nUrl = "";
        public string UrlProduct(int idCate)
        {
            var ListMenu = db.tblGroupProducts.Where(p => p.id == idCate).ToList();
            for (int i = 0; i < ListMenu.Count; i++)
            {
                nUrl = " <a href=\"/" + ListMenu[i].Tag + ".html\" title=\"" + ListMenu[i].Name + "\"> " + " " + ListMenu[i].Name + "</a> <i></i>" + nUrl;
                string ids = ListMenu[i].ParentID.ToString();
                if (ids != null && ids != "")
                {
                    int id = int.Parse(ListMenu[i].ParentID.ToString());
                    UrlProduct(id);
                }
            }
            return nUrl;
        }
        public ActionResult ProductDetail(string tag)
        {
            string url = Request.Url.ToString();
            string[] mangurl = url.Split('.');
            if (mangurl[mangurl.Length - 1] == "html")
            {
                string[] mangurl2 = tag.Split('_');
                string dodai = mangurl2[mangurl2.Length - 1];
                tag = tag.Substring(0,( tag.Length-(dodai.Length+1)) );
                return Redirect("/san-pham/" + tag + "");
            }
            var tblproduct = db.tblProducts.First(p => p.Tag == tag);
            tblproduct.Visit = tblproduct.Visit + 1;
            db.SaveChanges();
            ViewBag.Title = "<title>" + tblproduct.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblproduct.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblproduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblproduct.Keyword + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://bonnuoc.vn/san-pham/" + StringClass.NameToTag(tag) + "\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + tblproduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblproduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://bonnuoc.vn" + tblproduct.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblproduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://bonnuoc.vn" + tblproduct.ImageLinkThumb + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://bigsea.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblproduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta; int idcate = int.Parse(tblproduct.idCate.ToString());
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlProduct(idcate);
            string chuoitag = "";
            if (tblproduct.Keyword != null)
            {
                string Chuoi = tblproduct.Keyword;
                string[] Mang = Chuoi.Split(',');
                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tagsp = StringClass.NameToTag(Mang[i]);
                    chuoitag += "<h2><a href=\"/Tag/" + tagsp + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;
            int idcap = int.Parse(tblproduct.Capacity.ToString());
            var tblcapacity = db.tblCapacities.Find(idcap);
            ViewBag.capa = "<h3><a href=\"/" + tblcapacity.Tag + "-dt\" title=\"" + tblcapacity.Name + "\">" + tblcapacity.Capacity + "</a></h3>";
            ViewBag.cappacity = tblcapacity.Capacity;
            ViewBag.songuoisd = tblcapacity.Note;
            //Load tính năng
            var ListGroupCri = db.tblGroupCriterias.Where(p => p.idCate == idcate).ToList();
            List<int> Mang1 = new List<int>();
            for (int i = 0; i < ListGroupCri.Count; i++)
            {
                Mang1.Add(int.Parse(ListGroupCri[i].idCri.ToString()));
            }
            int idp = int.Parse(tblproduct.id.ToString());
            var ListCri = db.tblCriterias.Where(p => Mang1.Contains(p.id) && p.Active == true).ToList();
            string chuoi = "";
            #region[Lọc thuộc tính]


            for (int i = 0; i < ListCri.Count; i++)
            {
                int idCre = int.Parse(ListCri[i].id.ToString());
                var ListCr = (from a in db.tblConnectCriterias
                              join b in db.tblInfoCriterias on a.idCre equals b.id
                              where a.idpd == idp && b.idCri == idCre && b.Active == true
                              select new
                              {
                                  b.Name,
                                  b.Url,
                                  b.Ord
                              }).OrderBy(p => p.Ord).ToList();
                if (ListCr.Count > 0)
                {
                    chuoi += "<tr>";
                    chuoi += "<td>" + ListCri[i].Name + "</td>";
                    chuoi += "<td>";
                    int dem = 0;
                    string num = "";
                    if (ListCr.Count > 1)
                        num = "⊹ ";
                    foreach (var item in ListCr)
                        if (item.Url != null && item.Url != "")
                        {
                            chuoi += "<a href=\"" + item.Url + "\" title=\"" + item.Name + "\">";
                            if (dem == 1)
                                chuoi += num + item.Name;
                            else
                                chuoi += num + item.Name;
                            dem = 1;
                            chuoi += "</a>";
                        }
                        else
                        {
                            if (dem == 1)
                                chuoi += num + item.Name + "</br> ";
                            else
                                chuoi += num + item.Name + "</br> "; ;
                            dem = 1;
                        }
                    chuoi += "</td>";
                    chuoi += " </tr>";
                }
            }
            #endregion
            ViewBag.video = db.tblGroupProducts.Find(idcate).Video;
             ViewBag.chuoi = chuoi;
            //load sản phẩm gần giá
            string chuoilq = "";
             
            int nPrice = int.Parse(tblproduct.PriceSale.ToString());
            var List_01 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idcap && p.PriceSale <= nPrice && p.Tag != tag).OrderByDescending(p => p.PriceSale).Take(6).ToList();
            var List_02 = db.tblProducts.Where(p => p.Active == true && p.Capacity==idcap && p.PriceSale > nPrice).OrderBy(p => p.PriceSale).Take(6).ToList();
            for (int i = 0; i < 10; i++)
            {
                int gia1 = 0; int gia2 = 0;
                if (List_01.Count > i)
                {
                    gia1 = nPrice - int.Parse(List_01[i].PriceSale.ToString());
                }
                if (List_02.Count > i)
                {
                    gia2 = (nPrice - int.Parse(List_02[i].PriceSale.ToString())) * (-1);
                }
                if (gia1 > gia2)
                {
                    if (gia2 > 0)
                    {

                        chuoilq += " <div class=\"Tear_cg\">";
                        chuoilq += " <a href=\"/San-pham/" + List_02[i].Tag + "\" class=\"name_cg\" title=\"" + List_02[i].Name + "\">" + List_02[i].Name + "</a>";
                        chuoilq += " <img src=\"" + List_02[i].ImageLinkThumb + "\" title=\"" + List_02[i].Name + "\" />";
                        chuoilq += "<span class=\"Price\">Giá : <span>" + string.Format("{0:#,#}", List_02[i].PriceSale) + "đ</span></span>";
                        chuoilq += "<span class=\"PriceSale\">Giá thị trường : <span>" + string.Format("{0:#,#}", List_02[i].Price) + "đ</span></span>";
                        chuoilq += "</div>";
                    }
                    else
                    {
                        chuoilq += " <div class=\"Tear_cg\">";
                        chuoilq += " <a href=\"/San-pham/" + List_01[i].Tag + "\" class=\"name_cg\" title=\"" + List_01[i].Name + "\">" + List_01[i].Name + "</a>";
                        chuoilq += " <img src=\"" + List_01[i].ImageLinkThumb + "\" title=\"" + List_01[i].Name + "\" />";
                        chuoilq += "<span class=\"Price\">Giá : <span>" + string.Format("{0:#,#}", List_01[i].PriceSale) + "đ</span></span>";
                        chuoilq += "<span class=\"PriceSale\">Giá thị trường : <span>" + string.Format("{0:#,#}", List_01[i].Price) + "đ</span></span>";
                        chuoilq += "</div>";
                    }

                }

                else if (gia1 < gia2)
                {
                    if (gia1 > 0)
                    {
                        chuoilq += " <div class=\"Tear_cg\">";
                        chuoilq += " <a href=\"/San-pham/" + List_01[i].Tag + "\" class=\"name_cg\" title=\"" + List_01[i].Name + "\">" + List_01[i].Name + "</a>";
                        chuoilq += " <img src=\"" + List_01[i].ImageLinkThumb + "\" title=\"" + List_01[i].Name + "\" />";
                        chuoilq += "<span class=\"Price\">Giá : <span>" + string.Format("{0:#,#}", List_01[i].PriceSale) + "đ</span></span>";
                        chuoilq += "<span class=\"PriceSale\">Giá thị trường : <span>" + string.Format("{0:#,#}", List_01[i].Price) + "đ</span></span>";
                        chuoilq += "</div>";
                    }
                    else
                    {

                        chuoilq += " <div class=\"Tear_cg\">";
                        chuoilq += " <a href=\"/San-pham/" + List_02[i].Tag + "\" class=\"name_cg\" title=\"" + List_02[i].Name + "\">" + List_02[i].Name + "</a>";
                        chuoilq += " <img src=\"" + List_02[i].ImageLinkThumb + "\" title=\"" + List_02[i].Name + "\" />";
                        chuoilq += "<span class=\"Price\">Giá : <span>" + string.Format("{0:#,#}", List_02[i].PriceSale) + "đ</span></span>";
                        chuoilq += "<span class=\"PriceSale\">Giá thị trường : <span>" + string.Format("{0:#,#}", List_02[i].Price) + "đ</span></span>";
                        chuoilq += "</div>";
                    }
                }

                else
                {
                    if (gia1 > 0)
                    {
                        chuoilq += " <div class=\"Tear_cg\">";
                        chuoilq += " <a href=\"/San-pham/" + List_01[i].Tag + "\" class=\"name_cg\" title=\"" + List_01[i].Name + "\">" + List_01[i].Name + "</a>";
                        chuoilq += " <img src=\"" + List_01[i].ImageLinkThumb + "\" title=\"" + List_01[i].Name + "\" />";
                        chuoilq += "<span class=\"Price\">Giá  : <span>" + string.Format("{0:#,#}", List_01[i].PriceSale) + "đ</span></span>";
                        chuoilq += "<span class=\"PriceSale\">Giá thị trường: <span>" + string.Format("{0:#,#}", List_01[i].Price) + "đ</span></span>";
                        chuoilq += "</div>";
                    }
                }
            }
            ViewBag.chuoilq = chuoilq;
            var tblManu = (from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b).Take(1).ToList();
            ViewBag.manu = tblManu[0].Name;
            ViewBag.urlmanu = tblManu[0].Images;
            int nMaterial=int.Parse(tblproduct.Material.ToString());
            int nDesign=int.Parse(tblproduct.Design.ToString());
            var kiemtra = db.tblProducts.Where(p => p.Active == true && p.idCate==idcate && p.Design==nDesign && p.Material==nMaterial && p.id!=idp && p.Capacity==idcap).Take(1).ToList();
            if(kiemtra.Count>0)
                ViewBag.xemthem = "<div class=\"xemthemchitiet\">Bạn có thể xem thêm bồn nước " + tblManu[0].Name + " cùng dung tích khác : <a href=\"/san-pham/" + kiemtra[0].Tag + "\" title=\"" + kiemtra[0].Name + "\">   " + kiemtra[0].Name + "</a></div>";

            var ImageList = (from a in db.tblConnectImages join b in db.tblImages on a.idImg equals b.id where a.idCate == idcate select b).OrderByDescending(p => p.Ord).Take(1).ToList();
            if(ImageList.Count>0)
            {
                ViewBag.chuoianh = "<a href=\"" + ImageList[0].Url + "\" title=\"" + ImageList[0].Name + "\"><img src=\"" + ImageList[0].Images + "\" alt=\"" + ImageList[0].Name + "\" /></a>";
            }
            return View(tblproduct);
        }
        public PartialViewResult LeftPartialProductDetail(string tag)
        {
            StringBuilder chuoisupport = new StringBuilder();
            var listSupport = db.tblSupports.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            for (int i = 0; i < listSupport.Count; i++)
            {


                chuoisupport.Append("<div class=\"Line_Buttom\"></div>");
                chuoisupport.Append("<div class=\"Tear_Supports\">");
                chuoisupport.Append("<div class=\"Left_Tear_Support\">");
                chuoisupport.Append("<span class=\"htv1\">" + listSupport[i].Mission + ":</span>");
                chuoisupport.Append("<span class=\"htv2\">" + listSupport[i].Name + " :</span>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("<div class=\"Right_Tear_Support\">");
                chuoisupport.Append("<div class=\"topTearSupport\">");

                chuoisupport.Append("<a href=\"tel:" + listSupport[i].Mobile + "\" title=\"" + listSupport[i].Name + "\"><img src=\"/Content/Display/iCon/logo_zalo.png\" alt=\"" + listSupport[i].Name + "\" /></a>");
                chuoisupport.Append("<a href=\"tel:" + listSupport[i].Mobile + "\" title=\"" + listSupport[i].Name + "\"><img src=\"/Content/Display/iCon/Viber_logo.png\" alt=\"" + listSupport[i].Name + "\" /></a>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("<div class=\"bottomTearSupport\">");

                chuoisupport.Append("<span>" + listSupport[i].Mobile + "</span>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("</div>");
                chuoisupport.Append("</div>");

            }
            ViewBag.chuoisp = chuoisupport.ToString();
            int idCate = int.Parse(db.tblProducts.First(p => p.Tag == tag).idCate.ToString());
            var listMenu = from a in db.tblProducts join b in db.tblGroupProducts on a.idCate equals b.id where a.Tag == tag join c in db.tblGroupProducts on b.ParentID equals c.ParentID select c;
            string chuoi = "";
            foreach (var item in listMenu)
            {
                chuoi += "<h3> <a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">› " + item.Name + "</a></h3>";

            }
            ViewBag.chuoi = chuoi;

            string chuoipd = "";
            var ListProduct = db.tblProducts.Where(p=>p.Active==true && p.idCate==idCate && p.Tag!=tag).OrderByDescending(p => p.Visit).Take(8).ToList();
            foreach(var item in ListProduct)
            {
                int idcate = int.Parse(item.idCate.ToString());
                string imagemanu2 = "";
                var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                foreach (var item1 in listManu)
                {
                    imagemanu2 = item1.Images;

                }
                chuoipd += "<div class=\"Tear_1\">";
                chuoipd += "<div class=\"OrderNow\">";
                chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                chuoipd += "</div>";
                if (item.New == true)
                    chuoipd += "<div class=\"Note\"></div>";
                chuoipd += "<div class=\"Manu\" style=\"background:url(" + imagemanu2 + ") no-repeat\"></div>";
                chuoipd += "<div class=\"img\">";
                chuoipd += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                chuoipd += " </div>";
                chuoipd += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                chuoipd += "<div class=\"Box_Tear\">";
                chuoipd += "<div class=\"Left_BoxTear\">";
                chuoipd += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                chuoipd += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                chuoipd += "</div>";
                chuoipd += "<div class=\"Right_BoxTear\">";
                chuoipd += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                if (item.Material == 0)
                    chuoipd += " <span class=\"Material\">Inox SUS304" + item.Material + "" + item.Material + "</span>";
                else
                    chuoipd += " <span class=\"Material\">Nhựa" + item.Material + "" + item.Material + "</span>";
                chuoipd += "  </div>";
                chuoipd += " </div>";
                chuoipd += "<div class=\"Clear\"></div>";
                chuoipd += "  </div>";
                chuoipd += "<div class=\"Clear\"></div>";
            }
            ViewBag.chuoipd = chuoipd;
             return PartialView(db.tblConfigs.First());
        }
        public ActionResult ListCapacity(string tag,string hang)
        {
            var Capacity = db.tblCapacities.First(p => p.Tag == tag);
            if(hang==null || hang=="")
            {
                ViewBag.Title = "<title>" + Capacity.Title + "</title>";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + Capacity.Title + "\" />";
                ViewBag.Description = "<meta name=\"description\" content=\"" + Capacity.Description + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + Capacity.Keyword + "\" /> ";
                ViewBag.canonical = "<link rel=\"canonical\" href=\"http://bonnuoc.vn/" + StringClass.NameToTag(tag) + "-dt\" />";
                string meta = "";
                meta += "<meta itemprop=\"name\" content=\"" + Capacity.Name + "\" />";
                meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta itemprop=\"description\" content=\"" + Capacity.Description + "\" />";
                meta += "<meta itemprop=\"image\" content=\"http://bonnuoc.vn" + Capacity.Images + "\" />";
                meta += "<meta property=\"og:title\" content=\"" + Capacity.Title + "\" />";
                meta += "<meta property=\"og:type\" content=\"product\" />";
                meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta property=\"og:image\" content=\"http://bonnuoc.vn" + Capacity.Images + "\" />";
                meta += "<meta property=\"og:site_name\" content=\"http://bigsea.vn\" />";
                meta += "<meta property=\"og:description\" content=\"" + Capacity.Description + "\" />";
                meta += "<meta property=\"fb:admins\" content=\"\" />";
                ViewBag.Meta = meta;
                int idCap = Capacity.id;
                var ListManuitem = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap).OrderBy(p => p.Ord).ToList();
                List<string> Mangitem = new List<string>();
                foreach (var itemmanu in ListManuitem)
                {
                    Mangitem.Add(itemmanu.idCate.ToString());
                }
                var ListMenuProduct = db.tblGroupProducts.Where(p => p.Active == true && Mangitem.Contains(p.id.ToString())).OrderBy(p => p.Ord).ToList();
                string chuoimenu = "";
                foreach (var item in ListMenuProduct)
                {
                    chuoimenu += "<li><a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">" + item.Name + "</a></li>";
                }
                ViewBag.chuoimenu = chuoimenu;
                //Load danh sách sản phẩm
                string chuoi = "";
                chuoi += "<div class=\"cls_Product\">";

                chuoi += " <div class=\"Content_ClsProduct\">";
                chuoi += "<div class=\"ClsProduct_Tear\">";
                chuoi += "<div class=\"nVar_01\">";
                chuoi += "<div class=\"Left_Nvar_01\">";
                chuoi += "<h3>Bồn đứng</h3>";
                chuoi += "</div>";
                chuoi += "<div class=\"Center_Nvar_01\">";
                chuoi += "<a href=\"javascript:void(0)\" class=\"tn1" + Capacity.id + " set\" onclick=\"javascript:return Tab('n1" + Capacity.id + "-n2" + Capacity.id + "');\">Inox</a><a class=\"number tn2" + Capacity.id + "\" onclick=\"javascript:return Tab('n2" + Capacity.id + "-n1" + Capacity.id + "');\">Nhựa</a>";
                chuoi += "</div>";
                chuoi += "<div class=\"Right_Nvar_01\">";
                chuoi += "<div class=\"stairs\">";
                chuoi += "<a href=\"#neo-" + 1 + "\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                chuoi += "<i class=\"Elevator\"></i>";
                chuoi += "<a href=\"#neo-" + 1 + "\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                chuoi += "</div>";
                chuoi += "</div>";
                chuoi += "</div>";


                chuoi += "<div class=\"List_ProductHomes\">";
                chuoi += " <div id=\"vn1" + Capacity.id + "\">";


                var listProduct1 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap  && p.Design == 1 && p.Material == 0).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct1)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }


                chuoi += "</div>";
                chuoi += "<div id=\"vn2" + Capacity.id + "\" style=\"display:none\">";


                var listProduct2 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap  && p.Design == 1 && p.Material == 1).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct2)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu2 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu2 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu2 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }

                chuoi += "</div>";


                chuoi += "</div>";
                chuoi += "</div>";
                chuoi += " <div class=\"ClsProduct_Tear\">";
                chuoi += "<div class=\"nVar_01\">";
                chuoi += "<div class=\"Left_Nvar_01\">";
                chuoi += "<h3>Bồn nằm</h3>";
                chuoi += "</div>";
                chuoi += "<div class=\"Center_Nvar_01\">";
                chuoi += " <a href=\"javascript:void(0)\" class=\"tn3" + Capacity.id + " set\" onclick=\"javascript:return Tab('n3" + Capacity.id + "-n4" + Capacity.id + "');\">Inox</a><a title=\"v4\" class=\"number tn4" + Capacity.id + "\" onclick=\"javascript:return Tab('n4" + Capacity.id + "-n3" + Capacity.id + "');\">Nhựa</a>";
                chuoi += "</div>";
                chuoi += " <div class=\"Right_Nvar_01\">";
                chuoi += "<div class=\"stairs\">";
                chuoi += "<a href=\"\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                chuoi += "<i class=\"Elevator\"></i>";
                chuoi += "<a href=\"\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                chuoi += " </div>";
                chuoi += "</div>";
                chuoi += " </div>";
                chuoi += "<div class=\"List_ProductHomes\">";
                chuoi += "<div id=\"vn3" + Capacity.id + "\">";
                var listProduct3 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap  && p.Design == 0 && p.Material == 0).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct3)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu3 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu3 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu3 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }

                chuoi += "</div>";
                chuoi += "<div id=\"vn4" + Capacity.id + "\" style=\"display:none\">";


                var listProduct4 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap && p.Design == 0 && p.Material == 1).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct4)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu4 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu4 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu4 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }


                chuoi += "</div>";


                chuoi += "</div>";
                chuoi += " </div>";
                chuoi += " </div>";
                chuoi += " </div>";

                ViewBag.chuoi = chuoi;
            }
            else
            {
                var tblManufacture = db.tblManufactures.First(p => p.Tag==hang);
                ViewBag.Title = "<title>" + Capacity.Name + " " + tblManufacture.Name + "</title>";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + Capacity.Title + " " + tblManufacture.Name + "\" />";
                ViewBag.Description = "<meta name=\"description\" content=\"" + Capacity.Description + " - " + tblManufacture.Name + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + Capacity.Keyword + "," + tblManufacture.Name + "\" /> ";
                ViewBag.canonical = "<link rel=\"canonical\" href=\"http://bonnuoc.vn/" + StringClass.NameToTag(tag) + "-dt/" + tblManufacture.Tag + "\" />";
                string meta = "";
                meta += "<meta itemprop=\"name\" content=\"" + Capacity.Name + " " + tblManufacture.Name + "\" />";
                meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta itemprop=\"description\" content=\"" + Capacity.Description + "" + tblManufacture.Description + "\" />";
                meta += "<meta itemprop=\"image\" content=\"http://bonnuoc.vn" + Capacity.Images + "\" />";
                meta += "<meta property=\"og:title\" content=\"" + Capacity.Title + " " + tblManufacture.Name + "\" />";
                meta += "<meta property=\"og:type\" content=\"product\" />";
                meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta property=\"og:image\" content=\"http://bonnuoc.vn" + Capacity.Images + "\" />";
                meta += "<meta property=\"og:site_name\" content=\"http://bigsea.vn\" />";
                meta += "<meta property=\"og:description\" content=\"" + Capacity.Description + "" + tblManufacture.Description + "\" />";
                meta += "<meta property=\"fb:admins\" content=\"\" />";
                ViewBag.Meta = meta;
                int idCap = Capacity.id;
                var ListManuitem = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap).OrderBy(p => p.Ord).ToList();
                List<string> Mangitem = new List<string>();
                foreach(var itemmanu in ListManuitem)
                {
                    Mangitem.Add(itemmanu.idCate.ToString());
                }
                var ListMenuProduct = db.tblGroupProducts.Where(p => p.Active == true && Mangitem.Contains(p.id.ToString())).OrderBy(p => p.Ord).ToList();
                string chuoimenu = "";
                foreach (var item in ListMenuProduct)
                {
                    chuoimenu += "<li><a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">" + item.Name + "</a></li>";
                }
                ViewBag.chuoimenu = chuoimenu;
                //Load danh sách sản phẩm
                string chuoi = "";
                chuoi += "<div class=\"cls_Product\">";
                chuoi += "<div id=\"neo-1\" class=\"cneo\"></div>";

                chuoi += " <div class=\"Content_ClsProduct\">";
                chuoi += "<div class=\"ClsProduct_Tear\">";
                chuoi += "<div class=\"nVar_01\">";
                chuoi += "<div class=\"Left_Nvar_01\">";
                chuoi += "<h3>Bồn đứng</h3>";
                chuoi += "</div>";
                chuoi += "<div class=\"Center_Nvar_01\">";
                chuoi += "<a href=\"javascript:void(0)\" class=\"tn1" + Capacity.id + " set\" onclick=\"javascript:return Tab('n1" + Capacity.id + "-n2" + Capacity.id + "');\">Inox</a><a class=\"number tn2" + Capacity.id + "\" onclick=\"javascript:return Tab('n2" + Capacity.id + "-n1" + Capacity.id + "');\">Nhựa</a>";
                chuoi += "</div>";
                chuoi += "<div class=\"Right_Nvar_01\">";
                chuoi += "<div class=\"stairs\">";
                chuoi += "<a href=\"#neo-" + 1 + "\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                chuoi += "<i class=\"Elevator\"></i>";
                chuoi += "<a href=\"#neo-" + 1 + "\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                chuoi += "</div>";
                chuoi += "</div>";
                chuoi += "</div>";


                chuoi += "<div class=\"List_ProductHomes\">";
                chuoi += " <div id=\"vn1" + Capacity.id + "\">";
                                int idManu=tblManufacture.id;

                                var ListGroup = from a in db.tblConnectManuProducts join b in db.tblGroupProducts on a.idCate equals b.id where b.Active == true && a.idManu == idManu select b;
                 List<string> Mangidcate = new List<string>();
                foreach(var item in ListGroup)
                {
                    string idc = item.id.ToString();
                    Mangidcate.Add(idc);
                }
                var listProduct1 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap  && p.Design == 1 && p.Material == 0 && Mangidcate.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct1)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }


                chuoi += "</div>";
                chuoi += "<div id=\"vn2" + Capacity.id + "\" style=\"display:none\">";


                var listProduct2 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap && p.Design == 1 && p.Material == 1 && Mangidcate.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct2)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu2 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu2 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu2 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }

                chuoi += "</div>";


                chuoi += "</div>";
                chuoi += "</div>";
                chuoi += " <div class=\"ClsProduct_Tear\">";
                chuoi += "<div class=\"nVar_01\">";
                chuoi += "<div class=\"Left_Nvar_01\">";
                chuoi += "<h3>Bồn nằm</h3>";
                chuoi += "</div>";
                chuoi += "<div class=\"Center_Nvar_01\">";
                chuoi += " <a href=\"javascript:void(0)\" class=\"tn3" + Capacity.id + " set\" onclick=\"javascript:return Tab('n3" + Capacity.id + "-n4" + Capacity.id + "');\">Inox</a><a title=\"v4\" class=\"number tn4" + Capacity.id + "\" onclick=\"javascript:return Tab('n4" + Capacity.id + "-n3" + Capacity.id + "');\">Nhựa</a>";
                chuoi += "</div>";
                chuoi += " <div class=\"Right_Nvar_01\">";
                chuoi += "<div class=\"stairs\">";
                chuoi += "<a href=\"\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                chuoi += "<i class=\"Elevator\"></i>";
                chuoi += "<a href=\"\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                chuoi += " </div>";
                chuoi += "</div>";
                chuoi += " </div>";
                chuoi += "<div class=\"List_ProductHomes\">";
                chuoi += "<div id=\"vn3" + Capacity.id + "\">";
                var listProduct3 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap  && p.Design == 0 && p.Material == 0 && Mangidcate.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct3)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu3 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu3 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu3 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }

                chuoi += "</div>";
                chuoi += "<div id=\"vn4" + Capacity.id + "\" style=\"display:none\">";


                var listProduct4 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap && p.Design == 0 && p.Material == 1 && Mangidcate.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct4)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu4 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu4 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu4 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }


                chuoi += "</div>";


                chuoi += "</div>";
                chuoi += " </div>";
                chuoi += " </div>";
                chuoi += " </div>";

                ViewBag.chuoi = chuoi;
            }
            string chuoitag = "";

            if (Capacity.Keyword != null)
            {
                string Chuoi = Capacity.Keyword;
                string[] Mang = Chuoi.Split(',');
                List<int> araylist = new List<int>();
                for (int i = 0; i < Mang.Length; i++)
                {
                    string tagsp = StringClass.NameToTag(Mang[i]);
                    chuoitag += "<h2><a href=\"/TagCap/" + tagsp + "\" title=\"" + Mang[i] + "\">" + Mang[i] + "</a></h2>";
                }
            }
            ViewBag.chuoitag = chuoitag;
            return View(Capacity);
        }
        public ActionResult TagCapacity(string tag)
        {

            string[] Mang1 = StringClass.COnvertToUnSign1(tag.ToUpper()).Split('-');
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
            List<tblCapacity> ListCapicitys = (from c in db.tblCapacities select c).ToList();
            List<tblCapacity> ListCapaity = ListCapicitys.FindAll(delegate(tblCapacity math)
            {
                string kd = StringClass.COnvertToUnSign1(math.Keyword.ToUpper());
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
            ViewBag.Name = name;

            ViewBag.Title = "<title>" + name + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + name + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + name + " " + ListCapaity[0].Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + ListCapaity[0].Keyword + "\" /> ";
                 string meta = "";
                 meta += "<meta itemprop=\"name\" content=\"" + ListCapaity[0].Name + "\" />";
                meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
                meta += "<meta itemprop=\"description\" content=\"" + ListCapaity[0].Description + "\" />";
                meta += "<meta property=\"og:title\" content=\"" + ListCapaity[0].Title + "\" />";
                meta += "<meta property=\"og:type\" content=\"product\" />";
                meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
          
                meta += "<meta property=\"fb:admins\" content=\"\" />";
                ViewBag.Meta = meta;
                int idCap = ListCapaity[0].id;
                var ListMenuProduct = from a in db.tblProducts join b in db.tblGroupProducts on a.idCate equals b.id where a.Capacity == idCap select b;
                string chuoimenu = "";
                foreach (var item in ListMenuProduct)
                {
                    chuoimenu += "<li><a href=\"/" + item.Tag + ".html\" title=\"" + item.Name + "\">" + item.Name + "</a></li>";
                }
                ViewBag.chuoimenu = chuoimenu;
                //Load danh sách sản phẩm
                string chuoi = "";
                chuoi += "<div class=\"cls_Product\">";

                chuoi += " <div class=\"Content_ClsProduct\">";
                chuoi += "<div class=\"ClsProduct_Tear\">";
                chuoi += "<div class=\"nVar_01\">";
                chuoi += "<div class=\"Left_Nvar_01\">";
                chuoi += "<h3>Bồn đứng</h3>";
                chuoi += "</div>";
                chuoi += "<div class=\"Center_Nvar_01\">";
                chuoi += "<a href=\"javascript:void(0)\" class=\"tn1" + ListCapaity[0].id + " set\" onclick=\"javascript:return Tab('n1" + ListCapaity[0].id + "-n2" + ListCapaity[0].id + "');\">Inox</a><a class=\"number tn2" + ListCapaity[0].id + "\" onclick=\"javascript:return Tab('n2" + ListCapaity[0].id + "-n1" + ListCapaity[0].id + "');\">Nhựa</a>";
                chuoi += "</div>";
                chuoi += "<div class=\"Right_Nvar_01\">";
                chuoi += "<div class=\"stairs\">";
                chuoi += "<a href=\"#neo-" + 1 + "\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                chuoi += "<i class=\"Elevator\"></i>";
                chuoi += "<a href=\"#neo-" + 1 + "\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                chuoi += "</div>";
                chuoi += "</div>";
                chuoi += "</div>";


                chuoi += "<div class=\"List_ProductHomes\">";
                chuoi += " <div id=\"vn1" + ListCapaity[0].id + "\">";


                var listProduct1 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap   && p.Design == 1 && p.Material == 0).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct1)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }


                chuoi += "</div>";
                chuoi += "<div id=\"vn2" + ListCapaity[0].id + "\" style=\"display:none\">";


                var listProduct2 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap   && p.Design == 1 && p.Material == 1).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct2)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu2 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu2 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu2 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }

                chuoi += "</div>";


                chuoi += "</div>";
                chuoi += "</div>";
                chuoi += " <div class=\"ClsProduct_Tear\">";
                chuoi += "<div class=\"nVar_01\">";
                chuoi += "<div class=\"Left_Nvar_01\">";
                chuoi += "<h3>Bồn nằm</h3>";
                chuoi += "</div>";
                chuoi += "<div class=\"Center_Nvar_01\">";
                chuoi += " <a href=\"javascript:void(0)\" class=\"tn3" + ListCapaity[0].id + " set\" onclick=\"javascript:return Tab('n3" + ListCapaity[0].id + "-n4" + ListCapaity[0].id + "');\">Inox</a><a title=\"v4\" class=\"number tn4" + ListCapaity[0].id + "\" onclick=\"javascript:return Tab('n4" + ListCapaity[0].id + "-n3" + ListCapaity[0].id + "');\">Nhựa</a>";
                chuoi += "</div>";
                chuoi += " <div class=\"Right_Nvar_01\">";
                chuoi += "<div class=\"stairs\">";
                chuoi += "<a href=\"\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                chuoi += "<i class=\"Elevator\"></i>";
                chuoi += "<a href=\"\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                chuoi += " </div>";
                chuoi += "</div>";
                chuoi += " </div>";
                chuoi += "<div class=\"List_ProductHomes\">";
                chuoi += "<div id=\"vn3" + ListCapaity[0].id + "\">";
                var listProduct3 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap && p.Design == 0 && p.Material == 0).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct3)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu3 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu3 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu3 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }

                chuoi += "</div>";
                chuoi += "<div id=\"vn4" + ListCapaity[0].id + "\" style=\"display:none\">";


                var listProduct4 = db.tblProducts.Where(p => p.Active == true && p.Capacity == idCap && p.Design == 0 && p.Material == 1).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct4)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu4 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu4 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu4 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304" + item.Material + "</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa" + item.Material + "</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }


                chuoi += "</div>";


                chuoi += "</div>";
                chuoi += " </div>";
                chuoi += " </div>";
                chuoi += " </div>";
                ViewBag.chuoi = chuoi;
           
             
            return View();
        }
        List<string> Mangphantu = new List<string>();
        public List<string> Arrayid(int idParent)
        {

            var ListMenu = db.tblGroupProducts.Where(p => p.ParentID == idParent).ToList();

            for (int i = 0; i < ListMenu.Count; i++)
            {
                Mangphantu.Add(ListMenu[i].id.ToString());
                int id = int.Parse(ListMenu[i].id.ToString());
                Arrayid(id);

            }

            return Mangphantu;
        }
        public ActionResult ListProduct(string tag)
        {
            var tblgroupproduct = db.tblGroupProducts.First(p => p.Tag == tag);
            int id = tblgroupproduct.id;
            ViewBag.Title = "<title>" + tblgroupproduct.Title + "</title>";
            ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tblgroupproduct.Title + "\" />";
            ViewBag.Description = "<meta name=\"description\" content=\"" + tblgroupproduct.Description + "\"/>";
            ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tblgroupproduct.Keyword + "\" /> ";
            ViewBag.canonical = "<link rel=\"canonical\" href=\"http://bonnuoc.vn/" + StringClass.NameToTag(tblgroupproduct.Tag) + ".html\" />";
            string meta = "";
            meta += "<meta itemprop=\"name\" content=\"" + tblgroupproduct.Name + "\" />";
            meta += "<meta itemprop=\"url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta itemprop=\"description\" content=\"" + tblgroupproduct.Description + "\" />";
            meta += "<meta itemprop=\"image\" content=\"http://bonnuoc.vn" + tblgroupproduct.Images + "\" />";
            meta += "<meta property=\"og:title\" content=\"" + tblgroupproduct.Title + "\" />";
            meta += "<meta property=\"og:type\" content=\"product\" />";
            meta += "<meta property=\"og:url\" content=\"" + Request.Url.ToString() + "\" />";
            meta += "<meta property=\"og:image\" content=\"http://bonnuoc.vn" + tblgroupproduct.Images + "\" />";
            meta += "<meta property=\"og:site_name\" content=\"http://bigsea.vn\" />";
            meta += "<meta property=\"og:description\" content=\"" + tblgroupproduct.Description + "\" />";
            meta += "<meta property=\"fb:admins\" content=\"\" />";
            ViewBag.Meta = meta;
            ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + UrlProduct(tblgroupproduct.id)+"<h1>"+tblgroupproduct.Title+"</h1>"; 
            var Manu = (from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == id select b).First();
            if (Arrayid(id).Count > 0)
            {
                List<string> Mang = new List<string>();
                Mang = Arrayid(id);
                //var Listpd = db.tblProducts.Where(p =>p.Active == true && Mang.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).ToList();

                //List<int> Mangint = new List<int>();
                //foreach (var item in Listpd)
                //{
                //    if (item.Capacity.ToString() != null && item.Capacity.ToString()!="")
                //    {
                //        int idca = int.Parse(item.Capacity.ToString());
                //        Mangint.Add(idca);
                //    }
                    
                //}
                var mangints = db.tblProducts.Where(p => p.Active == true && Mang.Contains(p.idCate.ToString())).Select(p => p.Capacity).ToList();
                var ListCapacity = db.tblCapacities.Where(p => p.Active == true && mangints.Contains(p.id)).OrderBy(p => p.Ord).ToList();
                string chuoicap = "";
                foreach (var item in ListCapacity)
                {
                    chuoicap += "<li><a href=\"" + item.Tag + "-dt\" title=\"" + item.Name + "\">" + item.Name + "</a></li>";
                }
                ViewBag.chuoicap = chuoicap;
            }
            else
            {

                var Listpd = db.tblProducts.Where(p => p.idCate == id && p.Active == true).Select(p=>p.Capacity).ToList();

                //List<int> Mangint = new List<int>();
                //foreach (var item in Listpd)
                //{
                //    if (item.Capacity.ToString() != null && item.Capacity.ToString() != "")
                //    {
                //        int idca = int.Parse(item.Capacity.ToString());
                //        Mangint.Add(idca);
                //    }
                //}
                var ListCapacity = db.tblCapacities.Where(p => p.Active == true && Listpd.Contains(p.id)).OrderBy(p => p.Ord).ToList();
                string chuoicap = "";
                foreach (var item in ListCapacity)
                {
                    chuoicap += "<li><a href=\"" + item.Tag + "-dt/"+Manu.Tag+"\" title=\"" + item.Name + " "+Manu.Name+"\">" + item.Name + " "+Manu.Name+"</a></li>";
                }
                ViewBag.chuoicap = chuoicap;
            }

            List<string> MangList = new List<string>();
            MangList = Arrayid(tblgroupproduct.id);
            if(MangList.Count==0)
            {
                MangList.Add(tblgroupproduct.id.ToString());
            }
            string chuoi = "";
            chuoi += "<div class=\"cls_Product\">";
            
            chuoi += " <div class=\"Content_ClsProduct\">";
            chuoi += "<div class=\"ClsProduct_Tear\">";
            chuoi += "<div class=\"nVar_01\">";
            chuoi += "<div class=\"Left_Nvar_01\">";
            chuoi += "<h3>Bồn đứng</h3>";
            chuoi += "</div>";
            chuoi += "<div class=\"Center_Nvar_01\">";

            chuoi += "</div>";
            chuoi += "<div class=\"Right_Nvar_01\">";
            chuoi += "<div class=\"stairs\">";
            chuoi += "<a href=\"#neo-1\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
            chuoi += "<i class=\"Elevator\"></i>";
            chuoi += "<a href=\"#neo-1\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
            chuoi += "</div>";
            chuoi += "</div>";
            chuoi += "</div>";


            chuoi += "<div class=\"List_ProductHomes\">";
            chuoi += " <div id=\"vn11\">";


            var listProduct = db.tblProducts.Where(p => p.Active == true && MangList.Contains(p.idCate.ToString())).OrderBy(p => p.Ord).OrderBy(p=>p.Design).OrderBy(p=>p.Material).ToList();
            foreach (var item in listProduct)
            {
                int idcate = int.Parse(item.idCate.ToString());
                string imagemanu = "";
                var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                foreach (var item1 in listManu)
                {
                    imagemanu = item1.Images;

                }
                chuoi += "<div class=\"Tear_1\">";
                chuoi += "<div class=\"OrderNow\">";
                chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                chuoi += "</div>";
                if (item.New == true)
                    chuoi += "<div class=\"Note\"></div>";
                chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu + ") no-repeat\"></div>";
                chuoi += "<div class=\"img\">";
                chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                chuoi += " </div>";
                chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                chuoi += "<div class=\"Box_Tear\">";
                chuoi += "<div class=\"Left_BoxTear\">";
                chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                chuoi += "</div>";
                chuoi += "<div class=\"Right_BoxTear\">";
                chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                if (item.Material == 0)
                    chuoi += " <span class=\"Material\">Inox SUS304</span>";
                else
                    chuoi += " <span class=\"Material\">Nhựa</span>";
                chuoi += "  </div>";
                chuoi += " </div>";

                chuoi += "  </div>";

            }

            chuoi += "</div>";

            ViewBag.chuoi = chuoi;


            return View(tblgroupproduct);
        }
        public ActionResult Search( )
        {
            if(Session["Search"]!=null && Session["Search"]!="")
            {
                string tag = Session["Search"].ToString();
                ViewBag.Title = "<title>" + tag + "</title>";
                ViewBag.dcTitle = "<meta name=\"DC.title\" content=\"" + tag + "\" />";
                ViewBag.Description = "<meta name=\"description\" content=\"Tìm kiếm " + tag + "\"/>";
                ViewBag.Keyword = "<meta name=\"keywords\" content=\"" + tag + "\" /> ";
                ViewBag.Name = tag;
                ViewBag.nUrl = "<a href=\"/\" title=\"Trang chủ\" rel=\"nofollow\"><span class=\"iCon\"></span> Trang chủ</a><i></i>" + tag;



                var ListCapacity = db.tblCapacities.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
                string chuoicap = "";
                foreach (var item in ListCapacity)
                {
                    chuoicap += "<li><a href=\"/" + item.Tag + "-dt\" title=\"" + item.Name + " \">" + item.Name + " </a></li>";
                }
                ViewBag.chuoicap = chuoicap;



                string chuoi = "";
                chuoi += "<div class=\"cls_Product\">";

                chuoi += " <div class=\"Content_ClsProduct\">";
                chuoi += "<div class=\"ClsProduct_Tear\">";
                chuoi += "<div class=\"nVar_01\">";
                chuoi += "<div class=\"Left_Nvar_01\">";
                chuoi += "<h3>Bồn đứng</h3>";
                chuoi += "</div>";
                chuoi += "<div class=\"Center_Nvar_01\">";
                chuoi += "<a href=\"javascript:void(0)\" class=\"tn11 set\" onclick=\"javascript:return Tab('n11-n21');\">Inox</a><a class=\"number tn1\" onclick=\"javascript:return Tab('n21-n11');\">Nhựa</a>";
                chuoi += "</div>";
                chuoi += "<div class=\"Right_Nvar_01\">";
                chuoi += "<div class=\"stairs\">";
                chuoi += "<a href=\"#neo-1\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                chuoi += "<i class=\"Elevator\"></i>";
                chuoi += "<a href=\"#neo-1\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                chuoi += "</div>";
                chuoi += "</div>";
                chuoi += "</div>";


                chuoi += "<div class=\"List_ProductHomes\">";
                chuoi += " <div id=\"vn11\">";


                var listProduct1 = db.tblProducts.Where(p => p.Active == true && p.Name.Contains(tag) & p.Design == 1 && p.Material == 0).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct1)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }


                chuoi += "</div>";
                chuoi += "<div id=\"vn21\" style=\"display:none\">";


                var listProduct2 = db.tblProducts.Where(p => p.Active == true && p.Name.Contains(tag) && p.Design == 1 && p.Material == 1).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct2)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu2 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu2 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu2 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }

                chuoi += "</div>";


                chuoi += "</div>";
                chuoi += "</div>";
                chuoi += " <div class=\"ClsProduct_Tear\">";
                chuoi += "<div class=\"nVar_01\">";
                chuoi += "<div class=\"Left_Nvar_01\">";
                chuoi += "<h3>Bồn nằm</h3>";
                chuoi += "</div>";
                chuoi += "<div class=\"Center_Nvar_01\">";
                chuoi += " <a href=\"javascript:void(0)\" class=\"tn31 set\" onclick=\"javascript:return Tab('n31-n41');\">Inox</a><a title=\"v41\" class=\"number tn41\" onclick=\"javascript:return Tab('n41-n31');\">Nhựa</a>";
                chuoi += "</div>";
                chuoi += " <div class=\"Right_Nvar_01\">";
                chuoi += "<div class=\"stairs\">";
                chuoi += "<a href=\"\" title=\"Xuống tầng\"><i class=\"down\"></i> </a>";
                chuoi += "<i class=\"Elevator\"></i>";
                chuoi += "<a href=\"\" title=\"Lên tầng\"><i class=\"up\"></i></a>";
                chuoi += " </div>";
                chuoi += "</div>";
                chuoi += " </div>";
                chuoi += "<div class=\"List_ProductHomes\">";
                chuoi += "<div id=\"vn31\">";
                var listProduct3 = db.tblProducts.Where(p => p.Active == true && p.Name.Contains(tag) && p.Design == 0 && p.Material == 0).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct3)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu3 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu3 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu3 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }

                chuoi += "</div>";
                chuoi += "<div id=\"vn41\" style=\"display:none\">";


                var listProduct4 = db.tblProducts.Where(p => p.Active == true && p.Name.Contains(tag) && p.Design == 0 && p.Material == 1).OrderBy(p => p.Ord).ToList();
                foreach (var item in listProduct4)
                {
                    int idcate = int.Parse(item.idCate.ToString());
                    string imagemanu4 = "";
                    var listManu = from a in db.tblConnectManuProducts join b in db.tblManufactures on a.idManu equals b.id where a.idCate == idcate select b;
                    foreach (var item1 in listManu)
                    {
                        imagemanu4 = item1.Images;

                    }
                    chuoi += "<div class=\"Tear_1\">";
                    chuoi += "<div class=\"OrderNow\">";
                    chuoi += "<a rel=\"miendatwebPopup\" href=\"#popup_content\" onclick=\"javascript:return CreateOrder(" + item.id + ");\" title=\"Đặt hàng\">Đặt hàng</a>";
                    chuoi += "</div>";
                    if (item.New == true)
                        chuoi += "<div class=\"Note\"></div>";
                    chuoi += "<div class=\"Manu\" style=\"background:url(" + imagemanu4 + ") no-repeat\"></div>";
                    chuoi += "<div class=\"img\">";
                    chuoi += "<a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\"><img src=\"" + item.ImageLinkThumb + "\" alt=\"" + item.Name + "\" /></a>";
                    chuoi += " </div>";
                    chuoi += "<h3><a href=\"/san-pham/" + item.Tag + "\" title=\"" + item.Name + "\" class=\"Name\">" + item.Name + "</a></h3>";
                    chuoi += "<div class=\"Box_Tear\">";
                    chuoi += "<div class=\"Left_BoxTear\">";
                    chuoi += "<span class=\"PriceSale\">" + string.Format("{0:#,#}", item.PriceSale) + "đ</span>";
                    chuoi += "<span class=\"Price\">" + string.Format("{0:#,#}", item.Price) + "đ</span>";
                    chuoi += "</div>";
                    chuoi += "<div class=\"Right_BoxTear\">";
                    chuoi += " <span class=\"Quarantee\">Bảo hành <span>" + item.Warranty + "</span> năm</span>";
                    if (item.Material == 0)
                        chuoi += " <span class=\"Material\">Inox SUS304</span>";
                    else
                        chuoi += " <span class=\"Material\">Nhựa</span>";
                    chuoi += "  </div>";
                    chuoi += " </div>";

                    chuoi += "  </div>";

                }


                chuoi += "</div>";


                chuoi += "</div>";
                chuoi += " </div>";
                chuoi += " </div>";
                chuoi += " </div>";
                ViewBag.chuoi = chuoi;

            }
            Session["Search"] = "";
            return View();
        }

    }
}