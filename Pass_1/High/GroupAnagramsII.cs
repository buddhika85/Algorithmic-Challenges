using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class GroupAnagramsII
{
    private readonly bool _debug = false; // Set to true to print internal states

    public GroupAnagramsII()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Group Anagrams
     * ------------------------------------------------------------
     *  Given an array of strings strs, group the anagrams together.
     *  You can return the answer in any order.
     *
     *  An Anagram is a word or phrase formed by rearranging the
     *  letters of a different word or phrase, typically using all
     *  the original letters exactly once.
     *
     *  Example:
     *      Input:
     *          strs = ["eat","tea","tan","ate","nat","bat"]
     *
     *      Output:
     *          [
     *              ["bat"],
     *              ["nat","tan"],
     *              ["ate","eat","tea"]
     *          ]
     *
     * ------------------------------------------------------------
     *  Expected Time: 10–15 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string[] strs, string[][] expected)[]
        {
            (1, "Example case",
                new[] { "eat", "tea", "tan", "ate", "nat", "bat" },
                new[]
                {
                    new[] { "bat" },
                    new[] { "nat", "tan" },
                    new[] { "ate", "eat", "tea" }
                }),

            (2, "Single word",
                new[] { "abc" },
                new[]
                {
                    new[] { "abc" }
                }),

            (3, "Empty input",
                Array.Empty<string>(),
                Array.Empty<string[]>()),

            (4, "Duplicates",
                new[] { "a", "a", "a" },
                new[]
                {
                    new[] { "a", "a", "a" }
                })
        };

        foreach (var (num, desc, strs, expected) in testHarness)
        {
            Test(num, desc, strs, expected);
        }
    }

    private void Test(int num, string desc, string[] strs, string[][] expected)
    {
        var sw = Stopwatch.StartNew();
        var actual = Solve(strs);
        sw.Stop();

        bool pass = AreEqual(actual, expected);

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc}");
        }
    }

    private bool AreEqual(string[][] a, string[][] b)
    {
        if (a.Length != b.Length)
            return false;

        // Convert each group to sorted strings for comparison
        var normA = a.Select(g => g.OrderBy(x => x).ToArray()).OrderBy(g => g[0]).ToArray();
        var normB = b.Select(g => g.OrderBy(x => x).ToArray()).OrderBy(g => g[0]).ToArray();

        for (int i = 0; i < normA.Length; i++)
        {
            if (!normA[i].SequenceEqual(normB[i]))
                return false;
        }

        return true;
    }
    /*
     *  Given an array of strings strs, group the anagrams together.
         *  You can return the answer in any order.
         *
         *  An Anagram is a word or phrase formed by rearranging the
         *  letters of a different word or phrase, typically using all
         *  the original letters exactly once.
         *
         *  Example:
         *      Input:
         *          strs = ["eat","tea","tan","ate","nat","bat"]
         *
         *      Output:
         *          [
         *              ["bat"],
         *              ["nat","tan"],
         *              ["ate","eat","tea"]
         *          ]
    */
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // bdee --> singature of this 0#1#0#1#2#0#...0
    // we create a dictionary wich stores - siganture and words
    // to do this we will use 1 out loop and 1 inner loop
    // inner loop builds siganture and puts the word to dictionary
    // return word groups from diactionary
    // Best case Time Complexity: O(n.k) --> n being number of words and k being largets words number of chars
    // Average case Time Complexity: O(n.k) 
    // Worst case Time Complexity: O(n.k)
    // Space Complexity: O(n) --> we need additional n number of arrays with 26 slots
    // 10:30 AM - 10:51 AM
    // ------------------------------------------------------------
    private string[][] Solve(string[] strs)
    {
        var signatures = new Dictionary<string, List<string>>();

        foreach (var word in strs)
        {
            // building signature
            var signatureArray = new int[26];      // 0the index a, 1st is b..., 25th is z
            foreach (var character in word)
            {
                var indexOfChar = character - 'a';
                ++signatureArray[indexOfChar];
            }
            var signature = string.Join('#', signatureArray);

            // add to dictionary
            if (signatures.TryGetValue(signature, out var listOfWords))
            {
                listOfWords.Add(word);
            }
            else
            {
                signatures[signature] = new List<string> { word };
            }
        }

        //return signatures.Select(x => x.Value.ToArray()).ToArray();
        var list = new List<string[]>();
        foreach (var signature in signatures)
        {
            list.Add(signature.Value.ToArray());
        }
        return list.ToArray();
    }

    // Optional alternative approach (sorting each word)
    private string[][] Solve2(string[] strs)
    {
        // TODO: implement alternative solution
        return Array.Empty<string[]>();
    }
}
