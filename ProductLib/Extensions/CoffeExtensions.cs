using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLib.Extensions
{
    public static class CoffeExtensions
    {
        public static CoffeResponse ToResponse(this Coffe prd)
        {
            return new CoffeResponse()
            {
                Id = prd.Id,
                Code = prd.Code,
                Name = prd.Name,
                Category = Enum.GetName<Category>(prd.Category),
                Price = prd.Price,

            };
        }
        public static Coffe ToEntity(this CoffeCreateReq req)
        {
            var category = Category.None;
            Category.TryParse(req.Category, out category);
            return new Coffe()
            {
                Id = Guid.NewGuid().ToString(),
                Code = req.Code,
                Name = req.Name,
                Category = category,
                Price = req.Price,
                CreatedOn = DateTime.Now,
            };
        }
        public static void Copy(this Coffe prd, CoffeUpdateReq req)
        {
            var category = Category.None;
            Category.TryParse(req.Category,out category);
            prd.Name = req.Name;
            prd.Category = category;
            prd.Price = req.Price;
        }
        public static Coffe Clone(this Coffe prd)
        {
            return new Coffe()
            {
                Id = prd.Id,
                Code = prd.Code,
                Name = prd.Name,
                Category = prd.Category,
                Price= prd.Price,
                CreatedOn = prd.CreatedOn,
            };
        }
        public static void Copy(this Coffe prd, Coffe other)
        {
            prd.Id = other.Id;
            prd.Code = other.Code;
            prd.Name = other.Name;
            prd.Category = other.Category;
            prd.Price = other.Price;
            prd.CreatedOn = other.CreatedOn;
           
        }
    }
}
