using System;
using System.Collections.Generic;
using Course_Catalogue.Models;

namespace Course_Catalogue.Services
{
    public class EnrollmentService
    {
        private List<Enrollment> _enrollments;
        private Dictionary<string, Learner> _learners;
        private Course[] _courses;
        private int _nextEnrollmentId = 103;

        public EnrollmentService(List<Enrollment> enrollments, Dictionary<string, Learner> learners, Course[] courses)
        {
            _enrollments = enrollments;
            _learners = learners;
            _courses = courses;
        }

        public void EnrollLearnerInteractive()
        {
            Console.WriteLine("\n=== Course Enrollment Process ===");

            Console.Write("Enter Learner ID: ");
            string? learnerId = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(learnerId) || !_learners.ContainsKey(learnerId))
            {
                Console.WriteLine("-> Error: Learner ID does not exist or is invalid.");
                return;
            }

            Console.Write("Enter Course Code: ");
            string? courseCode = Console.ReadLine()?.Trim();

            Course? selectedCourse = null;
            foreach (var course in _courses)
            {
                if (course.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase))
                {
                    selectedCourse = course;
                    break;
                }
            }

            if (selectedCourse == null)
            {
                Console.WriteLine("-> Error: Course code does not exist.");
                return;
            }

            foreach (var en in _enrollments)
            {
                if (en.LearnerId.Equals(learnerId, StringComparison.OrdinalIgnoreCase) &&
                    en.CourseCode.Equals(selectedCourse.CourseCode, StringComparison.OrdinalIgnoreCase) &&
                    en.Status == EnrollmentStatus.Active)
                {
                    Console.WriteLine("-> Error: Learner is already actively enrolled in this course.");
                    return;
                }
            }

            if (selectedCourse.AvailableSeats <= 0)
            {
                Console.WriteLine("-> Error: Course is fully booked.");
                return;
            }

            decimal baseFee = selectedCourse.Fee;
            decimal discountAmount = 0m;
            decimal finalFee = baseFee - discountAmount;

            int newEnrollmentId = _nextEnrollmentId++;
            Enrollment newEnrollment = new Enrollment(
                newEnrollmentId,
                learnerId.ToUpper(),
                selectedCourse.CourseCode,
                DateTime.Now,
                baseFee,
                discountAmount,
                finalFee,
                EnrollmentStatus.Active
            );

            _enrollments.Add(newEnrollment);
            selectedCourse.EnrolledCount++;

            Console.WriteLine("\n--- Enrollment Confirmation ---");
            Console.WriteLine($"Enrollment ID : {newEnrollment.EnrollmentId}");
            Console.WriteLine($"Learner ID    : {newEnrollment.LearnerId}");
            Console.WriteLine($"Course        : [{selectedCourse.CourseCode}] {selectedCourse.Title}");
            Console.WriteLine($"Final Fee     : ₹{newEnrollment.FinalFee:N0}");
            Console.WriteLine($"Status        : {newEnrollment.Status}");
            Console.WriteLine("---------------------------------\n");
        }

        public void CancelEnrollmentInteractive()
        {
            Console.WriteLine("\n=== Enrollment Cancellation Process ===");
            Console.Write("Enter Enrollment ID to cancel: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int enrollmentId))
            {
                Console.WriteLine("-> Error: Invalid Enrollment ID format.");
                return;
            }

            Enrollment? targetEnrollment = null;
            foreach (var en in _enrollments)
            {
                if (en.EnrollmentId == enrollmentId)
                {
                    targetEnrollment = en;
                    break;
                }
            }

            if (targetEnrollment == null)
            {
                Console.WriteLine("-> Error: Enrollment record does not exist.");
                return;
            }

            if (targetEnrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("-> Error: Enrollment is already cancelled.");
                return;
            }

            targetEnrollment.Status = EnrollmentStatus.Cancelled;

            foreach (var course in _courses)
            {
                if (course.CourseCode.Equals(targetEnrollment.CourseCode, StringComparison.OrdinalIgnoreCase))
                {
                    if (course.EnrolledCount > 0)
                    {
                        course.EnrolledCount--;
                    }
                    break;
                }
            }

            Console.WriteLine("\n--- Cancellation Confirmation ---");
            Console.WriteLine($"Enrollment ID : {targetEnrollment.EnrollmentId}");
            Console.WriteLine($"Learner ID    : {targetEnrollment.LearnerId}");
            Console.WriteLine($"Course Code   : {targetEnrollment.CourseCode}");
            Console.WriteLine($"Status        : {targetEnrollment.Status}");
            Console.WriteLine("Seat released successfully. Record preserved.");
            Console.WriteLine("-------------------------------------\n");
        }
    }
}