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
        // var sw = Stopwatch.StartNew();
        // int actual = Solve4(input);
        // sw.Stop();

        // if (actual == expected)
        // {
        //     Console.WriteLine($"[PASS] {num}. {desc} | Input=\"{input}\" | Expected={expected}, Actual={actual} | Time={sw.ElapsedTicks} ticks");
        // }
        // else
        // {
        //     Console.WriteLine($"[FAIL] {num}. {desc} | Input=\"{input}\" | Expected={expected}, Actual={actual} | Time={sw.ElapsedTicks} ticks");
        // }


        var sw = Stopwatch.StartNew();
        (int Length, string UniqueStr) = Solve7(input);
        sw.Stop();

        if (Length == expected)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Input=\"{input}\" | Expected Length={expected}, Actual Length={Length}, Actual Unique String={UniqueStr} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Input=\"{input}\" | Expected Length={expected}, Actual Length={Length}, Actual Unique String={UniqueStr} | Time={sw.ElapsedTicks} ticks");
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


    /*
                0, 1, 2, 3
        str =   a, b, b, a

        if (previsly seen)
            startIdx  = max(lastSeen[str[i]] + 1, startIdx)

        windowSize  = i - startIndex + 1
        longestLength = max(windowSize, longestLength)


        i = 0, startIdx = 0, lastSeen = [ { a: 0 } ], longestSubString = "a", longestLength = 1
            windowSize  = i - startIndex + 1 = 0 - 0 + 1 = 1
            longestLength = max(windowSize, longestSubStringLength) = max(1, 0) = 1


        i = 1, startIdx = 0, lastSeen = [ { a: 0, b: 1 } ], longestSubString = "ab", longestLength = 2
            windowSize  = i - startIndex + 1 = 1 - 0 + 1 = 2
            longestLength = max(windowSize, longestSubStringLength) = max(2, 1) = 2

        i = 2, startIdx = 2, lastSeen = [ { a: 0, b: 1 => 2 } ], longestSubString = "ab", longestLength = 2
            startIdx  = max(lastSeen[str[i]] + 1, startIdx) = (1 + 1, 2) = 2
            windowSize  = i - startIndex + 1 = 2 - 2 + 1 = 1
            longestLength = max(windowSize, longestSubStringLength) = max(1, 2) = 2

        i = 3, startIdx = 2, lastSeen = [ { a: 0 => 3, b: 1 } ], longestSubString = "ab", longestLength = 2
            startIdx  = max(lastSeen[str[i]] + 1, startIdx) = (0 + 1, 2) = 2
            windowSize  = i - startIndex + 1 = 3 - 2 + 1 = 2
            longestLength = max(windowSize, longestSubStringLength) = max(2, 2) = 2

        So, longest sub string length = 2
        logest unique sub string = "ab"
    */
    private int Solve3(string s)
    {
        var windowStart = 0;
        var windowEnd = 0;
        var longestSubstringLength = 0;

        var lastSeen = new Dictionary<char, int>();

        while (windowEnd < s.Length)
        {
            var currentChar = s[windowEnd];
            if (lastSeen.TryGetValue(currentChar, out var lastSeenIndex))
            {
                // we have seen the char before              
                windowStart = Math.Max(lastSeenIndex + 1, windowStart);
            }

            lastSeen[currentChar] = windowEnd;
            var windowSize = windowEnd - windowStart + 1;
            longestSubstringLength = Math.Max(windowSize, longestSubstringLength);

            ++windowEnd;
        }
        return longestSubstringLength;
    }




    // ------------------------------------------------------------
    // Sliding Window Mastery Checklist (for me to internalize)
    // ------------------------------------------------------------
    //
    // Step 1 — Solve "abba"
    //      - Don’t trace every step
    //      - Don’t overthink
    //      - Just write the code
    //      - Trust the pattern
    //
    // Step 2 — Solve "abcabcbb"
    //      - Reinforces the “jump left forward” logic
    //
    // Step 3 — Solve "pwwkew"
    //      - Reinforces the “shrink then expand” logic
    //
    // Step 4 — Solve "tmmzuxt"
    //      - The final boss — once I solve this, the pattern is automatic
    //
    // After these 4, sliding window becomes muscle memory.
    // ------------------------------------------------------------

    /*
- Expand windowEnd normally.
- When we hit a repeat, jump windowStart to lastSeen[c] + 1.
- Use Math.Max to avoid moving windowStart backward.
- Window always contains unique characters.

        0 1 2 3
        a b b a

        if lastSeen
            windowStart = max(windowStart, lastSeenIndex + 1)

        windowSize = windowEnd - windowStart + 1
        longestLength = max(longestLength, windowSize)       

        windowEnd = 0,     windowStart = 0,     windowSize = 1,     lastSeen = [{a: 0}],                longestLength = 1,      longestString = a
        windowEnd = 1,     windowStart = 0,     windowSize = 2,     lastSeen = [{a: 0}, {b: 1}],        longestLength = 2,      longestString = ab
        windowEnd = 2,     windowStart = 2,     windowSize = 1,     lastSeen = [{a: 0}, {b: 2}],        longestLength = 2,      longestString = ab
        windowEnd = 3,     windowStart = 2,     windowSize = 1,     lastSeen = [{a: 3}, {b: 2}],        longestLength = 2,      longestString = ab
    */


    /*

    Current Window always contains set of unique characters.
    We try expanding Current Windows - Window End pointer.
    When we see a character that we have already seen (check using dictionary which stores char and last seen index) we move Window Start to Max of 'last seen index' + 1 OR Window Start 
        - This is to avoid Window Start going backwards, which may contain repeating characters which we considered before. 

    if character seen before
        windowStart = max(windowStart, lastSeenIndex + 1)
    
    windowSize = windowEnd - windowStart + 1;
    longest = max(windowSize, longest)

    0 1 2 3 4 5 6 7
    a b c a b c b b

    windowEnd = 0,      WindowStart = 0,                        windowSize = 1,     lastSeen = [ {a:0} ],                       longest = 1,    longestString = a
    windowEnd = 1,      WindowStart = 0,                        windowSize = 2,     lastSeen = [ {a:0}, {b:1} ],                longest = 2,    longestString = ab
    windowEnd = 2,      WindowStart = 0,                        windowSize = 3,     lastSeen = [ {a:0}, {b:1}, {c:2} ],         longest = 3,    longestString = abc
    windowEnd = 3,      WindowStart = max(0, 0 + 1) = 1,        windowSize = 3,     lastSeen = [ {a:3}, {b:1}, {c:2} ],         longest = 3,    longestString = abc
    windowEnd = 4,      WindowStart = max(1, 1 + 1) = 2,        windowSize = 3,     lastSeen = [ {a:3}, {b:4}, {c:2} ],         longest = 3,    longestString = abc
    windowEnd = 5,      WindowStart = max(2, 2 + 1) = 3,        windowSize = 3,     lastSeen = [ {a:3}, {b:4}, {c:5} ],         longest = 3,    longestString = abc
    windowEnd = 6,      WindowStart = max(3, 4 + 1) = 5,        windowSize = 2,     lastSeen = [ {a:3}, {b:6}, {c:5} ],         longest = 3,    longestString = abc
    windowEnd = 7,      WindowStart = max(5, 6 + 1) = 7,        windowSize = 1,     lastSeen = [ {a:3}, {b:7}, {c:5} ],         longest = 3,    longestString = abc

    */


    /*
    --Explain logic in 2-4 setences.
    Sliding window maintains a window which always contains unique characters.
    We keep the track of longest unique character window found.
    We iterate through each character of the string and,
    We store each character and its last seen index in a dictionary.

    --Write if blocks and formulas.
    if we have already seen the character:
        windowStart = max(windowStart, last seen index + 1)     --> max used to avoid windowStart going backwards, which may cause us to consider duplicates that we have already seen.
    
    windowLength = (windowEnd - windowStart) +  1
    longest = max(longest, windowLength)
   
    Write tracing block. Line per each iteration.

    0 1 2 3 4 5
    p w w k e w

    windowEnd = 0,     windowStart = 0,                    windowLength = 1,      lastSeen = [ {p: 0} ],                            longest = 1,     longestStr = "p"
    windowEnd = 1,     windowStart = 0,                    windowLength = 2,      lastSeen = [ {p: 0}, {w: 1} ],                    longest = 2,     longestStr = "pw"
    windowEnd = 2,     windowStart = max(0, 1+1) = 2,      windowLength = 1,      lastSeen = [ {p: 0}, {w: 2} ],                    longest = 2,     longestStr = "pw"
    windowEnd = 3,     windowStart = 2,                    windowLength = 2,      lastSeen = [ {p: 0}, {w: 2}, {k:3} ],             longest = 2,     longestStr = "pw"
    windowEnd = 4,     windowStart = 2,                    windowLength = 3,      lastSeen = [ {p: 0}, {w: 2}, {k:3}, {e:4} ],      longest = 3,     longestStr = "wke"
    windowEnd = 5,     windowStart = max(2, 2+1) = 3,      windowLength = 3,      lastSeen = [ {p: 0}, {w: 2}, {k:3}, {e:4} ],      longest = 3,     longestStr = "wke"
    */


    /*
--Explain logic in 2-4 setences.
Aim is to maintain a window of characters which are always unique.
And aims to count or maintain the longest window of unique characters that we have seen so far.
We maintain last seen index for each character in a dictionary.


--Write if blocks and formulas.
if character already seen:
   windowStart = max(windowStart, last seen index + 1)   --> max is used to avoid making the window start pointer backwards. To avoid already processed chacreters and duplicates.

window length = window end - window start + 1
longest = max(window length, longest)

--Write tracing block. Line per each iteration.
0 1 2 3 4 5 6 
t m m z u x t

windowEnd = 0       windowStart = 0                     windowLength = 1,       seen = [ {t:0} ],                            longest = 1,     longestStr = "t"
windowEnd = 1       windowStart = 0                     windowLength = 2,       seen = [ {t:0, m:1} ],                       longest = 2,     longestStr = "tm"
windowEnd = 2       windowStart = max(0, 1+1) = 2       windowLength = 1,       seen = [ {t:0, m:2} ],                       longest = 2,     longestStr = "tm"
windowEnd = 3       windowStart = 2                     windowLength = 2,       seen = [ {t:0, m:2, z:3} ],                  longest = 2,     longestStr = "tm"
windowEnd = 4       windowStart = 2                     windowLength = 3,       seen = [ {t:0, m:2, z:3, u:4} ],             longest = 3,     longestStr = "mzu"
windowEnd = 5       windowStart = 2                     windowLength = 4,       seen = [ {t:0, m:2, z:3, u:4, x:5} ],        longest = 4,     longestStr = "mzux"
windowEnd = 6       windowStart = max(2, 0+1) = 2       windowLength = 5,       seen = [ {t:6, m:2, z:3, u:4, x:5} ],        longest = 5,     longestStr = "mzuxt"
*/


    public int Solve4(string s)
    {
        var windowStart = 0;
        var windowEnd = 0;
        var seen = new Dictionary<char, int>();
        var longest = 0;


        while (windowEnd < s.Length)
        {
            var current = s[windowEnd];
            if (seen.TryGetValue(current, out var lastSeenIndex))
            {
                windowStart = Math.Max(windowStart, lastSeenIndex + 1);
            }

            seen[current] = windowEnd;
            var windowSize = windowEnd - windowStart + 1;
            longest = Math.Max(longest, windowSize);

            ++windowEnd;
        }

        return longest;
    }


    public (int Length, string UniqueStr) Solve5(string s)
    {
        var windowStart = 0;
        var windowEnd = 0;
        var seen = new Dictionary<char, int>();
        var longest = 0;
        var longestStr = "";


        while (windowEnd < s.Length)
        {
            var current = s[windowEnd];
            if (seen.TryGetValue(current, out var lastSeenIndex))
            {
                windowStart = Math.Max(windowStart, lastSeenIndex + 1);
            }

            seen[current] = windowEnd;
            var windowSize = windowEnd - windowStart + 1;

            if (windowSize > longest)
            {
                longest = windowSize;
                longestStr = s.Substring(windowStart, windowSize);
            }
            ++windowEnd;
        }

        return (longest, longestStr);
    }



    /*
--Explain logic in 2-4 setences.
Sliding window is about maitaining a window which always contains unique characters.
We always try to expand that window by adding more characters to the right side of the window.
If we meet a new character where the window already contains, we shrink the sliding window from left until we remove that character from the left.
We maintain a dictionary which contains character keys and last seen index of that character as value.
While we iterate to from left to right we keep track of longest unique string/its length.

--Write if blocks and formulas.
if character previously seen:
    windowStart = max(windowStart, last seen index + 1)     --> here max used to avoid moving windowStart pointer back, as back may contain already seen repeats, and this will make window non-unique.
windowLength = windowEnd - windowStart + 1;
longest = max(longest, windowLength)

--Write tracing block. Line per each iteration.
0 1 2 3 4 5 6 
t m m z u x t

loop variable is - windowEnd

windowEnd = 0       windowStart = 0                         seen = [t: 0,                       ]           windowLength = 1        longest = 1           longestStr = t
windowEnd = 1       windowStart = 0                         seen = [t: 0, m: 1                  ]           windowLength = 2        longest = 2           longestStr = tm
windowEnd = 2       windowStart = max(0, 1 + 1) = 2         seen = [t: 0, m: 2                  ]           windowLength = 1        longest = 2           longestStr = tm
windowEnd = 3       windowStart = 2                         seen = [t: 0, m: 2, z:3             ]           windowLength = 2        longest = 2           longestStr = tm
windowEnd = 4       windowStart = 2                         seen = [t: 0, m: 2, z:3, u:4        ]           windowLength = 3        longest = 3           longestStr = mzu
windowEnd = 5       windowStart = 2                         seen = [t: 0, m: 2, z:3, u:4, x:5   ]           windowLength = 4        longest = 4           longestStr = mzux
windowEnd = 6       windowStart = max(2, 0 + 1) = 2         seen = [t: 6, m: 2, z:3, u:4, x:5   ]           windowLength = 5        longest = 5           longestStr = mzuxt
*/
    public (int Length, string LogestStr) Solve6(string s)
    {
        var windowStart = 0;
        var windowEnd = 0;
        var longest = 0;
        var longestStr = "";
        var seen = new Dictionary<char, int>();

        while (windowEnd < s.Length)
        {
            var curr = s[windowEnd];
            if (seen.TryGetValue(curr, out var lastSeenIndex))
            {
                windowStart = Math.Max(windowStart, lastSeenIndex + 1);
            }

            seen[curr] = windowEnd;
            var windowLength = windowEnd - windowStart + 1;
            //longest = Math.Max(longest, windowLength);

            if (windowLength > longest)
            {
                longest = windowLength;
                longestStr = s.Substring(windowStart, windowLength);
            }

            ++windowEnd;
        }
        return (longest, longestStr);
    }


    /*
--Explain logic in 2-4 setences.
sliding window always maintains a unique set of chars. window is maintained by windowStart and windowEnd pointers.
we maintain max length window and its length.
we maintain a dictionary whith char and its last seen index.
we iterate thorugh the string and when we see a new not seen char, we expand the window by moving windowEnd pointer to 1 char right.
when we see a char which we have already seen we shrink sliding window by moving windowStart pointer like,
windowStart = max(windowStart + last seen index + 1) --> we use max to make sure that window start never moves backwards, which may add already seen char repeats.

--Write if blocks and formulas.
if (curr char already seen)
    windowStart = max(windowStart, last seen index + 1)
windowSize = windowEnd - windowStart + 1
longest = max(longest, windowSize)

--Write tracing block. Line per each iteration.
0 1 2 3 4 5 6 
t m m z u x t

loop variable is - windowEnd

windowEnd = 0       windowStart = 0                     seen = [ t:0                       ]       windowLength = 1             slidingWindow = t               longest = 1     longestStr = t
windowEnd = 1       windowStart = 0                     seen = [ t:0, m:1                  ]       windowLength = 2             slidingWindow = tm              longest = 2     longestStr = tm
windowEnd = 2       windowStart = max(0,1+1) = 2        seen = [ t:0, m:2                  ]       windowLength = 1             slidingWindow = m               longest = 2     longestStr = tm
windowEnd = 3       windowStart = 2                     seen = [ t:0, m:2, z:3             ]       windowLength = 2             slidingWindow = mz              longest = 2     longestStr = tm
windowEnd = 4       windowStart = 2                     seen = [ t:0, m:2, z:3, u:4        ]       windowLength = 3             slidingWindow = mzu             longest = 3     longestStr = mzu
windowEnd = 5       windowStart = 2                     seen = [ t:0, m:2, z:3, u:4, x:5   ]       windowLength = 4             slidingWindow = mzux            longest = 4     longestStr = mzux
windowEnd = 6       windowStart = max(2,0+1) = 2        seen = [ t:6, m:2, z:3, u:4, x:5   ]       windowLength = 5             slidingWindow = mzuxt           longest = 5     longestStr = mzuxt

3:10 PM - 3:27 PM
*/
    // code start - 3:28 PM - 3:36 PM
    public (int Length, string LogestStr) Solve7(string s)
    {
        var windowStart = 0;
        var windowEnd = 0;
        var seen = new Dictionary<char, int>();     // char --> last seen index
        var longest = 0;
        var longestStr = "";

        while (windowEnd < s.Length)
        {
            var curr = s[windowEnd];

            if (seen.TryGetValue(curr, out var lastSeenIndex))
            {
                // shrink
                windowStart = Math.Max(windowStart, lastSeenIndex + 1);
            }
            seen[curr] = windowEnd;
            var windowLength = windowEnd - windowStart + 1;
            if (windowLength > longest)
            {
                longest = windowLength;
                longestStr = s.Substring(windowStart, windowLength);
            }

            ++windowEnd;
        }

        return (longest, longestStr);

    }

}
