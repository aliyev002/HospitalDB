using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EFProject.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime ReservedTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
