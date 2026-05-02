using System;
using System.Diagnostics;
using System.Linq;

namespace AlgorithmsPractice.Pass_2;

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
     *  - Characters in t may appear multiple times.
     *  - The window must contain all characters of t with correct
     *    frequencies.
     *  - If multiple valid windows exist, return the smallest.
     *
     *  Example:
     *      Input:
     *          s = "ADOBECODEBANC"
     *          t = "ABC"
     *
     *      Output:
     *          "BANC"
     *
     * ------------------------------------------------------------
     *  Expected Time: 20–30 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string s, string t, string expected)[]
        {
            (1, "Example case", "ADOBECODEBANC", "ABC", "BANC"),
            (2, "Exact match", "ABC", "ABC", "ABC"),
            (3, "No possible window", "A", "AA", ""),
            (4, "Multiple windows", "aaflslflsldkalskaaa", "aaa", "aaa"),
            (5, "Single char repeated", "aa", "aa", "aa"),
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
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc}");
            Console.WriteLine($"  Expected=\"{expected}\"");
            Console.WriteLine($"  Actual=\"{actual}\"");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    /*
     *      Input:
         *          s = "ADOBECODEBANC"
         *          t = "ABC"
         *
         *      Output:
         *          "BANC"

         
         so we need to maintain windowStart & windowEnd pointers and minWindowLength.
         additionaly we need a dictionary which contains each char and its count.

         we iterate from left to right using windowStart & windowEnd and when windowEnd reaches end we terminate the loop.
         with in current window we check wether the windowEnd char as a key contains in the dictionary. 
         when window becomes valid substring we start shriking from the left, aiming to find a smaller window. when it becomes invalid, go back to expanding until it becomes valid again.


    */
    // Best case Time Complexity: O(1)          -- if s == t - it is valid substring
    // Average case Time Complexity: O(n)
    // Worst case Time Complexity: O(n)
    // Space Complexity: -- O(k)                -- number of unique chars in s. we need a dictionary of number of keys k.
    // ------------------------------------------------------------
    private string Solve(string s, string t)
    {
        if (s == t)
            return s;

        // step 1 - build master dictionary for t with unique char count
        var uniqueCharCount = 0;
        var masterDict = new Dictionary<char, int>();       // char -> count

        foreach (var item in t)
        {
            if (masterDict.TryGetValue(item, out var currCount))
            {
                masterDict[item] = currCount + 1;
            }
            else
            {
                ++uniqueCharCount;
                masterDict[item] = 1;
            }
        }

        // step 2 - sliding window
        var windowStart = 0;
        var windowEnd = 0;
        var minWindowLength = int.MaxValue;
        var smallestSubStrStart = 0;
        var uniqueCharCountSatisfied = 0;

        var windowDict = new Dictionary<char, int>();       // char -> count

        while (windowEnd < s.Length)
        {
            var currChar = s[windowEnd];
            if (masterDict.TryGetValue(currChar, out var neededCount))
            {
                windowDict[currChar] = windowDict.GetValueOrDefault(currChar, 0) + 1;
                if (windowDict[currChar] == neededCount)
                {
                    ++uniqueCharCountSatisfied;
                }
            }

            while (uniqueCharCountSatisfied == uniqueCharCount)          // while its a valid window
            {
                var currWindowLength = windowEnd - windowStart + 1;
                if (currWindowLength < minWindowLength)
                {
                    // we found a shorter - better window                    
                    smallestSubStrStart = windowStart;
                    minWindowLength = currWindowLength;
                }

                // shrink
                var windowStartChar = s[windowStart];
                windowStart++;
                if (windowDict.TryGetValue(windowStartChar, out var count))
                {
                    windowDict[windowStartChar] = count - 1;        // update count
                    if (windowDict[windowStartChar] < masterDict[windowStartChar])
                    {
                        // window has become invalid
                        --uniqueCharCountSatisfied;
                    }
                }
            }

            ++windowEnd;
        }

        return minWindowLength < int.MaxValue ? s.Substring(smallestSubStrStart, minWindowLength) : string.Empty;
    }

    // Optional brute-force approach
    private string Solve2(string s, string t)
    {
        // TODO: implement alternative solution
        return string.Empty;
    }
}
