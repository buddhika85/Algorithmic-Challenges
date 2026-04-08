using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.Medium;

public class FirstUniqueCharacter
{
    private readonly bool _debug = false;

    public FirstUniqueCharacter()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: First Unique Character in a String
     * ------------------------------------------------------------
     *  Given a string s, return the index of the first non-repeating
     *  character in the string. If it does not exist, return -1.
     *
     *  Example:
     *      Input:
     *          "leetcode"
     *
     *      Output:
     *          0
     *
     *      Explanation:
     *          'l' appears once and is the first unique character.
     *
     *  Example:
     *      Input:
     *          "loveleetcode"
     *
     *      Output:
     *          2
     *
     *      Explanation:
     *          'v' is the first unique character.
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var tests = new (int num, string desc, string input, int expected)[]
        {
            (1, "Example 1", "leetcode", 0),
            (2, "Example 2", "loveleetcode", 2),
            (3, "All repeating", "aabbcc", -1),
            (4, "Single char", "z", 0),
            (5, "Empty string", "", -1),
            (6, "Unique at end", "aabbccd", 6),
        };

        foreach (var (num, desc, input, expected) in tests)
        {
            Test(num, desc, input, expected);
        }
    }

    private void Test(int num, string desc, string input, int expected)
    {
        var sw = Stopwatch.StartNew();
        var actual = Solve(input);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc}");
            Console.WriteLine($"Input:    \"{input}\"");
            Console.WriteLine($"Expected: {expected}");
            Console.WriteLine($"Actual:   {actual}");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    //
    // Best Case Time Complexity: O(n)
    // Average Case Time Complexity: O(n)
    // Worst Case Time Complexity: O(n)
    // Space Complexity: O(1) --> because dictionary size is bounded (26 letters)
    // ------------------------------------------------------------
    private int Solve(string s)
    {
        // TODO: implement

        if (_debug)
        {
            Console.WriteLine("Debug mode enabled...");
        }

        return -1;
    }

    // Optional alternative approach (two-pass dictionary)
    private int Solve2(string s)
    {
        // TODO: implement alternative solution
        return -1;
    }
}
