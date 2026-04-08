using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class ValidAnagram
{
    private readonly bool _debug = false; // Set to true to print internal states

    public ValidAnagram()
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
     *  An anagram is a word or phrase formed by rearranging the
     *  letters of another word or phrase, using all the original
     *  letters exactly once.
     *
     *  Example:
     *      Input:
     *          s = "bat"
     *          t = "tab"
     *
     *      Output:
     *          true
     *
     *      Explanation:
     *          Both strings contain the same characters with the
     *          same frequencies.
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string s, string t, bool expected)[]
        {
            (1, "Both empty", "", "", true),
            (2, "Single char match", "a", "a", true),
            (3, "Single char mismatch", "a", "b", false),
            (4, "Simple anagram", "anagram", "nagaram", true),
            (5, "Not an anagram", "rat", "car", false),
            (6, "Different lengths", "ab", "a", false),
            (7, "Repeating characters", "aabbcc", "abcabc", true)
        };

        foreach (var (num, desc, s, t, expected) in testHarness)
        {
            Test(num, desc, s, t, expected);
        }
    }

    private void Test(int num, string desc, string s, string t, bool expected)
    {
        var sw = Stopwatch.StartNew();
        bool actual = Solve3(s, t);
        sw.Stop();

        if (actual == expected)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | s=\"{s}\" t=\"{t}\" | Expected={expected}, Actual={actual} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | s=\"{s}\" t=\"{t}\" | Expected={expected}, Actual={actual} | Time={sw.ElapsedTicks} ticks");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    //
    // Best Case Time Complexity:    O(n)
    // Average Case Time Complexity: O(n)
    // Worst Case Time Complexity:   O(n)
    //   - We must examine all characters in both strings.
    //
    // Space Complexity: O(1)
    //   - Even though you use a dictionary, it can only ever hold 26 keys (a–z). That’s constant space.
    // --------------------------------------------------------------
    private bool Solve(string s, string t)
    {
        if (s.Length != t.Length)
            return false;

        var dictS = new Dictionary<char, int>();

        for (var i = 0; i < s.Length; i++)
        {
            var current = s[i];
            if (dictS.TryGetValue(current, out int count))
                dictS[current] = ++count;
            else
                dictS[current] = 1;
        }

        foreach (var item in t)
        {
            if (dictS.TryGetValue(item, out int count))
            {
                dictS[item] = --count;
                if (count == 0)
                {
                    dictS.Remove(item);
                }
            }
            else
                return false;           // not an anagram
        }
        return dictS.Count == 0;
    }

    private bool Solve2(string s, string t)
    {
        if (s.Length != t.Length)
            return false;
        var frequencies = new int[26];      // a is 0th index, b is 1st index, ...., z is 25th index
        foreach (var item in s)
        {
            frequencies[item - 'a']++;      // (a - a)++
        }

        foreach (var item in t)
        {
            frequencies[item - 'a']--;
            if (frequencies[item - 'a'] < 0)
                return false;
        }
        return true;
    }

    // Optional alternative approach (sorting-based)
    private bool Solve3(string s, string t)
    {
        if (s.Length != t.Length)
            return false;

        var sArray = s.ToCharArray();
        var tArray = t.ToCharArray();

        Array.Sort(sArray);
        Array.Sort(tArray);

        return sArray.SequenceEqual(tArray);
    }
}
