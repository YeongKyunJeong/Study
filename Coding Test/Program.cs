using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Test
{
    class Program
    {
        //static void Main(string[] args)
        //{

        //}
    }
}



public class Example002
{
    public static void Main()
    {
        String[] s;

        Console.Clear();
        s = Console.ReadLine().Split(' ');

        int a = Int32.Parse(s[0]);
        int b = Int32.Parse(s[1]);

        Console.WriteLine("a = {0}", a);
        Console.WriteLine("b = {0}", b);
    }
}

public class Example003
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


public class Example004
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

public class Example005
{
    public static void Main()
    {
        Console.WriteLine("!@#$%^&*(\\'\"<>?:;"
                         );
    }
}

public class Example006
{
    public static void Main()
    {
        String[] s;

        Console.Clear();
        s = Console.ReadLine().Split(' ');

        int a = Int32.Parse(s[0]);
        int b = Int32.Parse(s[1]);

        Console.WriteLine("{0} + {1} = {2}", a, b, a + b);
    }
}

public class Example007
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

public class Example008
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

public class Example009
{
    public static void Main()
    {
        String[] s;

        Console.Clear();
        s = Console.ReadLine().Split(' ');

        int a = Int32.Parse(s[0]);

        if (a % 2 == 0)
        {
            Console.WriteLine("{0} is even", a);
        }
        else
        {
            Console.WriteLine("{0} is odd", a);
        }
    }
}

public class Solution
{
    public string solution(string my_string, string overwrite_string, int s)
    {
        string answer = "";
        for (int i = 0; i < s; i++)
        {
            answer += my_string[i];
        }
        for (int i = 0; i < overwrite_string.Length; i++)
        {
            answer += overwrite_string[i];
        }
        for (int i = s + overwrite_string.Length; i < my_string.Length; i++)
        {
            answer += my_string[i];
        }

        return answer;
    }
}
