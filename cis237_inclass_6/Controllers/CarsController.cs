using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cis237_inclass_6.Models;

namespace cis237_inclass_6.Controllers
{
    // This ensures that the user must be authenticated to use any of these
    // routes. This decorator can also be put on individual methods if needed.
    [Authorize]
    public class CarsController : Controller
    {
        private CarsTestEntities db = new CarsTestEntities();

        // GET: Cars
        public ActionResult Index()
        {
            // Setup a variable to hold the cars data
            DbSet<Car> CarsToFilter = db.Cars;

            // Setup some strings to hold the data that might be in the session.
            // If there is nothing in the session we can still use these variables
            // as default values.
            string filterMake = "";
            string filterMin = "";
            string filterMax = "";
            // Default min and max ints for the cylinders
            int min = 0;
            int max = 16;

            // Check to see if there is a value in the session, and if there is, assign it
            // to the variable that we setup to hold the value.
            if (!String.IsNullOrWhiteSpace((string)Session["session_make"]))
            {
                filterMake = (string)Session["session_make"];
            }
            if (!String.IsNullOrWhiteSpace((string)Session["session_min"]))
            {
                filterMin = (string)Session["session_min"];
                min = Int32.Parse(filterMin);
            }
            if (!String.IsNullOrWhiteSpace((string)Session["session_max"]))
            {
                filterMax = (string)Session["session_max"];
                max = Int32.Parse(filterMax);
            }

            // Do the filter on the CarsToFilter Dataset. Use the where that we used before
            // when doing the last inclass, only this time send in more lambda expressions to
            // narrow down further. Since we setup the default values for each of the filter
            // parameters, min, max, and filterMake, we can count on this always running
            // with no errors.
            IEnumerable<Car> filtered = CarsToFilter.Where(
                car => car.cylinders >= min &&
                       car.cylinders <= max &&
                       car.make.Contains(filterMake)
            );

            // Place the string representation of the values that are in the session into
            // the ViewBag so that they can be retrived and displayed on the view.
            ViewBag.filterMake = filterMake;
            ViewBag.filterMin = filterMin;
            ViewBag.filterMax = filterMax;

            // Return the view with the filtered selection of cars.
            return View(filtered.ToList());

            // This was the original line of this method
            // return View(db.Cars.ToList());
        }

        // POST: Cars/Filter
        // Mark the method as POST since it is reached from a form submit
        // Make sure to validate the Antiforgery Token too since we included it in the form.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Filter()
        {
            // Get the form data that we sent out of the request object.
            // The string that is used as a key to get the data matches the
            // name property of the form control
            string make = Request.Form.Get("make");
            string min = Request.Form.Get("min");
            string max = Request.Form.Get("max");

            // Now that we have the data pulled out from the request  object
            // let's put it into the session so that other methods can have access to it.
            Session["session_make"] = make;
            Session["session_min"] = min;
            Session["session_max"] = max;

            // Redirct to the index page
            return RedirectToAction("Index");
        }

        // GET: Cars/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,year,make,model,type,horsepower,cylinders")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,year,make,model,type,horsepower,cylinders")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Method to return the entire list of cars as a json string
        // I named the method Json, but it could be named anything.
        public ActionResult Json()
        {
            return Json(db.Cars.ToList(), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
