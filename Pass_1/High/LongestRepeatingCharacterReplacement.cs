using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class LongestRepeatingCharacterReplacement
{
    private readonly bool _debug = false; // Set to true to print internal states

    public LongestRepeatingCharacterReplacement()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Longest Repeating Character Replacement
     * ------------------------------------------------------------
     *  You are given a string s and an integer k.
     *
     *  You may choose any character in the string and change it to
     *  any other uppercase English letter. You may perform this
     *  operation at most k times.
     *
     *  Return the length of the longest substring containing the
     *  same letter you can achieve after performing at most k
     *  character replacements.
     *
     *  Example:
     *      Input:
     *          s = "ABAB", k = 2
     *
     *      Output:
     *          4
     *
     *      Explanation:
     *          Replace both 'A's with 'B's or both 'B's with 'A's.
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
            (5, "Mixed characters", "BAAAB", 2, 5),
            (6, "Edge: empty string", "", 3, 0)
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
            Console.WriteLine($"[PASS] {num}. {desc} | k={k} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | k={k} | Expected={expected}, Actual={actual}");
        }
    }
    /*
         *  Example:
         *      Input:
         *          s = "ABAB", k = 2
         *
         *      Output:
         *          4
         *
         *      Explanation:
         *          Replace both 'A's with 'B's or both 'B's with 'A's.

        

         windowLength = windowEnd - windowStart + 1
         longest = max(longest, windowLength)

         windowEnd = 0        windowStart = 0                  
    */
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // -- I have to fills this --
    // Best case Time Complexity: -- I have to fills this --
    // Average case Time Complexity: -- I have to fills this --
    // Worst case Time Complexity: -- I have to fills this --
    // Space Complexity: -- I have to fills this --
    // ------------------------------------------------------------
    private int Solve(string s, int k)
    {
        // TODO: implement

        if (_debug)
        {
            Console.WriteLine("Debug mode enabled...");
        }

        return 0;
    }

    // Optional brute-force approach
    private int Solve2(string s, int k)
    {
        // TODO: implement alternative solution
        return 0;
    }
}
