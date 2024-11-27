using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Test_Basic_Main
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Solutions01
    {
        public static void Courses_30_lessons_181952()
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181952

            String s;

            Console.Clear();
            s = Console.ReadLine();

            Console.WriteLine(s);
        }

        public static void Courses_30_lessons_181951()
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181951

            String[] s;

            Console.Clear();
            s = Console.ReadLine().Split(' ');

            int a = Int32.Parse(s[0]);
            int b = Int32.Parse(s[1]);

            Console.WriteLine("a = {0}", a);
            Console.WriteLine("b = {0}", b);
        }

        public static void Courses_30_lessons_181950()
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181950

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

        public static void Courses_30_lessons_181949()
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181949

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

        public static void Courses_30_lessons_181948()
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181948

            Console.WriteLine("!@#$%^&*(\\'\"<>?:;");
        }

        public static void Courses_30_lessons_181947()
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181947

            String[] s;

            Console.Clear();
            s = Console.ReadLine().Split(' ');

            int a = Int32.Parse(s[0]);
            int b = Int32.Parse(s[1]);

            Console.WriteLine("{0} + {1} = {2}", a, b, a + b);
        }

        public static void Courses_30_lessons_181946()
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181946

            String[] input;

            Console.Clear();
            input = Console.ReadLine().Split(' ');

            String s1 = input[0];
            String s2 = input[1];
            Console.WriteLine(s1 + s2);
        }

        public static void Courses_30_lessons_181945()
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181945

            String s;

            Console.Clear();
            s = Console.ReadLine();
            for (int i = 0; i < s.Length; i++)
            {
                Console.WriteLine(s[i]);
            }
        }

        public static void Courses_30_lessons_181944()
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181944

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


        public static string Courses_30_lessons_181943(string my_string, string overwrite_string, int s)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181943

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

        public static string Courses_30_lessons_181942(string str1, string str2)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181942

            string answer = "";
            for (int i = 0; i < str1.Length; i++)
            {
                answer += str1[i];
                answer += str2[i];
            }
            return answer;
        }

        public static string Courses_30_lessons_181941(string[] arr)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181941

            string answer = "";
            for (int i = 0; i < arr.Length; i++)
            {
                answer += arr[i];
            }
            return answer;
        }

        public static string Courses_30_lessons_181940(string my_string, int k)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181940

            string answer = "";
            for (int i = 0; i < k; i++)
            {
                answer += my_string;
            }
            return answer;
        }

        public static int Courses_30_lessons_181939(int a, int b)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181939

            int answer = 0;
            int A = int.Parse((a.ToString() + b.ToString()));
            int B = int.Parse((b.ToString() + a.ToString()));
            if (A >= B)
                answer = A;
            if (B > A)
                answer = B;
            return answer;
        }

        public static int Courses_30_lessons_181938(int a, int b)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181938

            int answer = 0;
            int op = int.Parse(a.ToString() + b.ToString());
            answer = (op > 2 * a * b) ? op : 2 * a * b;
            return answer;
        }

        public static int Courses_30_lessons_181937(int num, int n)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181937
            
            int answer = 0;
            answer = (num % n == 0) ? 1 : 0;
            return answer;

        }

        public static int Courses_30_lessons_181936(int number, int n, int m)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181936

            int answer = 0;
            answer = ((number % n == 0) && (number % m == 0)) ?
                1 : 0;
            return answer;
        }

        public static int Courses_30_lessons_181935(int n)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181935

            int answer = 0;
            int serial = 0;
            if (n % 2 == 0)
            {
                serial = 2;
                while (serial <= n)
                {
                    answer += serial * serial;
                    serial += 2;
                }
            }
            else
            {
                serial = 1;
                while (serial <= n)
                {
                    answer += serial;
                    serial += 2;
                }
            }

            return answer;
        }

        public static int Courses_30_lessons_181934(string ineq, string eq, int n, int m)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181934

            int answer = 0;

            if (n == m)
            {
                if (eq == "=")
                    return 1;
            }
            else if (n > m)
            {
                if (ineq == ">")
                    return 1;
            }
            else if (n < m)
            {
                if (ineq == "<")
                    return 1;
            }

            return answer;
        }

        public static int Courses_30_lessons_181933(int a, int b, bool flag)
        {
            // https://school.programmers.co.kr/learn/courses/30/lessons/181933

            int answer = 0;
            if (flag)
            {
                answer = a + b;
            }
            else
            {
                answer = a - b;
            }
            return answer;
        }

        //public static void Courses_30_lessons_()
        //{
        //    // https://school.programmers.co.kr/learn/courses/30/lessons/

        //}
    }
}

