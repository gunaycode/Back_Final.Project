using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class City:BaseAuditable
    {
        
        public string Name { get; set; } = null!;
        public Country Country { get; set; } = null!;
        public int CountryId { get; set; }
        public ICollection<Hotel> Hotels { get; set; }
       
    }
}
