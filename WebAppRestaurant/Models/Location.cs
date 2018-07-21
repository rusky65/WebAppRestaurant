using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppRestaurant.Models {

    public class Location {

        public int Id { get; set; }

        [Display(Name = "Név")]
        public string Name { get; set; }

        public bool isNonSmoking { get; set; }

        /// <summary>
        /// Tables' list of given location.
        /// virtual : Lazy loading enables for Entity Framework.
        /// </summary>
        public virtual List<Table> Tables { get; set; }
    }
}