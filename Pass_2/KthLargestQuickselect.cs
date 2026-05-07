using System;
using System.Collections.Concurrent;
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
            (5, "Already sorted asc", new[] { 1, 2, 3, 4 }, 1, 4),
            (6, "Tricky 1", new[] { 9, 1, 8, 2, 7, 3 }, 3, 7),
            (7, "Tricky 2", new[] { 3, 3, 3, 2, 2, 2 }, 2, 3),

            (8,  "All equal except one large at the end", new[] { 1, 1, 1, 1, 1, 100 }, 1, 100),
(9,  "All equal except one small at the end", new[] { 100, 100, 100, 100, 100, 1 }, 2, 100),
(10, "Pivot oscillation case", new[] { 5, 1, 4, 2, 3 }, 2, 4),
(11, "Reverse sorted with duplicates", new[] { 9, 9, 9, 8, 8, 8, 7 }, 4, 8),
(12, "Large block of smaller values on the right", new[] { 9, 8, 7, 1, 1, 1, 1 }, 3, 7),
(13, "Large block of larger values on the left", new[] { 9, 9, 9, 9, 1, 2, 3 }, 5, 3),
(14, "Alternating high/low pattern", new[] { 10, 1, 9, 2, 8, 3, 7 }, 3, 8),
(15, "Worst‑case pivot drift", new[] { 4, 6, 2, 7, 1, 8, 3, 5 }, 4, 5),
(16, "Many duplicates around target", new[] { 5, 5, 5, 5, 4, 4, 4, 4 }, 3, 5),
(17, "Target in the middle of duplicate blocks", new[] { 2, 2, 2, 9, 9, 9, 5, 5, 5 }, 4, 5),

(18, "Guaranteed failure: oscillating pivot", new[] { 4, 2, 5, 1, 3 }, 2, 4),
(19, "Pivot drift with duplicates", new[] { 5, 1, 5, 1, 5, 1 }, 3, 5)



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

    // ------------------------------------------------------------
    // Solve method: implement Quickselect here
    //
    // Best case Time Complexity: --
    // Average case Time Complexity: --
    // Worst case Time Complexity: --
    // Space Complexity: --
    // ------------------------------------------------------------

    /*      Input:
              nums = [3,2,1,5,6,4]
              k = 2
     
           Output:  5
     */
    private int FindKthLargest(int[] nums, int k)
    {
        var targetIndex = nums.Length - k;      // ascending sort

        var left = 0;
        var right = nums.Length - 1;
        while (true)
        {
            int pivotIndex = Partition(nums, left, right);
            if (pivotIndex == targetIndex)
            {
                // means target index now has correct valuse now, that is the Kth Largest/Smallest
                return nums[targetIndex];
            }

            // alaways left to pivot - have values less than pivot index value - but unsorted
            // always right to pivot - have values more than pivot index value - but unsorted

            else if (pivotIndex < targetIndex)              // ....p....t....       --> ignore to p [p+1....t....]
            {
                // value should be on the right of the pivot - means we can ignore from left index to pivot itself
                left = pivotIndex + 1;
            }

            else // if (pivotIndex > targetIndex)           // ....t...p...         --> [....t...] ignore from p to end
            {
                right = pivotIndex - 1;
            }
        }
    }

    // Returns a pivot index which is always in the right sorted place
    private int Partition(int[] nums, int left, int right)
    {
        var pivot = nums[right];
        var i = left;                   // we only consider values which are on index i or greater towards right
        var j = left;

        for (; j < right; j++)
        {
            if (nums[j] < pivot)
            {
                Swap(nums, i, j);
                i++;
            }
        }

        Swap(nums, i, right);
        return i;
    }

    private void Swap(int[] nums, int i, int j)
    {
        (nums[j], nums[i]) = (nums[i], nums[j]);
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
        var targetIndex = nums.Length - k;

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
