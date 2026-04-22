using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High
{
    public class LongestRepeatingCharacterReplacementII
    {
        private readonly bool _debug = false; // Set to true to print internal states

        public LongestRepeatingCharacterReplacementII()
        {
            RunAllTests();
        }

        /*
         * ------------------------------------------------------------
         *  Problem: Longest Repeating Character Replacement
         * ------------------------------------------------------------
         *  You are given a string s and an integer k. You may choose
         *  any character of the string and change it to any other
         *  uppercase English character. You can perform this operation
         *  at most k times.
         *
         *  Return the length of the longest substring containing the
         *  same letter you can get after performing the above operations.
         *
         *  Example:
         *      Input:
         *          s = "ABAB", k = 2
         *
         *      Output:
         *          4
         *
         *      Explanation:
         *          Replace two 'A's with 'B's or two 'B's with 'A's.
         *
         * ------------------------------------------------------------
         *  Expected Time: 10–15 minutes
         * ------------------------------------------------------------
         */

        private void RunAllTests()
        {
            var testHarness = new (int num, string desc, string s, int k, int expected)[]
            {
                (1, "Example case 1", "ABAB", 2, 4),
                (2, "Example case 2", "AABABBA", 1, 4),
                (3, "All same characters", "AAAA", 2, 4),
                (4, "No replacements allowed", "ABCDE", 0, 1),
                (5, "Mixed characters", "AABABBBAA", 2, 6)
            };

            foreach (var (num, desc, s, k, expected) in testHarness)
            {
                Test(num, desc, s, k, expected);
            }
        }

        private void Test(int num, string desc, string s, int k, int expected)
        {
            var sw = Stopwatch.StartNew();
            int actual = Solve(s, k);
            sw.Stop();

            bool pass = actual == expected;

            if (pass)
            {
                Console.WriteLine($"[PASS] {num}. {desc} | s=\"{s}\" k={k} | Time={sw.ElapsedTicks} ticks");
            }
            else
            {
                Console.WriteLine($"[FAIL] {num}. {desc} | s=\"{s}\" k={k} | Expected={expected}, Actual={actual}");
            }
        }
        /*
        s = "AABABBA", k = 1   => 4

        we expand until k is negative

        A                                           max = 1             dict = {A: 1}               maxChar = A         dictCount - maxCharCount = 0        > k = false
        AA                                          max = 2             dict = {A: 2}               maxChar = A         dictCount - maxCharCount = 0        > k = false   
        AAB -> k 0                                  max = 3             dict = {A: 2, B:1}          maxChar = A         dictCount - maxCharCount = 1        > k = false
        AABA -> k 0                                 max = 4             dict = {A: 3, B:1}          maxChar = A         dictCount - maxCharCount = 1        > k = false
        AABAB -> k -1 invalid window                max = 4             dict = {A: 3, B:2}          maxChar = A         dictCount - maxCharCount = 2        > k = true  --> invalid window

        we start removing chars from left until it becomes valid

        ABAB                                        max = 4             dict = {A: 2, B:2}          maxChar = A         dictCount - maxCharCount = 2        > k = true  --> invalid window
        BAB                                         max = 4             dict = {A: 1, B:2}          maxChar = B         dictCount - maxCharCount = 1        > k = false  --> window valid

        we continue to expand now until window becomes invalid

        BABB                                        max = 4             dict = {A: 1, B:3}          maxChar = B         dictCount - maxCharCount = 1        > k = false  --> window valid
        BABBA                                       max = 4             dict = {A: 2, B:3}          maxChar = B         dictCount - maxCharCount = 2        > k = true   --> invalid window
        */
        // ------------------------------------------------------------
        // Solve method: implement logic here
        // 
        // Best case Time Complexity: O(n)
        // Average case Time Complexity: O(n)
        // Worst case Time Complexity: O(n)
        // Space Complexity: O(26) == O(1)--> 26 = alphabet chars, max is 26, 26 is constant so - O(1)
        // ------------------------------------------------------------
        private int Solve(string s, int k)
        {
            var windowStart = 0;
            var windowEnd = 0;
            var maxWindowSize = 0;

            //var maxChar = s[0];
            var maxCharCount = 0;

            var dict = new Dictionary<char, int>();     // char --> count


            while (windowEnd < s.Length)
            {
                var curr = s[windowEnd];
                dict[curr] = dict.GetValueOrDefault(curr, 0) + 1;

                if (dict[curr] > maxCharCount)
                {
                    maxCharCount = dict[curr];
                    //maxChar = curr;
                }
                var windowSize = windowEnd - windowStart + 1;
                var replacementsNeeded = windowSize - maxCharCount;
                if (replacementsNeeded <= k)
                {
                    maxWindowSize = Math.Max(maxWindowSize, windowSize);
                }

                while (replacementsNeeded > k)
                {
                    // window invalid -- shrink from left and try to make it valid
                    var windowStartChar = s[windowStart];
                    dict[windowStartChar] = dict[windowStartChar] - 1;
                    ++windowStart;
                    windowSize = windowEnd - windowStart + 1;
                    replacementsNeeded = windowSize - maxCharCount;
                }



                ++windowEnd;
            }

            return maxWindowSize;
        }

        // Optional brute-force approach
        private int Solve2(string s, int k)
        {
            // TODO: implement alternative solution
            return 0;
        }
    }
}
