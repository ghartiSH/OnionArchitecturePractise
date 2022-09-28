﻿using System;
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
        public string? Name { get; set; }
        public string? PaymentType { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Product>? Products { get; set; }


    }
}
