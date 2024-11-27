using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Returning_Different_Value_by_Even_or_Odd
{
    public class Solution
    {
        public int solution(int n)
        {
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
    }
}