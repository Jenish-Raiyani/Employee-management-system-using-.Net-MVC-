using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Models
{
    public class Accountsmodel
    {
       
        public int id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
        ErrorMessage = "Invalid email")]
 
        public string email { get; set; }
        [Required]
        public string mobileno { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }
    }
}