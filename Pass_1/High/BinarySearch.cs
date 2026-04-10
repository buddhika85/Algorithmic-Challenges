using System;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_1.High;

public class BinarySearch
{
    private readonly bool _debug = false; // Set to true to print internal states

    public BinarySearch()
    {
        RunAllTests();
    }

    /*
     * ------------------------------------------------------------
     *  Problem: Binary Search
     * ------------------------------------------------------------
     *  Given a sorted array of integers nums and an integer target,
     *  return the index of target if it exists. Otherwise, return -1.
     *
     *  Binary Search works by repeatedly dividing the search interval
     *  in half. Compare the target with the middle element:
     *
     *      - If equal → return mid
     *      - If target < mid → search left half
     *      - If target > mid → search right half
     *
     *  Example:
     *      Input:
     *          nums = [-1, 0, 3, 5, 9, 12]
     *          target = 9
     *
     *      Output:
     *          4
     *
     *      Explanation:
     *          nums[4] = 9
     *
     * ------------------------------------------------------------
     *  Expected Time: 5–10 minutes
     * ------------------------------------------------------------
     */

    private void RunAllTests()
    {
        var testHarness = new (int num, string desc, int[] nums, int target, int expected)[]
        {
            (1, "Example case", new[] { -1, 0, 3, 5, 9, 12 }, 9, 4),
            (2, "Target at start", new[] { 2, 4, 6, 8, 10 }, 2, 0),
            (3, "Target at end", new[] { 1, 3, 5, 7, 9 }, 9, 4),
            (4, "Target not found", new[] { 1, 2, 3, 4, 5 }, 100, -1),
            (5, "Single element match", new[] { 7 }, 7, 0),
            (6, "Single element no match", new[] { 7 }, 3, -1)
        };

        foreach (var (num, desc, nums, target, expected) in testHarness)
        {
            Test(num, desc, nums, target, expected);
        }
    }

    private void Test(int num, string desc, int[] nums, int target, int expected)
    {
        var sw = Stopwatch.StartNew();
        int actual = Solve(nums, target);
        sw.Stop();

        bool pass = actual == expected;

        if (pass)
        {
            Console.WriteLine($"[PASS] {num}. {desc} | Target={target} | Time={sw.ElapsedTicks} ticks");
        }
        else
        {
            Console.WriteLine($"[FAIL] {num}. {desc} | Target={target} | Expected={expected}, Actual={actual}");
        }
    }

    // ------------------------------------------------------------
    // Solve method: implement logic here
    // use left, right, mid pointers
    // use a while loop left <= right
    // mid = (left + right) / 2
    // if (mid index value ==  target) ==> we found index
    // if (mid index value >  target) ==> its in left porition, so right = mid - 1
    // else ==> its in right porition, so left = mid + 1
    // after the end of loop, if it reaches, means target not in data set, return -1
    // Best case Time Complexity: O(1)  -- the target is in the exact middle of the data set, just 1 iteration requires of the loop.
    // Average case Time Complexity: O(log n)       -- we reduce number iterations by half every time
    // Worst case Time Complexity: O(log n)
    // Space Complexity: O(1) -- we dont use any 
    // ------------------------------------------------------------
    private int Solve(int[] nums, int target)
    {
        if (nums is null || nums.Length == 0)
            return -1;

        var left = 0;
        var right = nums.Length - 1;

        while (left <= right)
        {
            var mid = (left + right) / 2;
            if (nums[mid] == target)
                return mid;

            if (nums[mid] < target)
                left = mid + 1;         // its in right portion - so left needs to move right
            else
                right = mid - 1;        // its in left portion - so right needs to move left
        }


        return -1;
    }

    // Optional brute-force approach (linear scan)
    private int Solve2(int[] nums, int target)
    {
        // TODO: implement alternative solution
        return -1;
    }
}
