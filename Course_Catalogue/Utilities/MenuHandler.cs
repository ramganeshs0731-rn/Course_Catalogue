using System;
using System.Collections.Generic;
using Course_Catalogue.Models;
using Course_Catalogue.Services;
using Course_Catalogue.Payments;

namespace Course_Catalogue.Utilities
{
    public class MenuHandler
    {
        private TrainingService _trainingService;
        private LearnerService _learnerService;
        private EnrollmentService _enrollmentService;
        private PaymentService _paymentService;
        private ReportService _reportService;
        private List<Enrollment> _enrollments;

        public MenuHandler(TrainingService trainingService, LearnerService learnerService, EnrollmentService enrollmentService, PaymentService paymentService, ReportService reportService, List<Enrollment> enrollments)
        {
            _trainingService = trainingService;
            _learnerService = learnerService;
            _enrollmentService = enrollmentService;
            _paymentService = paymentService;
            _reportService = reportService;
            _enrollments = enrollments;
        }

        public void RunMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("    TRAINING MANAGEMENT SYSTEM MENU");
                Console.WriteLine("========================================");
                Console.WriteLine("1. List all courses");
                Console.WriteLine("2. Search courses");
                Console.WriteLine("3. Sort courses by fee");
                Console.WriteLine("4. Register a learner");
                Console.WriteLine("5. List learners");
                Console.WriteLine("6. Search for a learner");
                Console.WriteLine("7. Enroll in a course");
                Console.WriteLine("8. View enrollment records");
                Console.WriteLine("9. Cancel an enrollment");
                Console.WriteLine("10. Display a fee breakdown");
                Console.WriteLine("11. Process a simulated payment");
                Console.WriteLine("12. View payment history");
                Console.WriteLine("13. Display reports");
                Console.WriteLine("14. Exit");
                Console.WriteLine("========================================");

                int choice = InputValidator.GetValidInteger("Enter your choice (1-14): ");

                switch (choice)
                {
                    case 1:
                        _trainingService.DisplayCourseTable();
                        break;
                    case 2:
                        Console.Write("Enter course code to search: ");
                        string code = Console.ReadLine();
                        _trainingService.SearchByCode(code);
                        break;
                    case 3:
                        int sortChoice = InputValidator.GetValidInteger("Sort order - 1. Ascending | 2. Descending: ");
                        _trainingService.SortCoursesByFee(sortChoice == 1);
                        Console.WriteLine("-> Courses sorted successfully.");
                        _trainingService.DisplayCourseTable();
                        break;
                    case 4:
                        Console.Write("Enter Learner ID: ");
                        string lId = Console.ReadLine();
                        Console.Write("Enter Learner Name: ");
                        string lName = Console.ReadLine();
                        Console.Write("Enter Contact Number: ");
                        string lContact = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        string lEmail = Console.ReadLine();
                        _learnerService.RegisterLearner(lId, lName, lContact, lEmail);
                        break;
                    case 5:
                        _learnerService.DisplayAllLearners();
                        break;
                    case 6:
                        Console.Write("Enter Learner ID to search: ");
                        string searchId = Console.ReadLine();
                        _learnerService.SearchLearnerById(searchId);
                        break;
                    case 7:
                        _enrollmentService.EnrollLearnerInteractive();
                        break;
                    case 8:
                        DisplayEnrollmentTable(_enrollments);
                        break;
                    case 9:
                        _enrollmentService.CancelEnrollmentInteractive();
                        break;
                    case 10:
                        FeeCalculator calculator = new FeeCalculator();
                        decimal sampleBase = InputValidator.GetValidDecimal("Enter base fee: ");
                        var breakdown = calculator.CalculateFee(sampleBase, isEarlyBird: true, isGroupDiscount: false, surcharge: 200m);
                        FeeSummary summary = new FeeSummary(breakdown.baseFee, breakdown.totalDiscount, breakdown.finalFee);
                        calculator.DisplayFeeBreakdown(in summary);
                        break;
                    case 11:
                        int enId = InputValidator.GetValidInteger("Enter Enrollment ID for payment: ");
                        int mode = InputValidator.GetValidInteger("Payment Mode - 1. Online | 2. Classroom: ");
                        IPaymentProcessor processor = mode == 1 ? new OnlinePaymentProcessor() : new ClassroomPaymentProcessor();
                        _paymentService.ProcessPayment(enId, mode == 1, processor);
                        break;
                    case 12:
                        _paymentService.DisplayPaymentHistory();
                        break;
                    case 13:
                        DisplayReportsMenu();
                        break;
                    case 14:
                        exit = true;
                        Console.WriteLine("Exiting application. Thank you!");
                        break;
                    default:
                        Console.WriteLine("-> Error: Invalid menu choice. Please select between 1 and 14.");
                        break;
                }
            }
        }

        private void DisplayEnrollmentTable(List<Enrollment> enrollmentList)
        {
            Console.WriteLine(new string('-', 105));
            Console.WriteLine($"| {"ID",-6} | {"Learner ID",-12} | {"Course",-8} | {"Date",-12} | {"Base Fee",-10} | {"Discount",-10} | {"Final Fee",-10} | {"Status",-10} |");
            Console.WriteLine(new string('-', 105));

            foreach (var en in enrollmentList)
            {
                Console.WriteLine($"| {en.EnrollmentId,-6} | {en.LearnerId,-12} | {en.CourseCode,-8} | {en.EnrollmentDate:yyyy-MM-dd} | ₹{en.BaseFee,-9:N0} | ₹{en.DiscountAmount,-9:N0} | ₹{en.FinalFee,-9:N0} | {en.Status,-10} |");
            }
            Console.WriteLine(new string('-', 105));
        }

        private void DisplayReportsMenu()
        {
            Console.WriteLine("\n--- Reports Menu ---");
            Console.WriteLine("1. Course Occupancy Report");
            Console.WriteLine("2. Expected Fees Report");
            Console.WriteLine("3. Payment Summary Report");
            Console.WriteLine("4. Course Enrollment Summary Report");
            int repChoice = InputValidator.GetValidInteger("Select report (1-4): ");

            switch (repChoice)
            {
                case 1:
                    _reportService.GenerateCourseOccupancyReport();
                    break;
                case 2:
                    _reportService.GenerateExpectedFeesReport();
                    break;
                case 3:
                    _reportService.GeneratePaymentSummaryReport();
                    break;
                case 4:
                    _reportService.GenerateCourseEnrollmentSummaryReport();
                    break;
                default:
                    Console.WriteLine("-> Invalid report choice.");
                    break;
            }
        }
    }
}