
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
            Building frequency map → O(n)

            Subtracting counts → O(n)

            Dictionary ops → O(1) average

            Total: O(n)  
            Space: O(1) if alphabet is fixed (ASCII), otherwise O(n).

        All letters match in count and frequency, just in a different order.
    */

    public bool Solve(string s, string t)
    {
        if (s.Length != t.Length)
            return false;

        var frequencyDict = new Dictionary<char, int>();

        // build frequencyDict
        foreach (var item in s)
        {
            frequencyDict[item] = frequencyDict.GetValueOrDefault(item, 0) + 1;
        }

        //removing from frequencyDict
        foreach (var item in t)
        {
            if (!frequencyDict.TryGetValue(item, out int count))
                return false;           // not found

            // if found then reduce count by 1 or remove if new count is 0
            if (--count == 0)
                frequencyDict.Remove(item);
            else
                frequencyDict[item] = count;
        }

        return frequencyDict.Count == 0;
    }
}
