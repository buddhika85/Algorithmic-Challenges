
using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.Medium
{
    public class ValidPalindromeII
    {
        private readonly bool _debug = false; // Set to true to print internal states

        public ValidPalindromeII()
        {
            RunAllTests();
        }

        /*
         * 
         *  Problem: Valid Palindrome
         * 
         *  Given a string s, determine if it is a palindrome,
         *  considering only alphanumeric characters and ignoring case.
         *
         *  A palindrome reads the same forward and backward.
         *
         *  Example:
         *      Input:
         *          s = "A man, a plan, a canal: Panama"
         *
         *      Output:
         *          true
         *
         *      Explanation:
         *          After removing non-alphanumeric characters and
         *          converting to lowercase, the string becomes:
         *          "amanaplanacanalpanama"
         *          which is a palindrome.
         *
         * 
         *  Expected Time: 5–10 minutes
         * 
         */

        private void RunAllTests()
        {
            var testHarness = new (int num, string desc, string input, bool expected)[]
            {
                (1, "Example case", "A man, a plan, a canal: Panama", true),
                (2, "Simple palindrome", "racecar", true),
                (3, "Not a palindrome", "hello", false),
                (4, "Only symbols", ".,,!!", true),
                (5, "Mixed case", "Noon", true),
                (6, "Edge case", "0P", false)
            };

            foreach (var (num, desc, input, expected) in testHarness)
            {
                Test(num, desc, input, expected);
            }
        }

        private void Test(int num, string desc, string input, bool expected)
        {
            var sw = Stopwatch.StartNew();
            bool actual = Solve(input);
            sw.Stop();

            bool pass = actual == expected;

            if (pass)
            {
                Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
            }
            else
            {
                Console.WriteLine($"[FAIL] {num}. {desc}");
                Console.WriteLine($"       Input:    \"{input}\"");
                Console.WriteLine($"       Expected: {expected}");
                Console.WriteLine($"       Actual:   {actual}");
            }
        }

        // 
        // Solve method: implement logic here
        // use 2 pointers - go from left to middle, right to middle
        // ignore non letters and non numbers, convert to known case
        // compare and return false if they dont match
        // Best case Time Complexity: O(n)
        // Average case Time Complexity: O(n)
        // Worst case Time Complexity: O(n)
        // Space Complexity: O(1) --> no additional DS used
        // 10:48 - 10:55
        private bool Solve(string s)
        {
            s = s.ToLower();
            var left = 0;
            var right = s.Length - 1;

            while (left < right)
            {
                if (!char.IsLetterOrDigit(s[left]))
                {
                    ++left;
                }
                else if (!char.IsLetterOrDigit(s[right]))
                {
                    --right;
                }
                else
                {
                    if (s[left++] != s[right--])
                        return false;
                }
            }
            return true;
        }

        // Optional brute-force approach
        private bool Solve2(string s)
        {
            // TODO: implement alternative solution
            return false;
        }
    }
}
