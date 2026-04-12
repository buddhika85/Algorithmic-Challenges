using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class ValidAnagramII
{
    private readonly bool _debug = false; // Set to true to print internal states

    public ValidAnagramII()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Valid Anagram
     * ------------------------------------------------------------
     *  Given two strings s and t, return true if t is an anagram
     *  of s, and false otherwise.
     *
     *  An Anagram is a word or phrase formed by rearranging the
     *  letters of a different word or phrase, typically using all
     *  the original letters exactly once.
     *
     *  Example:
     *      Input:
     *          s = "anagram"
     *          t = "nagaram"
     *
     *      Output:
     *          true
     *
     *      Input:
     *          s = "rat"
     *          t = "car"
     *
     *      Output:
     *          false
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string s, string t, bool expected)[]
        {
            (1, "Example 1", "anagram", "nagaram", true),
            (2, "Example 2", "rat", "car", false),
            (3, "Different lengths", "a", "aa", false),
            (4, "Unicode mismatch", "你好", "好你你", false),
            (5, "Empty strings", "", "", true),
            (6, "Case sensitivity", "Hello", "hello", false)
        };

        foreach (var (num, desc, s, t, expected) in testHarness)
        {
            Test(num, desc, s, t, expected);
        }
    }

    private void Test(int num, string desc, string s, string t, bool expected)
    {
        var sw = Stopwatch.StartNew();
        bool actual = Solve(s, t);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Expected={expected}, Actual={actual}");
        }
    }

    /*
     *  Given two strings s and t, return true if t is an anagram
     *  of s, and false otherwise.
     *
     *  An Anagram is a word or phrase formed by rearranging the
     *  letters of a different word or phrase, typically using all
     *  the original letters exactly once.
     *
     *  Example:
     *      Input:
     *          s = "bat"
     *          t = "tab"
     *
     *      Output:
     *          true
    */

    // ------------------------------------------------------------
    // Solve method: implement logic here
    // if the length of 2 words are not equal, not an anagram - return false
    // use a loop, go through each char in 1st word, 
    //      and populate a dictionary, with key char, and count of that char as value.
    // then use another loop, go though each char in 2nd word, 
    //      check if that char exists in dictionary, if not exits, not anagram, return false
    //      if exists remove char count by 1, then if new char count is zero, remove that char entry from dictionary
    // at the check if dictionary is empty - if empty, its anagram
    // Best case Time Complexity: O(1) --> single char for both words, so 1 iteration 
    // Average case Time Complexity: O(n) --> computations increase with the length of the words
    // Worst case Time Complexity: O(n) --> same
    // Space Complexity: O(n) --> n is number of iterations, k is number of unique chars in 1st word, K < n, in most cases, so O(n)
    // 9:36 AM -9:54 AM
    // ------------------------------------------------------------
    private bool Solve(string s, string t)
    {
        if (s.Length != t.Length)
            return false;

        // building dictionary from 1st string
        var seen = new Dictionary<char, int>();      // char --> count
        foreach (var item in s)
        {
            if (seen.TryGetValue(item, out var count))
                seen[item] = count + 1;
            else
                seen[item] = 1;
        }

        // check if exists in 2nd string
        foreach (var item in t)
        {
            if (seen.TryGetValue(item, out var count))
            {
                seen[item] = count - 1;
                if (seen[item] == 0)
                    seen.Remove(item);
            }
            else
            {
                return false;
            }
        }

        return seen.Count == 0;
    }

    // Optional alternative approach (sorting)
    private bool Solve2(string s, string t)
    {
        // TODO: implement alternative solution
        return false;
    }
}
