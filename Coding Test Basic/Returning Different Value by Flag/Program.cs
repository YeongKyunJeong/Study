using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Returning_Different_Value_by_Flag
{
    public class Solution
    {
        public int solution(int a, int b, bool flag)
        {
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
    }
}
