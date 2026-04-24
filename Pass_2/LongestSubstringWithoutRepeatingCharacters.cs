using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace AlgorithmsPractice.Pass_2
{
    public class LongestSubstringWithoutRepeatingCharactersIII
    {
        private readonly bool _debug = false; // Set to true to print internal states

        public LongestSubstringWithoutRepeatingCharactersIII()
        {
            RunAllTests();
        }

        /*
         * ------------------------------------------------------------
         *  Problem: Longest Substring Without Repeating Characters
         * ------------------------------------------------------------
         *  Given a string s, find the length of the longest substring
         *  without repeating characters.
         *
         *  Example:
         *      Input:
         *          s = "abcabcbb"
         *
         *      Output:
         *          3
         *
         *      Explanation:
         *          The answer is "abc", with length 3.
         *
         * ------------------------------------------------------------
         *  Expected Time: 10–15 minutes
         * ------------------------------------------------------------
         */

        private void RunAllTests()
        {
            var testHarness = new (int num, string desc, string s, int expected)[]
            {
                (1, "Example case", "abcabcbb", 3),
                (2, "All unique", "abcdef", 6),
                (3, "All same characters", "aaaaaa", 1),
                (4, "Repeating in middle", "abba", 2),
                (5, "Empty string", "", 0),
                (6, "Single character", "z", 1),
                (7, "Mixed characters", "pwwkew", 3)
            };

            foreach (var (num, desc, s, expected) in testHarness)
            {
                Test(num, desc, s, expected);
            }
        }

        private void Test(int num, string desc, string s, int expected)
        {
            var sw = Stopwatch.StartNew();
            int actual = Solve(s);
            sw.Stop();

            bool pass = actual == expected;

            if (pass)
            {
                Console.WriteLine($"[PASS] {num}. {desc} | Input=\"{s}\" | Time={sw.ElapsedTicks} ticks");
            }
            else
            {
                Console.WriteLine($"[FAIL] {num}. {desc} | Input=\"{s}\" | Expected={expected}, Actual={actual}");
            }
        }


        /*
           *  Example:
         *      Input:
         *          s = "abcabcbb"
         *
         *      Output:
         *          3
         *
         *      Explanation:
         *          The answer is "abc", with length 3.

--Write tracing block. Line per each iteration.
0 1 2 3 4 5 6 
t m m z u x t


windowEnd           windowStart         dict                                windowSize        maxWindowSize         currentWindow
0++                 0                   {t: 0}                              0 - 0 + 1 = 1     1                     t
1++                 0                   {t: 0, m: 1}                        1 - 0 + 1 = 1     2                     tm
2++                 1 + 1 = 2           {t: 0, m: 2}                        2 - 2 + 1 = 1     2                     m
3++                 2                   {t: 0, m: 2, z: 3}                  3 - 2 + 1 = 2     2                     mz
4++                 2                   {t: 0, m: 2, z: 3, u: 4}            4 - 2 + 1 = 3     3                     mzu
5++                 2                   {t: 0, m: 2, z: 3, u: 4, x:5}       5 - 2 + 1 = 4     4                     mzux
6++            max(0 + 1 = 1, 2) = 2    {t: 6, m: 2, z: 3, u: 4, x:5}       6 - 2 + 1 = 5     5                     mzuxt
7 --> loop terminate
        */

        // ------------------------------------------------------------
        // Solve method: implement logic here
        // Sliding window approach.
        // we expand using windowRight, we make sure that window does not contain any duplicate chars using dictionary which we store char --> last seen index.
        // if we have not seen the char of window right before we expand, else we shrink by max(windowStart, lastSeenIndex + 1)  --> we use max to ignore already processed chars, see 6++ iterarion of the trace
        // Best case Time Complexity: O(n) --> loop with n iterations which n is num of chars of string s input
        // Average case Time Complexity: O(n)
        // Worst case Time Complexity: O(n)
        // Space Complexity: O(k) --> Dictionary of k kets, k being number of unique chars with in string s input. 
        // ------------------------------------------------------------
        private int Solve(string s)
        {
            var windowStart = 0;
            var windowEnd = 0;
            var maxWindowSize = 0;
            var dict = new Dictionary<char, int>();     // char --> last seen index

            while (windowEnd < s.Length)
            {
                var curr = s[windowEnd];
                if (dict.TryGetValue(curr, out var lastSeenIndex))
                {
                    // shrink
                    windowStart = Math.Max(windowStart, lastSeenIndex + 1);
                }

                var windowSize = windowEnd - windowStart + 1;
                maxWindowSize = Math.Max(maxWindowSize, windowSize);        // possibly we have a bigger window now

                dict[curr] = windowEnd;
                ++windowEnd;        // expand
            }

            return maxWindowSize;
        }

        // Optional brute-force approach
        private int Solve2(string s)
        {
            // TODO: implement alternative solution
            return 0;
        }
    }
}
