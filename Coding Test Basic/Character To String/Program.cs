using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character_To_String
{
    public class Solution
    {
        public string solution(string[] arr)
        {
            string answer = "";
            for (int i = 0; i < arr.Length; i++)
            {
                answer += arr[i];
            }
            return answer;
        }
    }
}