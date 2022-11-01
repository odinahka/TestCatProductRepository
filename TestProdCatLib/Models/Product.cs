using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProdCatLib.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Category Category { get; set; }
        public long CategoryId { get; set; }
    }
}
