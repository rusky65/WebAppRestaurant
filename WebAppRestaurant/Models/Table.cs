using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppRestaurant.Models {

    public class Table {

        public Table() {
            //setting default value
            AssignableLocations = new List<SelectListItem>();

            //We could expand the null object pattern for the Location property too
            //It is necessary only if there are no virtual properties.
            //Location = new Location();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        //todo: required field
        //Lazy loading
        public virtual Location Location { get; set; }

        #region parts of ViewModel
        [NotMapped] //it's just viewmodel, it is not in the database.
        public int LocationId { get; set; }

        [NotMapped] //it's just viewmodel, it is not in the database.
        public List<SelectListItem> AssignableLocations { get; set; }

        #endregion parts of ViewModel
    }
}