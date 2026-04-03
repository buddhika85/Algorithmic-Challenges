namespace AlgorithmsPractice.HashMap
{
    public class FirstUniqueCharacter
    {
        public FirstUniqueCharacter()
        {
            var testData = new (string? input, int expected)[]
            {
            ("leetcode", 0),     // 'l' is the first unique
            ("loveleetcode", 2), // 'v' is the first unique
            ("aabb", -1),        // no unique characters
            ("", -1),            // empty
            (null, -1),          // null
            ("x", 0),            // single char
            };

            foreach (var (input, expected) in testData)
            {
                Console.WriteLine($"{input} => {Solve(input)} (expected: {expected})");
            }
        }

        /*
            Problem: First Unique Character in a String

            Description:
                Given a string s, return the index of the first non-repeating 
                character. If it does not exist, return -1.

            Examples:
                Input:  "leetcode"
                Output: 0   ('l')

                Input:  "loveleetcode"
                Output: 2   ('v')

                Input:  "aabb"
                Output: -1  (no unique characters)

            Approach:
                - Use a HashMap (Dictionary<char, int>) to count frequencies
                - First pass: count each character
                - Second pass: find the first character with count == 1
                - Time: O(n)
                - Space: O(1) because alphabet is fixed (26 lowercase letters)
        */
        public int Solve(string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return -1;

            var charCounts = new Dictionary<char, int>();
            foreach (var item in s)
            {
                charCounts[item] = charCounts.GetValueOrDefault(item) + 1;
            }

            for (var i = 0; i < s.Length; i++)
            {
                if (charCounts[s[i]] == 1)
                    return i;
            }

            return -1;
        }



        public int Solve2(string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return -1;

            var charIndexes = new Dictionary<char, List<int>>();

            for (var i = 0; i < s.Length; i++)
            {
                var current = s[i];
                if (charIndexes.ContainsKey(current))
                {
                    charIndexes[current].Add(i);
                }
                else
                {
                    charIndexes[current] = [i];
                }
            }

            var nonRepeatingChars = charIndexes.Where(x => x.Value.Count == 1);
            if (!nonRepeatingChars.Any())
                return -1;
            return nonRepeatingChars.Select(x => x.Value.Single()).Min();
        }
    }
}