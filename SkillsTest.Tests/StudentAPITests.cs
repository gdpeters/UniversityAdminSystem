using SkillsTest.Lib;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SkillsTest.Tests
{
    public class StudentAPITests
    {
        private readonly DbStudentAPI _studentAPI;

        public StudentAPITests()
        {
            _studentAPI = new DbStudentAPI(DataContextHelper.GetMockDb(nameof(StudentAPITests)));
        }

        
        [Fact]
        public void Can_Get_Student_With_Id_1()
        {
            var student = _studentAPI.GetById(1);

            Assert.NotNull(student);
            Assert.Equal(1, student.StudentId);
        }

        [Fact]
        public void WillReturn_AllStudents_OnPageZero_WithPageSizeTen()
        {
            int page = 0;
            int pageSize = 10;
            var students = _studentAPI.GetStudents(page, pageSize).ToList();

            var expectedStudentIds = new List<int>() { 1, 10, 100, 11, 12, 13, 14, 15, 16, 17 };

            var resultStudentIds = students.Select(student => student.StudentId);

            Assert.NotNull(students);
            Assert.True(pageSize == students.Count);
            Assert.Equal(expectedStudentIds, resultStudentIds);
        }

        [Fact]
        public void WillReturn_AllStudents_OnPageNine_WithPageSizeTen()
        {
            int page = 9;
            int pageSize = 10;
            var students = _studentAPI.GetStudents(page, pageSize).ToList();

            var expectedStudentIds = new List<int>() { 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 };

            var resultStudentIds = students.Select(student => student.StudentId);

            Assert.NotNull(students);
            Assert.True(pageSize == students.Count);
            Assert.Equal(expectedStudentIds, resultStudentIds);
        }


        [Fact]
        public void WillReturn_AllStudents_WithCourseId_2()
        {
            var courseId = 2;
            var expectedStudentIds = new List<int>();

            foreach (var studentId in Enumerable.Range(1, 100))
            {
                if (studentId % courseId == 0)
                {
                    expectedStudentIds.Add(studentId);
                }
            }

            var students = _studentAPI.GetStudentsByCourseId(courseId).ToList();
            var resultStudentIds = students.Select(student => student.StudentId).ToList();

            Assert.NotNull(students);
            Assert.Equal(expectedStudentIds, resultStudentIds);
        }

        [Fact]
        public void WillReturnAllStudents_WithLastName_Student_2()
        {
            var lastName = "Student 2";
            var students = _studentAPI.GetStudentsByLastName(lastName).ToList();

            Assert.NotNull(students);
            Assert.True(students.Count == 1);
            Assert.Equal(lastName, students.SingleOrDefault().LastName);
        }
    }
}