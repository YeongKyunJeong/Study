using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Test_Study
{
    public class Program
    {
        //static int test = 0;
        static void Main(string[] args)
        {
            //Console.WriteLine(Example01_04(5));
            //Console.WriteLine(Example01_04(97615282));
            //Console.WriteLine(Example01_04(1024));
            Test("aAaBbBsSS");
        }

        public static int Example01_01(int N)
        {
            int answer = 0;
            for (int i = 1; i < N + 1; i++)
            {
                if ((i % 3 == 0) || i % 5 == 0)
                {
                    answer += i;
                }
            }

            return answer;
        }
        public static int Example01_02(int[] arr, int N)
        {
            for (int i = 0; i < N - 1; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    if (arr[i] + arr[j] == 100)
                        return 1;
                }
            }
            return 0;
        }
        public static int Example01_03(int N)
        {
            for (int i = 0; i < N; i++)
            {
                int sqare = i * i;
                if (sqare > N)
                {
                    return 0;
                }
                if (i * i == N)
                {
                    Console.WriteLine(i);
                    return 1;
                }
            }
            return 0;
        }

        public static int Example01_04(int N)
        {
            int answer = 1;
            while (2*answer <= N)
            {
                answer *= 2;
            }
            return answer;
        }


        public static void Test(string str)
        {
            string answer = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == str[i].ToUpper())
                {
                    answer += str[i].ToLower();
                }
                else
                {
                    answer += str[i].ToUpper();
                }

            }
            Console.WriteLine(answer);
        }
    }

}
