using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class Reservation:BaseAuditable
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        //if (room.reservation !=null)
        //{
       //foreach(var item in rooms.reservations)
       //{
       //if(!(item.start<dto.start&&item.end<=dto.start||item.start>=dto.end))
       //{
                //return 404 return messsage artik hemin vaxta rezervasya olunub
       //}
       //}
       //} usteki ifin scobu
       // Reservation reservation=new Reservation()
       //{ reservation.start=dto.start
       //.......}
       //
    }
}
