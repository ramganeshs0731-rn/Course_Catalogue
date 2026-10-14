using System;
using System.Collections.Generic;
using Course_Catalogue.Models;
using Course_Catalogue.Services;
using Course_Catalogue.Utilities;

namespace Course_Catalogue
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Course[] courses = new Course[]
                {
                    new Course("CS101", "Introduction to C# Programming", 12500m, 5, 30, 12),
                    new Course("DS201", "Data Structures and Algorithms", 18000m, 10, 25, 20),
                    new Course("WD301", "Full Stack Web Development", 22000m, 15, 40, 35),
                    new Course("DB401", "Database Design & SQL", 9500m, 4, 30, 8),
                    new Course("CC501", "Cloud Computing Basics", 15000m, 7, 20, 5)
                };

                Dictionary<string, Learner> learners = new Dictionary<string, Learner>(StringComparer.OrdinalIgnoreCase);
                learners.Add("L001", new Learner("L001", "Aarav Sharma", "9876543210", "aarav.sharma@example.com"));
                learners.Add("L002", new Learner("L002", "Priya Patel", "9123456789", "priya.patel@example.com"));

                List<Enrollment> enrollments = new List<Enrollment>();
                enrollments.Add(new Enrollment(101, "L001", "CS101", DateTime.Now.AddDays(-5), 12500m, 1250m, 11250m, EnrollmentStatus.Active));
                enrollments.Add(new Enrollment(102, "L002", "DS201", DateTime.Now.AddDays(-2), 18000m, 0m, 18000m, EnrollmentStatus.Active));

                List<Payment> payments = new List<Payment>();
                payments.Add(new Payment(1001, 101, 11250m, DateTime.Now.AddDays(-4), PaymentStatus.Successful));

                TrainingService trainingService = new TrainingService(courses);
                LearnerService learnerService = new LearnerService(learners);
                EnrollmentService enrollmentService = new EnrollmentService(enrollments, learners, courses);
                PaymentService paymentService = new PaymentService(payments, enrollments);
                ReportService reportService = new ReportService(courses, enrollments, payments);

                MenuHandler menuHandler = new MenuHandler(trainingService, learnerService, enrollmentService, paymentService, reportService, enrollments);
                menuHandler.RunMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nApplication session closed cleanly.");
            }
        }
    }
}