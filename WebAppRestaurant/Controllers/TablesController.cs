﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppRestaurant.Models;

namespace WebAppRestaurant.Controllers
{
    // Only authorized member of "admin" and "waiter" user group can use this controller.  -->  [Authorize(Roles ="admin,waiter")
    [Authorize(Roles = "admin,waiter")]
    public class TablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tables
        public ActionResult Index()
        {
            return View(db.Tables.ToList());
        }

        // GET: Tables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);

            FillAssignableLocations(table);

            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // GET: Tables/Create
        public ActionResult Create()
        {
            var table = new Table();

            FillAssignableLocations(table);

            return View(table);
        }

        // POST: Tables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LocationId")] Table table)
        {
            //todo: if the Location field is required, then the modell must be revalid !

            if (ModelState.IsValid)
            {
                var location = db.Locations.Find(table.LocationId);
                table.Location = location;

                db.Tables.Add(table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table);
        }

        // GET: Tables/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);

            FillAssignableLocations(table);


            if (table == null) {
                return HttpNotFound();
            }

            return View(table);
        }

        /// <summary>
        /// Fills up the list of DropDownList with the values of Location.
        /// </summary>
        /// <param name="table">The given table, what have the location.</param>
        private void FillAssignableLocations(Table table) {
            //Filling up the list of DropDownList
            table.AssignableLocations = db.Locations
                                            .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                                            .ToList()
                                            ;

            //Setting DropDownList field actual value.
            //The actual value of LocationId
            // See this vs. "FillAssignableCategories(menuItem);" in MenuItemsController !!!
            if (table.Location!= null) {
                table.LocationId = table.Location.Id;
            }
            // if table.Location == null then the table.LocationId default value is "0".
        }

        // POST: Tables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LocationId")] Table table)
        {
            //todo: if the Location field is required, then the modell must be revalid !

            if (ModelState.IsValid)
            {
                //Loading the Location item from database.
                var location = db.Locations.Find(table.LocationId);

                //this 2 steps are enough cause of "Lazy loading"
                //Loading the table actual value
                //it is necessary to assigne the "virtual" for property ("public virtual Location Location { get; set; }" )
                var tableToUpdate = db.Tables.Find(table.Id);
                // sets the new value of location
                tableToUpdate.Location = location;

                //We have to set the other property, what on the view are
                tableToUpdate.Name = table.Name;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table);
        }

        // GET: Tables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            FillAssignableLocations(table);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        // POST: Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table table = db.Tables.Find(id);
            db.Tables.Remove(table);
            db.SaveChanges();
            return RedirectToAction("Index");
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
