using System;
using System.Diagnostics;
using System.Linq;

namespace AlgorithmsPractice.Pass_1.Easy
{
    public class ValidPalindrome
    {
        private readonly bool _debug = false; // Set to true to print internal states

        public ValidPalindrome()
        {
            RunAllTests();
        }

        /*
         * ------------------------------------------------------------
         *  Problem: Valid Palindrome
         * ------------------------------------------------------------
         *  Given a string s, determine if it is a palindrome,
         *  considering only alphanumeric characters and ignoring case.
         *
         *  Example:
         *      Input:
         *          "A man, a plan, a canal: Panama"
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
         * ------------------------------------------------------------
         *  Expected Time: 5–10 minutes
         * ------------------------------------------------------------
         */

        private void RunAllTests()
        {
            var testHarness = new (int num, string desc, string input, bool expected)[]
            {
                (1, "Example case", "A man, a plan, a canal: Panama", true),
                (2, "Simple palindrome", "racecar", true),
                (3, "Not a palindrome", "hello", false),
                (4, "Empty string", "", true),
                (5, "Only symbols", "!!!", true),
                (6, "Mixed case", "Noon", true),
                (7, "Edge case", "0P", false),

                // --- Stronger tests ---
    (8, "Symbols between letters", "a!!!a", true),
    (9, "Symbols between mismatch", "ab!!c", false),
    (10, "Multiple skip left", "!!!abA", true),
    (11, "Multiple skip right", "Aba!!!", true),
    (12, "Both skip in same iteration", "a--b--a", true),
    (13, "Alphanumeric mix", "1a2a1", true),
    (14, "Alphanumeric mismatch", "1a2", false),
    (15, "Unicode punctuation", "a—b—a", true), // em-dash
    (16, "Unicode punctuation mismatch", "a—c—a", true),
    (17, "Spaces everywhere", "   a   b   a   ", true),
    (18, "Long non-alphanumeric run", "a************a", true),
    (19, "Case-insensitive with symbols", "M@a#d$a#m", true),
    (20, "Tricky alternating", "a!b!a!b!a", true),
    (21, "Tricky alternating mismatch", "a!b!c!b!d", false),
    (22, "Numbers only palindrome", "12321", true),
    (23, "Numbers only mismatch", "12341", false),
    (24, "Mixed numbers + letters", "1b2b1", true),
    (25, "Mixed numbers + letters mismatch", "1b2c1", false)
            };

            foreach (var (num, desc, input, expected) in testHarness)
            {
                Test(num, desc, input, expected);
            }
        }

        private void Test(int num, string desc, string input, bool expected)
        {
            var sw = Stopwatch.StartNew();
            bool actual = Solve3(input);
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

        // ------------------------------------------------------------
        // Solve method: implement logic here
        // N,o: o,n --> true
        // first remove all non alapha numberic chars --> Noon
        // convert to known case - choosing lower case --> noon
        // use 2 pointers : left = 0, right = s.length - 1
        // while left < right
        //      if s[left] != s[right] --> return false
        //      ++left; right--;
        // return true
        // Best case Time Complexity: -- O(n)       --> number of iterations grow with input size propotionally. So, O(n)
        // Average case Time Complexity: -- O(n)
        // Worst case Time Complexity: -- O(n)  
        // Space Complexity: -- O(1)        --> we did not use any extra data structures
        // 6:42 AM - 6:55 AM
        // ------------------------------------------------------------
        private bool Solve(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return true;

            s = s.Trim()
                .Replace(" ", string.Empty)
                .Replace(",", string.Empty)
                .Replace(":", string.Empty)
                .Replace(";", string.Empty)
                .ToLower();

            var left = 0;
            var right = s.Length - 1;

            while (left < right)
            {
                if (s[left] != s[right])
                    return false;

                // moving inwards
                left++;
                right--;
            }

            return true;
        }


        private bool Solve2(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return true;

            s = s.Trim()
                .ToLower();

            var left = 0;
            var right = s.Length - 1;

            while (left < right)
            {
                // skipping non-alapha numberic chars
                char leftChar = '#';
                char rightChar = '#';

                if (char.IsLetterOrDigit(s[left]))
                {
                    leftChar = s[left];
                }
                else
                {
                    //  move left to inwards to find a suitable char
                    ++left;
                }
                if (char.IsLetterOrDigit(s[right]))
                {
                    rightChar = s[right];
                }
                else
                {
                    //  move right to inwards to find a suitable char
                    --right;
                }

                if (leftChar != '#' && rightChar != '#')
                {
                    if (leftChar != rightChar)
                        return false;
                    else
                    {
                        // both chars are equal, move inwards
                        ++left;
                        --right;
                    }
                }
            }

            return true;
        }


        private bool Solve3(string s)
        {


            if (string.IsNullOrWhiteSpace(s))
                return true;

            s = s.ToLower();

            int left = 0;
            int right = s.Length - 1;

            while (left < right)
            {
                // Move left pointer until alphanumeric
                while (left < right && !char.IsLetterOrDigit(s[left]))
                    left++;

                // Move right pointer until alphanumeric
                while (left < right && !char.IsLetterOrDigit(s[right]))
                    right--;

                // Compare
                if (s[left] != s[right])
                    return false;

                // Move inward
                left++;
                right--;
            }

            return true;


        }
    }
}
