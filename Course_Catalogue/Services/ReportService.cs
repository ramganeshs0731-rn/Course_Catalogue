using System;
using System.Collections.Generic;
using Course_Catalogue.Models;

namespace Course_Catalogue.Services
{
    public class ReportService
    {
        private Course[] _courses;
        private List<Enrollment> _enrollments;
        private List<Payment> _payments;

        public ReportService(Course[] courses, List<Enrollment> enrollments, List<Payment> payments)
        {
            _courses = courses;
            _enrollments = enrollments;
            _payments = payments;
        }

        public void GenerateCourseOccupancyReport()
        {
            Console.WriteLine("\n--- Report A: Course Occupancy ---");
            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"| {"Code",-8} | {"Total Seats",-12} | {"Occupied",-10} | {"Available",-10} | {"Occupancy %",-12} |");
            Console.WriteLine(new string('-', 75));

            foreach (var course in _courses)
            {
                double percentage = course.TotalSeats > 0 ? ((double)course.EnrolledCount / course.TotalSeats) * 100 : 0;
                Console.WriteLine($"| {course.CourseCode,-8} | {course.TotalSeats,-12} | {course.EnrolledCount,-10} | {course.AvailableSeats,-10} | {percentage:N1}%       |");
            }
            Console.WriteLine(new string('-', 75));
        }

        public void GenerateExpectedFeesReport()
        {
            Console.WriteLine("\n--- Report B: Expected Fees ---");
            decimal totalExpected = 0m;
            foreach (var en in _enrollments)
            {
                if (en.Status == EnrollmentStatus.Active)
                {
                    totalExpected += en.FinalFee;
                }
            }
            Console.WriteLine($"Total Expected Fees from Active Enrollments: ₹{totalExpected:N2}");
            Console.WriteLine("------------------------------------\n");
        }

        public void GeneratePaymentSummaryReport()
        {
            Console.WriteLine("\n--- Report C: Payment Summary ---");
            int successCount = 0;
            int failedCount = 0;
            decimal totalCollected = 0m;

            foreach (var p in _payments)
            {
                if (p.Status == PaymentStatus.Successful)
                {
                    successCount++;
                    totalCollected += p.Amount;
                }
                else if (p.Status == PaymentStatus.Failed)
                {
                    failedCount++;
                }
            }

            Console.WriteLine($"Successful Payments : {successCount}");
            Console.WriteLine($"Failed Payments     : {failedCount}");
            Console.WriteLine($"Total Collected     : ₹{totalCollected:N2}");
            Console.WriteLine("---------------------------------\n");
        }

        public void GenerateCourseEnrollmentSummaryReport()
        {
            Console.WriteLine("\n--- Report D: Course Enrollment Summary ---");
            Console.WriteLine(new string('-', 65));
            Console.WriteLine($"| {"Course Code",-12} | {"Active Enrollments",-20} | {"Status",-25} |");
            Console.WriteLine(new string('-', 65));

            foreach (var course in _courses)
            {
                string fullStatus = course.AvailableSeats <= 0 ? "Fully Booked" : "Seats Available";
                Console.WriteLine($"| {course.CourseCode,-12} | {course.EnrolledCount,-20} | {fullStatus,-25} |");
            }
            Console.WriteLine(new string('-', 65));
        }
    }
}