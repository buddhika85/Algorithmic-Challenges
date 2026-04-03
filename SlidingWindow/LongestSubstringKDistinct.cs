namespace AlgorithmsPractice.SlidingWindow;

public class LongestSubstringKDistinct
{
    public LongestSubstringKDistinct()
    {
        var testData = new (string? s, int k)[]
        {
            ("", 2),             // expected: 0
            (null, 2),           // expected: 0
            ("a", 1),            // expected: 1 ("a")
            ("eceba", 2),        // expected: 3 ("ece")
            ("aa", 1),           // expected: 2 ("aa")
            ("abcadcacacaca", 3) // expected: 11 ("cadcacacaca")
        };

        foreach (var (s, k) in testData)
        {
            Console.WriteLine($"Input: {s}, k={k} => {Solve(s, k)}");
        }
    }

    /*
        Problem: Longest Substring With At Most K Distinct Characters

        Description:
            Given a string s and an integer k, return the length of the 
            longest substring that contains at most k distinct characters.

        Examples:
            Input:  s = "eceba", k = 2
            Output: 3   ("ece")

            Input:  s = "aa", k = 1
            Output: 2   ("aa")

        Approach:
            - Use a sliding window with two pointers (left and right)
            - Use a Dictionary<char, int> to track character frequencies
            - Expand the right pointer and add characters to the map
            - If the number of distinct characters exceeds k,
              shrink the window from the left until valid again
            - Track the maximum window size
    */

    public int Solve(string? s, int k)
    {
        return 0;
    }
}
