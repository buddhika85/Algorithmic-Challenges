
using AlgorithmsPractice.BinarySearch;
using AlgorithmsPractice.HashMap;
using AlgorithmsPractice.SlidingWindow;
using AlgorithmsPractice.SortingGreedy;
using AlgorithmsPractice.StackProblems;
using AlgorithmsPractice.TwoPointers;

Console.WriteLine("Hello, Algorithms!\n");

//new ReverseString();
//new LongestSubstring();
//new FirstUniqueCharacter();
//new MinimumMeetingRooms();
//new BinarySearchBasic();
new ValidParentheses();



/* AI Prompt -

can you pls give me class stub for this expected time to solve it

the problem description block (like LongestSubstring)

a mini test harness for this one with in constructor with expected output
 like

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

   
        // Problem: Longest Substring Without Repeating Characters

        // Description:
        //     Given a string s, find the length of the longest substring 
        //     without repeating characters.

        // Examples:
        //     Input:  "abcabcbb"
        //     Output: 3   ("abc")

        //     Input:  "bbbbb"
        //     Output: 1   ("b")

        //     Input:  "pwwkew"
        //     Output: 3   ("wke")

        // Approach:
        //     - Use a sliding window with two pointers (left and right)
        //     - Expand right pointer while tracking characters in a HashSet/Map
        //     - If a duplicate is found, shrink from the left
        //     - Track the maximum window size
   

    public int Solve(string? s)
    {
        // TODO: implement
        return 0;
    }
}

*/