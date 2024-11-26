using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparing_Two_Results_of_Operation
{
    public class Solution
    {
        public int solution(int a, int b)
        {
            int answer = 0;
            int op = int.Parse(a.ToString() + b.ToString());
            answer = (op > 2 * a * b) ? op : 2 * a * b;
            return answer;
        }
    }
}