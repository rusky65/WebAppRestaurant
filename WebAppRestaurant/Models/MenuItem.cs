using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppRestaurant.Models {

    public class MenuItem {

        public MenuItem() {
            AssignedCategories = new List<SelectListItem>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Fogás neve")]
        public string Name { get; set; }

        [Display(Name = "Leírás")]
        public string Description { get; set; }

        [Display(Name = "Ár")]
        public double Price { get; set; }
        /// <summary>
        /// Navigation property
        /// </summary>
        [Required]
        [Display(Name = "Kategória")]
        public Category Category { get; set; }

        #region Properties just for Views
        /// <summary>
        /// Assigned this property for Code First not to get into database
        /// This property contains the all possible value.
        /// </summary>
        [NotMapped]
        public List<SelectListItem> AssignedCategories { get; set; }

        /// This property contains the actual selected value.
        [NotMapped]
        public int CategoryId { get; set; }
        #endregion Properties just for Views
    }
}