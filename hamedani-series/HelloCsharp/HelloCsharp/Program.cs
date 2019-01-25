using HelloCsharp.Math;
using System;

namespace HelloCsharp
{
    class Program
    {
        // const vs normal variables
        private const string Value = "Hello World!";
        string OtherValue = "Hello There!";

        static void Main(string[] args)
        {

            Console.WriteLine(Value);

            //overflowing
            byte number = 255;
            number = (byte)(number + 1); // this would equal 0
            Console.WriteLine(number);

            //checked
            //{
            //    byte number = 255;
            //    number = (byte)(number + 1);
            //}  // this would throw an error 


            // variables 
            byte Vnum = 2;
            int Vcount = 10;
            // float totalPrice = 20.95; // without appending an f, compiler doesn't know this is a float
            float VtotalPrice = 20.95f; // without appending an f, compiler doesn't know this is a float
            char Vcharacter = 'A';
            bool VisWorking = false;

            // variables with var, inferred type
            var num = 2; //int by default
            var count = 10;
            // float totalPrice = 20.95; // without appending an f, compiler doesn't know this is a float
            var totalPrice = 20.95f; // without appending an f, compiler doesn't know this is a float
            var character = 'A';
            var isWorking = false;

            Console.WriteLine(number);
            Console.WriteLine(count);
            Console.WriteLine(totalPrice);
            Console.WriteLine(character);
            Console.WriteLine(isWorking);

            Console.WriteLine("byte min and max:");
            Console.WriteLine("{0} {1}", byte.MinValue, byte.MaxValue);

            Console.WriteLine("float min and max:");
            Console.WriteLine("{0} {1}", float.MinValue, float.MaxValue);

            // constants
            const float Pi = 3.14f;

            // implicit type conversion
            byte b = 1;
            int i = b;

            int j = 1;
            // byte c = j; // cannot implicitly go from int to byte unless you specifically type cast

            byte c = (byte) j;

            // converting non compatible types
            var s = "1";
            int k = Convert.ToInt32(s);
            int m = int.Parse(s);

            // handling exceptions
            try
            {
                var numString = "1234";
                byte myByte = Convert.ToByte(numString);
                Console.WriteLine(myByte);
            }
            catch (Exception)
            {
                Console.WriteLine("Hey, that didn't work out.  Number could not be converted" +
                    "to a byte");
            }

            /*
             * multiline comments (not very common)
            */

            // classes
            Person john = new Person();
            john.FirstName = "John";
            john.LastName = "Smith";
            john.Introduce();

            Calculator calculator = new Calculator();
            Console.WriteLine("Calculator result: " + calculator.Add(1, 2));

            // arrays
            int[] myIntArray = new int[3]; // ints default value are 0.  another example is that bools are initialized to false.
            var anotherIntArray = new int[3];

            var names = new string[] { "Jack", "John", "Mary" };
            Console.WriteLine("first name " + names[0]);
            Console.WriteLine("second name " + names[1]);
            Console.WriteLine("third name " + names[2]);
        }
    }
}
