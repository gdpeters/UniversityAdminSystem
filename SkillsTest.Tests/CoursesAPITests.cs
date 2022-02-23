using SkillsTest.Lib;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SkillsTest.Tests
{
    public class CoursesAPITests
    {
        private readonly DbCourseAPI _courseAPI;

        public CoursesAPITests()
        {
            _courseAPI = new DbCourseAPI(DataContextHelper.GetMockDb(nameof(CoursesAPITests)));
        }

        [Fact]
        public void Can_Get_Course_With_Id_1()
        {
            var course = _courseAPI.GetById(1);

            Assert.NotNull(course);
            Assert.True(course.CourseId == 1);
        }

        [Fact]
        public void WillReturn_AllCourses_OnPageZero_WithPageSizeOne()
        {
            int page = 0;
            int pageSize = 1;
            var courses = _courseAPI.GetCourses(page, pageSize).ToList();

            Assert.NotNull(courses);
            Assert.True(courses.Count == 1);
            Assert.Equal(1, courses.SingleOrDefault().CourseId); 
        }

        [Fact]
        public void WillReturn_AllCourses_OnPageZero_WithPageSizeTen()
        {
            int page = 0;
            int pageSize = 10;
            var courses = _courseAPI.GetCourses(page, pageSize).ToList();

            var expectedCourseIds = new List<int>() { 1, 2, 3 };
            var actualCourseIds = courses.Select(course => course.CourseId);

            Assert.NotNull(courses);
            Assert.True(courses.Count == expectedCourseIds.Count);
            Assert.Equal(expectedCourseIds, actualCourseIds);
        }

        [Fact]
        public void WillReturnCourse_WithName_AdvancedBasketweaving()
        {
            var course = _courseAPI.GetByName("Advanced Basketweaving");

            Assert.NotNull(course);
            Assert.True(course.CourseId == 1);
        }
    }
}