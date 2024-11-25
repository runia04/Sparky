using NUnit.Framework;
using Sparky;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkyNUnitTest
{
    [TestFixture]
    public  class GradingCalculatorNUnitTests
    {
        private GradingCalculator gradingCalculator;
        [SetUp]
        public void SetUp()
        {
            gradingCalculator = new GradingCalculator();
        }
        [Test]
        public void GradeCalc_InputScore95Attendance90_GetAGrade()
        {
            gradingCalculator.Score = 95;//45
            gradingCalculator.AttendancePercentage = 90;
            string result = gradingCalculator.GetGrade();
            Assert.That(result, Is.EqualTo("A"));
        }
        [Test]
        public void GradeCalc_InputScore85Attendance90_GetAGrade()
        {
            gradingCalculator.Score = 85;//45
            gradingCalculator.AttendancePercentage = 90;
            string result = gradingCalculator.GetGrade();
            Assert.That(result, Is.EqualTo("B"));
        }
        [Test]
        public void GradeCalc_InputScore65Attendance90_GetAGrade()
        {
            gradingCalculator.Score = 65;//45
            gradingCalculator.AttendancePercentage = 90;
            string result = gradingCalculator.GetGrade();
            Assert.That(result, Is.EqualTo("C"));
        }
        [Test]
        public void GradeCalc_InputScore95Attendance65_GetAGrade()
        {
            gradingCalculator.Score =95;//45
            gradingCalculator.AttendancePercentage = 65;
            string result = gradingCalculator.GetGrade();
            Assert.That(result, Is.EqualTo("B"));
        }
        [Test]
        [TestCase(95,55)]
        [TestCase(65, 55)]
        [TestCase(50, 90)]
        public void GradeCalc_FailuresScenariors_GetFGrade(int score,int attendence)
        {
            gradingCalculator.Score = score;//45
            gradingCalculator.AttendancePercentage = attendence;
            string result = gradingCalculator.GetGrade();
            Assert.That(result, Is.EqualTo("F"));
            
        }
        [Test]
        [TestCase(95, 90,ExpectedResult ="A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string GradeCalc_AllGradeLogicalScenariors_GetOutPut(int score, int attendence)
        {
            gradingCalculator.Score = score;//45
            gradingCalculator.AttendancePercentage = attendence;
            return gradingCalculator.GetGrade();
            

        }
    }
}
