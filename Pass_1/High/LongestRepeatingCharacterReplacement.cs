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
        int actual = Solve4(s, k);
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

thanks, so, if we take
0 1 2 3 4 5 6
A A B A B B A

windowStart = 0
windowEnd = 0
have we seen windowEnd char before? No
freqs = [a:1]
windowSize = 0 - 0 + 1 = 1
windowSize - maxFreq <= k   = 1 - 1 <= 1 --> true


then,
windowStart = 0
windowEnd = 1
have we seen windowEnd char before? Yes
freqs = [a:2]
windowSize = 1 - 0 + 1 = 2
windowSize - maxFreq <= k   = 2 - 2 <= 1 --> true



then,
windowStart = 0
windowEnd = 2
have we seen windowEnd char before? No
freqs = [a:2, b:1]
windowSize = 2- 0 + 1 = 3
windowSize - maxFreq <= k   = 3 - 2 <= 1 --> true

then,
windowStart = 0
windowEnd = 3
have we seen windowEnd char before? Yes
freqs = [a:3, b:1]
windowSize = 3 - 0 + 1 = 4
windowSize - maxFreq <= k   = 4 - 3 <= 1 --> true

then,
windowStart = 0
windowEnd = 5
have we seen windowEnd char before? Yes
freqs = [a:3, b:2]
windowSize = 3 - 0 + 1 = 4
windowSize - maxFreq <= k   = 4 - 3 <= 1 --> true

    */
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // I walk through the string with a window.
    // I keep track of how many times each character appears.
    // As long as the window can be fixed with ≤ k replacements, I keep expanding.
    // If it becomes too expensive, I shrink from the left.
    // Best case Time Complexity: O(n)
    // Average case Time Complexity: O(n)
    // Worst case Time Complexity: O(n)
    // Space Complexity: O(1) - frequency dictionary has a fixed upper bound
    // ------------------------------------------------------------
    /*
    s = "AABABBBAA"
    k = 2

    0 1 2 3 4 5 6 7 8
    A A B A B B B A A

    windowStart     windowEnd           freqs                       maxFreqs         windowSize                 replacements                    longest
    0               0                   {A:1}                       1                0 - 0 + 1 = 1              1 - 1 = 0                       1 
    0               1                   {A:2}                       2                1 - 0 + 1 = 2              2 - 2 = 0                       2 
    0               2                   {A:2, B:1}                  2                2 - 0 + 1 = 3              3 - 2 = 1                       3 
    0               3                   {A:3, B:1}                  3                3 - 0 + 1 = 4              4 - 3 = 1                       4 
    0               4                   {A:3, B:2}                  3                4 - 0 + 1 = 5              5 - 3 = 2                       5 

    0               5                   {A:3, B:3}                  3                5 - 0 + 1 = 6              6 - 3 = 3                       5   ** invalid
    1               5                   {A:2, B:3}                  3                5 - 1 + 1 = 5              5 - 3 = 2                       5       

    1               6                   {A:2, B:4}                  4                6 - 1 + 1 = 6              6 - 4 = 2                       6 

    1               7                   {A:3, B:4}                  4                7 - 1 + 1 = 7              7 - 4 = 3                       6  ** invalid
    2               7                   {A:2, B:4}                  4                7 - 2 + 1 = 6              6 - 4 = 2                       6  

    2               8                   {A:3, B:4}                  4                8 - 2 + 1 = 7              7 - 4 = 3                       6  ** invalid 
    3               8                   {A:2, B:4}                  4                8 - 3 + 1 = 6              6 - 4 = 2                       6  

    longest = 6

    */

    /*
    s = "AABABBA"
k = 1

    */
    private int Solve(string s, int k)
    {
        var windowStart = 0;
        var windowEnd = 0;
        var freqs = new Dictionary<char, int>();
        var longest = 0;
        var maxFreq = 0;

        while (windowEnd < s.Length)
        {
            var currChar = s[windowEnd];
            var count = freqs.GetValueOrDefault(currChar, 0);
            freqs[currChar] = ++count;
            if (count > maxFreq)
                maxFreq = count;
            var windowSize = windowEnd - windowStart + 1;
            var replacementsNeeded = windowSize - maxFreq;
            if (replacementsNeeded > k)
            {
                // window invalid
                var windowStartChar = s[windowStart];
                freqs[windowStartChar]--;        // remove windowStart from Freq
                windowStart++;
                windowSize = windowEnd - windowStart + 1;
            }
            longest = Math.Max(longest, windowSize);
            windowEnd++;
        }
        return longest;
    }

    // Optional brute-force approach
    private int Solve2(string s, int k)
    {
        var windowStart = 0;
        var freqs = new Dictionary<char, int>();
        var longest = 0;
        var maxFreq = 0;

        for (int windowEnd = 0; windowEnd < s.Length; windowEnd++)
        {
            char currChar = s[windowEnd];

            freqs[currChar] = freqs.GetValueOrDefault(currChar, 0) + 1;
            maxFreq = Math.Max(maxFreq, freqs[currChar]);

            int windowSize = windowEnd - windowStart + 1;
            int replacementsNeeded = windowSize - maxFreq;

            if (replacementsNeeded > k)
            {
                char leftChar = s[windowStart];
                freqs[leftChar]--;
                windowStart++;
            }

            longest = Math.Max(longest, windowEnd - windowStart + 1);
        }

        return longest;
    }

    // 11 mins
    private int Solve3(string s, int k)
    {
        var windowStart = 0;
        var windowEnd = 0;
        var longest = 0;
        var freqs = new Dictionary<char, int>();        // char --> count
        var maxFreq = 0;

        while (windowEnd < s.Length)
        {
            var currChar = s[windowEnd];
            var currCount = freqs.GetValueOrDefault(currChar, 0) + 1;
            freqs[currChar] = currCount;
            maxFreq = Math.Max(maxFreq, currCount);

            var windowSize = windowEnd - windowStart + 1;
            var replacementsNeeded = windowSize - maxFreq;

            if (replacementsNeeded > k)
            {
                // this means we have an invalid window - means the window contains chars that cannot be fixed with k replacements, we need k + 1
                // shrink from left
                var windowStartChar = s[windowStart];
                freqs[windowStartChar]--;
                windowStart++;

                // recalculate window size as it got shrinked
                windowSize = windowEnd - windowStart + 1;
            }

            longest = Math.Max(longest, windowSize);
            ++windowEnd;
        }
        return longest;
    }

    // 7:37 AM - 7:42 AM
    /*
    we use sliding window with char frequency counting dictionary.
    with in window we can have n number of chars, and character count other than most frequent char can be either k or less than k.
    so we try to expand the sliding window by moving windowEnd pointer to right. and we check if the other character count (not most frequent char) is less than or equal to k.
    if so update longest count, and move forward.
    else means, the sliding window is in valid. so we move window start to left. and we update freeuencies dictionary.
    */
    // 7:43 AM - 7:52 AM
    private int Solve4(string s, int k)
    {
        var windowStart = 0;
        var windowEnd = 0;
        var maxFreq = 0;
        var longest = 0;
        var freqs = new Dictionary<char, int>();        // char --> count

        while (windowEnd < s.Length)
        {
            var currChar = s[windowEnd];
            var count = freqs.GetValueOrDefault(currChar, 0) + 1;
            freqs[currChar] = count;
            maxFreq = Math.Max(maxFreq, count);
            var windowSize = windowEnd - windowStart + 1;
            var replacementsNeeded = windowSize - maxFreq;
            if (replacementsNeeded > k)
            {
                // trying to replace more than k chars
                // window invalid - we need shrink from window start
                var windowsStartToRemove = s[windowStart];
                freqs[windowsStartToRemove]--;
                windowStart++;

                // reduce window size also by 1, as we moveed window start to 1 char right - shrink
                windowSize--;
            }

            longest = Math.Max(longest, windowSize);
            ++windowEnd;
        }
        return longest;
    }

    /*
    
    Longest substring without repeating characters

Longest substring with at most K distinct characters

Fruits into baskets

Character replacement (the one we just did)
    
    */
}
