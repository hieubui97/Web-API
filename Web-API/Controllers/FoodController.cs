using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class FoodController : ApiController
    {
        [HttpGet]
        public List<Food> GetFoodLists()
        {
            FoodDbContext db = new FoodDbContext();
            return db.Foods.ToList();
        }

        [HttpGet]
        public Food GetFood(int id)
        {
            FoodDbContext db = new FoodDbContext();
            var food = db.Foods.Where(x => x.id == id).FirstOrDefault();

            return food;
        }

        [HttpPost]
        public bool AddNewFood(Food f)
        {
            try
            {
                var db = new FoodDbContext();
                Food food = new Food();
                food.name = f.name;
                food.type = f.type;
                food.price = f.price;

                db.Foods.Add(food);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateFood(Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using(var db = new FoodDbContext())
            {
                var existingFood = db.Foods.Where(f => f.id == food.id).FirstOrDefault();

                if(existingFood != null)
                {
                    existingFood.name = food.name;
                    existingFood.type = food.type;
                    existingFood.price = food.price;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }

            }
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteFood(int id)
        {
            if (id <= 0)
                return BadRequest("Not valid food id");

            using(var db = new FoodDbContext())
            {
                Food food = db.Foods.Where(f => f.id == id).FirstOrDefault();
                if(food != null)
                {
                    db.Foods.Remove(food);
                    db.SaveChanges();
                    return Ok();
                }
                return BadRequest("Not valid food id");
            }
        }
    }
}
