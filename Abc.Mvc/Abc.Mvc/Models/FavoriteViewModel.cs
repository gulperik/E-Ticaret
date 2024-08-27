using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.Mvc.Models
{
    public class FavoriteViewModel
    {
        public List<ProductModel> FavoriteProducts { get; set; }

        public class ProductModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
            public decimal Price { get; set; }
            // Diğer ürün detayları
        }
    }

  
}