using System;
using Course_Catalogue.Models;

namespace Course_Catalogue.Services
{
    public class TrainingService
    {
        private Course[] _courses;

        public TrainingService(Course[] courses)
        {
            _courses = courses;
        }

        public Course[] GetCourses()
        {
            return _courses;
        }

        public void DisplayCourseTable()
        {
            Console.WriteLine(new string('-', 96));
            Console.WriteLine($"| {"Code",-8} | {"Course Title",-35} | {"Fee (INR)",-10} | {"Days",-6} | {"Total",-6} | {"Available",-9} |");
            Console.WriteLine(new string('-', 96));

            foreach (var course in _courses)
            {
                Console.WriteLine($"| {course.CourseCode,-8} | {course.Title,-35} | ₹{course.Fee,-9:N0} | {course.DurationDays,-6} | {course.TotalSeats,-6} | {course.AvailableSeats,-9} |");
            }
            Console.WriteLine(new string('-', 96));
        }

        public void SearchByCode(string code)
        {
            bool found = false;
            foreach (var course in _courses)
            {
                if (course.CourseCode.Equals(code, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"-> Found: [{course.CourseCode}] {course.Title} | Fee: ₹{course.Fee:N0} | Available Seats: {course.AvailableSeats}");
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("-> No course found with that code.");
            }
        }

        public void SortCoursesByFee(bool ascending)
        {
            Array.Sort(_courses, (c1, c2) =>
            {
                if (c1 == null || c2 == null) return 0;
                return ascending ? c1.Fee.CompareTo(c2.Fee) : c2.Fee.CompareTo(c1.Fee);
            });
        }
    }
}