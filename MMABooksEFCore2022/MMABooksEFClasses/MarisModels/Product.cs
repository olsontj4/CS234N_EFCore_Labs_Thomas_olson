using MMABooksEFClasses.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace MMABooksEFClasses.MarisModels
{
    public partial class Product
    {
        public Product()
        {
            Invoicelineitems = new HashSet<Invoicelineitem>();
        }

        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int OnHandQuantity { get; set; }

        public override string ToString()
        {
            return ProductCode + ", " + Description + ", " + UnitPrice + ", " + OnHandQuantity;
        }
        public virtual ICollection<Invoicelineitem> Invoicelineitems { get; set; }
    }
}
