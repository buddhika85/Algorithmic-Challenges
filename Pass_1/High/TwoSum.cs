using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class TwoSum
{
    private readonly bool _debug = false; // Set to true to print internal states

    public TwoSum()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Two Sum
     * ------------------------------------------------------------
     *  Given an array of integers nums and an integer target,
     *  return the indices of the two numbers such that they add
     *  up to the target.
     *
     *  - Each input will have exactly one solution.
     *  - You may not use the same element twice.
     *  - Return the answer in any order.
     *
     *  Example:
     *      Input:
     *          nums = [2, 7, 11, 15]
     *          target = 9
     *
     *      Output:
     *          [0, 1]
     *
     *      Explanation:
     *          nums[0] + nums[1] = 2 + 7 = 9
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] nums, int target, int[] expected)[]
        {
            (1, "Example case", new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 }),
            (2, "Match in middle", new[] { 3, 2, 4 }, 6, new[] { 1, 2 }),
            (3, "Duplicate values", new[] { 3, 3 }, 6, new[] { 0, 1 }),
            (4, "No match", new[] { 1, 2, 3 }, 100, Array.Empty<int>())
        };

        foreach (var (num, desc, nums, target, expected) in testHarness)
        {
            Test(num, desc, nums, target, expected);
        }
    }

    private void Test(int num, string desc, int[] nums, int target, int[] expected)
    {
        var sw = Stopwatch.StartNew();
        int[] actual = Solve(nums, target);
        sw.Stop();

        bool pass = actual.Length == expected.Length &&
                    actual.Zip(expected, (a, b) => a == b).All(x => x);

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Target={target} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Target={target} | Expected=[{string.Join(",", expected)}], Actual=[{string.Join(",", actual)}]");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    //
    // Best Case Time Complexity:    O(1)  --> The amount of work does NOT grow with n. its always 2 iterations.
    //   - We find the answer immediately (e.g., nums[0] + nums[1] = target)
    //
    // Average Case Time Complexity: O(n)
    //   - We scan through part of the array before finding the complement
    //
    // Worst Case Time Complexity:   O(n)
    //   - We scan the entire array before finding the complement
    //   - Dictionary lookups are O(1) on average
    //
    // Space Complexity: O(n)
    //   - Dictionary may store up to n elements in the worst case
    // ------------------------------------------------------------
    private int[] Solve(int[] nums, int target)
    {
        var seenDict = new Dictionary<int, int>();          // num, index of that number

        for (var i = 0; i < nums.Length; i++)
        {
            var diff = target - nums[i];
            if (seenDict.TryGetValue(diff, out int index))
                return [index, i];

            seenDict.Add(nums[i], i);
        }

        return Array.Empty<int>();
    }

    // Optional brute-force approach
    private int[] Solve2(int[] nums, int target)
    {
        // TODO: implement alternative solution
        return Array.Empty<int>();
    }
}
