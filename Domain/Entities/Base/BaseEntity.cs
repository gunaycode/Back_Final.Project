using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool Created { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActived { get; set; }
    }
}
