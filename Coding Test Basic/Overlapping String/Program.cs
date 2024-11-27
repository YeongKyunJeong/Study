using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overlapping_String
{
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
}