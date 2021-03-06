using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void BookCalculatesStatistics()
        {
            var book = new InMemoryBook("", "");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            var result = book.GetStatistics();

            Assert.Equal(90.5, result.HighGrade, 1);
            Assert.Equal(77.3, result.LowGrade, 1);
            Assert.Equal(85.6, result.AverageGrade, 1);
            Assert.Equal('B', result.LetterGrade);
        }

        [Fact]
        public void DoesNotAddGradesOutsideOfAcceptableRange()
        {
            var book = new InMemoryBook("", "");

            Assert.Throws<ArgumentException>(() =>
            {
                book.AddGrade(200.0);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                book.AddGrade(-200.00);
            });
        }
    }
}
