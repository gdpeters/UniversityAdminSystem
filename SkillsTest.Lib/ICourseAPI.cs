using System.Collections.Generic;
using System.Linq;

namespace SkillsTest.Lib
{
    public interface ICourseAPI
    {
        Course GetById(int id);
        IEnumerable<Course> GetCourses(int page, int pageSize);
        Course GetByName(string name);

    }

    public class DbCourseAPI : ICourseAPI
    {
        private readonly DataContext _courseContext;
        public DbCourseAPI(DataContext courseContext)
        {
            _courseContext = courseContext;   
        }

        /// <summary>
        /// Search for a course by id
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <returns>The course with the specified id</returns>
        public Course GetById(int courseId)
        {
            return _courseContext.Courses.Where(course => course.CourseId == courseId).SingleOrDefault();
        }

        /// <summary>
        /// Retrieves a specified page of courses in alphabetical order
        /// </summary>
        /// <param name="page">The page to retrieve. The first page is 0</param>
        /// <param name="pageSize">The number of courses to view on each page</param>
        /// <returns>Courses on a specified page</returns>
        public IEnumerable<Course> GetCourses(int page, int pageSize)
        {
            return _courseContext.Courses.OrderBy(course => course.Name).Skip(page * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Search for a course by name
        /// </summary>
        /// <param name="courseName">The name of the course</param>
        /// <returns>The course that matches the course name</returns>
        public Course GetByName(string courseName)
        {
            return _courseContext.Courses.Where(course => course.Name.Equals(courseName)).SingleOrDefault();
        }


    }
}