
namespace AlgorithmsPractice.HashMapCounting;

public class ValidAnagram
{
    public ValidAnagram()
    {
        var testData = new (int testCaseNum, string s, string t, bool expected)[]
        {
            (1, "anagram", "nagaram", true),
            (2, "rat", "car", false),
            (3, "a", "a", true),
            (4, "ab", "ba", true),
            (5, "hello", "bello", false),
        };

        foreach (var (testCaseNum, s, t, expected) in testData)
        {
            var result = Solve(s, t);
            var status = result == expected ? "Passed" : "FAILED";
            Console.WriteLine($"{testCaseNum}. {status}\t=> result={result}, expected={expected}");
        }
    }

    /*
        Problem: Valid Anagram

        Description:
            Given two strings s and t, return true if t is an anagram of s,
            and false otherwise.

        Approach:
            - If lengths differ → false.
            - Use a frequency map (Dictionary<char,int>).
            - Count characters in s.
            - Subtract counts using characters in t.
            - If any count goes negative → false.

        Time Complexity:
            O(n) — single pass through both strings.
    */

    public bool Solve(string s, string t)
    {
        return false;
    }
}
