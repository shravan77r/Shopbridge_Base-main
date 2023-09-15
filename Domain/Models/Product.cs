using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Models
{
    public class Product
    {
        [Key]
        public int Product_Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Product_Name { get; set; }
        [StringLength(1000)]
        public string Product_Description { get; set; }        
        public decimal Product_Price { get; set; }
        public string Product_Image { get; set; }
        [StringLength(1000)]
        public string Product_Features { get; set; }
        [Range(0,1)]
        public int Status { get; set; }
    }
}
