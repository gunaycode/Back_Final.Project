using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SearchDto
{
    public class SearchResult
    {
        public int Count { get; set; }
        public int CityId { get; set; }
        public DateTime Date { get; set; }

    }
}
