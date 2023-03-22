using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eUseControl.Domain.Enums;

namespace eUseControl.Domain.Entities.User
{
    public class URegisterData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public URole Level { get; set; }
    }
}