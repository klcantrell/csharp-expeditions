using System;
using Xunit;

namespace GradeBook.Tests
{
    public class TypeTests
    {
        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Kal";
            string newName = MakeUpperCase(name);

            Assert.NotEqual("KAL", name);
            Assert.Equal("KAL", newName);
        }

        [Fact]
        public void CSharpCanPassByRefWithValueTypes()
        {
            var x = GetInt();
            RefSetInt(out x);

            Assert.Equal(42, x);
        }

        [Fact]
        public void CSharpIsPassByValueWithValueTypes()
        {
            var x = GetInt();
            SetInt(x);

            Assert.Equal(3, x);
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            RefGetBookSetName(ref book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1", book1.Name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
        }

        private void SetName(Book book, string name)
        {
            book.Name = name;
        }

        private void GetBookSetName(Book book, string name)
        {
            book = new Book(name, "");
            book.Name = name;
        }

        private void RefGetBookSetName(ref Book book, string name)
        {
            book = new Book(name, "");
            book.Name = name;
        }

        private int GetInt()
        {
            return 3;
        }

        private void SetInt(int x)
        {
            x = 42;
        }

        private void RefSetInt(out int x)
        {
            x = 42;
        }


        private string MakeUpperCase(string s)
        {
            return s.ToUpper();
        }

        private Book GetBook(string name)
        {
            return new Book(name, "");
        }
    }
}
