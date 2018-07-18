using System;
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
    public class MenuItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Open actions parts
        // GET: MenuItems
        public ActionResult Index()
        {
            return View(db.MenuItems.ToList());
        }

        // GET: MenuItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }
        #endregion Open actions parts

        #region Only for authorized users
        // GET: MenuItems/Create
        /// <summary>
        /// Only authorized user can use this controller.
        /// </summary>
        [Authorize]
        public ActionResult Create() {
            var menuItem = new MenuItem();

            FillAssignableCategories(menuItem);
            return View(menuItem);
        }

        /// <summary>
        /// Fills up the DropDownList field with the actual items.
        /// </summary>
        /// <param name="menuItem"></param>
        private void FillAssignableCategories(MenuItem menuItem) {
            foreach (var category in db.Categories.ToList()) {
                menuItem.AssignedCategories.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,CategoryId")] MenuItem menuItem)
        {
            // Fill up the MenuItem.Category
            // Search for the aktual value from database
            var category = db.Categories.Find(menuItem.CategoryId);
            // Give value to Category property
            menuItem.Category = category;

            // Have to validate the model once more
            ModelState.Clear();
            var isValid = TryValidateModel(menuItem);

            if (ModelState.IsValid)
            {
                db.MenuItems.Add(menuItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            FillAssignableCategories(menuItem);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }

            // if the menuItem is NOT null
            FillAssignableCategories(menuItem);
            
            // Fill up the MenuItem.Category
            // Search for the aktual value from database
            // Give value to Category property
            menuItem.CategoryId = menuItem.Category.Id;

            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,CategoryId")] MenuItem menuItem)
        {
            // Fill up the MenuItem.Category
            // Search for the aktual value from database
            // It's necessary for validation.
            var category = db.Categories.Find(menuItem.CategoryId);

            // we intruduce the menuItem to EntityFramework
            //with this can load Navigation Property of menuItem
            db.MenuItems.Attach(menuItem);

            var menuItemEntry = db.Entry(menuItem);

            // we load the Navigation Property !
            // EntityFramework knows from now about the Navigation Property and save its changes.
            // Warning ! If we would set the category before this ("menuItem.Category = category;"), this function wouldn't do anything.
            menuItemEntry.Reference(x => x.Category)
                            .Load();

            // we set the value of Category (Navigation Property)
            // It's necessary for EntityFramework to save into database.
            menuItem.Category = category;

            // Have to validate the model once more
            ModelState.Clear();
            var isValid = TryValidateModel(menuItem);

            if (ModelState.IsValid) {
                // This signs the changes of datas to Entity Framework
                menuItemEntry.State = EntityState.Modified;

                // Saving datas.
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuItem menuItem = db.MenuItems.Find(id);
            if (menuItem == null)
            {
                return HttpNotFound();
            }
            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItem menuItem = db.MenuItems.Find(id);
            db.MenuItems.Remove(menuItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion Only for authorized users

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
