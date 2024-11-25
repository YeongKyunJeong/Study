using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixing_String
{
    public class Solution
    {
        public string solution(string str1, string str2)
        {
            string answer = "";
            for (int i = 0; i < str1.Length; i++)
            {
                answer += str1[i];
                answer += str2[i];
            }
            return answer;
        }
    }
}
