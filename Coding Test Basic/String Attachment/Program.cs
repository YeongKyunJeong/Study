using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_Attachment
{
    public class Example
    {
        public static void Main()
        {
            String[] input;

            Console.Clear();
            input = Console.ReadLine().Split(' ');

            String s1 = input[0];
            String s2 = input[1];
            Console.WriteLine(s1 + s2);
        }
    }
}