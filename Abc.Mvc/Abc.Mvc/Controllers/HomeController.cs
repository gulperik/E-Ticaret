using Abc.Mvc.Entity;
using Abc.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Abc.Mvc.Controllers
{
    public class HomeController : Controller
    {

        DataContext _context = new DataContext();
        // GET: Home

        public ActionResult Index()
        {

           
            var userId = User.Identity.Name; // veya uygun bir kullanıcı ID'si

            // 1. Adım: Veritabanından ürünleri çek
            var urunler = _context.Products
                .Where(i => i.IsHome && i.IsApproved)
                .Select(i => new
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image,
                    CategoryId = i.CategoryId
                })
                .ToList();

            // 2. Adım: Favori ürünler listesini al
            var favoriteProductIds = _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.productId)
                .ToList();

            // 3. Adım: Anonim türlerden ProductModel nesnelerine dönüştürme
            var productModels = urunler
                .Select(i => new ProductModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image,
                    CategoryId = i.CategoryId,
                    IsFavorite = favoriteProductIds.Contains(i.Id) // Favori kontrolü
                })
                .ToList();

            return View(productModels);
        }
        public ActionResult Details(int id)
        {

            Session["id"] = id ;
            //return View(_context.Products.Where(i => i.Id==id).FirstOrDefault());

            var product = _context.Products.FirstOrDefault(i => i.Id == id);
            var comments = _context.Comments.Where(c => c.ProductId == id).ToList();

            var viewModel = new DetailsComment
            {
                Product = product,
                Comment = comments,
                
            };




            return View(viewModel);
        }
      
        public ActionResult List(int? id)
        {

          
            var userId = User.Identity.Name; // veya uygun bir kullanıcı ID'si
            var urunler = _context.Products
            .Where(i => i.IsApproved);

            if (id != null)
            {
                urunler = urunler.Where(i => i.CategoryId == id);
            }
            var favoriteProductIds = _context.Favorites
              .Where(f => f.UserId == userId)
              .Select(f => f.productId)
              .ToList();
            // Sorguyu tamamlayıp veriyi belleğe çekiyoruz
            var urunlerList = urunler.ToList();

            // Veriyi aldıktan sonra, ViewModel'e dönüştürüyoruz
            var viewModel = urunlerList.Select(i => new ProductModel()
            {
                Id = i.Id,
                Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                Description = !string.IsNullOrEmpty(i.Description)
                              ? (i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description)
                              : "No description available.",
                Price = i.Price,
                Stock = i.Stock,
                Image = i.Image ?? "1.jpg",
                CategoryId = i.CategoryId,
                IsFavorite = favoriteProductIds.Contains(i.Id)

            }).AsQueryable();

            return View(viewModel.ToList());
        }


        public PartialViewResult GetCategories()
        {
            return PartialView(_context.Categories.ToList());
        }





    }
}