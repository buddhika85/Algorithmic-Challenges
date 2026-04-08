namespace AlgorithmsPractice.HashMap;

public class GroupAnagrams
{
    public GroupAnagrams()
    {
        var testData = new (int testCaseNum, string[] words, List<List<string>> expected)[]
        {
            (
                1,
                new string[] { "eat", "tea", "tan", "ate", "nat", "bat" },
                new List<List<string>>
                {
                    new() { "eat", "tea", "ate" },
                    new() { "tan", "nat" },
                    new() { "bat" }
                }
            ),
            (
                2,
                new string[] { "" },
                new List<List<string>> { new() { "" } }
            ),
            (
                3,
                new string[] { "a" },
                new List<List<string>> { new() { "a" } }
            )
        };

        foreach (var (testCaseNum, words, expected) in testData)
        {
            var result = Solve2(words);
            var testCaseResult = AreEqual(result, expected) ? "Passed" : "FAILED";
            Console.WriteLine($"{testCaseNum}. {testCaseResult}");
        }
    }

    bool AreEqual(List<List<string>> result, List<List<string>> expected)
    {
        // Step 1: sort inner lists
        var sortedResult = result
            .Select(group => group.OrderBy(x => x).ToList())
            .OrderBy(group => string.Join(",", group))
            .ToList();

        var sortedExpected = expected
            .Select(group => group.OrderBy(x => x).ToList())
            .OrderBy(group => string.Join(",", group))
            .ToList();

        // Step 2: compare outer list sizes
        if (sortedResult.Count != sortedExpected.Count)
            return false;

        // Step 3: compare each group
        for (int i = 0; i < sortedResult.Count; i++)
        {
            if (!sortedResult[i].SequenceEqual(sortedExpected[i]))
                return false;
        }

        return true;
    }


    /*
        Problem: Group Anagrams

        Description:
            Given an array of strings strs, group the anagrams together.

        Approach:
            - For each string, build a frequency signature (26-length array).
            - Convert signature to a key (e.g., "1#0#0#2#...").
            - Use Dictionary<string, List<string>> to group by signature.

        Time Complexity:
            O(n * k) — n strings, k = max string length.
    */

    public List<List<string>> Solve(string[] words)
    {
        var anagramGroups = new Dictionary<string, List<string>>();     // Key: Signature Value: List of words which has same signature
        // assuming inputs are always lower case letters
        var letterIndexes = new Dictionary<char, int>
            {
                { 'a', 0 }, { 'b', 1 }, { 'c', 2 }, { 'd', 3 }, { 'e', 4 },
                { 'f', 5 }, { 'g', 6 }, { 'h', 7 }, { 'i', 8 }, { 'j', 9 },
                { 'k', 10 }, { 'l', 11 }, { 'm', 12 }, { 'n', 13 }, { 'o', 14 },
                { 'p', 15 }, { 'q', 16 }, { 'r', 17 }, { 's', 18 }, { 't', 19 },
                { 'u', 20 }, { 'v', 21 }, { 'w', 22 }, { 'x', 23 }, { 'y', 24 },
                { 'z', 25 }
            };
        foreach (var word in words)
        {
            var letterCounts = new int[26];
            // counting characters
            foreach (var character in word)
            {
                var indexToIncrement = letterIndexes[character];
                ++letterCounts[indexToIncrement];
            }

            // build siganature
            var signature = string.Join('#', letterCounts);
            //Console.WriteLine($"{word}\t:{signature}");

            // adding to dictionary
            if (anagramGroups.TryGetValue(signature, out List<string>? wordsGroup))
            {
                wordsGroup.Add(word);
            }
            else
            {
                anagramGroups[signature] = [word];
            }
        }

        // getting output in the format test cases accept
        return anagramGroups.Select(x => x.Value).ToList();
    }


    public List<List<string>> Solve2(string[] words)
    {
        var anagramGroups = new Dictionary<string, List<string>>();     // Key: Signature Value: List of words which has same signature

        foreach (var word in words)
        {
            var letterCounts = new int[26];
            // counting characters
            foreach (var character in word)
            {
                // each char is represented by a int and a to z is continous chars, so (character - a) gives index
                // a - a = 0, b - a = 1, ...., z - a = 25
                var indexToIncrement = character - 'a';
                ++letterCounts[indexToIncrement];
            }

            // build siganature
            var signature = string.Join('#', letterCounts);
            //Console.WriteLine($"{word}\t:{signature}");

            // adding to dictionary
            if (anagramGroups.TryGetValue(signature, out List<string>? wordsGroup))
            {
                wordsGroup.Add(word);
            }
            else
            {
                anagramGroups[signature] = [word];
            }
        }

        // getting output in the format test cases accept
        return anagramGroups.Select(x => x.Value).ToList();
    }
}
