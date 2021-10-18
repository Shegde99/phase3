using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coreSessionManagementApplication.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserType { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UserAddedDate { get; set; }
    }
}
