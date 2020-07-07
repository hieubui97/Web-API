using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Food> ListFoods = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44352/api/");
                var responeTask = client.GetAsync("food");
                responeTask.Wait();

                var result = responeTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Food>>();
                    readTask.Wait();

                    ListFoods = readTask.Result;
                }
                else
                {
                    ListFoods = Enumerable.Empty<Food>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }

            return View(ListFoods);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Food food)
        {
            //if (ModelState.IsValid)
            //{
            //    var db = new FoodDbContext();
            //    db.Foods.Add(food);
            //    db.SaveChanges();

            //    return RedirectToAction("Index");
            //}

            //return View(food);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44352/api/");

                //HttpPost
                var postTask = client.PostAsJsonAsync<Food>("food", food);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server error");

                
                return View(food);
            }
        }

        public ActionResult Details(int id)
        {
            Food food = new Food();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44352/api/");
                var responseTask = client.GetAsync("food/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Food>();
                    readTask.Wait();
                    food = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error");
                }
            }

            return View(food);
        }

        public ActionResult Edit(int id)
        {
            Food food = null;
            using(var client =  new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44352/api/");
                var responseTask = client.GetAsync("food/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Food>();
                    readTask.Wait();

                    food = readTask.Result;
                }
            }

            return View(food);
        }

        [HttpPost]
        public ActionResult Edit(Food food)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44352/api/");
                var putTask = client.PutAsJsonAsync<Food>("food", food);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(food);
        }

        
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44352/api/");
                var deleteTask = client.DeleteAsync("food/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }

        }
    }
}
