using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            log += ReturnMessage;
            log += AnotherReturnMessage;

            log("hello!");

            Assert.Equal(3, count);
        }

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

        private string ReturnMessage(string message)
        {
            count++; // bad but proving a point
            return message;
        }

        private string AnotherReturnMessage(string message)
        {
            count++; // bad but proving a point
            return message.ToLower();
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name, "");
            book.Name = name;
        }

        private void RefGetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name, "");
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

        private InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name, "");
        }
    }
}
