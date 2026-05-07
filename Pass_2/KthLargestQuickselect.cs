using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_2;

public class KthLargestQuickselect
{
    private readonly bool _debug = false; // Set to true to print internal states

    public KthLargestQuickselect()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Kth Largest Element in an Array (Quickselect)
     * ------------------------------------------------------------
     *  Given an integer array nums and an integer k,
     *  return the k-th largest element in the array.
     *
     *  - The k-th largest means sorted in descending order.
     *  - Example: [3,2,1,5,6,4], k=2 → 5
     *
     *  Example:
     *      Input:
     *          nums = [3,2,1,5,6,4]
     *          k = 2
     *
     *      Output:
     *          5
     *
     *  Note:
     *      Quickselect achieves average O(n) time.
     *
     * ------------------------------------------------------------
     *  Expected Time: 10–15 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var tests = new (int num, string desc, int[] nums, int k, int expected)[]
        {
            (1, "Example case", new[] { 3, 2, 1, 5, 6, 4 }, 2, 5),
            (2, "Multiple duplicates", new[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 }, 4, 4),
            (3, "Single element", new[] { 1 }, 1, 1),
            (4, "Already sorted desc", new[] { 9, 8, 7, 6 }, 3, 7),
            (5, "Already sorted asc", new[] { 1, 2, 3, 4 }, 1, 4)
        };

        foreach (var (num, desc, nums, k, expected) in tests)
        {
            Test(num, desc, nums, k, expected);
        }
    }

    private void Test(int num, string desc, int[] nums, int k, int expected)
    {
        var sw = Stopwatch.StartNew();
        int actual = Solve(nums, k);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | k={k} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | k={k} | Expected={expected}, Actual={actual}");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement Quickselect here
    //
    // Best case Time Complexity: --
    // Average case Time Complexity: --
    // Worst case Time Complexity: --
    // Space Complexity: --
    // ------------------------------------------------------------
    private int Solve(int[] nums, int k)
    {
        // TODO: implement Quickselect

        if (_debug)
        {
            Console.WriteLine("Debug mode enabled...");
        }

        return -1;
    }

    // Optional: alternative approach (e.g., sorting)
    private int Solve2(int[] nums, int k)
    {
        // TODO: implement alternative solution
        return -1;
    }
}
