using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Upper_or_To_Lower
{
    public class Example
    {
        public static void Main()
        {
            String s;

            Console.Clear();
            s = Console.ReadLine();

            String answer = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == s.ToUpper()[i])
                {
                    answer += s.ToLower()[i];
                }
                else
                {
                    answer += s.ToUpper()[i];
                }
            }
            Console.WriteLine(answer);
        }
    }
}