using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eUseControl.Domain.Enums;

namespace eUseControl.Models
{
    public class UserData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public URole Level { get; set; }
        public List<string> Products { get; set; }
        public string SingleProduct { get; set; }
    }
}