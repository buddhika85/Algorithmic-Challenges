using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace AlgorithmsPractice.Pass_2;

public class KthSmallestQuickSelect
{
    private readonly bool _debug = false; // Set to true to print internal states

    public KthSmallestQuickSelect()
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
            (1, "Example case", new[] {  8, 5, 2, 9, 7, 6, 3 }, 3, 5),




        };

        foreach (var (num, desc, nums, k, expected) in tests)
        {
            Test(num, desc, nums, k, expected);
        }
    }

    private void Test(int num, string desc, int[] nums, int k, int expected)
    {
        var sw = Stopwatch.StartNew();
        int actual = Solve(nums, k); //FindKthLargest(nums, k);
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



    // Optional: alternative approach (e.g., sorting)
    /*      Input:
          nums = [3,2,1,5,6,4]
          k = 2

       Output:  5

       targetIndex = 6 - 2 = 4

       [3,2,1,5,4,6]        - swaps = 1
       [3,2,1,4,5,6]        - swaps = 1
       [3,2,1,4,5,6]        - swaps = 0
 */
    private int Solve(int[] nums, int k)
    {
        var targetIndex = k - 1;

        while (true)
        {
            var swapCount = SwapAscending(nums, targetIndex);
            if (swapCount == 0)
                return nums[targetIndex];
        }


    }

    private int SwapAscending(int[] nums, int targetIndex)
    {
        var swapCount = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (i < targetIndex)
            {
                // left side
                if (nums[i] > nums[targetIndex])
                {
                    (nums[i], nums[targetIndex]) = (nums[targetIndex], nums[i]);
                    ++swapCount;
                }
            }
            else if (i > targetIndex)
            {
                // right side
                if (nums[i] < nums[targetIndex])
                {
                    (nums[i], nums[targetIndex]) = (nums[targetIndex], nums[i]);
                    ++swapCount;
                }
            }
        }
        return swapCount;
    }
}
