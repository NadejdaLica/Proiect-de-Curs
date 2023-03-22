using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.Product
{
    public class UProductData
    {
        public string Prod_Name { get; set; }
        public string Prod_Desc { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Prod_Id { get; set; }
        public string Prod_Price { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}