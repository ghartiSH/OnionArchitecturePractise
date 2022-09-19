using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class People
    {
        [Key]   
        public int PeopleId { get; set; }

        [Required(ErrorMessage = "Name must not be empty")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Name field must not contain any special characters or numbers")]
        public string? Name { get; set; }

        [Required (ErrorMessage = "Payment Type must not be empty")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Payment field must not contain any special characters or numbers")]
        public string? PaymentType { get; set; }

        [Required (ErrorMessage ="Address must not be empty")]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Address field must not contain any special characters or numbers")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Email must not be empty")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
                            ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }

        [Required (ErrorMessage = "Phone number must not be empty")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must not contain any string characters and should have exactly 10 numbers")]
        public string? PhoneNumber { get; set; }

        public ICollection<Product>? Products { get; set; }


    }
}
