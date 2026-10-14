using System;
using Course_Catalogue.Models;

namespace Course_Catalogue.Sessions
{
    public class OnlineSession : CourseSession
    {
        public string Platform { get; set; }
        public decimal ServerFee { get; set; }

        public OnlineSession(string courseCode, DateTime startDate, SessionDuration duration, string platform, decimal serverFee)
            : base(courseCode, startDate, duration)
        {
            Platform = platform;
            ServerFee = serverFee;
        }

        public override decimal CalculateSessionCharge()
        {
            return ServerFee + 200m;
        }
    }
}