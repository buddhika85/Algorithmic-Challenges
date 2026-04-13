using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class FirstUniqueCharacterII
{
    private readonly bool _debug = false; // Set to true to print internal states

    public FirstUniqueCharacterII()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: First Unique Character in a String
     * ------------------------------------------------------------
     *  Given a string s, return the index of the first non-repeating
     *  character in it. If it does not exist, return -1.
     *
     *  Example:
     *      Input:
     *          s = "leetcode"
     *
     *      Output:
     *          0
     *
     *      Explanation:
     *          'l' is the first character that appears only once.
     *
     *      Input:
     *          s = "loveleetcode"
     *
     *      Output:
     *          2
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string s, int expected)[]
        {
            (1, "Example 1", "leetcode", 0),
            (2, "Example 2", "loveleetcode", 2),
            (3, "All repeating", "aabbcc", -1),
            (4, "Single char", "z", 0),
            (5, "Empty string", "", -1),
            (6, "Mixed", "aadadaad", -1),
        };

        foreach (var (num, desc, s, expected) in testHarness)
        {
            Test(num, desc, s, expected);
        }
    }

    private void Test(int num, string desc, string s, int expected)
    {
        var sw = Stopwatch.StartNew();
        int actual = Solve3(s);
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
    *  Given a string s, return the index of the first non-repeating
         *  character in it. If it does not exist, return -1.
         *
         *  Example:
         *      Input:
         *          s = "leetcode"
         *
         *      Output:
         *          0
         *
         *      Explanation:
         *          'l' is the first character that appears only once.
         *
         *      Input:
         *          s = "loveleetcode"
         *
         *      Output:
         *          2
    */
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // first create a dictionary containing character and list of indexes which it is present.
    // then get charcters which number of indexes is 1 - these are non repeating chars
    // then sort their indexes
    // return first
    // Best case Time Complexity: O(1) --> 1 char word, so nothing to repeat, return 0
    // Average case Time Complexity: O(n log n) --> O(n log n) > O(n) , sorting takes (n lon n) time
    // Worst case Time Complexity: O(n log n)
    // Space Complexity: O(n) --> we need additional list of ints with n size in a worst case
    // 11:20 AM - 11:42 AM
    // ------------------------------------------------------------
    private int Solve(string s)
    {
        // edge
        if (string.IsNullOrWhiteSpace(s))
            return -1;

        // best case
        if (s.Length == 1)
            return 0;

        // building dictionary containing character and list of indexes 
        var charIndexes = new Dictionary<char, List<int>>();
        for (int i = 0; i < s.Length; i++)
        {
            char item = s[i];
            if (charIndexes.TryGetValue(item, out var listOfIndexes))
            {
                listOfIndexes.Add(i);
            }
            else
            {
                charIndexes[item] = new List<int> { i };
            }
        }

        // linq solution
        // var nonRepeatingCharIndexes = charIndexes.Where(x => x.Value.Count() == 1).Select(x => x.Value.First()).OrderBy(x => x);
        // return nonRepeatingCharIndexes.Any() ? nonRepeatingCharIndexes.First() : -1;

        // loop solution
        // building non-repeating char indexes
        var nonRepeatingCharIndexes = new List<int>();
        foreach (var item in charIndexes)
        {
            if (item.Value.Count == 1)
            {
                // non-reapting
                nonRepeatingCharIndexes.Add(item.Value.Single());
            }
        }

        if (nonRepeatingCharIndexes.Count == 0)
            return -1;

        // sorting indexes  - O(n log n)
        nonRepeatingCharIndexes.Sort();

        return nonRepeatingCharIndexes.First();

        // for me this is not elegant enough - using ? mark in - var nonRepeatingCharIndexes = new List<int?>();
        //return nonRepeatingCharIndexes.FirstOrDefault() ?? -1;
    }

    // Optional alternative approach (dictionary)
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // first create a dictionary containing character and list of indexes which it is present. - need a for loop
    // for each character of string, - need a for loop
    //      check if its index count is 1, if so, return characters index
    // Best case Time Complexity: O(1) --> 1 char word, so nothing to repeat, return 0
    // Average case Time Complexity: O(n) --> computations increase propotionaly with number of characters in the string
    // Worst case Time Complexity: O(n)
    // Space Complexity: O(n) --> we need additional dictionary with possible n size
    // ------------------------------------------------------------
    private int Solve2(string s)
    {
        // edge
        if (string.IsNullOrWhiteSpace(s))
            return -1;

        // best case
        if (s.Length == 1)
            return 0;

        // building dictionary containing characters and its indexes
        var charIndexes = new Dictionary<char, List<int>>();
        for (int i = 0; i < s.Length; i++)
        {
            char item = s[i];
            if (charIndexes.TryGetValue(item, out var listOfIndexes))
            {
                listOfIndexes.Add(i);
            }
            else
            {
                charIndexes[item] = new List<int> { i };
            }
        }

        // lookup for first char with count 1
        for (int i = 0; i < s.Length; i++)
        {
            char item = s[i];
            if (charIndexes.TryGetValue(item, out var listOfIndexes) && listOfIndexes.Count == 1)       // dictionary look up - O(1)
            {
                return i;
            }
        }

        return -1;
    }


    // Optional alternative approach (using an array)
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // charArray = first create an array with 26 slots containing counts of each char
    // for each character of string, - need a for loop
    //      check if charArray[character - 'a'] == 1    --> return character index
    // Best case Time Complexity: O(1) --> 1 char word, so nothing to repeat, return 0
    // Average case Time Complexity: O(n) --> computations increase propotionaly with number of characters in the string
    // Worst case Time Complexity: O(n)
    // Space Complexity: O(1) --> we need additional array with max size 26, 26 will never increase or decrease, so, its O(1)
    // ------------------------------------------------------------
    private int Solve3(string s)
    {
        // edge
        if (string.IsNullOrWhiteSpace(s))
            return -1;

        // best case
        if (s.Length == 1)
            return 0;

        // building array containing characters counts
        var charCounts = new int[26];
        for (var i = 0; i < s.Length; i++)
        {
            var currChar = s[i];
            charCounts[currChar - 'a']++;
        }

        // lookup for first char with count 1
        for (var i = 0; i < s.Length; i++)
        {
            var item = s[i];
            var indexToCheck = item - 'a';
            if (charCounts[indexToCheck] == 1)
                return i;
        }

        return -1;
    }
}
