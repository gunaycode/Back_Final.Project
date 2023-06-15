using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PostCountryDto
    {
        public string Name { get; set; } = null!;
        public int CityId { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
