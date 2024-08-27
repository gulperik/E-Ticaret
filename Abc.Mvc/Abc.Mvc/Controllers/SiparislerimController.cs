using Abc.Mvc.Entity;
using Abc.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.Mvc.Controllers
{
    public class SiparislerimController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Siparislerim
        public ActionResult Index()
        {
             var userName = Session["UserId"] as string;

            if (string.IsNullOrEmpty(userName))
            {

                return RedirectToAction("Login", "Account");
            }

            var siparislerim = db.Orders
                                  .Where(s => s.Username == userName)
                                  .ToList();

            return View(siparislerim);
        }

        public ActionResult iptal(int id)
        {
            var siparis = db.Orders.Find(id);

            if(siparis.OrderState == EnumOrderState.Waiting)
            {
                db.Orders.Remove(siparis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Siparişi iptal edemezsiniz. Sipariş durumu uygun değil.";
                return RedirectToAction("Index");

            }

          


           
        }


        public ActionResult Detay(int id)
        {
        //    var detay = db.Orders.FirstOrDefault(i => i.Id == id);

        //    return View(detay);

              var entity = db.Orders.Where(i => i.Id == id)
                .Select(i => new OrderDetailsModel()
                {
                    OrderId = i.Id,
                    UserName = i.Username,
                    OrderNumber = i.OrderNumber,
                    Total = i.Total,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    AdresBasligi = i.AdresBasligi,
                    Adres = i.Adres,
                    Sehir = i.Sehir,
                    Semt = i.Semt,
                    Mahalle = i.Mahalle,
                    PostaKodu = i.PostaKodu,
                    Orderlines = i.OrderLines.Select(a => new OrderLineModel()
                    {
                        ProductId = a.ProductId,
                        ProductName = a.Product.Name.Length > 50 ? a.Product.Name.Substring(0, 47) + "..." : a.Product.Name,
                        Image = a.Product.Image,
                        Quantity = a.Quantity,
                        Price = a.Price
                    }).ToList()
                }).FirstOrDefault();

            return View(entity);




    }



    }
}