using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class MinimumWindowSubstring
{
    private readonly bool _debug = false; // Set to true to print internal states

    public MinimumWindowSubstring()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Minimum Window Substring
     * ------------------------------------------------------------
     *  Given two strings s and t, return the minimum window in s 
     *  which contains all the characters in t. If no such window 
     *  exists, return an empty string "".
     *
     *  - Characters in t may appear in any order.
     *  - Window must be contiguous.
     *  - If multiple windows have the same length, return any.
     *
     *  Example:
     *      Input:
     *          s = "ADOBECODEBANC"
     *          t = "ABC"
     *
     *      Output:
     *          "BANC"
     *
     *      Explanation:
     *          The smallest substring of s that contains A, B, and C
     *          is "BANC".
     *
     * ------------------------------------------------------------
     *  Expected Time: 15–20 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string s, string t, string expected)[]
        {
            (1, "Example case", "ADOBECODEBANC", "ABC", "BANC"),
            (2, "Exact match", "ABC", "ABC", "ABC"),
            (3, "No possible window", "A", "AA", ""),
            (4, "Multiple possible windows", "aaflslflsldkalskaaa", "aaa", "aaa"),
            (5, "Single character repeated", "aa", "aa", "aa")
        };

        foreach (var (num, desc, s, t, expected) in testHarness)
        {
            Test(num, desc, s, t, expected);
        }
    }

    private void Test(int num, string desc, string s, string t, string expected)
    {
        var sw = Stopwatch.StartNew();
        string actual = Solve(s, t);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | s=\"{s}\" t=\"{t}\" | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | s=\"{s}\" t=\"{t}\" | Expected=\"{expected}\", Actual=\"{actual}\"");
        }
    }
    /*
         *  Example:
         *      Input:
         *          s = "ADOBECODEBANC"
         *          t = "ABC"
         *
         *      Output:
         *          "BANC"

         One thing to notice is always substring starts and ends with a valid char, in between we  need to remaining all chars we need and minimum possible invalid chars.

        A D O B E C O D E B A N C

        A D O B E C     --> has A, B, C, now we shrink until we meet another valid char at the begining

        B E C           --> A is missing, so we expand until we meet a A

        B E C O D E B A     --> has A, B, C, now we shrink until we meet another valid char at the begining

        C O D E B A         --> has A, B, C, now we shrink until we meet another valid char at the begining

        B A             --> C is missing, so we expand until we meet a C

        B A N C
    */
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // 
    // Best case Time Complexity: 
    // Average case Time Complexity:
    // Worst case Time Complexity:
    // Space Complexity:
    // ------------------------------------------------------------
    private string Solve(string s, string t)
    {

        // edge cases
        if (s == t)
            return s;

        var need = new Dictionary<char, int>();
        foreach (var item in t)
        {
            need[item] = need.GetValueOrDefault(item, 0) + 1;
        }

        var window = new Dictionary<char, int>();
        var have = 0;
        var windowStart = 0;
        var windowEnd = 0;
        var minStart = 0;
        var minEnd = 0;

        while (windowEnd < s.Length)
        {
            var curr = s[windowEnd];

            if (need.ContainsKey(curr))
            {
                window[curr] = window.GetValueOrDefault(curr, 0) + 1;
                if (window[curr] == need[curr])
                    ++have;
            }

            while (need.Count == have)
            {
                // we have a valid window now
                if (minStart == 0 && minEnd == 0)
                {
                    // first time a valid window seen
                    minStart = windowStart;
                    minEnd = windowEnd;
                }
                else
                {
                    // we have seen valid windows before
                    var currWindowSize = windowEnd - windowStart + 1;
                    var minWindowSize = minEnd - minStart + 1;
                    // try to override selected window, if its shorter
                    if (currWindowSize < minWindowSize)
                    {
                        minStart = windowStart;
                        minEnd = windowEnd;
                    }
                }

                // now we need to shrink and try find a sub string with less length
                var windowStartChar = s[windowStart];
                ++windowStart;
                if (window.TryGetValue(windowStartChar, out int currCount))
                {
                    // if its a target char, we reduce count and try to make current window smaller
                    window[windowStartChar] = currCount - 1;
                    if (window[windowStartChar] == need[windowStartChar])
                    {
                        // found a shorter, but still valid, window/substring
                        minStart = windowStart;
                        minEnd = windowEnd;
                    }
                    else
                        --have;     // window invalid, next round we will expand
                }
            }

            ++windowEnd;
        }

        return minStart == 0 && minStart == 0 ? string.Empty : s.Substring(minStart, minEnd - minStart + 1);
    }

    // Optional brute-force approach
    private string Solve2(string s, string t)
    {
        // TODO: implement brute-force solution
        return string.Empty;
    }
}
