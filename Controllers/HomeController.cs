using DCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace DCL.Controllers
{
    //  ================================================================
    //  This code is used in the view to only display to Role = "Admin" 
    //  ================================================================
    //  <p>                                                             
    //  @if(User.IsInRole("admin")){                                    
    //  @Html.ActionLink("Create New", "Create")                        
    //  }                                                               
    //  </p>                                                            


    // Use this one 
    public class HomeController : Controller
    {
        DCLDb _db = new DCLDb();

        
        //  AutoComplete MUST have a parameter of "term" and the JSON s/h a label or value property, or BOTH, via the jQueryUI.com    
        public ActionResult Autocomplete(string term)
        {
            var model =
                _db.Restaurants
                   .Where(r => r.Name.StartsWith(term))
                   .Take(10)
                   .Select(r => new
                   {
                       label = r.Name
                   });

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        // Paged List       
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var model =
                _db.Restaurants
                   .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                   .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
                   .Select(r => new RestaurantListViewModel
                            {
                                Id = r.Id,
                                Name = r.Name,
                                City = r.City,
                                Country = r.Country,
                                CountOfReviews = r.Reviews.Count()
                            }).ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }

            return View(model);
        }

        [Authorize (Roles="Admin")]
        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Scott";
            model.Location = "Maryland, USA";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
