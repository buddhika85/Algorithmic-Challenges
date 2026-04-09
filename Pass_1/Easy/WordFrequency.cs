using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace AlgorithmsPractice.Pass_1.Easy
{
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
         *  Given a string s, return a frequency map of either:
         *      - each word (word frequency), OR
         *      - each character (character count)
         *
         *  You may choose the interpretation based on the test harness.
         *
         *  Example (word frequency):
         *      Input:
         *          "the cat and the hat"
         *
         *      Output:
         *          { the: 2, cat: 1, and: 1, hat: 1 }
         *
         *  Example (character count):
         *      Input:
         *          "hello"
         *
         *      Output:
         *          { h:1, e:1, l:2, o:1 }
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
                    new Dictionary<string,int> { { "the",2 }, { "cat",1 }, { "and",1 }, { "hat",1 } }),

                (2, "Character count basic",
                    "hello",
                    new Dictionary<string,int> { { "h",1 }, { "e",1 }, { "l",2 }, { "o",1 } }),

                (3, "Empty string",
                    "",
                    new Dictionary<string,int>()),

                (4, "Symbols ignored or counted based on your logic",
                    "a!!a",
                    new Dictionary<string,int> { { "a",2 }, { "!",2 } })
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

            bool pass = DictionariesEqual(actual, expected);

            if (pass)
            {
                Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
            }
            else
            {
                Console.WriteLine($"[FAIL] {num}. {desc}");
                Console.WriteLine($"       Input:    \"{input}\"");
                Console.WriteLine($"       Expected: {FormatDict(expected)}");
                Console.WriteLine($"       Actual:   {FormatDict(actual)}");
            }
        }

        private string FormatDict(Dictionary<string, int> dict)
        {
            return "{ " + string.Join(", ", dict.Select(kv => $"{kv.Key}:{kv.Value}")) + " }";
        }

        private bool DictionariesEqual(Dictionary<string, int> a, Dictionary<string, int> b)
        {
            if (a.Count != b.Count)
                return false;

            foreach (var kv in a)
            {
                if (!b.TryGetValue(kv.Key, out var val) || val != kv.Value)
                    return false;
            }

            return true;
        }

        // ------------------------------------------------------------
        // Solve method: implement logic here
        // assumes case senstivity is not important - so a & A are considerd same
        // use a loop and go though each  char/word, add them if the token does not exist in dict with count 1, else increase count by 1
        // Best case Time Complexity: -- O(n) - number of iterations/computations increase propotionaly with inputs token count
        // Average case Time Complexity: -- O(n)
        // Worst case Time Complexity: -- O(n)
        // Space Complexity: -- O(n) - in a worst case scenario we need a dictionary containing n number of tokens
        // ------------------------------------------------------------
        private Dictionary<string, int> Solve(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return [];

            s = s.Trim().ToLower();
            if (s.Contains(" "))
                return WordFrequencies(s);
            return CharFrequencies(s);
        }

        private Dictionary<string, int> WordFrequencies(string s)
        {
            var freqs = new Dictionary<string, int>();

            foreach (var word in s.Split(" "))
            {
                if (freqs.TryGetValue(word, out var count))
                    freqs[word] = count + 1;
                else
                    freqs[word] = 1;
            }
            return freqs;
        }

        private Dictionary<string, int> CharFrequencies(string s)
        {
            var freqs = new Dictionary<string, int>();

            foreach (var character in s)
            {
                var charStr = character.ToString();
                if (freqs.TryGetValue(charStr, out var count))
                    freqs[charStr] = count + 1;
                else
                    freqs[charStr] = 1;
            }
            return freqs;
        }

        // Optional brute-force approach
        private Dictionary<string, int> Solve2(string s)
        {
            // TODO: implement alternative solution
            return new Dictionary<string, int>();
        }
    }
}
