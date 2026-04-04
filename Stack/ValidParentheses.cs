namespace AlgorithmsPractice.StackProblems;

public class ValidParentheses
{
    public ValidParentheses()
    {
        var testData = new (int testCaseNum, string s, bool expected)[]
        {
            (1, "", true),
            (2, "()", true),
            (3, "()[]{}", true),
            (4, "(]", false),
            (5, "([)]", false),
            (6, "{[]}", true),
            (7, "(((((", false),
            (8, "(()())", true)
        };

        foreach (var (testCaseNum, s, expected) in testData)
        {
            var result = Solve(s);
            var testCaseStatus = expected == result ? "Passed" : "FAILED";
            Console.WriteLine($"{testCaseNum}. {testCaseStatus}\t => {s} => {result} (expected: {expected})");
        }
    }

    /*
        Problem: Valid Parentheses

        Description:
            Given a string containing only the characters '(', ')', '{', '}', '[' and ']',
            determine if the input string is valid.

            A string is valid if:
                - Open brackets are closed by the same type of brackets.
                - Brackets close in the correct order.

        Examples:
            Input:  "()"
            Output: true

            Input:  "()[]{}"
            Output: true

            Input:  "(]"
            Output: false

            Input:  "([)]"
            Output: false

        Approach:
            - Use a stack to push opening brackets.
            - When encountering a closing bracket, check if it matches the top of the stack.
            - If mismatch or stack is empty → invalid.
            - At the end, stack must be empty.

        Expected Time Complexity:
            O(n)
            9:10 - 9:50
    */

    public bool Solve(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return true;

        var stack = new Stack<char>();

        var isValid = true;
        foreach (var currChar in s)
        {
            switch (currChar)
            {
                case '(':
                case '{':
                case '[':
                    stack.Push(currChar);
                    break;
                case ')':
                    if (stack.TryPop(out char prevChar) && prevChar != '(')
                        isValid = false;
                    break;
                case '}':
                    if (stack.TryPop(out prevChar) && prevChar != '{')
                        isValid = false;
                    break;
                case ']':
                    if (stack.TryPop(out prevChar) && prevChar != '[')
                        isValid = false;
                    break;
                default:
                    break;
            }

            if (!isValid)
                return false;
        }

        return stack.Count == 0;
    }
}
