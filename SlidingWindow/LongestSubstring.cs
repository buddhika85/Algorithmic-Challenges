namespace AlgorithmsPractice.SlidingWindow;

public class LongestSubstring
{
    public LongestSubstring()
    {
        var testData = new string?[]
        {
            "",          // expected: 0
            null,        // expected: 0
            "a",         // expected: 1
            "abcabcbb",  // expected: 3 ("abc")
            "bbbbb",     // expected: 1 ("b")
            "pwwkew"     // expected: 3 ("wke")
        };

        foreach (var item in testData)
        {
            Console.WriteLine($"{item} => {Solve(item)}");
        }
    }

    /*
        Problem: Longest Substring Without Repeating Characters

        Description:
            Given a string s, find the length of the longest substring 
            without repeating characters.

        Examples:
            Input:  "abcabcbb"
            Output: 3   ("abc")

            Input:  "bbbbb"
            Output: 1   ("b")

            Input:  "pwwkew"
            Output: 3   ("wke")

        Approach:
            - Use a sliding window with two pointers (left and right)
            - Expand right pointer while tracking characters in a HashSet/Map
            - If a duplicate is found, shrink from the left
            - Track the maximum window size
    */
    public int Solve(string? s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return 0;

        var window = new HashSet<char>();
        var left = 0;
        var right = 0;
        var maxLength = 0;

        for (; right < s.Length; right++)
        {
            var current = s[right];
            while(window.Contains(current))
            {
                window.Remove(s[left++]);
            }
            window.Add(current);

            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }
}

