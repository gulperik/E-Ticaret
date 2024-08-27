using Abc.Mvc.Entity;
using Abc.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.Mvc.Controllers
{
    
    public class FavoriteController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Favorite


       
        public ActionResult Index()
        {
           var userId = Session["UserId"] as string;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Kullanıcı ID'si yoksa giriş sayfasına yönlendir
            }


            var favoriteList = db.Favorites
                     .Where(f => f.UserId == userId)
                     .ToList();

            var favoriteProductIds = favoriteList
                                    .Select(f => f.productId)
                                    .ToList();

            var products = db.Products
                            .Where(p => favoriteProductIds.Contains(p.Id))
                            .ToList(); 

            
            var favoriteProducts = products
                                    .Select(p => new ProductModel
                                    {
                                        Id = p.Id,
                                        Name = p.Name.Length > 20 ? p.Description.Substring(0, 17) + "..." : p.Description,
                                        Image = p.Image,
                                        Price = p.Price,
                                        Description = p.Description.Length > 50 ? p.Description.Substring(0, 47) + "..." : p.Description,
                                        Stock = p.Stock,
                                        CategoryId = p.CategoryId,
                                        IsFavorite = favoriteProductIds.Contains(p.Id) //favori kontrolü
                                    })
                                    .ToList();

            return View(favoriteProducts);



        }


        public ActionResult AddedFavorite( int ProductId,bool isChecked)
        {
            if (isChecked)
            {
                // Yeni Favorite nesnesi oluşturun ve veritabanına ekleyin
                var favorite = new Favorite
                {
                    UserId = Session["UserId"] as string,
                    productId = ProductId
                };

                db.Favorites.Add(favorite);
                db.SaveChanges();
            }
            else
            {
                // Favoriyi kaldırın
                var favorite = db.Favorites
                    .FirstOrDefault(f => f.UserId == Session["UserId"] as string && f.productId == ProductId);

                if (favorite != null)
                {
                    db.Favorites.Remove(favorite);
                    db.SaveChanges();
                }
            }

            return Json(new { success = true });
        }

        public ActionResult RemoveFavorite(int ProductId, bool isChecked)
        {
            if (!isChecked)
            {
                // Kullanıcı kimliğini al
                var userId = Session["UserId"] as string;

                // Kullanıcı ve ürün ID'sine göre favoriyi bul
                var favorite = db.Favorites
                    .FirstOrDefault(f => f.UserId == userId && f.productId == ProductId);

                if (favorite != null)
                {
                    db.Favorites.Remove(favorite);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
            }

           
            return Json(new { success = false, message = "Favorite not found." });
        }


    }
}










