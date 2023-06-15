using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hotel:BaseEntity
    {
        
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public ICollection<City> Cities { get; set; }=null!;
        public ICollection<ImageHotel> Images { get; set; } = null!;
       
    }
}
