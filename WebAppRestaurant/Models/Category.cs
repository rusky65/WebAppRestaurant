using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppRestaurant.Models {

    public class Category {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Navigation property
        /// </summary>
        public List<MenuItem> MenuItems { get; set; }
    }
}