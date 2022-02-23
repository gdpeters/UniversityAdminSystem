using Microsoft.EntityFrameworkCore;
using SkillsTest.Lib;
using System.Linq;

namespace SkillsTest.Tests
{
    internal class DataContextHelper
    {
        /// <summary>
        /// Returns an instance of the In-Memory db context for testing
        /// </summary>
        /// <returns></returns>
        public static DataContext GetMockDb(string name)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(name)
                .Options;

            var db = new DataContext(options);

            // Seed the database

            if (!db.Courses.Any())
            {
                db.Courses.AddRange(new[]
                {
                    new Course { CourseId = 1, Name = "Advanced Basketweaving" },
                    new Course { CourseId = 2, Name = "Math for Liberal Arts Majors" },
                    new Course { CourseId = 3, Name = "The Cosmos: An Introduction" }
                });

                db.SaveChanges();
            }

            if (!db.Students.Any())
            {
                foreach (var iteration in Enumerable.Range(1, 100))
                {
                    db.Students.Add(new Student
                    {
                        StudentId = iteration,
                        FirstName = "Test",
                        LastName = $"Student {iteration}",
                        Courses = db.Courses.Where(course => iteration % course.CourseId == 0).ToList()
                    });
                }

                db.SaveChanges();
            }

            return db;
        }
    }
}