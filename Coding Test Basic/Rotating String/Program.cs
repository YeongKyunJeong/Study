using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotating_String
{
    public class Example
    {
        public static void Main()
        {
            String s;

            Console.Clear();
            s = Console.ReadLine();
            for (int i = 0; i < s.Length; i++)
            {
                Console.WriteLine(s[i]);
            }
        }
    }
}