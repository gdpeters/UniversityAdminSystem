using System.Collections.Generic;
using System.Linq;

namespace SkillsTest.Lib
{
    public interface IStudentAPI
    {
        Student GetById(int id);
        IEnumerable<Student> GetStudents(int page, int pageSize);
        IEnumerable<Student> GetStudentsByCourseId(int courseId);
        IEnumerable<Student> GetStudentsByLastName(string lastName);
    }

    public class DbStudentAPI : IStudentAPI
    {
        private readonly DataContext _studentContext;

        public DbStudentAPI(DataContext studentContext)
        {
            _studentContext = studentContext; 
        }

        /// <summary>
        /// Search for a student by student id
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <returns>The student with the specified id</returns>
        public Student GetById(int studentId)
        {
            return _studentContext.Students.Where(student => student.StudentId == studentId).SingleOrDefault();
        }

        /// <summary>
        /// Retrieve a specified page of students in alphabetical order by last name
        /// </summary>
        /// <param name="page">The page to retrieve. The first page is 0</param>
        /// <param name="pageSize">The number of students to view on one page</param>
        /// <returns>Students on a specified page</returns>
        public IEnumerable<Student> GetStudents(int page, int pageSize)
        {
            return _studentContext.Students.OrderBy(student => student.LastName).Skip(page * pageSize).Take(pageSize);

        }

        /// <summary>
        /// Retrieve all students enrolled in a specified course
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <returns>Students enrolled in the specified course</returns>
        public IEnumerable<Student> GetStudentsByCourseId(int courseId)
        {
            return _studentContext.Students.SelectMany(student => student.Courses, (student, course) => new { student, courseId })
                                           .Where(studentCourseIds => studentCourseIds.courseId == courseId)
                                           .Select(studentCourse => studentCourse.student)
                                           .ToList();
        }

        /// <summary>
        /// Retrieve all students with a specified last name
        /// </summary>
        /// <param name="lastName">The student's last name</param>
        /// <returns>Students having the specified last name</returns>
        public IEnumerable<Student> GetStudentsByLastName(string lastName)
        {
            return _studentContext.Students.Where(student => student.LastName.ToLower().Equals(lastName.ToLower())); 
        }
    }
}
