using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsPractice.Pass_1.High;

public class TopKFrequentElements
{
    private readonly bool _debug = false; // Set to true to print internal states

    public TopKFrequentElements()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Top K Frequent Elements
     * ------------------------------------------------------------
     *  Given an integer array nums and an integer k, return the
     *  k most frequent elements.
     *
     *  You must solve it in O(n log k) time using a Min-Heap.
     *
     *  Example:
     *      Input:
     *          nums = [1,1,1,2,2,3], k = 2
     *
     *      Output:
     *          [1,2]
     *
     *      Explanation:
     *          1 appears 3 times
     *          2 appears 2 times
     *          3 appears 1 time
     *          Top 2 frequent = [1,2]
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] nums, int k, int[] expected)[]
        {
            (1, "Example case", new[] { 1,1,1,2,2,3 }, 2, new[] { 1,2 }),
            (2, "Single element", new[] { 5 }, 1, new[] { 5 }),
            (3, "All same", new[] { 7,7,7,7 }, 1, new[] { 7 }),
            (4, "Multiple ties", new[] { 4,4,6,6,8,8 }, 2, new[] { 4,6 }), // any order is fine
            (5, "K equals array length", new[] { 9,8,7,6 }, 4, new[] { 9,8,7,6 })
        };

        foreach (var (num, desc, nums, k, expected) in testHarness)
        {
            Test(num, desc, nums, k, expected);
        }
    }

    private void Test(int num, string desc, int[] nums, int k, int[] expected)
    {
        var sw = Stopwatch.StartNew();
        int[] actual = Solve(nums, k);
        sw.Stop();

        bool pass = expected.All(x => actual.Contains(x)) && actual.Length == k;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | k={k} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | k={k} | Expected=[{string.Join(",", expected)}], Actual=[{string.Join(",", actual)}]");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    //
    // Steps:
    //   1. Count frequencies using Dictionary<int,int>
    //   2. Use a Min-Heap (PriorityQueue) of size k
    //      - Push (num, freq) with priority = freq
    //      - If heap grows beyond k, pop smallest freq
    //   3. Extract elements from heap into result array
    //
    // Time Complexity:  O(n log k)
    // Space Complexity: O(n + k)
    // ------------------------------------------------------------
    private int[] Solve(int[] nums, int k)
    {
        // TODO: implement using Min-Heap of size k

        if (_debug)
        {
            Console.WriteLine("Debug mode enabled...");
        }

        return Array.Empty<int>();
    }

    // Optional alternative: sorting by frequency (O(n log n))
    private int[] Solve2(int[] nums, int k)
    {
        // TODO: implement alternative solution using sorting
        return Array.Empty<int>();
    }
}
