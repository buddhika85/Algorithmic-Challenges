using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.Medium;

public class MergeSortedArray
{
    private readonly bool _debug = false; // Set to true to print internal states

    public MergeSortedArray()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Merge Sorted Array
     * ------------------------------------------------------------
     *  You are given two integer arrays nums1 and nums2, sorted in
     *  non-decreasing order, and two integers m and n, representing
     *  the number of elements in nums1 and nums2 respectively.
     *
     *  nums1 has a length of m + n, where the last n elements are
     *  set to 0 and should be ignored. nums2 has length n.
     *
     *  Merge nums2 into nums1 as one sorted array.
     *
     *  The merge must be done in-place inside nums1.
     *
     *  Example:
     *      Input:
     *          nums1 = [1,2,3,0,0,0], m = 3
     *          nums2 = [2,5,6],       n = 3
     *
     *      Output:
     *          nums1 = [1,2,2,3,5,6]
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] nums1, int m, int[] nums2, int n, int[] expected)[]
        {
            (
                1,
                "Example case",
                new[] { 1, 2, 3, 0, 0, 0 },
                3,
                new[] { 2, 5, 6 },
                3,
                new[] { 1, 2, 2, 3, 5, 6 }
            ),
            (
                2,
                "All nums2 smaller",
                new[] { 4, 5, 6, 0, 0, 0 },
                3,
                new[] { 1, 2, 3 },
                3,
                new[] { 1, 2, 3, 4, 5, 6 }
            ),
            (
                3,
                "All nums2 larger",
                new[] { 1, 2, 3, 0, 0, 0 },
                3,
                new[] { 4, 5, 6 },
                3,
                new[] { 1, 2, 3, 4, 5, 6 }
            ),
            (
                4,
                "nums1 empty, nums2 full",
                new[] { 0, 0, 0 },
                0,
                new[] { 2, 5, 6 },
                3,
                new[] { 2, 5, 6 }
            ),
            (
                5,
                "nums2 empty",
                new[] { 1, 2, 3 },
                3,
                Array.Empty<int>(),
                0,
                new[] { 1, 2, 3 }
            )
        };

        foreach (var (num, desc, nums1, m, nums2, n, expected) in testHarness)
        {
            Test(num, desc, nums1, m, nums2, n, expected);
        }
    }

    private void Test(int num, string desc, int[] nums1, int m, int[] nums2, int n, int[] expected)
    {
        var sw = Stopwatch.StartNew();
        Solve(nums1, m, nums2, n);
        sw.Stop();

        bool pass = nums1.Length == expected.Length &&
                    nums1.Zip(expected, (a, b) => a == b).All(x => x);

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Expected=[{string.Join(",", expected)}], Actual=[{string.Join(",", nums1)}]");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    /*
    *  You are given two integer arrays nums1 and nums2, sorted in
     *  non-decreasing order, and two integers m and n, representing
     *  the number of elements in nums1 and nums2 respectively.
     *
     *  nums1 has a length of m + n, where the last n elements are
     *  set to 0 and should be ignored. nums2 has length n.
     *
     *  Merge nums2 into nums1 as one sorted array.
     *
     *  The merge must be done in-place inside nums1.
     *
     *  Example:
     *      Input:
     *          nums1 = [1,2,3,0,0,0], m = 3
     *          nums2 = [2,5,6],       n = 3
     *
     *      Output:
     *          nums1 = [1,2,2,3,5,6]
    */
    // -- You have to fill this --
    // Best case Time Complexity:   --
    // Average case Time Complexity: --
    // Worst case Time Complexity:   --
    // Space Complexity:             --
    // ------------------------------------------------------------
    private void Solve(int[] nums1, int m, int[] nums2, int n)
    {
        var num1Index = m - 1;
        var num2Index = n - 1;

    }

    // Optional brute-force approach
    private void Solve2(int[] nums1, int m, int[] nums2, int n)
    {
        // TODO: implement alternative solution
    }
}
