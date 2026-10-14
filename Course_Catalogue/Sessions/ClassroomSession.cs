using System;
using Course_Catalogue.Models;

namespace Course_Catalogue.Sessions
{
    public class ClassroomSession : CourseSession
    {
        public string RoomNumber { get; set; }
        public decimal VenueFee { get; set; }

        public ClassroomSession(string courseCode, DateTime startDate, SessionDuration duration, string roomNumber, decimal venueFee)
            : base(courseCode, startDate, duration)
        {
            RoomNumber = roomNumber;
            VenueFee = venueFee;
        }

        public override decimal CalculateSessionCharge()
        {
            return VenueFee + 800m;
        }
    }
}