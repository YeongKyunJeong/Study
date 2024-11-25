using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_of_n
{
    public class Solution
    {
        public int solution(int num, int n)
        {
            int answer = 0;
            answer = (num % n == 0) ? 1 : 0;
            return answer;
        }
    }
}
