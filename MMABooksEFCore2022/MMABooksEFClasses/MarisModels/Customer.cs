﻿using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.MarisModels
{
    public partial class Customer
    {
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }

        public override string ToString()
        {
            return CustomerId + ", " + Name + ", " + Address + ", " + City + ", " + State + ", " + ZipCode;
        }
        public virtual State? State { get; set; }// = null!;
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
