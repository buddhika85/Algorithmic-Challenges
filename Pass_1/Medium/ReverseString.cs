using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.Medium
{
    public class ReverseString
    {
        private readonly bool _debug = false; // Set to true to print internal states

        public ReverseString()
        {
            RunAllTests();
        }

        /*
         * ------------------------------------------------------------
         *  Problem: Reverse String
         * ------------------------------------------------------------
         *  Write a function that reverses a string.
         *
         *  The input string is given as an array of characters s.
         *  You must do this by modifying the input array in-place
         *  with O(1) extra memory.
         *
         *  Example:
         *      Input:
         *          s = ['h','e','l','l','o']
         *
         *      Output:
         *          ['o','l','l','e','h']
         *
         *  Explanation:
         *      Reverse the characters by swapping from both ends.
         *
         * ------------------------------------------------------------
         *  Expected Time: 5 minutes
         * ------------------------------------------------------------
         */

        private void RunAllTests()
        {
            var testHarness = new (int num, string desc, char[] input, char[] expected)[]
            {
                (1, "Example case", new[] { 'h','e','l','l','o' }, new[] { 'o','l','l','e','h' }),
                (2, "Single character", new[] { 'a' }, new[] { 'a' }),
                (3, "Even length", new[] { 'a','b','c','d' }, new[] { 'd','c','b','a' }),
                (4, "Palindrome", new[] { 'm','a','d','a','m' }, new[] { 'm','a','d','a','m' })
            };

            foreach (var (num, desc, input, expected) in testHarness)
            {
                Test(num, desc, input, expected);
            }
        }

        private void Test(int num, string desc, char[] input, char[] expected)
        {
            var copy = (char[])input.Clone();

            var sw = Stopwatch.StartNew();
            Solve(copy);
            sw.Stop();

            bool pass = copy.Length == expected.Length &&
                        copy.Zip(expected, (a, b) => a == b).All(x => x);

            if (pass)
            {
                Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
            }
            else
            {
                Console.WriteLine($"[FAIL] {num}. {desc} | Expected=[{string.Join(",", expected)}], Actual=[{string.Join(",", copy)}]");
            }
        }

        // ------------------------------------------------------------
        // Solve method: implement logic here
        // use 2 pointers 1 left end, 1 right end swap them, ++left, --right, until they left <= right
        // Best case Time Complexity:   O(n)
        // Average case Time Complexity: O(n)   --> computations increase with number of chars in input
        // Worst case Time Complexity: O(n)
        // Space Complexity: O(1)   --> no any extra data strucures used 
        // 10:17 - 10:21
        // ------------------------------------------------------------
        private void Solve(char[] s)
        {
            if (s.Length <= 2)      // happy path
                return;

            var left = 0;
            var right = s.Length - 1;

            while (left <= right)
            {
                var temp = s[left];
                s[left++] = s[right];
                s[right--] = temp;
            }
        }

        // Optional alternative approach (if needed)
        private void Solve2(char[] s)
        {
            // TODO: implement alternative solution
        }
    }
}
