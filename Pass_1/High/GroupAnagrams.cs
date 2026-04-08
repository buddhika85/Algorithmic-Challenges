using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class GroupAnagrams
{
    //private readonly bool _debug = false; // Set to true to print internal states

    public GroupAnagrams()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Group Anagrams
     * ------------------------------------------------------------
     *  Given an array of strings strs, group the anagrams together.
     *
     *  An anagram is a word formed by rearranging the letters of
     *  another word, using all the original letters exactly once.
     *
     *  Return the result as a list of groups, where each group
     *  contains strings that are anagrams of each other.
     *
     *  Example:
     *      Input:
     *          ["eat", "tea", "tan", "ate", "nat", "bat"]
     *
     *      Output:
     *          [
     *              ["eat", "tea", "ate"],
     *              ["tan", "nat"],
     *              ["bat"]
     *          ]
     *
     *      Explanation:
     *          "eat", "tea", "ate" are anagrams.
     *          "tan", "nat" are anagrams.
     *          "bat" stands alone.
     *
     * ------------------------------------------------------------
     *  Expected Time: 10–15 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string[] input, List<List<string>> expected)[]
        {
            (
                1,
                "Example case",
                new[] { "eat", "tea", "tan", "ate", "nat", "bat" },
                new List<List<string>>
                {
                    new() { "eat", "tea", "ate" },
                    new() { "tan", "nat" },
                    new() { "bat" }
                }
            ),
            (
                2,
                "Single word",
                new[] { "abc" },
                new List<List<string>> { new() { "abc" } }
            ),
            (
                3,
                "Empty input",
                Array.Empty<string>(),
                new List<List<string>>() // empty
            ),
            (
                4,
                "All identical",
                new[] { "aa", "aa", "aa" },
                new List<List<string>> { new() { "aa", "aa", "aa" } }
            )
        };

        foreach (var (num, desc, input, expected) in testHarness)
        {
            Test(num, desc, input, expected);
        }
    }

    private void Test(int num, string desc, string[] input, List<List<string>> expected)
    {
        var sw = Stopwatch.StartNew();
        var actual = Solve2(input);
        sw.Stop();

        bool pass = Compare(actual, expected);

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc}");
            Console.WriteLine($"Expected: {Format(expected)}");
            Console.WriteLine($"Actual:   {Format(actual)}");
        }
    }

    private string Format(List<List<string>> groups)
    {
        return "[" + string.Join(", ",
            groups.Select(g => "[" + string.Join(",", g) + "]")) + "]";
    }

    private bool Compare(List<List<string>> a, List<List<string>> b)
    {
        if (a.Count != b.Count) return false;

        // Normalize groups by sorting internally + externally
        var normA = a.Select(g => g.OrderBy(x => x).ToList()).OrderBy(g => g.First()).ToList();
        var normB = b.Select(g => g.OrderBy(x => x).ToList()).OrderBy(g => g.First()).ToList();

        for (int i = 0; i < normA.Count; i++)
        {
            if (!normA[i].SequenceEqual(normB[i]))
                return false;
        }

        return true;
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    //
    // Best Case Time Complexity: O(n) --> because we have to anyway get all words, build siganture and group them by siganture in a loop which runs N times
    // Average Case Time Complexity: O(n) --> same
    // Worst Case Time Complexity: O(n) --> same
    // Space Complexity: O(n) --> we need additional Dictionary of k Group count contaning N words
    // ------------------------------------------------------------
    private List<List<string>> Solve(string[] strs)
    {
        if (strs.Length == 0)
            return [];

        /*
        signature --> words  =   
            [
                {"0#1#1" : {'bc', 'cb'}}
                {"1#1#1" : {'abc', 'cba'}}
            ]
        */
        var signaturesWithWords = new Dictionary<string, List<string>>();

        foreach (var word in strs)
        {
            var signature = new int[26];        // [0, 0, ... , 0]      --> 26 zeros representing alphabet
            foreach (var character in word)
            {
                signature[character - 'a']++;
            }
            var sigantureStr = string.Join('#', signature);        // aabc --> 2#1#1#....0000
            if (signaturesWithWords.TryGetValue(sigantureStr, out var words) && words is not null)
                words.Add(word);
            else
                signaturesWithWords[sigantureStr] = [word];
        }

        return [.. signaturesWithWords.Select(x => x.Value)];
    }

    // Optional alternative approach (sorting-based key)
    private List<List<string>> Solve2(string[] words)
    {
        if (words.Length == 0)
            return [];

        var signatures = new Dictionary<string, List<string>>();        // singature --> list of words

        for (int i = 0; i < words.Length; i++)
        {
            var word = words[i];
            var freqs = new Dictionary<char, int>();
            /*
                [
                    'a': 1, 'b': 2
                ]
            */
            foreach (var c in word)
            {
                if (freqs.TryGetValue(c, out var count))
                    freqs[c] = ++count;
                else
                    freqs[c] = 1;
            }

            /*
                abb => a:1#b:2
            */
            var signature = string.Join('#', freqs.OrderBy(x => x.Key).Select(x => $"{x.Key}:{x.Value}"));

            /*
                [
                    a1#b2 => { abb, bba },
                    a1#b1 => { ab, ba }
                ]
            */
            if (signatures.TryGetValue(signature, out var wordsWithSameSig))
                wordsWithSameSig.Add(word);
            else
                signatures[signature] = [word];
        }


        return signatures.Values.ToList();
    }
}
