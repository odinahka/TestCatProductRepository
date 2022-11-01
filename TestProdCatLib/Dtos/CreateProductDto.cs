using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFS.ProdCat.Model.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CategoryId { get; set; }
        public string Code { get; set; }
    }
}
