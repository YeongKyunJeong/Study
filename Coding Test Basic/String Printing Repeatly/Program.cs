using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_Printing_Repeatly
{
    public class Example
    {
        public static void Main()
        {
            String[] input;

            Console.Clear();
            input = Console.ReadLine().Split(' ');

            String s1 = input[0];
            int a = Int32.Parse(input[1]);

            String answer = null;
            for (int i = 0; i < a; i++)
            {
                answer += s1;
            }
            Console.WriteLine(answer);
        }
    }
}
