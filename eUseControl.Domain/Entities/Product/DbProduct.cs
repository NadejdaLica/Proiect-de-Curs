using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.Product
{
    public class DbProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Prod_Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Name shorter than 5 char or longer than 30")]
        public string Prod_Name { get; set; }

        [Required]
        [Display(Name = "Product description")]
        [StringLength(250, MinimumLength = 8, ErrorMessage = "Minim 8, max 250")]
        public string Prod_Desc { get; set; }

        [Required]
        [Display(Name = "Product price")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "Minim 1, max 250")]
        public string Prod_Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastEditTime { get; set; }
    }
}