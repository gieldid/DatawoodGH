using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatawoodGH.Utils
{
	public static class Utils
	{
        /// <summary>
        /// Get's the index of a given character in a string based on the amount of times given character is found
        /// ie: GetNthIndex("abcaba","a",2); will return 3
        /// </summary>
        /// <param name="s">String to check in</param>
        /// <param name="t">Char to look for</param>
        /// <param name="n">Count of given char to return</param>
        /// <returns>The index of the char</returns>
		public static int GetNthIndex(string s, char t, int n) {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
	}
}
