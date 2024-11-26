using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigger_Summation
{
    public class Solution
    {
        public int solution(int a, int b)
        {
            int answer = 0;
            int A = int.Parse((a.ToString() + b.ToString()));
            int B = int.Parse((b.ToString() + a.ToString()));
            if (A >= B)
                answer = A;
            if (B > A)
                answer = B;
            return answer;
        }
    }
}

