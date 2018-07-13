using System.ComponentModel.DataAnnotations;

namespace WebAppRestaurant.Models {

    public class Category {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}