using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void BookCalculatesStatistics()
        {
            var book = new Book("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            var result = book.GetStatistics();

            Assert.Equal(90.5, result.High, 1);
            Assert.Equal(77.3, result.Low, 1);
            Assert.Equal(85.6, result.Average, 1);
        }

        [Fact]
        public void DoesNotAddGradesOutsideOfAcceptableRange()
        {
            Statistics result;
            var book = new Book("");

            book.AddGrade(200.0);
            result = book.GetStatistics();

            Assert.Equal(0.0, result.High, 1);
            Assert.Equal(0.0, result.Low, 1);
            Assert.Equal(0.0, result.Average, 1);

            book.AddGrade(-200.00);
            result = book.GetStatistics();

            Assert.Equal(0.0, result.High, 1);
            Assert.Equal(0.0, result.Low, 1);
            Assert.Equal(0.0, result.Average, 1);
        }
    }
}
