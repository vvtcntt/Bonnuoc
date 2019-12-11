using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bonnuoc.Models;
namespace Bonnuoc.Controllers.Display.Footer
{
    public class FooterController : Controller
    {
        BonnuocContext db = new BonnuocContext();
        // GET: Footer
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ControlFooter()
        {
            string chuoi = "";
            var listHotline = db.tblHotlines.Where(p => p.Active == true).OrderBy(p => p.Ord).ToList();
            foreach(var item in listHotline)
            {
                chuoi += "<div class=\"Tear_ft_1\">";
                chuoi += "  <div class=\"nVar_Tear_ft_1\">";
                chuoi += " <span class=\"name_ft\">"+item.Name+" <span>(Chi nhánh)</span></span>";
                chuoi += "</div>";
                chuoi += "<div class=\"Content_Tear_ft_1\">";
                chuoi += " <span class=\"ct_ft\">Địa chỉ : <span>" + item.Address + "</span></span>";
                chuoi += " <span class=\"ct_ft\">Điện thoại : <span>"+item.Mobile+"</span>   </span>";
                chuoi += " <span class=\"ct_ft\"> Hotline :<span>" + item.Hotline + "</span></span>";
                chuoi += "<span class=\"ct_ft\">Email: <span>"+item.Email+"</span></span>";
                chuoi += " <span class=\"Map_ft\"><span class=\"icon\"></span> Bản đồ đường đi</span>";
                chuoi += " </div>";
                chuoi += "</div>";
            }
            ViewBag.chuoi = chuoi;

            var Listchinhsach = db.tblNews.Where(p => p.Active == true && p.idCate == 3).OrderBy(p => p.Ord).ToList();
            string chuoichinhsach = "";
            foreach(var item in Listchinhsach)
            {
                chuoichinhsach += "<li><a href=\"/Tin-tuc/" + item.Tag + "\" title=\"" + item.Name + "\">" + item.Name + "</a></li>";
            }
            ViewBag.chinhsach = chuoichinhsach;
            var ListBaogia = db.tblGroupProducts.Where(p => p.Active == true && p.ParentID==null).OrderBy(p => p.Ord).Take(5).ToList();
            string chuoibaogia = "";
            foreach (var item in ListBaogia)
            {
                chuoibaogia += "<li><a href=\"/Bao-gia/" + item.Tag + "\" title=\"" + item.Name + "\">" + item.Name + "</a></li>";
            }
            ViewBag.chuoibaogia = chuoibaogia;
            var Listmuahang = db.tblNews.Where(p => p.Active == true && p.idCate ==8).OrderBy(p => p.Ord).ToList();
            string Chuoimuahang = "";
            foreach (var item in Listmuahang)
            {
                Chuoimuahang += "<li><a href=\"/Tin-tuc/" + item.Tag + "\" title=\"" + item.Name + "\">" + item.Name + "</a></li>";
            }
            ViewBag.muahang = Chuoimuahang;

            var Imagesadw = db.tblImages.Where(p => p.Active == true && p.idCate == 11).OrderByDescending(p => p.Ord).Take(1).ToList();
            if (Imagesadw.Count > 0)
                ViewBag.Chuoiimg = "<a href=\"" + Imagesadw[0].Url + "\" title=\"" + Imagesadw[0].Name + "\"><img src=\"" + Imagesadw[0].Images + "\" alt=\"" + Imagesadw[0].Name + "\" style=\"max-width:100%;\" /> </a>";

            return PartialView(db.tblConfigs.First());
        }
        public ActionResult Command(FormCollection collection)
        {

            string Name=collection["txtName"];
            string Mobile = collection["txtMobile"];
            string Email = collection["txtEmail"];
            var listregister = db.tblRegisters.Where(p => p.Email == Email).ToList();
            if(listregister.Count>0)
            { Session["Register"] = "<script>$(document).ready(function(){ alert('Đăng ký không thành công, Email của bạn đã được đăng ký từ trước, nếu bạn không nhận được thông tin khuyến mại vui lòng liên hệ qua hotline !') });</script>"; }
            else
            {
                tblRegister tblregister = new tblRegister();
                tblregister.Name = Name;
                tblregister.Mobile = Mobile;
                tblregister.Email = Email;
                db.tblRegisters.Add(tblregister);
                db.SaveChanges();
                Session["Register"] = "<script>$(document).ready(function(){ alert('Ban đã đặt đăng ký thành công') });</script>";
            }
           
            return Redirect("/");
        }
        public PartialViewResult callPartial()
        {
            return PartialView(db.tblConfigs.First());
        }
    }
}