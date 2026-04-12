using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class TwoSumII
{
    private readonly bool _debug = false; // Set to true to print internal states

    public TwoSumII()
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

    /*  Given an array of integers nums and an integer target,
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
         */
    // ------------------------------------------------------------
    // Solve method: implement logic here
    // we will use a dictionary where we store number and lastSeen index
    // we use a for loop
    // and we get difference (which is target - current element)
    // if we can find the difference in dictionary we get that index and current index and return them
    // if not we add current value with current index into the dictionary
    // outside of for loop we return and empty array - indicating that we could not find the target from by adding 2 elements 

    // Best case Time Complexity: -- O(1)   --> if 2 elements and 2 elements adds to target
    // Average case Time Complexity: -- O(n)    --> combination found when we reach middle of the array using loop
    // Worst case Time Complexity: -- O(n) --> combination found at the end of the array using loop
    // Space Complexity: -- O(n) --> we use an additional dictionary with n size
    // 9:15 AM - 9:26 AM
    // ------------------------------------------------------------
    private int[] Solve(int[] nums, int target)
    {
        var lastSeen = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            var diff = target - nums[i];
            if (lastSeen.TryGetValue(diff, out var lastSeenIndex))
                return [lastSeenIndex, i];
            else
                lastSeen.Add(nums[i], i);
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
