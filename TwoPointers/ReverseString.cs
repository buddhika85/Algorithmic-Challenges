namespace AlgorithmsPractice.TwoPointers
{
    public class ReverseString
    {

        public ReverseString()
        {
            var testData = new string? [] { "", null, "a", "hello", "a1Ba2"};
            foreach (var item in testData)
            {
                Console.WriteLine($"{item} rerversed => {Solve(item)}");
            }
        }

        /*
        Problem: Reverse a string using the two‑pointer technique.

        Example:
            Input:  "hello"
            Output: "olleh"

        Approach:
            - Use two pointers: left at start, right at end
            - Swap characters while left < right
            - Move pointers inward
        */


        // Time Complexity: O(n)
        //   We perform a single pass from both ends toward the center.
        // Space Complexity: O(n)
        //   Strings are immutable, so converting to a char[] requires additional linear space.
        public string Solve(string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return string.Empty;
            
            var charArray = s.ToCharArray();
            var left = 0;
            var right = charArray.Length - 1;
            while (left < right)
            {
                if (charArray[left] != charArray[right])
                {
                    var temp = charArray[left];
                    charArray[left++] = charArray[right];
                    charArray[right--] = temp;
                }
            }
            return string.Join(string.Empty, charArray);
        }

        
    }
}