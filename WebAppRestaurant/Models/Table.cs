using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppRestaurant.Models {

    public class Table {
        public int Id { get; set; }

        public string Name { get; set; }

        //Lazy loading
        public virtual Location Location { get; set; }
    }
}