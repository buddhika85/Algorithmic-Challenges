using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class LongestSubstringWithoutRepeatingCharacters
{
    //private readonly bool _debug = false; // Set to true to print internal states

    public LongestSubstringWithoutRepeatingCharacters()
    {
        RunAllTests();
    }

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string input, int expected)[]
        {
            (1, "Empty string", "", 0),
            (2, "Single character", "a", 1),
            (3, "All same characters", "aa", 1),
            (4, "Two distinct characters", "ab", 2),
            (5, "Example 1", "abcabcbb", 3),
            (6, "Example 2", "bbbbb", 1),
            (7, "Example 3", "pwwkew", 3),
            (8, "Overlapping window", "dvdf", 3),
            (9, "Repeating with reset", "abba", 2)
        };

        foreach (var (num, desc, input, expected) in testHarness)
        {
            Test(num, desc, input, expected);
        }
    }

    private void Test(int num, string desc, string input, int expected)
    {
        var sw = Stopwatch.StartNew();
        int actual = Solve2(input);
        sw.Stop();

        if (actual == expected)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Input=\"{input}\" | Expected={expected}, Actual={actual} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Input=\"{input}\" | Expected={expected}, Actual={actual} | Time={sw.ElapsedTicks} ticks");
        }
    }

    // Solve method: implement logic here
    // TO DO: Explain your approach with a simple test case
    // we have a window - left and right indexes points to left and right indexes & a dictionary storing lest seen index of char
    // we go through each charater of string using a loop count as right index
    // abcba
    // Time Complexity:
    // Best Case:    O(n)
    // Average Case: O(n)
    // Worst Case:   O(n)
    // Explanation: Each character is processed at most twice — once by the right pointer,
    // and at most once by the left pointer when it jumps forward. The dictionary lookup
    // and updates are O(1) on average, so the entire algorithm runs in linear time.
    //
    // Space Complexity: O(n)
    // Explanation: The dictionary stores the last-seen index for characters in the window.
    // In the worst case (all characters unique), it grows to size n.
    // Total Expected Time: 10–15 minutes
    private int Solve(string s)
    {
        var lastSeen = new Dictionary<char, int>();
        int left = 0;
        int longest = 0;

        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];

            if (lastSeen.ContainsKey(c))
            {
                left = Math.Max(left, lastSeen[c] + 1);
                //left = lastSeen[c] + 1;                           // this makes left pointer move backwards sometimes, so avoid this - a,b,b,a
            }

            lastSeen[c] = right;

            longest = Math.Max(longest, right - left + 1);
        }

        return longest;
    }


    /*
    This is the optimized version of sliding window where you jump the left pointer instead of shrinking step‑by‑step. 
    You store the last seen index of each character in a dictionary. 
    When you see a duplicate, you move the left pointer to lastSeen[c] + 1, but never backwards — enforced by Math.Max(left, lastSeen[c] + 1). 
    This pattern is clean, deterministic, and avoids nested loops. It’s ideal for problems involving duplicates, distinct characters, or longest substring logic. 
    This is the version senior engineers prefer and the one that matches your natural thinking style.
    */
    private int Solve2(string s)
    {
        var lastSeen = new Dictionary<char, int>();
        int left = 0;
        int longest = 0;

        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];

            if (lastSeen.TryGetValue(c, out int lastSeenIndex))
            {
                left = Math.Max(left, lastSeenIndex + 1);
            }

            lastSeen[c] = right;

            longest = Math.Max(longest, right - left + 1);
        }

        return longest;
    }
}
