using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class WordFrequency
{
    private readonly bool _debug = false; // Set to true to print internal states

    public WordFrequency()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Word Frequency / Character Count
     * ------------------------------------------------------------
     *  Given a string input, return a dictionary (or array of pairs)
     *  representing the frequency of each word OR each character.
     *
     *  You may assume:
     *  - Words are separated by spaces.
     *  - Characters include letters, digits, and punctuation.
     *  - Case sensitivity depends on the problem variant.
     *
     *  Example (Word Frequency):
     *      Input:
     *          "the cat and the hat"
     *
     *      Output:
     *          { "the": 2, "cat": 1, "and": 1, "hat": 1 }
     *
     *  Example (Character Count):
     *      Input:
     *          "hello"
     *
     *      Output:
     *          { 'h': 1, 'e': 1, 'l': 2, 'o': 1 }
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, string input, Dictionary<string, int> expected)[]
        {
            (1, "Word frequency basic",
                "the cat and the hat",
                new Dictionary<string,int>
                {
                    { "the", 2 }, { "cat", 1 }, { "and", 1 }, { "hat", 1 }
                }),

            (2, "Single word",
                "hello",
                new Dictionary<string,int>
                {
                    { "h", 1 }, { "e", 1 }, { "l", 2 },  { "o", 1 }
                }),

            (3, "Empty string",
                "",
                new Dictionary<string,int>()),

            (4, "Character count mode",
                "aabbc",
                new Dictionary<string,int>
                {
                    { "a", 2 }, { "b", 2 }, { "c", 1 }
                })
        };

        foreach (var (num, desc, input, expected) in testHarness)
        {
            Test(num, desc, input, expected);
        }
    }

    private void Test(int num, string desc, string input, Dictionary<string, int> expected)
    {
        var sw = Stopwatch.StartNew();
        var actual = Solve(input);
        sw.Stop();

        bool pass = actual.Count == expected.Count &&
                    actual.All(kvp => expected.ContainsKey(kvp.Key) &&
                                      expected[kvp.Key] == kvp.Value);

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

    private string Format(Dictionary<string, int> dict)
    {
        return "{ " + string.Join(", ", dict.Select(kvp => $"{kvp.Key}:{kvp.Value}")) + " }";
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here

    // token = word
    // wordsArray = get word array by splitting using space
    // create a dictionary to store string word (token) and count
    // for each word in wordsArray
    //      if word already seen -> dictionary[word] = dictionary[word] + 1
    //      else -> dictionary[word] = 1
    //  return dictionary

    // token = char
    // create a dictionary to store char (token) and count
    // for each character in wordsArray
    //      if character already seen -> dictionary[character] = dictionary[character] + 1
    //      else -> dictionary[character] = 1
    //  return dictionary

    // Best case Time Complexity: O(1) --> Single char / Single word
    // Average case Time Complexity: O(n)  --> computations increase propotionaly with char count or word count
    // Worst case Time Complexity: O(n)
    // Space Complexity: O(k) --> we need an additional dictionary for max word count or char count 
    // 12:59 PM - 1:17 PM
    // ------------------------------------------------------------
    private Dictionary<string, int> Solve(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return [];

        var words = input.Split(' ');
        if (words.Length == 1)
            return UniqueCharCount(input);
        return UniqueWordCount(words);
    }

    private Dictionary<string, int> UniqueCharCount(string input)
    {
        var charCounts = new Dictionary<string, int>();
        foreach (var item in input)
        {
            var itemStr = item.ToString();
            if (charCounts.TryGetValue(itemStr, out var count))
                charCounts[itemStr] = count + 1;
            else
                charCounts[itemStr] = 1;
        }
        return charCounts;
    }

    private Dictionary<string, int> UniqueWordCount(string[] words)
    {
        var wordCounts = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (wordCounts.TryGetValue(word, out var count))
                wordCounts[word] = count + 1;
            else
                wordCounts[word] = 1;
        }
        return wordCounts;
    }

    // Optional alternative approach (Character Count only)
    private Dictionary<string, int> Solve2(string input)
    {
        // TODO: implement alternative solution
        return new Dictionary<string, int>();
    }
}
