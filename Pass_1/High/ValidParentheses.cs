using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlgorithmsPractice.Pass_1.High;

public class ValidParentheses
{
    private readonly bool _debug = false; // Set to true to print internal states

    public ValidParentheses()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Valid Parentheses
     * ------------------------------------------------------------
     *  Given a string s containing just the characters '(', ')',
     *  '{', '}', '[' and ']', determine if the input string is valid.
     *
     *  An input string is valid if:
     *      - Open brackets must be closed by the same type of brackets.
     *      - Open brackets must be closed in the correct order.
     *      - Every closing bracket must have a corresponding opening bracket.
     *
     *  Example:
     *      Input:
     *          s = "()[]{}"
     *
     *      Output:
     *          true
     *
     *      Explanation:
     *          All bracket pairs are correctly matched.
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string s, bool expected)[]
        {
            (1, "Simple valid", "()", true),
            (2, "Mixed valid", "()[]{}", true),
            (3, "Nested valid", "{[()]}", true),
            (4, "Invalid order", "(]", false),
            (5, "Mismatched types", "([)]", false),
            (6, "Starts with closing", "}", false),
            (7, "Empty string", "", true),
            (8, "Long valid", "((({{{[[[]]]}}})))", true),
            (9, "Long invalid", "((({{{[[[]]]}}}))", false)
        };

        foreach (var (num, desc, s, expected) in testHarness)
        {
            Test(num, desc, s, expected);
        }
    }

    private void Test(int num, string desc, string s, bool expected)
    {
        var sw = Stopwatch.StartNew();
        bool actual = Solve(s);
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

    // ------------------------------------------------------------
    // Solve method: implement logic here
    // {[()]} --> true
    // stack --> LIFO
    // for each brace
    //      if starting brace add to stack
    //      if one of the end braces remove 1 on the top/added last, then, check compatibility => return false if incompabible
    // at the end of the loop if stack is empty --> return rue, else false
    // Best case Time Complexity: just 2 chars, so always 2 iterations - O(1)
    // Average case Time Complexity: O(n) --> computations grow with number of chars in input string
    // Worst case Time Complexity: O(n) --> computations grow with number of chars in input string
    // Space Complexity: O(n) --> we use an additional stack with n/2 max size - n being num of chars in input string.
    // 2:49 PM - 2:58 PM
    // Testing and bug fixing - 2:59 PM - 3:02 PM
    // ------------------------------------------------------------
    private bool Solve(string s)
    {
        var startBrackets = new Stack<char>();
        foreach (var item in s)
        {
            switch (item)
            {
                case '(':
                case '{':
                case '[':
                    startBrackets.Push(item);
                    break;
                case ')':
                    if (startBrackets.Count > 0 && startBrackets.Pop() != '(')
                        return false;
                    break;
                case '}':
                    if (startBrackets.Count > 0 && startBrackets.Pop() != '{')
                        return false;
                    break;
                case ']':
                    if (startBrackets.Count > 0 && startBrackets.Pop() != '[')
                        return false;
                    break;
                default:
                    return false;
            }
        }
        return startBrackets.Count == 0;
    }

    // Optional brute-force approach
    private bool Solve2(string s)
    {
        // TODO: implement alternative solution
        return false;
    }
}
