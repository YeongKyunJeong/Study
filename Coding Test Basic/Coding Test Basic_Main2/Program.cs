using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding_Test_Basic_Main2
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public class Solutions01
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181921
        public int[] Courses_30_lessons_181921(int l, int r)
        {
            int[] answer = new int[] { };

            string intToString = "";
            bool isPass;
            for (int i = l; i < r + 1; i++)
            {

                intToString = i.ToString();
                isPass = true;

                for (int j = 0; j < intToString.Length; j++)
                {
                    if ((intToString[j] != '5') && (intToString[j] != '0'))
                    {
                        isPass = false;
                        break;
                    }
                }

                if (isPass)
                {
                    answer = IncreaseArrayLength(answer, i);
                }
            }
            if (answer.Length == 0)
            {
                answer = new int[1] { -1 };
            }

            return answer;
        }

        public int[] IncreaseArrayLength(int[] arr, int new_num)
        {

            int[] new_arr = new int[arr.Length + 1];
            for (int i = 0; i < arr.Length; i++)
            {
                new_arr[i] = arr[i];
            }
            new_arr[arr.Length] = new_num;

            return new_arr;
        }

    }
    public class Solutions02
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181920
        public int[] Courses_30_lessons_181920(int start_num, int end_num)
        {
            int[] answer = new int[end_num - start_num + 1];
            for (int i = start_num; i < end_num + 1; i++)
            {
                answer[i - start_num] = i;
            }
            return answer;
        }
    }
    public class Solutions03
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181919
        public int[] Courses_30_lessons_181919(int n)
        {
            int[] answer = new int[1] { n };
            int length = 1;
            while (n > 1)
            {
                if (n % 2 == 0)
                {
                    n /= 2;
                }
                else
                {
                    n = 3 * n + 1;
                }
                length++;
                answer = IncreaseArrayLength(answer, length);
                answer[length - 1] = n;
            }
            return answer;
        }
        public int[] IncreaseArrayLength(int[] arr, int newLength)
        {
            int[] copy = arr;
            arr = new int[newLength];
            for (int i = 0; i < newLength - 1; i++)
            {
                arr[i] = copy[i];
            }
            return arr;
        }
    }
    public class Solutions04
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181918
        public int[] Courses_30_lessons_181918(int[] arr)
        {
            int[] stk = new int[] { };
            int arrLength = arr.Length;

            int i = 0;
            while (i < arrLength)
            {
                if (stk.Length == 0)
                {
                    stk = new int[1] { arr[i] };
                    i++;
                }
                else
                {
                    int[] copy = stk;
                    if (stk[stk.Length - 1] < arr[i])
                    {
                        stk = new int[copy.Length + 1];
                        for (int j = 0; j < copy.Length; j++)
                        {
                            stk[j] = copy[j];
                        }
                        stk[stk.Length - 1] = arr[i];
                        i++;
                    }
                    else
                    {
                        stk = new int[stk.Length - 1];
                        for (int j = 0; j < stk.Length; j++)
                        {
                            stk[j] = copy[j];
                        }
                    }
                }
            }

            return stk;
        }
    }
    public class Solutions05
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181917
        public bool Courses_30_lessons_181917(bool x1, bool x2, bool x3, bool x4)
        {
            bool answer = false;
            if (x1 || x2)
            {
                if (x3 || x4)
                {
                    answer = true;
                }
            }
            else
                answer = false;
            return answer;
        }
    }
    public class Solutions06
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181916
        public int Courses_30_lessons_181916(int a, int b, int c, int d)
        {
            int answer = 1;
            int[] nums = new int[6];

            nums = GetDistribution(nums, a, b, c, d);

            int[] max = new int[2] { 0, 0 };
            int[] min = new int[2] { 0, 5 };

            GetMinMax(nums, ref max, ref min);

            answer = GetPoint(nums, max, min);
            return answer;
        }

        public int[] GetDistribution(int[] arr, int a, int b, int c, int d)
        {
            arr = new int[6];
            arr[a - 1]++;
            arr[b - 1]++;
            arr[c - 1]++;
            arr[d - 1]++;
            return arr;
        }

        public void GetMinMax(int[] arr, ref int[] max, ref int[] min)
        {

            for (int i = 0; i < 6; i++)
            {
                if (arr[i] != 0)
                {

                    if (arr[i] >= max[1])
                    {
                        max = new int[2] { i + 1, arr[i] };
                    }

                    if (arr[i] < min[1])
                    {
                        min = new int[2] { i + 1, arr[i] };
                    }
                }
            }
        }
        public int GetPoint(int[] arr, int[] max, int[] min)
        {
            int answer = 1;

            if (max[1] == 4)
            {
                answer = max[0] * 1111;
            }
            else if (max[1] == 3)
            {
                int sqrt = 10 * max[0] + min[0];
                answer = sqrt * sqrt;
            }
            else if (max[1] == 2)
            {
                if (min[1] == 2)
                {
                    answer = 0;
                    answer = (max[0] + min[0]) * (max[0] - min[0]);
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (arr[i] == 1)
                            answer *= (i + 1);
                    }
                }
            }
            else if (max[1] == 1)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (arr[i] != 0)
                    {
                        answer = (i + 1);
                        break;
                    }
                }
            }
            return answer;
        }
    }
    public class Solutions07
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181915
        public string Courses_30_lessons_181915(string my_string, int[] index_list)
        {
            string answer = "";
            for (int i = 0; i < index_list.Length; i++)
            {
                answer += my_string[index_list[i]];
            }
            return answer;
        }
    }
    public class Solutions08
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181914
        public int Courses_30_lessons_181914(string number)
        {
            int answer = 0;

            for (int i = 0; i < number.Length; i++)
            {
                //answer += int.Parse(number[i].ToString());
                answer += number[i] - '0';
            }
            answer = answer % 9;
            return answer;
        }
    }
    public class Solutions09
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181913
        public string Courses_30_lessons_181913(string my_string, int[,] queries)
        {
            string answer = my_string;
            for (int i = 0; i < queries.GetLength(0); i++)
            {
                answer = Reverse(answer, queries[i, 0], queries[i, 1]);
            }
            Console.WriteLine(answer);

            return answer;
        }

        public string Reverse(string str, int st, int ed)
        {
            string result = "";
            int reverseLength = ed - st + 1;
            for (int i = 0; i < str.Length; i++)
            {
                if (i < st)
                {
                    result += str[i];
                }
                else if (i <= ed)
                {
                    result += str[st + ed - i];
                }
                else
                {
                    result += str[i];
                }
            }

            return result;
        }
    }
    public class Solutions10
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181922
        public int[] Courses_30_lessons_181922(int[] arr, int[,] queries)
        {
            int[] answer = new int[] { };
            int[] query = new int[3];

            for (int i = 0; i < queries.GetLength(0); i++)
            {
                for (int j = 0; j < 3; j++)
                    query[j] = queries[i, j];
                arr = DoQuery(arr, query);
            }

            answer = arr;
            return answer;
        }

        public int[] DoQuery(int[] arr, int[] query)
        {
            int multipleCheck = query[2];
            for (int i = query[0]; i < query[1] + 1; i++)
            {
                if (i % multipleCheck == 0)
                    arr[i]++;
            }
            return arr;
        }
    }
    public class Solutions11
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181932
        public string Courses_30_lessons_181932(string code)
        {
            string answer = "";
            bool isMode0 = true;

            for (int idx = 0; idx < code.Length; idx++)
            {
                if (isMode0)
                {
                    if (code[idx] == '1')
                        isMode0 = false;

                    else if (idx % 2 == 0)
                    {
                        answer += code[idx];
                    }
                }
                else
                {
                    if (code[idx] == '1')
                        isMode0 = true;
                    else if (idx % 2 == 1)
                    {
                        answer += code[idx];
                    }

                }
            }

            if (answer == "")
                return "EMPTY";

            return answer;
        }
    }
    public class Solutions12
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181931
        public int Courses_30_lessons_181931(int a, int d, bool[] included)
        {
            int answer = 0;
            for (int i = 0; i < included.Length; i++)
            {
                if (included[i])
                {
                    answer += (a + d * i);
                }
            }

            return answer;
        }
    }
    public class Solutions13
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181930
        public int Courses_30_lessons_181930(int a, int b, int c)
        {
            int answer = 0;

            int[] numCounts = new int[7];
            numCounts[a]++;
            numCounts[b]++;
            numCounts[c]++;

            int maxCount = 0;
            for (int i = 1; i < 7; i++)
            {
                if (maxCount < numCounts[i])
                    maxCount = numCounts[i];
            }

            if (maxCount == 3)
                answer = 27 * (a * a * a * a * a * a);
            else if (maxCount == 2)
                answer = (a + b + c) * (a * a + b * b + c * c);
            else
                answer = (a + b + c);

            return answer;
        }
    }
    public class Solutions14
    {
        //https://school.programmers.co.kr/learn/courses/30/lessons/181929
        public int Courses_30_lessons_181929(int[] num_list)
        {
            int mult = 1;
            int sumSqr = 0;

            for (int i = 0; i < num_list.Length; i++)
            {
                mult *= num_list[i];
                sumSqr += num_list[i];
            }

            sumSqr *= sumSqr;

            if (mult < sumSqr)
                return 1;
            else
                return 0;
        }
    }
}

