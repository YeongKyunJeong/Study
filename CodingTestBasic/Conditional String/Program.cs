using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conditional_String
{
    public class Solution
    {
        public int solution(string ineq, string eq, int n, int m)
        {
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
    }
}
