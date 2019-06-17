using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ComputerAccessoriesStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        [Column("Id")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please, enter a product name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a product description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a product price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter a ptoduct category")]
        public string Category { get; set; }
    }
}
