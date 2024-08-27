using Abc.Mvc.Entity;
using Abc.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.Mvc.Controllers
{
    public class SepetController : Controller
    {
        private DataContext db = new DataContext();

        [Authorize]
        public ActionResult Index()
        {
            var userName = Session["UserId"] as string;

            if (string.IsNullOrEmpty(userName))
            {
                return View(new List<ProductModel>());
            }

           
            var sepetUrunleri = db.Sepet
                                  .Where(s => s.UserName == userName)
                                  .GroupBy(s => s.ProductId)
                                  .Select(g => new
                                  {
                                      ProductId = g.Key, 
                                      Quantity = g.Sum(s => s.Quantity)  
                                  })
                                  .ToList();

         
            var sepetViewModel = sepetUrunleri
                .Select(u => {
                    var product = db.Products.FirstOrDefault(p => p.Id == u.ProductId);

                    return new ProductModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Stock = product.Stock,
                        Image = product.Image,
                        CategoryId = product.CategoryId,
                        Quantity = u.Quantity  
                    };
                })
                .ToList();

            
            if (sepetViewModel.Count != 0)
            {
                double totalPrice = sepetViewModel.Sum(p => p.Price * p.Quantity);
                ViewBag.TotalPrice = totalPrice;
            }

            return View(sepetViewModel);
        }

        [HttpPost]
        public ActionResult AddSepet(int id)
        {
            var userName = Session["UserId"] as string;

          
            var existingProduct = db.Sepet
                                    .FirstOrDefault(s => s.ProductId == id && s.UserName == userName);

            if (existingProduct != null)
            {
                existingProduct.Quantity += 1;
            }
            else
            {
                var product = db.Products.FirstOrDefault(i => i.Id == id);

                if (product != null)
                {
                    var sepet = new Sepet
                    {
                        UserName = userName,
                        ProductId = product.Id, 
                        Quantity = 1 
                    };

                    db.Sepet.Add(sepet);
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveSepet(int id)
        {
          
            var userId = Session["UserId"] as string;

           
            var sepet = db.Sepet
                .FirstOrDefault(f => f.UserName == userId && f.ProductId == id);

            if (sepet != null)
            {
                if (sepet.Quantity > 1)
                {
                   
                    sepet.Quantity -= 1;
                }
                else
                {
                    
                    db.Sepet.Remove(sepet);
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
           
            var userName = Session["UserId"] as string;

            if (string.IsNullOrEmpty(userName))
            {
                
                return RedirectToAction("Login", "Account");
            }

          
            var cart = db.Sepet
                          .Where(s => s.UserName == userName)
                          .ToList();

           
            if (cart.Count == 0)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün yok!");
                return View(entity);
            }

            if (ModelState.IsValid)
            {
               
                SaveOrder(cart, entity);

               
                db.Sepet.RemoveRange(cart);
                db.SaveChanges();

               
                return View("Completed");
            }
            else
            {
                
                return View(entity);
            }
        }
        private void SaveOrder(List<Sepet> cart , ShippingDetails entity)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(111111, 999999).ToString();

         
            order.Total = cart.Sum(p => p.Product.Price * p.Quantity);

            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;
            order.Username = User.Identity.Name;

            order.AdresBasligi = entity.AdresBasligi;
            order.Adres = entity.Adres;
            order.Sehir = entity.Sehir;
            order.Semt = entity.Semt;
            order.Mahalle = entity.Mahalle;
            order.PostaKodu = entity.PostaKodu;
            order.OrderLines = new List<OrderLine>();

            foreach (var pr in cart)
            {
                var orderline = new OrderLine();
                orderline.Quantity = pr.Quantity;
                orderline.Price = pr.Product.Price; 
                orderline.ProductId = pr.ProductId;

                order.OrderLines.Add(orderline);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }








    }
}



