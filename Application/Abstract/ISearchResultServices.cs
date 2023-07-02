using Application.DTOs.SearchDto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface ISearchResultServices
    {
        Task<List<Hotel>> Search(int? count, int? city, DateTime? date);
    }
}
